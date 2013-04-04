using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FitbitUploader
{
    public partial class FrmBrowser : Form
    {
        private String authPin = null;
        private String url = null;

        public FrmBrowser(String url)
        {
            InitializeComponent();
            this.url = url;
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser.Document != null)
            {
                var authResponse = webBrowser.Document.GetElementById("content");

                if (authResponse != null)
                {
                    var verificationString = authResponse.InnerText;

                    if (verificationString.Contains("and enter the following PIN when requested"))
                    {
                        this.authPin = verificationString.Remove(0, verificationString.LastIndexOf("\r\n") + "\r\n".Length); ;

                        Close();
                    }
                }
            }
        }

        private void FrmBrowser_Shown(object sender, EventArgs e)
        {
            webBrowser.Navigate(url);
        }

        public String getAuthPin()
        {
            return authPin;
        }
    }
}
