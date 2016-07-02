using System;
using System.Windows.Forms;

using PrimjerAplikacije.lib.models;
using PrimjerAplikacije.lib.services;

namespace PrimjerAplikacije
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /**
         * Event handler for Facebook login button - open FB login page in web browser
         */
        private void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Hide();

            LoginDialogForm ldf = new LoginDialogForm();

            ldf.ShowDialog();

            /** 
             * If Dialog result is OK, it means user is fetched from Facebook API
             * and his oAuth data (access token) is saved in FacebookService
             * 
             * Fetch user data from Facebook with stored access token
             */
            if (ldf.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                User user = FacebookService.getUserDataFromFacebook(); // Using stored token

                if (user == null)
                    this.Dispose();
                else
                    showUserInfo(user);
            }
            else
            {
                this.Dispose();
            }
        }

        /**
         * Set user info for labels and picture and display it
         */
        private void showUserInfo(User user)
        {
            pictureBox1.Load(user.picture);
            label1.Text = "ID: " + user.id;
            label2.Text = "Name: " + user.name;
            label3.Text = "Email: " + user.email;

            pictureBox1.Show();
            label1.Show();
            label2.Show();
            label3.Show();
        }
    }
}
