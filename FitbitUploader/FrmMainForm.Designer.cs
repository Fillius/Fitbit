namespace FitbitUploader
{
    partial class FrmMainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainForm));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdoDataType2 = new System.Windows.Forms.RadioButton();
            this.rdoDataType1 = new System.Windows.Forms.RadioButton();
            this.dateDay = new System.Windows.Forms.DateTimePicker();
            this.btnGetFitbitData = new System.Windows.Forms.Button();
            this.btnUploadEntry = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnLoadPolar = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnBPLoad = new System.Windows.Forms.Button();
            this.btnBPUpload = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnGetPolarSessions = new System.Windows.Forms.Button();
            this.cmbFavorites = new System.Windows.Forms.ComboBox();
            this.btnReloadFavorites = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdoDataType2);
            this.groupBox4.Controls.Add(this.rdoDataType1);
            this.groupBox4.Controls.Add(this.dateDay);
            this.groupBox4.Controls.Add(this.btnGetFitbitData);
            this.groupBox4.Location = new System.Drawing.Point(18, 768);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(339, 149);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Retreive Fitbit Data";
            // 
            // rdoDataType2
            // 
            this.rdoDataType2.AutoSize = true;
            this.rdoDataType2.Checked = true;
            this.rdoDataType2.Location = new System.Drawing.Point(20, 105);
            this.rdoDataType2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoDataType2.Name = "rdoDataType2";
            this.rdoDataType2.Size = new System.Drawing.Size(96, 24);
            this.rdoDataType2.TabIndex = 16;
            this.rdoDataType2.TabStop = true;
            this.rdoDataType2.Text = "Activities";
            this.rdoDataType2.UseVisualStyleBackColor = true;
            // 
            // rdoDataType1
            // 
            this.rdoDataType1.AutoSize = true;
            this.rdoDataType1.Location = new System.Drawing.Point(20, 69);
            this.rdoDataType1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoDataType1.Name = "rdoDataType1";
            this.rdoDataType1.Size = new System.Drawing.Size(113, 24);
            this.rdoDataType1.TabIndex = 15;
            this.rdoDataType1.TabStop = true;
            this.rdoDataType1.Text = "Heart Rate";
            this.rdoDataType1.UseVisualStyleBackColor = true;
            // 
            // dateDay
            // 
            this.dateDay.Location = new System.Drawing.Point(20, 29);
            this.dateDay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dateDay.Name = "dateDay";
            this.dateDay.Size = new System.Drawing.Size(298, 26);
            this.dateDay.TabIndex = 11;
            // 
            // btnGetFitbitData
            // 
            this.btnGetFitbitData.Location = new System.Drawing.Point(146, 95);
            this.btnGetFitbitData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGetFitbitData.Name = "btnGetFitbitData";
            this.btnGetFitbitData.Size = new System.Drawing.Size(174, 35);
            this.btnGetFitbitData.TabIndex = 6;
            this.btnGetFitbitData.Text = "Get Fitbit Data";
            this.btnGetFitbitData.UseVisualStyleBackColor = true;
            this.btnGetFitbitData.Click += new System.EventHandler(this.btnGetFitbitData_Click);
            // 
            // btnUploadEntry
            // 
            this.btnUploadEntry.Location = new System.Drawing.Point(304, 723);
            this.btnUploadEntry.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnUploadEntry.Name = "btnUploadEntry";
            this.btnUploadEntry.Size = new System.Drawing.Size(152, 35);
            this.btnUploadEntry.TabIndex = 14;
            this.btnUploadEntry.Text = "Upload Selected";
            this.btnUploadEntry.UseVisualStyleBackColor = true;
            this.btnUploadEntry.Click += new System.EventHandler(this.btnCreateActivity_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(468, -3);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1089, 515);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // btnLoadPolar
            // 
            this.btnLoadPolar.Location = new System.Drawing.Point(18, 569);
            this.btnLoadPolar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLoadPolar.Name = "btnLoadPolar";
            this.btnLoadPolar.Size = new System.Drawing.Size(152, 35);
            this.btnLoadPolar.TabIndex = 12;
            this.btnLoadPolar.Text = "Load Polar XML";
            this.btnLoadPolar.UseVisualStyleBackColor = true;
            this.btnLoadPolar.Click += new System.EventHandler(this.btnLoadPolar_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(304, 678);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(152, 35);
            this.btnDelete.TabIndex = 17;
            this.btnDelete.Text = "Delete Entry";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(18, 678);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(152, 35);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnBPLoad
            // 
            this.btnBPLoad.Location = new System.Drawing.Point(18, 926);
            this.btnBPLoad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBPLoad.Name = "btnBPLoad";
            this.btnBPLoad.Size = new System.Drawing.Size(152, 35);
            this.btnBPLoad.TabIndex = 21;
            this.btnBPLoad.Text = "Load BP CSV";
            this.btnBPLoad.UseVisualStyleBackColor = true;
            this.btnBPLoad.Click += new System.EventHandler(this.btnBPLoad_Click);
            // 
            // btnBPUpload
            // 
            this.btnBPUpload.Location = new System.Drawing.Point(206, 926);
            this.btnBPUpload.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBPUpload.Name = "btnBPUpload";
            this.btnBPUpload.Size = new System.Drawing.Size(152, 35);
            this.btnBPUpload.TabIndex = 22;
            this.btnBPUpload.Text = "Upload BP Reading";
            this.btnBPUpload.UseVisualStyleBackColor = true;
            this.btnBPUpload.Click += new System.EventHandler(this.btnBPUpload_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(18, 723);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(152, 35);
            this.btnSettings.TabIndex = 23;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnGetPolarSessions
            // 
            this.btnGetPolarSessions.Location = new System.Drawing.Point(18, 614);
            this.btnGetPolarSessions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGetPolarSessions.Name = "btnGetPolarSessions";
            this.btnGetPolarSessions.Size = new System.Drawing.Size(152, 55);
            this.btnGetPolarSessions.TabIndex = 24;
            this.btnGetPolarSessions.Text = "Download Polar Sessions";
            this.btnGetPolarSessions.UseVisualStyleBackColor = true;
            this.btnGetPolarSessions.Click += new System.EventHandler(this.btnGetPolarSessions_Click);
            // 
            // cmbFavorites
            // 
            this.cmbFavorites.FormattingEnabled = true;
            this.cmbFavorites.Items.AddRange(new object[] {
            "Favorite Activities"});
            this.cmbFavorites.Location = new System.Drawing.Point(304, 572);
            this.cmbFavorites.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbFavorites.Name = "cmbFavorites";
            this.cmbFavorites.Size = new System.Drawing.Size(150, 28);
            this.cmbFavorites.TabIndex = 25;
            this.cmbFavorites.Text = "Set Sport";
            this.cmbFavorites.DropDown += new System.EventHandler(this.cmbFavorites_DropDown);
            this.cmbFavorites.SelectionChangeCommitted += new System.EventHandler(this.cmbFavorites_SelectionChangeCommitted);
            // 
            // btnReloadFavorites
            // 
            this.btnReloadFavorites.Location = new System.Drawing.Point(304, 614);
            this.btnReloadFavorites.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnReloadFavorites.Name = "btnReloadFavorites";
            this.btnReloadFavorites.Size = new System.Drawing.Size(152, 35);
            this.btnReloadFavorites.TabIndex = 26;
            this.btnReloadFavorites.Text = "Reload Favorites";
            this.btnReloadFavorites.UseVisualStyleBackColor = true;
            this.btnReloadFavorites.Click += new System.EventHandler(this.btnReloadFavorites_Click);
            // 
            // FrmMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::FitbitUploader.Properties.Resources.Fitbit_One1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1763, 1039);
            this.Controls.Add(this.btnReloadFavorites);
            this.Controls.Add(this.cmbFavorites);
            this.Controls.Add(this.btnGetPolarSessions);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnBPUpload);
            this.Controls.Add(this.btnBPLoad);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUploadEntry);
            this.Controls.Add(this.btnLoadPolar);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox4);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmMainForm";
            this.Text = "Fitbit Data Uploader";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnGetFitbitData;
        private System.Windows.Forms.DateTimePicker dateDay;
        private System.Windows.Forms.Button btnUploadEntry;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnLoadPolar;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.RadioButton rdoDataType2;
        private System.Windows.Forms.RadioButton rdoDataType1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnBPLoad;
        private System.Windows.Forms.Button btnBPUpload;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnGetPolarSessions;
        private System.Windows.Forms.ComboBox cmbFavorites;
        private System.Windows.Forms.Button btnReloadFavorites;
    }
}

