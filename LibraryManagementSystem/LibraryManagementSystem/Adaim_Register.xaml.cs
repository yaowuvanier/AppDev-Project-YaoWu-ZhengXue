using System;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for Adaim_Register.xaml 
    /// </summary>
    public partial class Adaim_Register : Window
    {
        AdminOpr adminOpr;
        public Adaim_Register()
        {
            adminOpr = new AdminOpr();
            InitializeComponent();

        }
        private void Button_Click(object sender, RoutedEventArgs e)  //register
        {
            if (id.Text.Equals(""))
            {
                MessageBox.Show("The account id cann't be empty");
            }
            else
            { 
                if (!adminOpr.sameAdmin(id.Text))
                {
                    if (password.Text.Equals(""))
                    {
                        MessageBox.Show("The password cann't be empty");
                    }
                    else if(!password.Text.Equals(password1.Text))
                    {
                        MessageBox.Show("The two passwords need the same");
                    }
                    else
                    {
                        adminOpr.AdminName = name.Text;
                        adminOpr.Id = id.Text;
                        //gender
                        adminOpr.Sex = GetSelectedGender(); // Call the function to get the selected gender

                        adminOpr.Age = Convert.ToInt32(age.Text);
                        adminOpr.Phone = phone.Text;
                        adminOpr.Password = password.Text;
                        if (adminOpr.adminRegister(adminOpr) > 0)
                        {
                            MessageBox.Show("register successfully");
                            Administrator_Register administrator_Register = new Administrator_Register();
                            Application.Current.MainWindow = administrator_Register;
                           administrator_Register.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("register failed");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("This account already exists");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (maleRadioButton.IsChecked == true)
            {
                adminOpr.Sex = "Male";
            }
            else if (femaleRadioButton.IsChecked == true)
            {
                adminOpr.Sex = "Female";
            }
            adminOpr.Sex = string.Empty; // Return an empty string or handle other cases as needed

        }


        // A separate function to get the selected gender
        private string GetSelectedGender()
        {
            if (maleRadioButton.IsChecked == true)
            {
                return "Male";
            }
            else if (femaleRadioButton.IsChecked == true)
            {
                return "Female";
            }
            return string.Empty; // Return an empty string or handle other cases as needed
        }
    }
}
