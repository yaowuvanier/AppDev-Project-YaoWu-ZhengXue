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
    /// Interaction logic for UserLoginPage.xaml
    /// </summary>
    public partial class UserLoginPage : Window
    {
        public UserLoginPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if ((tbUserEmail.Text != string.Empty || tbUserPass.Text != string.Empty) || (tbUserEmail.Text != string.Empty && tbUserPass.Text != string.Empty))
            {
                try
                {
                    
                    
                    if (tbUserEmail.Text=="user"&& tbUserPass.Text=="user")
                    {
                       
                        MessageBox.Show("Logged in successfully...");
                        UserHomePage UserHome = new UserHomePage();
                        UserHome.Show();
                        tbUserEmail.Clear();
                        tbUserPass.Clear();
                    }
                    else
                    {
                        alertUser.Content = "Invalid email id or password...";
                        tbUserEmail.Clear();
                        tbUserPass.Clear();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Some unknown exception is occured!!!, Try again..");
                }
            }
            else
            {
                alertUser.Content = "Enter the fields properly...";
            }

        }

        

        private void ULclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
