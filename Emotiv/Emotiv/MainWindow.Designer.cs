namespace Emotiv
{
    partial class MainWindow
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbEmoStatus = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbPofilePath = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.grpEmo = new System.Windows.Forms.GroupBox();
            this.lbHeadsetStatus = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioExp = new System.Windows.Forms.RadioButton();
            this.radioKeyboard = new System.Windows.Forms.RadioButton();
            this.radioCog = new System.Windows.Forms.RadioButton();
            this.lbConnection = new System.Windows.Forms.Label();
            this.cbDevices = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btSleep = new System.Windows.Forms.Button();
            this.trbrSpeed = new System.Windows.Forms.TrackBar();
            this.lbSpeed = new System.Windows.Forms.Label();
            this.chbCalibration = new System.Windows.Forms.CheckBox();
            this.trbrCalibration = new System.Windows.Forms.TrackBar();
            this.pbHeadsetBattery = new System.Windows.Forms.ProgressBar();
            this.lbBattery = new System.Windows.Forms.Label();
            this.rbGyro = new System.Windows.Forms.RadioButton();
            this.grpEmo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbrSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbrCalibration)).BeginInit();
            this.SuspendLayout();
            // 
            // lbEmoStatus
            // 
            this.lbEmoStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbEmoStatus.AutoSize = true;
            this.lbEmoStatus.Location = new System.Drawing.Point(328, 24);
            this.lbEmoStatus.Name = "lbEmoStatus";
            this.lbEmoStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbEmoStatus.Size = new System.Drawing.Size(66, 13);
            this.lbEmoStatus.TabIndex = 0;
            this.lbEmoStatus.Text = "lbEmoStatus";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(412, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Disconnect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(412, 74);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Durchsuchen";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // tbPofilePath
            // 
            this.tbPofilePath.Enabled = false;
            this.tbPofilePath.Location = new System.Drawing.Point(87, 76);
            this.tbPofilePath.Name = "tbPofilePath";
            this.tbPofilePath.Size = new System.Drawing.Size(319, 20);
            this.tbPofilePath.TabIndex = 3;
            // 
            // btnOpen
            // 
            this.btnOpen.Enabled = false;
            this.btnOpen.Location = new System.Drawing.Point(6, 74);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 4;
            this.btnOpen.Text = "Öffnen";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // grpEmo
            // 
            this.grpEmo.Controls.Add(this.lbBattery);
            this.grpEmo.Controls.Add(this.pbHeadsetBattery);
            this.grpEmo.Controls.Add(this.lbHeadsetStatus);
            this.grpEmo.Controls.Add(this.button1);
            this.grpEmo.Controls.Add(this.tbPofilePath);
            this.grpEmo.Controls.Add(this.btnOpen);
            this.grpEmo.Controls.Add(this.lbEmoStatus);
            this.grpEmo.Controls.Add(this.btnSearch);
            this.grpEmo.Location = new System.Drawing.Point(12, 12);
            this.grpEmo.Name = "grpEmo";
            this.grpEmo.Size = new System.Drawing.Size(493, 122);
            this.grpEmo.TabIndex = 5;
            this.grpEmo.TabStop = false;
            this.grpEmo.Text = "Emotiv Control";
            // 
            // lbHeadsetStatus
            // 
            this.lbHeadsetStatus.AutoSize = true;
            this.lbHeadsetStatus.Location = new System.Drawing.Point(7, 19);
            this.lbHeadsetStatus.Name = "lbHeadsetStatus";
            this.lbHeadsetStatus.Size = new System.Drawing.Size(35, 13);
            this.lbHeadsetStatus.TabIndex = 5;
            this.lbHeadsetStatus.Text = "label1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbGyro);
            this.groupBox1.Controls.Add(this.radioExp);
            this.groupBox1.Controls.Add(this.radioKeyboard);
            this.groupBox1.Controls.Add(this.radioCog);
            this.groupBox1.Controls.Add(this.lbConnection);
            this.groupBox1.Controls.Add(this.cbDevices);
            this.groupBox1.Location = new System.Drawing.Point(12, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 66);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // radioExp
            // 
            this.radioExp.AutoSize = true;
            this.radioExp.Location = new System.Drawing.Point(87, 19);
            this.radioExp.Name = "radioExp";
            this.radioExp.Size = new System.Drawing.Size(52, 17);
            this.radioExp.TabIndex = 4;
            this.radioExp.TabStop = true;
            this.radioExp.Text = "Mimik";
            this.radioExp.UseVisualStyleBackColor = true;
            this.radioExp.Click += new System.EventHandler(this.RadioButtons_Checked);
            // 
            // radioKeyboard
            // 
            this.radioKeyboard.AutoSize = true;
            this.radioKeyboard.Location = new System.Drawing.Point(6, 42);
            this.radioKeyboard.Name = "radioKeyboard";
            this.radioKeyboard.Size = new System.Drawing.Size(64, 17);
            this.radioKeyboard.TabIndex = 3;
            this.radioKeyboard.TabStop = true;
            this.radioKeyboard.Text = "Tastatur";
            this.radioKeyboard.UseVisualStyleBackColor = true;
            this.radioKeyboard.Click += new System.EventHandler(this.RadioButtons_Checked);
            this.radioKeyboard.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainWindow_KeyPress);
            this.radioKeyboard.KeyUp += new System.Windows.Forms.KeyEventHandler(this.radioKeyboard_KeyUp);
            // 
            // radioCog
            // 
            this.radioCog.AutoSize = true;
            this.radioCog.Location = new System.Drawing.Point(6, 19);
            this.radioCog.Name = "radioCog";
            this.radioCog.Size = new System.Drawing.Size(75, 17);
            this.radioCog.TabIndex = 2;
            this.radioCog.TabStop = true;
            this.radioCog.Text = "Gedanken";
            this.radioCog.UseVisualStyleBackColor = true;
            this.radioCog.Click += new System.EventHandler(this.RadioButtons_Checked);
            // 
            // lbConnection
            // 
            this.lbConnection.AutoSize = true;
            this.lbConnection.Location = new System.Drawing.Point(366, 47);
            this.lbConnection.Name = "lbConnection";
            this.lbConnection.Size = new System.Drawing.Size(69, 13);
            this.lbConnection.TabIndex = 1;
            this.lbConnection.Text = "lbConnection";
            // 
            // cbDevices
            // 
            this.cbDevices.FormattingEnabled = true;
            this.cbDevices.Items.AddRange(new object[] {
            "Sphero"});
            this.cbDevices.Location = new System.Drawing.Point(366, 19);
            this.cbDevices.Name = "cbDevices";
            this.cbDevices.Size = new System.Drawing.Size(121, 21);
            this.cbDevices.TabIndex = 0;
            this.cbDevices.Text = "keine";
            this.cbDevices.SelectedIndexChanged += new System.EventHandler(this.cbDevices_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btSleep);
            this.groupBox2.Controls.Add(this.trbrSpeed);
            this.groupBox2.Controls.Add(this.lbSpeed);
            this.groupBox2.Controls.Add(this.chbCalibration);
            this.groupBox2.Controls.Add(this.trbrCalibration);
            this.groupBox2.Location = new System.Drawing.Point(12, 213);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(493, 167);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sphero";
            // 
            // btSleep
            // 
            this.btSleep.Location = new System.Drawing.Point(402, 41);
            this.btSleep.Name = "btSleep";
            this.btSleep.Size = new System.Drawing.Size(85, 23);
            this.btSleep.TabIndex = 4;
            this.btSleep.Text = "Schlaf-Modus";
            this.btSleep.UseVisualStyleBackColor = true;
            this.btSleep.Visible = false;
            // 
            // trbrSpeed
            // 
            this.trbrSpeed.Location = new System.Drawing.Point(87, 70);
            this.trbrSpeed.Name = "trbrSpeed";
            this.trbrSpeed.Size = new System.Drawing.Size(201, 45);
            this.trbrSpeed.TabIndex = 3;
            this.trbrSpeed.Value = 4;
            this.trbrSpeed.Scroll += new System.EventHandler(this.trbrSpeed_Scroll);
            // 
            // lbSpeed
            // 
            this.lbSpeed.AutoSize = true;
            this.lbSpeed.Location = new System.Drawing.Point(3, 70);
            this.lbSpeed.Name = "lbSpeed";
            this.lbSpeed.Size = new System.Drawing.Size(46, 13);
            this.lbSpeed.TabIndex = 2;
            this.lbSpeed.Text = "lbSpeed";
            // 
            // chbCalibration
            // 
            this.chbCalibration.AutoSize = true;
            this.chbCalibration.Location = new System.Drawing.Point(6, 19);
            this.chbCalibration.Name = "chbCalibration";
            this.chbCalibration.Size = new System.Drawing.Size(81, 17);
            this.chbCalibration.TabIndex = 1;
            this.chbCalibration.Text = "Kalibrierung";
            this.chbCalibration.UseVisualStyleBackColor = true;
            this.chbCalibration.CheckedChanged += new System.EventHandler(this.chbCalibration_CheckedChanged);
            // 
            // trbrCalibration
            // 
            this.trbrCalibration.Location = new System.Drawing.Point(87, 19);
            this.trbrCalibration.Maximum = 365;
            this.trbrCalibration.Name = "trbrCalibration";
            this.trbrCalibration.Size = new System.Drawing.Size(400, 45);
            this.trbrCalibration.TabIndex = 0;
            this.trbrCalibration.TabStop = false;
            this.trbrCalibration.TickFrequency = 359;
            this.trbrCalibration.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbrCalibration.Value = 180;
            this.trbrCalibration.Scroll += new System.EventHandler(this.trbrCalibration_Scroll);
            // 
            // pbHeadsetBattery
            // 
            this.pbHeadsetBattery.Location = new System.Drawing.Point(59, 45);
            this.pbHeadsetBattery.Name = "pbHeadsetBattery";
            this.pbHeadsetBattery.Size = new System.Drawing.Size(100, 23);
            this.pbHeadsetBattery.Step = 5;
            this.pbHeadsetBattery.TabIndex = 6;
            // 
            // lbBattery
            // 
            this.lbBattery.AutoSize = true;
            this.lbBattery.Location = new System.Drawing.Point(7, 47);
            this.lbBattery.Name = "lbBattery";
            this.lbBattery.Size = new System.Drawing.Size(46, 13);
            this.lbBattery.TabIndex = 7;
            this.lbBattery.Text = "Batterie:";
            // 
            // rbGyro
            // 
            this.rbGyro.AutoSize = true;
            this.rbGyro.Location = new System.Drawing.Point(87, 42);
            this.rbGyro.Name = "rbGyro";
            this.rbGyro.Size = new System.Drawing.Size(70, 17);
            this.rbGyro.TabIndex = 5;
            this.rbGyro.TabStop = true;
            this.rbGyro.Text = "Gyroskop";
            this.rbGyro.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 395);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpEmo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.grpEmo.ResumeLayout(false);
            this.grpEmo.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbrSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbrCalibration)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbEmoStatus;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox tbPofilePath;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.GroupBox grpEmo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chbCalibration;
        private System.Windows.Forms.TrackBar trbrCalibration;
        private System.Windows.Forms.Label lbConnection;
        private System.Windows.Forms.ComboBox cbDevices;
        private System.Windows.Forms.RadioButton radioKeyboard;
        private System.Windows.Forms.RadioButton radioCog;
        private System.Windows.Forms.Label lbHeadsetStatus;
        private System.Windows.Forms.RadioButton radioExp;
        private System.Windows.Forms.TrackBar trbrSpeed;
        private System.Windows.Forms.Label lbSpeed;
        private System.Windows.Forms.Button btSleep;
        private System.Windows.Forms.ProgressBar pbHeadsetBattery;
        private System.Windows.Forms.Label lbBattery;
        private System.Windows.Forms.RadioButton rbGyro;

    }
}

