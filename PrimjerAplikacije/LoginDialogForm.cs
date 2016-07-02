using System;
using System.Windows.Forms;

using CefSharp;
using CefSharp.WinForms;
using PrimjerAplikacije.lib.services;

namespace PrimjerAplikacije
{
    public partial class LoginDialogForm : Form
    {
        private Uri _loginUrl;

        // Using custom Chromium web browser, 
        // instead of default Internet Explorer (https://www.nuget.org/packages/CefSharp.WinForms)

        private ChromiumWebBrowser chromium; 

        public LoginDialogForm()
        {
            WindowState = FormWindowState.Maximized;
            InitializeComponent();
        }

        /**
         * Event handler for LoginDialogForm
         * Open Facebook login page in web browser on form load
         * FacebookService is used to generate Facebook URL to be opened in browser
         */
        private void LoginDialogForm_Load(object sender, EventArgs e)
        {
            _loginUrl = FacebookService.generateLoginUrl();

            chromium = new ChromiumWebBrowser(_loginUrl.AbsoluteUri) 
            {
                Dock = DockStyle.Fill
            };

            this.Controls.Add(chromium);

            chromium.AddressChanged += OnBrowserAddressChanged;
        }

        /**
         * Event handler for Chromium web browser address change
         * Check url each time browser navigates to new one
         * If url is a result of Facebook oAuth, close the dialog
         * oAuth data is stored in FacebookService
         */
        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
        {
            Uri callbackUrl = new Uri(args.Address);

            FacebookService.getAccessTokenFromCallback(callbackUrl);

            if (FacebookService.userAuthenticated())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
