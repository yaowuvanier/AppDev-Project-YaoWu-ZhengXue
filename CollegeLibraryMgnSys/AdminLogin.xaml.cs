using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CollegeLibraryMgnSys
{
    /// <summary>
    /// Interaction logic for AdminLogin.xaml
    /// </summary>
    public partial class AdminLogin : Window
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if ((tbAdminEmail.Text != string.Empty || tbAdminPass.Text != string.Empty) || (tbAdminEmail.Text != string.Empty && tbAdminPass.Text != string.Empty))
            {
                try
                {
                    if (tbAdminEmail.Text == "admin" && tbAdminPass.Text == "admin")
                    {
                        alertAdmin.Content = "";
                        MessageBox.Show("Logged in successfully...");
                        AdminHomePage adminHome = new AdminHomePage();
                        adminHome.Show();
                        tbAdminEmail.Clear();
                        tbAdminPass.Clear();
                    }
                    else
                    {

                        alertAdmin.Content = "Invalid email id or password...";
                        tbAdminEmail.Clear();
                        tbAdminPass.Clear();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Some unknown exception is occured!!!, Try again..");
                }
            }
            else
            {
                alertAdmin.Content = "Enter the fields properly...";
            }
        }

        private void ALclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
