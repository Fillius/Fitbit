using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FitbitUploader
{
    public partial class Authorize : Form
    {
        private MainForm _parentForm;

        public Authorize()
        {
            InitializeComponent();
        }

        private void Verifier_Load(object sender, EventArgs e)
        {

        }

        public void Navigate(string url, MainForm mainForm)
        {
            _parentForm = mainForm;
            webBrowser1.Navigate(url);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Document != null)
            {
                var authResponse = webBrowser1.Document.GetElementById("content");
                if (authResponse != null)
                {
                    var verificationString = authResponse.InnerText;
                    if (verificationString.Contains("and enter the following PIN when requested"))
                    {
                        _parentForm.AuthorizeFormAuthorizeCompleted(verificationString.Remove(0, verificationString.LastIndexOf("\r\n") + "\r\n".Length));

                        Close();
                    }
                }
            }
        }
    }
}
