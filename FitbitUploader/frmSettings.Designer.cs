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
            this.btnPolarXMLHistory = new System.Windows.Forms.Button();
            this.btnPolarXMLHistorySchema = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCreateAuthorization
            // 
            this.btnCreateAuthorization.Location = new System.Drawing.Point(12, 12);
            this.btnCreateAuthorization.Name = "btnCreateAuthorization";
            this.btnCreateAuthorization.Size = new System.Drawing.Size(177, 23);
            this.btnCreateAuthorization.TabIndex = 2;
            this.btnCreateAuthorization.Text = "Authorize";
            this.btnCreateAuthorization.UseVisualStyleBackColor = true;
            this.btnCreateAuthorization.Click += new System.EventHandler(this.btnCreateAuthorization_Click);
            // 
            // btnPolarXMLHistory
            // 
            this.btnPolarXMLHistory.Location = new System.Drawing.Point(12, 41);
            this.btnPolarXMLHistory.Name = "btnPolarXMLHistory";
            this.btnPolarXMLHistory.Size = new System.Drawing.Size(177, 23);
            this.btnPolarXMLHistory.TabIndex = 3;
            this.btnPolarXMLHistory.Text = "Polar XML History File";
            this.btnPolarXMLHistory.UseVisualStyleBackColor = true;
            this.btnPolarXMLHistory.Click += new System.EventHandler(this.btnPolarXMLHistory_Click);
            // 
            // btnPolarXMLHistorySchema
            // 
            this.btnPolarXMLHistorySchema.Location = new System.Drawing.Point(12, 70);
            this.btnPolarXMLHistorySchema.Name = "btnPolarXMLHistorySchema";
            this.btnPolarXMLHistorySchema.Size = new System.Drawing.Size(177, 23);
            this.btnPolarXMLHistorySchema.TabIndex = 4;
            this.btnPolarXMLHistorySchema.Text = "Polar XML History Schema File";
            this.btnPolarXMLHistorySchema.UseVisualStyleBackColor = true;
            this.btnPolarXMLHistorySchema.Click += new System.EventHandler(this.btnPolarXMLHistorySchema_Click);
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(203, 104);
            this.Controls.Add(this.btnPolarXMLHistorySchema);
            this.Controls.Add(this.btnPolarXMLHistory);
            this.Controls.Add(this.btnCreateAuthorization);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSettings";
            this.Text = "Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateAuthorization;
        private System.Windows.Forms.Button btnPolarXMLHistory;
        private System.Windows.Forms.Button btnPolarXMLHistorySchema;
    }
}