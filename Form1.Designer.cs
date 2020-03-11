namespace NG_Monitor
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.IDGnrtrBtn = new System.Windows.Forms.Button();
            this.textVersion = new System.Windows.Forms.TextBox();
            this.textBat = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.textID = new System.Windows.Forms.TextBox();
            this.ClrLogBtn = new System.Windows.Forms.Button();
            this.rstBtn = new System.Windows.Forms.Button();
            this.prntBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textPin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OpenPortBtn = new System.Windows.Forms.Button();
            this.comboPorts = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboTypes = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboHardware = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtOficialVer = new System.Windows.Forms.Label();
            this.txtPinLmt = new System.Windows.Forms.Label();
            this.textValue = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.statusText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxNotGenerae = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkReused = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.PortName = "COM4";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.richTextBox1.Location = new System.Drawing.Point(265, 245);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(365, 208);
            this.richTextBox1.TabIndex = 72;
            this.richTextBox1.Text = "";
            // 
            // IDGnrtrBtn
            // 
            this.IDGnrtrBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.IDGnrtrBtn.Location = new System.Drawing.Point(414, 460);
            this.IDGnrtrBtn.Name = "IDGnrtrBtn";
            this.IDGnrtrBtn.Size = new System.Drawing.Size(94, 22);
            this.IDGnrtrBtn.TabIndex = 101;
            this.IDGnrtrBtn.Text = "Generate ID";
            this.IDGnrtrBtn.UseVisualStyleBackColor = true;
            this.IDGnrtrBtn.Visible = false;
            this.IDGnrtrBtn.Click += new System.EventHandler(this.IDGnrtrBtn_Click);
            // 
            // textVersion
            // 
            this.textVersion.BackColor = System.Drawing.SystemColors.Window;
            this.textVersion.Enabled = false;
            this.textVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textVersion.Location = new System.Drawing.Point(111, 155);
            this.textVersion.Name = "textVersion";
            this.textVersion.Size = new System.Drawing.Size(94, 20);
            this.textVersion.TabIndex = 102;
            // 
            // textBat
            // 
            this.textBat.BackColor = System.Drawing.SystemColors.Window;
            this.textBat.Enabled = false;
            this.textBat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBat.Location = new System.Drawing.Point(111, 87);
            this.textBat.Name = "textBat";
            this.textBat.Size = new System.Drawing.Size(94, 20);
            this.textBat.TabIndex = 98;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label27.Location = new System.Drawing.Point(20, 157);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(89, 16);
            this.label27.TabIndex = 101;
            this.label27.Text = "Rom Version:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label29.Location = new System.Drawing.Point(6, 89);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(103, 16);
            this.label29.TabIndex = 96;
            this.label29.Text = "Battery Voltage:";
            // 
            // textID
            // 
            this.textID.BackColor = System.Drawing.SystemColors.Window;
            this.textID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textID.Location = new System.Drawing.Point(111, 53);
            this.textID.Name = "textID";
            this.textID.Size = new System.Drawing.Size(94, 20);
            this.textID.TabIndex = 104;
            this.textID.Text = "0";
            // 
            // ClrLogBtn
            // 
            this.ClrLogBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ClrLogBtn.Location = new System.Drawing.Point(497, 459);
            this.ClrLogBtn.Name = "ClrLogBtn";
            this.ClrLogBtn.Size = new System.Drawing.Size(133, 23);
            this.ClrLogBtn.TabIndex = 81;
            this.ClrLogBtn.Text = "Clear Log";
            this.ClrLogBtn.UseVisualStyleBackColor = true;
            this.ClrLogBtn.Click += new System.EventHandler(this.ClrLogBtn_Click);
            // 
            // rstBtn
            // 
            this.rstBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.rstBtn.Location = new System.Drawing.Point(111, 263);
            this.rstBtn.Name = "rstBtn";
            this.rstBtn.Size = new System.Drawing.Size(94, 25);
            this.rstBtn.TabIndex = 109;
            this.rstBtn.Text = "Reset";
            this.rstBtn.UseVisualStyleBackColor = true;
            this.rstBtn.Click += new System.EventHandler(this.rstBtn_Click);
            // 
            // prntBtn
            // 
            this.prntBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.prntBtn.Location = new System.Drawing.Point(111, 227);
            this.prntBtn.Name = "prntBtn";
            this.prntBtn.Size = new System.Drawing.Size(94, 25);
            this.prntBtn.TabIndex = 110;
            this.prntBtn.Text = "Print";
            this.prntBtn.UseVisualStyleBackColor = true;
            this.prntBtn.Click += new System.EventHandler(this.prntBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(85, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 16);
            this.label3.TabIndex = 111;
            this.label3.Text = "ID:";
            // 
            // textPin
            // 
            this.textPin.BackColor = System.Drawing.SystemColors.Window;
            this.textPin.Enabled = false;
            this.textPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textPin.Location = new System.Drawing.Point(111, 189);
            this.textPin.Name = "textPin";
            this.textPin.Size = new System.Drawing.Size(94, 20);
            this.textPin.TabIndex = 115;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.Location = new System.Drawing.Point(75, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 16);
            this.label5.TabIndex = 114;
            this.label5.Text = "Pin=";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(5, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port to Connect:";
            // 
            // OpenPortBtn
            // 
            this.OpenPortBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.OpenPortBtn.Location = new System.Drawing.Point(20, 100);
            this.OpenPortBtn.Name = "OpenPortBtn";
            this.OpenPortBtn.Size = new System.Drawing.Size(195, 33);
            this.OpenPortBtn.TabIndex = 1;
            this.OpenPortBtn.Text = "Open Port";
            this.OpenPortBtn.UseVisualStyleBackColor = true;
            this.OpenPortBtn.Click += new System.EventHandler(this.OpenPort_Click);
            // 
            // comboPorts
            // 
            this.comboPorts.FormattingEnabled = true;
            this.comboPorts.Location = new System.Drawing.Point(108, 20);
            this.comboPorts.Name = "comboPorts";
            this.comboPorts.Size = new System.Drawing.Size(94, 21);
            this.comboPorts.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(38, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 105;
            this.label2.Text = "Type:";
            // 
            // comboTypes
            // 
            this.comboTypes.FormattingEnabled = true;
            this.comboTypes.Location = new System.Drawing.Point(87, 45);
            this.comboTypes.Name = "comboTypes";
            this.comboTypes.Size = new System.Drawing.Size(115, 21);
            this.comboTypes.TabIndex = 108;
            this.comboTypes.SelectedIndexChanged += new System.EventHandler(this.comboTypes_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboHardware);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.comboTypes);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboPorts);
            this.groupBox1.Controls.Add(this.OpenPortBtn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 147);
            this.groupBox1.TabIndex = 116;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configurattion";
            // 
            // comboHardware
            // 
            this.comboHardware.FormattingEnabled = true;
            this.comboHardware.Items.AddRange(new object[] {
            "WBRD102 rev3",
            "WBRD103"});
            this.comboHardware.Location = new System.Drawing.Point(87, 72);
            this.comboHardware.Name = "comboHardware";
            this.comboHardware.Size = new System.Drawing.Size(115, 21);
            this.comboHardware.TabIndex = 110;
            this.comboHardware.SelectedIndexChanged += new System.EventHandler(this.comboHardware_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label9.Location = new System.Drawing.Point(11, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 16);
            this.label9.TabIndex = 109;
            this.label9.Text = "Hardware:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtOficialVer);
            this.groupBox2.Controls.Add(this.txtPinLmt);
            this.groupBox2.Controls.Add(this.textValue);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.statusText);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textPin);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.prntBtn);
            this.groupBox2.Controls.Add(this.rstBtn);
            this.groupBox2.Controls.Add(this.textID);
            this.groupBox2.Controls.Add(this.textBat);
            this.groupBox2.Controls.Add(this.textVersion);
            this.groupBox2.Controls.Add(this.label29);
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Location = new System.Drawing.Point(12, 187);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(238, 294);
            this.groupBox2.TabIndex = 117;
            this.groupBox2.TabStop = false;
            // 
            // txtOficialVer
            // 
            this.txtOficialVer.AutoSize = true;
            this.txtOficialVer.Enabled = false;
            this.txtOficialVer.Location = new System.Drawing.Point(114, 175);
            this.txtOficialVer.Name = "txtOficialVer";
            this.txtOficialVer.Size = new System.Drawing.Size(14, 13);
            this.txtOficialVer.TabIndex = 124;
            this.txtOficialVer.Text = "X";
            // 
            // txtPinLmt
            // 
            this.txtPinLmt.AutoSize = true;
            this.txtPinLmt.Enabled = false;
            this.txtPinLmt.Location = new System.Drawing.Point(115, 211);
            this.txtPinLmt.Name = "txtPinLmt";
            this.txtPinLmt.Size = new System.Drawing.Size(14, 13);
            this.txtPinLmt.TabIndex = 123;
            this.txtPinLmt.Text = "X";
            // 
            // textValue
            // 
            this.textValue.BackColor = System.Drawing.SystemColors.Window;
            this.textValue.Enabled = false;
            this.textValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textValue.Location = new System.Drawing.Point(111, 121);
            this.textValue.Name = "textValue";
            this.textValue.Size = new System.Drawing.Size(94, 20);
            this.textValue.TabIndex = 122;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label8.Location = new System.Drawing.Point(28, 123);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 16);
            this.label8.TabIndex = 121;
            this.label8.Text = "Measurment:";
            // 
            // statusText
            // 
            this.statusText.BackColor = System.Drawing.SystemColors.Window;
            this.statusText.Enabled = false;
            this.statusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.statusText.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.statusText.Location = new System.Drawing.Point(111, 19);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(94, 20);
            this.statusText.TabIndex = 118;
            this.statusText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label6.Location = new System.Drawing.Point(61, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 117;
            this.label6.Text = "Status:";
            // 
            // checkBoxNotGenerae
            // 
            this.checkBoxNotGenerae.AutoSize = true;
            this.checkBoxNotGenerae.Location = new System.Drawing.Point(223, 227);
            this.checkBoxNotGenerae.Name = "checkBoxNotGenerae";
            this.checkBoxNotGenerae.Size = new System.Drawing.Size(110, 17);
            this.checkBoxNotGenerae.TabIndex = 124;
            this.checkBoxNotGenerae.Text = "Dont Generate ID";
            this.checkBoxNotGenerae.UseVisualStyleBackColor = true;
            this.checkBoxNotGenerae.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NG_Monitor.Properties.Resources.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(265, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(365, 170);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 118;
            this.pictureBox1.TabStop = false;
            // 
            // checkReused
            // 
            this.checkReused.AutoSize = true;
            this.checkReused.Location = new System.Drawing.Point(19, 168);
            this.checkReused.Name = "checkReused";
            this.checkReused.Size = new System.Drawing.Size(143, 17);
            this.checkReused.TabIndex = 125;
            this.checkReused.Text = "Sign sensor as REUSED";
            this.checkReused.UseVisualStyleBackColor = true;
            this.checkReused.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 491);
            this.Controls.Add(this.checkReused);
            this.Controls.Add(this.checkBoxNotGenerae);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.IDGnrtrBtn);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.ClrLogBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Config Wireless Sensor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        //        private System.Windows.Forms.Button eepFileBtn;
        //        private System.Windows.Forms.Button LoadXmlBtn;
        private System.Windows.Forms.Button IDGnrtrBtn;
        private System.Windows.Forms.TextBox textVersion;
        private System.Windows.Forms.TextBox textBat;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox textID;
        private System.Windows.Forms.Button ClrLogBtn;
        private System.Windows.Forms.Button rstBtn;
        private System.Windows.Forms.Button prntBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textPin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OpenPortBtn;
        private System.Windows.Forms.ComboBox comboPorts;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboTypes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox statusText;
        private System.Windows.Forms.TextBox textValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label txtPinLmt;
        private System.Windows.Forms.ComboBox comboHardware;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBoxNotGenerae;
        private System.Windows.Forms.Label txtOficialVer;
        private System.Windows.Forms.CheckBox checkReused;
    }
}

