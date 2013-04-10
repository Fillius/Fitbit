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
        private static SimpleAES simpleAES = new SimpleAES();

        public FrmSettings()
        {
            InitializeComponent();
        }

        private void btnCreateAuthorization_Click(object sender, EventArgs e)
        {
            AppSettings.Default.AuthToken = "";
            AppSettings.Default.AuthTokenSecret = "";
            AppSettings.Default.UserId = "";
            AppSettings.Default.Save();

            MessageBox.Show("Please restart the program to re-authorize it to Fitbit");
        }

        private void savePolarUser()
        {
            if (txtPolarUser.Text == "Username")
                return;

            SimpleAES simpleAES = new SimpleAES();

            AppSettings.Default.PPTUser = simpleAES.EncryptToString(txtPolarUser.Text);
        }

        private void savePolarPassword()
        {
            if (txtPolarPassword.Text == "Password")
                return;

            SimpleAES simpleAES = new SimpleAES();

            AppSettings.Default.PPTPassword = simpleAES.EncryptToString(txtPolarPassword.Text);
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            if (AppSettings.Default.PPTUser.Length > 0)
                txtPolarUser.Text = simpleAES.DecryptString(AppSettings.Default.PPTUser);

            if (AppSettings.Default.PPTPassword.Length > 0)
                txtPolarPassword.Text = simpleAES.DecryptString(AppSettings.Default.PPTPassword);

            if (AppSettings.Default.LastUploadedSession.Equals(new DateTime()))
                dateTimePicker1.Value = DateTime.Today.Subtract(new TimeSpan(30, 0, 0, 0));
            else
                dateTimePicker1.Value = AppSettings.Default.LastUploadedSession;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            AppSettings.Default.LastUploadedSession = dateTimePicker1.Value;
        }

        private void txtPolarUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                savePolarUser();
        }

        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppSettings.Default.Save();
        }

        private void txtPolarPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                savePolarPassword();
        }

        private void txtPolarUser_Leave(object sender, EventArgs e)
        {
            savePolarUser();
        }

        private void txtPolarPassword_Leave(object sender, EventArgs e)
        {
            savePolarPassword();
        }
    }
}
