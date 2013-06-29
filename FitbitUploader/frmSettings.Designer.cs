namespace FitbitUploader
{
    partial class FrmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            this.btnCreateAuthorization = new System.Windows.Forms.Button();
            this.txtPolarUser = new System.Windows.Forms.TextBox();
            this.lblPPTUser = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkRequireSport = new System.Windows.Forms.CheckBox();
            this.lblPPTPassword = new System.Windows.Forms.Label();
            this.txtPolarPassword = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.chkUploadHR = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateAuthorization
            // 
            this.btnCreateAuthorization.Location = new System.Drawing.Point(9, 19);
            this.btnCreateAuthorization.Name = "btnCreateAuthorization";
            this.btnCreateAuthorization.Size = new System.Drawing.Size(202, 23);
            this.btnCreateAuthorization.TabIndex = 2;
            this.btnCreateAuthorization.Text = "Re-Authorize";
            this.btnCreateAuthorization.UseVisualStyleBackColor = true;
            this.btnCreateAuthorization.Click += new System.EventHandler(this.btnCreateAuthorization_Click);
            // 
            // txtPolarUser
            // 
            this.txtPolarUser.Location = new System.Drawing.Point(9, 32);
            this.txtPolarUser.Name = "txtPolarUser";
            this.txtPolarUser.Size = new System.Drawing.Size(202, 20);
            this.txtPolarUser.TabIndex = 5;
            this.txtPolarUser.Text = "Username";
            this.txtPolarUser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPolarUser_KeyDown);
            this.txtPolarUser.Leave += new System.EventHandler(this.txtPolarUser_Leave);
            // 
            // lblPPTUser
            // 
            this.lblPPTUser.AutoSize = true;
            this.lblPPTUser.Location = new System.Drawing.Point(6, 16);
            this.lblPPTUser.Name = "lblPPTUser";
            this.lblPPTUser.Size = new System.Drawing.Size(88, 13);
            this.lblPPTUser.TabIndex = 7;
            this.lblPPTUser.Text = "Username (email)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkRequireSport);
            this.groupBox1.Controls.Add(this.lblPPTPassword);
            this.groupBox1.Controls.Add(this.txtPolarPassword);
            this.groupBox1.Controls.Add(this.lblPPTUser);
            this.groupBox1.Controls.Add(this.txtPolarUser);
            this.groupBox1.Location = new System.Drawing.Point(12, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(219, 123);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PolarPersonalTrainer.com";
            // 
            // chkRequireSport
            // 
            this.chkRequireSport.AutoSize = true;
            this.chkRequireSport.Location = new System.Drawing.Point(9, 100);
            this.chkRequireSport.Name = "chkRequireSport";
            this.chkRequireSport.Size = new System.Drawing.Size(202, 17);
            this.chkRequireSport.TabIndex = 11;
            this.chkRequireSport.Text = "Skip exercises without assigned sport";
            this.chkRequireSport.UseVisualStyleBackColor = true;
            this.chkRequireSport.CheckedChanged += new System.EventHandler(this.chkRequireSport_CheckedChanged);
            // 
            // lblPPTPassword
            // 
            this.lblPPTPassword.AutoSize = true;
            this.lblPPTPassword.Location = new System.Drawing.Point(6, 58);
            this.lblPPTPassword.Name = "lblPPTPassword";
            this.lblPPTPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPPTPassword.TabIndex = 9;
            this.lblPPTPassword.Text = "Password";
            // 
            // txtPolarPassword
            // 
            this.txtPolarPassword.Location = new System.Drawing.Point(9, 74);
            this.txtPolarPassword.Name = "txtPolarPassword";
            this.txtPolarPassword.Size = new System.Drawing.Size(202, 20);
            this.txtPolarPassword.TabIndex = 8;
            this.txtPolarPassword.Text = "Password";
            this.txtPolarPassword.UseSystemPasswordChar = true;
            this.txtPolarPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPolarPassword_KeyDown);
            this.txtPolarPassword.Leave += new System.EventHandler(this.txtPolarPassword_Leave);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCreateAuthorization);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(219, 55);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fitbit";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(11, 202);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(220, 20);
            this.dateTimePicker1.TabIndex = 10;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // chkUploadHR
            // 
            this.chkUploadHR.AutoSize = true;
            this.chkUploadHR.Location = new System.Drawing.Point(12, 228);
            this.chkUploadHR.Name = "chkUploadHR";
            this.chkUploadHR.Size = new System.Drawing.Size(211, 17);
            this.chkUploadHR.TabIndex = 12;
            this.chkUploadHR.Text = "Upload heart rate summary with Activity";
            this.chkUploadHR.UseVisualStyleBackColor = true;
            this.chkUploadHR.CheckedChanged += new System.EventHandler(this.chkUploadHR_CheckedChanged);
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 255);
            this.Controls.Add(this.chkUploadHR);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSettings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSettings_FormClosing);
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateAuthorization;
        private System.Windows.Forms.TextBox txtPolarUser;
        private System.Windows.Forms.Label lblPPTUser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPPTPassword;
        private System.Windows.Forms.TextBox txtPolarPassword;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.CheckBox chkRequireSport;
        private System.Windows.Forms.CheckBox chkUploadHR;
    }
}