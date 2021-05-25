using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.IO;

namespace ArduCamControler
{
    public partial class ArduCamForm : Form
    {
        SerialPort portUsed = new SerialPort();
        Thread serialReadThread;

        private List<byte> tempBuffer = new List<byte>();
        int bytesReceived = 0;
        int activeByte = 0x10;                      // 0x10 for strings, 0x11 for image processing
        bool readStop = false;
        bool imgStart = true;
        bool JPEGformat = true;
        bool allowReceiving = false;
        Bitmap receivedImage = null;
        bool automatic = false;
        int photoNumber = 0;
        string[] exposure = new string[11] { "-5", "-4", "-3", "-2", "-1", "0", "1", "2", "3", "4", "5" };
        string[] resolution = new string[8] { "320x240", "640x480", "1024x768", "1280x960", "1600x1200", "1920x1080", "2048x1536", "2592x1944"};
        int imgSize = 0;
        public ArduCamForm()
        {
            InitializeComponent();
        }

        private void ArduCamForm_Load(object sender, EventArgs e)
        {
            // Add serial ports to list
            string[] comPorts = SerialPort.GetPortNames();
            Array.Sort(comPorts);
            if (comPorts.Length != 0)
            {
                PortsList.Items.AddRange((object[])comPorts);
            }
            else
            {
                PortsList.Items.Add("No serial ports found.");
            }

            PortsList.SelectedIndex = 0;
            // Add all baud rates
            string[] baudRates = new string[7]
            {
                "9600",
                "19200",
                "38400",
                "256000",
                "57600",
                "115200",
                "921600"
            };

            ExposureList.Items.AddRange(exposure);
            ResolutionList.Items.AddRange(resolution);
            BaudRateList.Items.AddRange(baudRates);
            SetDefaultOptions();

        }

        private void FindPortsBtn_Click(object sender, EventArgs e)
        {

            // Add serial ports to list
            string[] comPorts = SerialPort.GetPortNames();
            Array.Sort(comPorts);
            PortsList.Items.Clear();
            if (comPorts.Length != 0)
            {
                PortsList.Items.AddRange((object[])comPorts);
            }
            else
            {
                PortsList.Items.Add("No serial ports found.");
            }

            // Auto select first port listed
            PortsList.SelectedIndex = 0;
        }

        private void OpenClosePortBtn_Click(object sender, EventArgs e)
        {
            if (!portUsed.IsOpen)
            {
                readStop = false;
                LogBox.AppendText("Opening port " + PortsList.Text + "\n");
                portUsed.PortName = PortsList.Text;
                portUsed.BaudRate = Int32.Parse(BaudRateList.Text);
                portUsed.NewLine = "\r\n";
                RawImageCheck.Enabled = true;
                portUsed.ReadTimeout = 1000;
                portUsed.WriteTimeout = 1000;
                try
                {
                    portUsed.Open();
                    Thread.Sleep(500);
                }
                catch(Exception ex)
                {
                    LogBox.AppendText(ex.Message);
                    LogBox.AppendText("Please try again, check the connection and scan for available ports\n");
                    return;
                }
                ResolutionList.SelectedIndex = ResolutionList.Items.IndexOf("2592x1944");
                ExposureList.SelectedIndex = ExposureList.Items.IndexOf("0");

                serialReadThread = new Thread(() => PortListener());
                serialReadThread.IsBackground = true;
                serialReadThread.Start();
                this.portUsed.DtrEnable = true;
                OpenClosePortBtn.Text = "Close";
            }
            else
            {
                allowReceiving = false;
                readStop = true;
                tempBuffer = new List<byte>();
                activeByte = 0x10;
                LogBox.AppendText("Closing port " + portUsed.PortName + "\n");
                try
                {
                    portUsed.Close();
                }
                catch(Exception ex)
                {
                    LogBox.AppendText("Unable to close port.\n");
                    LogBox.AppendText(ex.Message);
                }
                SetDefaultOptions();
                OpenClosePortBtn.Text = "Open";
            }
        }

        private void PortListener()
        {
            while (true)
            {
                try
                {
                    if (portUsed.IsOpen)
                    {
                        if (portUsed.BytesToRead > 0)
                        {
                            if (activeByte == 0x10)
                            {
                                byte[] buffer = new byte[portUsed.BytesToRead];
                                portUsed.Read(buffer, 0, portUsed.BytesToRead);
                                bytesReceived += buffer.Count();
                                var str = Encoding.ASCII.GetString(buffer);
                                Invoke((Action)delegate { LogBox.AppendText(str); });
                                Invoke((Action)delegate { label4.Text = bytesReceived.ToString(); });
                                if (!allowReceiving && str == "Setup end.\r\n")
                                {
                                    Invoke((Action)delegate { allowReceiving = true; });
                                    Invoke((Action)delegate { PortsList.Enabled = false; });
                                    Invoke((Action)delegate { BaudRateList.Enabled = false; });
                                    Invoke((Action)delegate { ResolutionList.Enabled = true; });
                                    Invoke((Action)delegate { ExposureList.Enabled = true;});

                                    Invoke((Action)delegate { CaptureBtn.Enabled = true; });
                                }
                            }
                            else if (JPEGformat == true)
                            {
                                if (portUsed.BytesToRead > 4 && imgStart)
                                {

                                    /* Image comes in form of: 0x10FF01FF - 'Size of image' - 'Image with jpeg header 0xFFD8 */

                                    /* We need to allocate more bytes than we excpect to receive because in the time
                                     * between allocating memory and reading from serial port, additional bytes might've
                                     * been sent and are unaccounted for, throwing exceptions
                                     */
                                    byte[] buffer = new byte[30000];
                                    int bytesRead = portUsed.Read(buffer, 0, portUsed.BytesToRead);
                                    bytesReceived += bytesRead;
                                    Array.Resize(ref buffer, bytesRead);
                                    
                                    var str = Encoding.ASCII.GetString(buffer);
                                    
                                    //Invoke((Action)delegate { LogBox.AppendText("str" + "\n"); });
                                    tempBuffer.AddRange(buffer);
                                    if (tempBuffer[0] == 0x10 && tempBuffer[1] == 0xFF && tempBuffer [2] == 0x01 && tempBuffer[3] == 0xFF)      // Check for starting bits
                                    {
                                        tempBuffer.RemoveRange(0, 4);
                                        imgStart = false;
                                    }
                                    int index = 0;
                                    while (tempBuffer[index] != 0xFF && tempBuffer[++index] != 0xD8)
                                        ;

                                    var imgSizeBuff = new List<byte>(index);
                                    imgSizeBuff.AddRange(tempBuffer.GetRange(0, index));
                                    tempBuffer.RemoveRange(0, index);
                                    Invoke((Action)delegate { LogBox.AppendText("Size of image: " + Encoding.ASCII.GetString(imgSizeBuff.ToArray()) + " bytes\n"); });

                                    Invoke((Action)delegate { label4.Text = bytesReceived.ToString(); });
                                    Invoke((Action)delegate { dbgHexBox.AppendText(ByteArrToHexString(buffer) + "\n\n"); });
                                    Invoke((Action)delegate { dbgStringBox.AppendText(str + "\n\n"); });

                                    //Invoke((Action)delegate { LogBox.AppendText("Contents of temporary buffer:\n" + ByteArrToHexString(tempBuffer.ToArray())); });



                                }
                                if (portUsed.BytesToRead > 0 && !imgStart)
                                {
                                    /* We need to allocate more bytes than we excpect to receive because in the time
                                     * between allocating memory and reading from serial port, additional bytes might've
                                     * been sent and are unaccounted for, throwing exceptions
                                     */
                                    byte[] buffer = new byte[30000];
                                    int bytesRead = portUsed.Read(buffer, 0, portUsed.BytesToRead);
                                    bytesReceived += bytesRead;
                                    Array.Resize(ref buffer, bytesRead);



                                    tempBuffer.AddRange(buffer);
                                    var str = Encoding.ASCII.GetString(buffer);
                                    Invoke((Action)delegate { label4.Text = bytesReceived.ToString(); });
                                    Invoke((Action)delegate { dbgHexBox.AppendText(ByteArrToHexString(buffer) + "\n\n"); });
                                    Invoke((Action)delegate { dbgStringBox.AppendText(str + "\n\n"); });

                                    if (tempBuffer.ElementAt(tempBuffer.Count - 2) == 0xFF && tempBuffer.ElementAt(tempBuffer.Count-1) == 0xD9)
                                    {
                                        imgStart = true;
                                        activeByte = 0x10;
                                        ShowImageOnScreen();
                                    }

                                }

                            }
                            else if (!JPEGformat)
                            {
                                int totalBytes = portUsed.BytesToRead;
                                if (totalBytes > 4 )
                                {
                                    byte[] buffer = new byte[totalBytes];
                                    portUsed.Read(buffer, 0, totalBytes);
                                    bytesReceived += buffer.Count();

                                    var str = Encoding.ASCII.GetString(buffer);

                                    tempBuffer.AddRange(buffer);
                                    Invoke((Action)delegate { dbgHexBox.AppendText(ByteArrToHexString(buffer) + "\n\n"); });
                                    Invoke((Action)delegate { dbgStringBox.AppendText(str + "\n\n"); });

                                    if (tempBuffer.Count >= 4 && tempBuffer[0] == 0x10 && tempBuffer[1] == 0xFF && tempBuffer[2] == 0x01 && tempBuffer[3] == 0xFF)      // Check for starting bits
                                    {
                                        int index = 0;
                                        while (tempBuffer[index] != 0x0D && tempBuffer[++index] != 0x0A)
                                            ;
                                        tempBuffer.RemoveRange(0, index+2);

                                        Invoke((Action)delegate { LogBox.AppendText("Contents of tempBuffer:\n" + ByteArrToHexString(tempBuffer.ToArray())); });
                                        imgStart = false;
                                    }

                                    Invoke((Action)delegate { label4.Text = bytesReceived.ToString(); });
                                }
                                if(tempBuffer.Count == imgSize)
                                {
                                    string imageFolderPath = "";
                                    string imageName = "";
                                    string currExposure = "", currResolution = "";
                                    Invoke((MethodInvoker)delegate () { currExposure = ExposureList.SelectedText.ToString(); });

                                    Invoke((MethodInvoker)delegate () { currResolution = ResolutionList.SelectedText.ToString(); });

                                    if (ImageNameBox.Text != "")
                                    {
                                        if (ImageNameBox.Text.EndsWith(".jpg"))
                                        {
                                            imageName = ImageNameBox.Text.Replace(".jpg", "");
                                        }
                                        else
                                        {
                                            imageName = ImageNameBox.Text;
                                        }
                                    }
                                    else
                                    {
                                        imageName = "exposure_" + currExposure + "res_" + currResolution + ".jpg";
                                    }
                                    if (FolderBox.Text == "")
                                    {
                                        string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                                        var di = Directory.CreateDirectory(documents + "/ArduCam");
                                        imageFolderPath = di.FullName;
                                    }
                                    else
                                    {
                                        var di = Directory.CreateDirectory(FolderBox.Text);
                                        imageFolderPath = FolderBox.Text;
                                    }
                                    File.WriteAllBytes(imageFolderPath + "/" + imageName + "_" + photoNumber.ToString() + ".RAW", tempBuffer.ToArray());
                                    tempBuffer.Clear();
                                    imgStart = true;
                                    activeByte = 0x10;
                                    Invoke((MethodInvoker)delegate () {ResolutionList.Enabled = true; });
                                    Invoke((MethodInvoker)delegate () {ExposureList.Enabled = true; });
                                    Invoke((MethodInvoker)delegate () {CaptureBtn.Enabled = true; });
                                }
                            }
                        }
                        
                    }
                }
                catch(Exception ex)
                {
                    Invoke((Action)delegate { LogBox.AppendText(ex.Message + "\n"); });

                    tempBuffer = new List<byte>();
                }
                if (readStop)
                {
                    break;
                }
            }
        }

        private void ShowImageOnScreen()
        {
            string imageFolderPath = "";
            string imageName = "";
            string currExposure = "", currResolution = "";
            Invoke((MethodInvoker)delegate() {  currExposure = ExposureList.SelectedText.ToString(); });
            
            Invoke((MethodInvoker)delegate() {  currResolution = ResolutionList.SelectedText.ToString(); });
            
            if (ImageNameBox.Text != "")
            {
                if (ImageNameBox.Text.EndsWith(".jpg"))
                {
                    imageName = ImageNameBox.Text.Replace(".jpg", "");
                }
                else
                {
                    imageName = ImageNameBox.Text;
                }
            }
            else
            {
                imageName = "exposure_" + currExposure + "res_" + currResolution + ".jpg"; 
            }

            if (FolderBox.Text == "")
            {
                string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var di = Directory.CreateDirectory(documents + "/ArduCam");
                imageFolderPath = di.FullName;
            }
            else
            {
                var di = Directory.CreateDirectory(FolderBox.Text);
                imageFolderPath = FolderBox.Text;
            }

            try
            {
                Invoke((Action)delegate { LogBox.AppendText("Image received\n"); });
                receivedImage = (Bitmap)((new ImageConverter()).ConvertFrom(tempBuffer.ToArray()));
               
                pictureBox1.Image = receivedImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                if (saturationCheckBtn.Checked)
                {
                    bool saturated = CheckSaturation(receivedImage);
                    if (saturated)
                    {
                        Invoke((Action)delegate { LogBox.AppendText("The picture is saturated!\n"); });
                    }
                    else
                    {
                        Invoke((Action)delegate { LogBox.AppendText("Received picture is not saturated!\n"); });

                    }
                }

                Invoke((Action)delegate { ResolutionList.Enabled = true; });
                Invoke((Action)delegate { ExposureList.Enabled = true; });

                Invoke((Action)delegate { CaptureBtn.Enabled = true; });
            }
            catch(Exception ex)
            {
                Invoke((Action)delegate { LogBox.AppendText(ex.Message); });
                
            }
            finally
            {
                File.WriteAllBytes(imageFolderPath + "/" + imageName + "_" + photoNumber.ToString() + ".jpg", tempBuffer.ToArray());
                tempBuffer.Clear();
            }

           
        }


        private void LogBox_TextChanged(object sender, EventArgs e)
        {
            LogBox.SelectionStart = LogBox.Text.Length;
            LogBox.ScrollToCaret();
        }

        private void RawImageCheck_CheckedChanged(object sender, EventArgs e)
        {
            byte[] rawImageBytes = { 0x00, 0x01 };
            if (portUsed.IsOpen)
            {
                if (RawImageCheck.Checked)
                {
                    portUsed.Write(rawImageBytes, 0, 1);
                }
                else
                {
                    portUsed.Write(rawImageBytes, 1, 1);
                }
            }
        }

        private void CaptureBtn_Click(object sender, EventArgs e)
        {
            JPEGformat = !RawImageCheck.Checked ? true : false;
            byte[] getImage = { 0x20 };
            if (portUsed.IsOpen)
            {
                
                activeByte = 0x11;
                portUsed.Write(getImage, 0, 1);
                ResolutionList.Enabled = false;
                ExposureList.Enabled = false;
                CaptureBtn.Enabled = false;
            }
        }

        private void ExposureList_TextChanged(object sender, EventArgs e)
        {
            byte[] buffer = new byte[1];

            switch (ExposureList.Text)
            {
                case "-5":
                    buffer[0] = 0x40;
                    break;
                case "-4":
                    buffer[0] = 0x41;
                    break;
                case "-3":
                    buffer[0] = 0x42;
                    break;
                case "-2":
                    buffer[0] = 0x43;
                    break;
                case "-1":
                    buffer[0] = 0x44;
                    break;
                case "0":
                    buffer[0] = 0x45;
                    break;
                case "1":
                    buffer[0] = 0x46;
                    break;
                case "2":
                    buffer[0] = 0x47;
                    break;
                case "3":
                    buffer[0] = 0x48;
                    break;
                case "4":
                    buffer[0] = 0x49;
                    break;
                case "5":
                    buffer[0] = 0x4A;
                    break;
            }

            try
            {
                portUsed.Write(buffer, 0, 1);
            }
            catch(Exception ex)
            {
                LogBox.AppendText(ex.Message + "\n");
            }
        }

        private void ResolutionList_TextChanges(object sender, EventArgs e)
        {
            byte[] buffer = new byte[1];
            switch (ResolutionList.Text)
            {
                
                case "320x240":
                    buffer[0] = 0x30;
                    imgSize = 320 * 240;
                    break;
                case "640x480":
                    buffer[0] = 0x31;
                    imgSize = 640*480;
                    break;
                case "1024x768":
                    buffer[0] = 0x32;
                    imgSize = 1024*768;
                    break;
                case "1280x960":
                    buffer[0] = 0x33;
                    imgSize = 1280*960;
                    break;
                case "1600x1200":
                    buffer[0] = 0x34;
                    imgSize = 1600*1200;
                    break;
                case "1920x1080":
                    buffer[0] = 0x35;
                    imgSize = 1920*1080;
                    break;
                case "2048x1536":
                    buffer[0] = 0x36;
                    imgSize = 2048*1536;
                    break;
                case "2592x1944":
                    buffer[0] = 0x37;
                    imgSize = 2592 * 1944;
                    break;
            }
            try
            {
                portUsed.Write(buffer, 0, 1);
            }
            catch (Exception ex)
            {
                LogBox.AppendText(ex.Message + "\n");
            }
        }

        private string ByteArrToHexString(byte[] byteArr)
        {
            StringBuilder hex = new StringBuilder();
            foreach(byte b in byteArr)
            {
                hex.Append("0x");
                hex.AppendFormat("{0:x2}", b);
                hex.Append(" ");
            }
            return hex.ToString();
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            dbgHexBox.Clear();
            dbgStringBox.Clear();
            LogBox.Clear();
        }

        private void SetDefaultOptions()
        {
            BaudRateList.SelectedIndex = BaudRateList.Items.IndexOf("921600");
            RawImageCheck.Checked = false;
            LogBox.Clear();
            dbgHexBox.Clear();
            dbgStringBox.Clear();
            CaptureBtn.Enabled = false;
            pictureBox1.Image = null;
            ResolutionList.Enabled = false;
            ExposureList.Enabled = false;
            PortsList.Enabled = true;
            BaudRateList.Enabled = true;
            RawImageCheck.Enabled = false;
        }

        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    FolderBox.Text = folderBrowserDialog.SelectedPath;


                }

            }
        }

        private void DebugBtn_Click(object sender, EventArgs e)
        {
            if (ClientSize.Height == 669)
            {
                this.ClientSize = new Size(1214, 449);
            }
            else
            {
                this.ClientSize = new Size(1214, 669);
            }
        }

        private void AutomaticBtn_Click(object sender, EventArgs e)
        {
            automatic = true;
            CaptureBtn.PerformClick();
        }

        // If R, G and B values are all beyond a certain treshold, picture is saturated
        private bool CheckSaturation(Bitmap photo)
        {
            int Rbound = String.IsNullOrEmpty(RboundBox.Text) ? 250 : int.Parse(RboundBox.Text);
            int Gbound = String.IsNullOrEmpty(GboundBox.Text) ? 250 : int.Parse(GboundBox.Text);
            int Bbound = String.IsNullOrEmpty(BboundBox.Text) ? 250 : int.Parse(BboundBox.Text);
            for (int i = 0; i < photo.Width; i++)
            {
                for(int j = 0; j < photo.Height; j++)
                {
                    Color c = photo.GetPixel(i, j);
                    if(c.R > Rbound && c.G > Gbound && c.B > Bbound)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


    }
}
