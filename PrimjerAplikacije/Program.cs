using System;
using System.Windows.Forms;

namespace PrimjerAplikacije
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// 
        /// This application is displaying simple Facebook login process, using:
        /// 
        /// - Facebook C# SDK dependency (https://www.nuget.org/packages/Facebook/)
        /// - Chromium Web Browser dependency (https://www.nuget.org/packages/CefSharp.WinForms)
        /// 
        /// Application will open a Facebook login dialog for user and after succesfull login redirect him back to
        /// the application. 
        /// Then, it will create a HTTP request to the Facebook Graph API (https://developers.facebook.com/docs/graph-api)
        /// using Facebook SDK, and it will return user`s Facebook ID, name, profile picture, and email.
        /// 
        /// User info will be displayed in MainForm
        /// 
        /// Application GitHub repository: https://github.com/VickoFranic/c-sharp-primjer-aplikacije
        /// Created by: Vicko Franić
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
