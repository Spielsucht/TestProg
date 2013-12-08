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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbPofilePath = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.grpEmo = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.trbrCalibration = new System.Windows.Forms.TrackBar();
            this.chbCalibration = new System.Windows.Forms.CheckBox();
            this.grpEmo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbrCalibration)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(328, 24);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
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
            this.grpEmo.Controls.Add(this.button1);
            this.grpEmo.Controls.Add(this.tbPofilePath);
            this.grpEmo.Controls.Add(this.btnOpen);
            this.grpEmo.Controls.Add(this.label1);
            this.grpEmo.Controls.Add(this.btnSearch);
            this.grpEmo.Location = new System.Drawing.Point(12, 12);
            this.grpEmo.Name = "grpEmo";
            this.grpEmo.Size = new System.Drawing.Size(493, 122);
            this.grpEmo.TabIndex = 5;
            this.grpEmo.TabStop = false;
            this.grpEmo.Text = "Emotiv Control";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 163);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chbCalibration);
            this.groupBox2.Controls.Add(this.trbrCalibration);
            this.groupBox2.Location = new System.Drawing.Point(12, 311);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(493, 167);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Sphero"});
            this.comboBox1.Location = new System.Drawing.Point(366, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Text = "keine";
            // 
            // trbrCalibration
            // 
            this.trbrCalibration.Location = new System.Drawing.Point(87, 19);
            this.trbrCalibration.Maximum = 365;
            this.trbrCalibration.Name = "trbrCalibration";
            this.trbrCalibration.Size = new System.Drawing.Size(400, 45);
            this.trbrCalibration.TabIndex = 0;
            this.trbrCalibration.TickFrequency = 10;
            this.trbrCalibration.TickStyle = System.Windows.Forms.TickStyle.None;
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
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 490);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpEmo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.grpEmo.ResumeLayout(false);
            this.grpEmo.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbrCalibration)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox tbPofilePath;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.GroupBox grpEmo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox chbCalibration;
        private System.Windows.Forms.TrackBar trbrCalibration;

    }
}

