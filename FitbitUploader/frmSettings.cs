﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FitbitUploader
{
    public partial class FrmSettings : Form
    {
        private FrmMainForm mainForm;

        public FrmSettings(FrmMainForm frmMain)
        {
            InitializeComponent();

            mainForm = frmMain;

            if (AppSettings.Default.AuthToken.Length > 0)
                btnCreateAuthorization.Text = "Re-Authorize";
        }

        private void btnCreateAuthorization_Click(object sender, EventArgs e)
        {
            string authLink;

            AppSettings.Default.Reset();
            AppSettings.Default.Save();
            authLink = mainForm._oauth.AuthorizationLinkGet();

            var authorizeForm = new FrmBrowser();
            authorizeForm.Show();
            authorizeForm.navigate(authLink, mainForm);

            if (AppSettings.Default.AuthToken.Length > 0)
                btnCreateAuthorization.Text = "Re-Authorize";
        }

        private void btnPolarXMLHistory_Click(object sender, EventArgs e)
        {
            var filePicker = new OpenFileDialog();
            filePicker.DefaultExt = ".xml";
            filePicker.Filter = "Polar XML History File (*.xml)|*.xml";

            if (filePicker.ShowDialog() == DialogResult.OK)
            {
                AppSettings.Default.PolarXMLHistoryFile = filePicker.FileName;
                mainForm._uploadedFile = filePicker.FileName;
                AppSettings.Default.Save();
            }
        }

        private void btnPolarXMLHistorySchema_Click(object sender, EventArgs e)
        {
            var filePicker = new OpenFileDialog();
            filePicker.DefaultExt = ".xml";
            filePicker.Filter = "Polar XML History Schema File (*.xml)|*.xml";

            if (filePicker.ShowDialog() == DialogResult.OK)
            {
                AppSettings.Default.PolarXMLHistorySchemaFile = filePicker.FileName;
                mainForm._uploadedSchema = filePicker.FileName;
                AppSettings.Default.Save();
            }
        }
    }
}
