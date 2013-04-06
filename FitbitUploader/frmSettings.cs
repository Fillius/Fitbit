using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FitbitUploader.Properties;
using FitbitUploader.Encryption;

namespace FitbitUploader
{
    public partial class FrmSettings : Form
    {
        private FrmMainForm mainForm;

        private static SimpleAES simpleAES = new SimpleAES();

        public FrmSettings(FrmMainForm frmMain)
        {
            InitializeComponent();

            mainForm = frmMain;

            if (AppSettings.Default.AuthToken.Length > 0)
                btnCreateAuthorization.Text = "Re-Authorize";
        }

        private void btnCreateAuthorization_Click(object sender, EventArgs e)
        {
            AppSettings.Default.AuthToken = "";
            AppSettings.Default.AuthTokenSecret = "";
            AppSettings.Default.UserId = "";
            AppSettings.Default.Save();

            MessageBox.Show("Title", "Please restart the program to re-authorize it to Fitbit");
        }

        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            SimpleAES simpleAES = new SimpleAES();

            AppSettings.Default.PPTUser = simpleAES.EncryptToString(txtPolarUser.Text);
            AppSettings.Default.PPTPassword = simpleAES.EncryptToString(txtPolarPassword.Text);

            AppSettings.Default.Save();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            if (AppSettings.Default.PPTUser.Length > 0)
                txtPolarUser.Text = simpleAES.DecryptString(AppSettings.Default.PPTUser);

            if (AppSettings.Default.PPTPassword.Length > 0)
                txtPolarPassword.Text = simpleAES.DecryptString(AppSettings.Default.PPTPassword);
        }
    }
}
