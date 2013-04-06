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
            this.lblPPTPassword = new System.Windows.Forms.Label();
            this.txtPolarPassword = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateAuthorization
            // 
            this.btnCreateAuthorization.Location = new System.Drawing.Point(9, 19);
            this.btnCreateAuthorization.Name = "btnCreateAuthorization";
            this.btnCreateAuthorization.Size = new System.Drawing.Size(156, 23);
            this.btnCreateAuthorization.TabIndex = 2;
            this.btnCreateAuthorization.Text = "Authorize";
            this.btnCreateAuthorization.UseVisualStyleBackColor = true;
            this.btnCreateAuthorization.Click += new System.EventHandler(this.btnCreateAuthorization_Click);
            // 
            // txtPolarUser
            // 
            this.txtPolarUser.Location = new System.Drawing.Point(9, 32);
            this.txtPolarUser.Name = "txtPolarUser";
            this.txtPolarUser.Size = new System.Drawing.Size(156, 20);
            this.txtPolarUser.TabIndex = 5;
            this.txtPolarUser.Text = "Username";
            // 
            // lblPPTUser
            // 
            this.lblPPTUser.AutoSize = true;
            this.lblPPTUser.Location = new System.Drawing.Point(6, 16);
            this.lblPPTUser.Name = "lblPPTUser";
            this.lblPPTUser.Size = new System.Drawing.Size(55, 13);
            this.lblPPTUser.TabIndex = 7;
            this.lblPPTUser.Text = "Username";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPPTPassword);
            this.groupBox1.Controls.Add(this.txtPolarPassword);
            this.groupBox1.Controls.Add(this.lblPPTUser);
            this.groupBox1.Controls.Add(this.txtPolarUser);
            this.groupBox1.Location = new System.Drawing.Point(12, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 100);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PolarPersonalTrainer.com";
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
            this.txtPolarPassword.Size = new System.Drawing.Size(156, 20);
            this.txtPolarPassword.TabIndex = 8;
            this.txtPolarPassword.Text = "Password";
            this.txtPolarPassword.UseSystemPasswordChar = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCreateAuthorization);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(172, 55);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fitbit";
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(197, 183);
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

        }

        #endregion

        private System.Windows.Forms.Button btnCreateAuthorization;
        private System.Windows.Forms.TextBox txtPolarUser;
        private System.Windows.Forms.Label lblPPTUser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPPTPassword;
        private System.Windows.Forms.TextBox txtPolarPassword;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}