
using System;

namespace ArduCamControler
{
    partial class ArduCamForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LogBox = new System.Windows.Forms.RichTextBox();
            this.OpenClosePortBtn = new System.Windows.Forms.Button();
            this.CaptureBtn = new System.Windows.Forms.Button();
            this.RawImageCheck = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.PortsList = new System.Windows.Forms.ComboBox();
            this.FindPortsBtn = new System.Windows.Forms.Button();
            this.BaudRateList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ResolutionList = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dbgStringBox = new System.Windows.Forms.RichTextBox();
            this.dbgHexBox = new System.Windows.Forms.RichTextBox();
            this.ClearBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ExposureList = new System.Windows.Forms.ComboBox();
            this.ImageNameBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.FolderBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.BrowseBtn = new System.Windows.Forms.Button();
            this.DebugBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LogBox
            // 
            this.LogBox.Location = new System.Drawing.Point(21, 312);
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.Size = new System.Drawing.Size(528, 96);
            this.LogBox.TabIndex = 0;
            this.LogBox.Text = "";
            this.LogBox.TextChanged += new System.EventHandler(this.LogBox_TextChanged);
            // 
            // OpenClosePortBtn
            // 
            this.OpenClosePortBtn.Location = new System.Drawing.Point(35, 37);
            this.OpenClosePortBtn.Name = "OpenClosePortBtn";
            this.OpenClosePortBtn.Size = new System.Drawing.Size(75, 23);
            this.OpenClosePortBtn.TabIndex = 1;
            this.OpenClosePortBtn.Text = "Open port";
            this.OpenClosePortBtn.UseVisualStyleBackColor = true;
            this.OpenClosePortBtn.Click += new System.EventHandler(this.OpenClosePortBtn_Click);
            // 
            // CaptureBtn
            // 
            this.CaptureBtn.Location = new System.Drawing.Point(35, 66);
            this.CaptureBtn.Name = "CaptureBtn";
            this.CaptureBtn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CaptureBtn.Size = new System.Drawing.Size(75, 23);
            this.CaptureBtn.TabIndex = 2;
            this.CaptureBtn.Text = "Capture";
            this.CaptureBtn.UseVisualStyleBackColor = true;
            this.CaptureBtn.Click += new System.EventHandler(this.CaptureBtn_Click);
            // 
            // RawImageCheck
            // 
            this.RawImageCheck.AutoSize = true;
            this.RawImageCheck.Enabled = false;
            this.RawImageCheck.Location = new System.Drawing.Point(35, 256);
            this.RawImageCheck.Name = "RawImageCheck";
            this.RawImageCheck.Size = new System.Drawing.Size(79, 17);
            this.RawImageCheck.TabIndex = 3;
            this.RawImageCheck.Text = "Raw image";
            this.RawImageCheck.UseVisualStyleBackColor = true;
            this.RawImageCheck.CheckedChanged += new System.EventHandler(this.RawImageCheck_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(35, 233);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(82, 17);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Save image";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // PortsList
            // 
            this.PortsList.FormattingEnabled = true;
            this.PortsList.Location = new System.Drawing.Point(191, 30);
            this.PortsList.Name = "PortsList";
            this.PortsList.Size = new System.Drawing.Size(121, 21);
            this.PortsList.TabIndex = 6;
            // 
            // FindPortsBtn
            // 
            this.FindPortsBtn.Location = new System.Drawing.Point(35, 95);
            this.FindPortsBtn.Name = "FindPortsBtn";
            this.FindPortsBtn.Size = new System.Drawing.Size(75, 23);
            this.FindPortsBtn.TabIndex = 7;
            this.FindPortsBtn.Text = "Scan ports";
            this.FindPortsBtn.UseVisualStyleBackColor = true;
            this.FindPortsBtn.Click += new System.EventHandler(this.FindPortsBtn_Click);
            // 
            // BaudRateList
            // 
            this.BaudRateList.FormattingEnabled = true;
            this.BaudRateList.Location = new System.Drawing.Point(191, 57);
            this.BaudRateList.Name = "BaudRateList";
            this.BaudRateList.Size = new System.Drawing.Size(121, 21);
            this.BaudRateList.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Serial ports";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(126, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Baud rate:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 415);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Bytes received in current session: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(192, 415);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.DimGray;
            this.pictureBox1.Location = new System.Drawing.Point(587, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(615, 426);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // ResolutionList
            // 
            this.ResolutionList.FormattingEnabled = true;
            this.ResolutionList.Location = new System.Drawing.Point(191, 84);
            this.ResolutionList.Name = "ResolutionList";
            this.ResolutionList.Size = new System.Drawing.Size(121, 21);
            this.ResolutionList.TabIndex = 14;
            this.ResolutionList.TextChanged += new System.EventHandler(this.ResolutionList_TextChanges);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(126, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Resolution:";
            // 
            // dbgStringBox
            // 
            this.dbgStringBox.Location = new System.Drawing.Point(21, 467);
            this.dbgStringBox.Name = "dbgStringBox";
            this.dbgStringBox.ReadOnly = true;
            this.dbgStringBox.Size = new System.Drawing.Size(528, 167);
            this.dbgStringBox.TabIndex = 16;
            this.dbgStringBox.Text = "";
            // 
            // dbgHexBox
            // 
            this.dbgHexBox.Location = new System.Drawing.Point(587, 467);
            this.dbgHexBox.Name = "dbgHexBox";
            this.dbgHexBox.ReadOnly = true;
            this.dbgHexBox.Size = new System.Drawing.Size(615, 167);
            this.dbgHexBox.TabIndex = 17;
            this.dbgHexBox.Text = "";
            // 
            // ClearBtn
            // 
            this.ClearBtn.Location = new System.Drawing.Point(364, 283);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(75, 23);
            this.ClearBtn.TabIndex = 18;
            this.ClearBtn.Text = "Clear text";
            this.ClearBtn.UseVisualStyleBackColor = true;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 448);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Debug box";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(584, 448);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Debug box";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(126, 114);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Exposure:";
            // 
            // ExposureList
            // 
            this.ExposureList.FormattingEnabled = true;
            this.ExposureList.Location = new System.Drawing.Point(191, 111);
            this.ExposureList.Name = "ExposureList";
            this.ExposureList.Size = new System.Drawing.Size(121, 21);
            this.ExposureList.TabIndex = 22;
            this.ExposureList.TextChanged += new System.EventHandler(this.ExposureList_TextChanged);
            // 
            // ImageNameBox
            // 
            this.ImageNameBox.Location = new System.Drawing.Point(35, 168);
            this.ImageNameBox.Name = "ImageNameBox";
            this.ImageNameBox.Size = new System.Drawing.Size(100, 20);
            this.ImageNameBox.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(32, 152);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(181, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Image name (leave empty for default)";
            // 
            // FolderBox
            // 
            this.FolderBox.Location = new System.Drawing.Point(35, 207);
            this.FolderBox.Name = "FolderBox";
            this.FolderBox.Size = new System.Drawing.Size(100, 20);
            this.FolderBox.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(32, 191);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Save folder:";
            // 
            // BrowseBtn
            // 
            this.BrowseBtn.Location = new System.Drawing.Point(141, 204);
            this.BrowseBtn.Name = "BrowseBtn";
            this.BrowseBtn.Size = new System.Drawing.Size(75, 23);
            this.BrowseBtn.TabIndex = 27;
            this.BrowseBtn.Text = "Browse...";
            this.BrowseBtn.UseVisualStyleBackColor = true;
            this.BrowseBtn.Click += new System.EventHandler(this.BrowseBtn_Click);
            // 
            // DebugBtn
            // 
            this.DebugBtn.Location = new System.Drawing.Point(445, 283);
            this.DebugBtn.Name = "DebugBtn";
            this.DebugBtn.Size = new System.Drawing.Size(104, 23);
            this.DebugBtn.TabIndex = 28;
            this.DebugBtn.Text = "Show/hide debug";
            this.DebugBtn.UseVisualStyleBackColor = true;
            this.DebugBtn.Click += new System.EventHandler(this.DebugBtn_Click);
            // 
            // ArduCamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 449);
            this.Controls.Add(this.DebugBtn);
            this.Controls.Add(this.BrowseBtn);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.FolderBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ImageNameBox);
            this.Controls.Add(this.ExposureList);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ClearBtn);
            this.Controls.Add(this.dbgHexBox);
            this.Controls.Add(this.dbgStringBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ResolutionList);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BaudRateList);
            this.Controls.Add(this.FindPortsBtn);
            this.Controls.Add(this.PortsList);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.RawImageCheck);
            this.Controls.Add(this.CaptureBtn);
            this.Controls.Add(this.OpenClosePortBtn);
            this.Controls.Add(this.LogBox);
            this.MaximizeBox = false;
            this.Name = "ArduCamForm";
            this.Text = "ArduCam";
            this.Load += new System.EventHandler(this.ArduCamForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.RichTextBox LogBox;
        private System.Windows.Forms.Button OpenClosePortBtn;
        private System.Windows.Forms.Button CaptureBtn;
        private System.Windows.Forms.CheckBox RawImageCheck;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ComboBox PortsList;
        private System.Windows.Forms.Button FindPortsBtn;
        private System.Windows.Forms.ComboBox BaudRateList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox ResolutionList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox dbgStringBox;
        private System.Windows.Forms.RichTextBox dbgHexBox;
        private System.Windows.Forms.Button ClearBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox ExposureList;
        private System.Windows.Forms.TextBox ImageNameBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox FolderBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button BrowseBtn;
        private System.Windows.Forms.Button DebugBtn;
    }
}

