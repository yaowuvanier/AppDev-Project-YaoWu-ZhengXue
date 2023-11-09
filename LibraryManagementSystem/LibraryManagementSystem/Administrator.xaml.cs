
using System;
using System.Diagnostics;
using System.Windows;
using Npgsql;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for Administrator.xaml 
    /// </summary>
    public partial class Administrator : Window
    {
        AdminOpr adminOpr;

        // PostgreSQL connection string
        const string conString = "Host=localhost; Port=5432; Username=postgres; Password=125521; Database=vanierAEC2023fall; SearchPath=assignment";

        public Administrator()
        {
            InitializeComponent();

        }
        public Administrator(AdminOpr adminOpr)
        {
            this.adminOpr = adminOpr;
            InitializeComponent();
            name.Text = adminOpr.AdminName;
            id.Text = adminOpr.Id;
            age.Text = adminOpr.Age.ToString();
            phone.Text = adminOpr.Phone;
            password.Password = adminOpr.Password;
            if (adminOpr.Sex == "Male")
                boy.IsChecked = true;
            else
                girl.IsChecked = true;


        }

        private void Button_Click(object sender, RoutedEventArgs e)//book management
        {
            Admain_Opr_Books admain_Opr_Books = new Admain_Opr_Books();
            Application.Current.MainWindow = admain_Opr_Books;
            admain_Opr_Books.Show();
        }

        private void Button_Click_ReaderManage(object sender, RoutedEventArgs e)  // reader management
        {

            try
            {
                Admin_Opr_Reader admin_Opr_Reader = new Admin_Opr_Reader(adminOpr);
                Application.Current.MainWindow = admin_Opr_Reader;
                admin_Opr_Reader.Show();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.ToString());
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void Button_Click_Logout(object sender, RoutedEventArgs e)  //return to mainwindow (login page)
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            this.Close();
            mainWindow.Show();
        }

        private void Button_Click_ModifyAdmin(object sender, RoutedEventArgs e)   //modify admin information
        {
            using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(conString))
            {
                int result = 0;
                try
                {
                    npgsqlConnection.Open();
                    using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
                    {
                        string cmdStr = "UPDATE manager SET manager_name = @name, age = @age, sex = @sex, telephone = @phone, password = @password WHERE manager_id = @id";

                        npgsqlCommand.CommandText = cmdStr;
                        npgsqlCommand.Parameters.AddWithValue("@name", name.Text);
                        int ageValue = int.Parse(age.Text);
                        npgsqlCommand.Parameters.AddWithValue("@age", ageValue);
                        npgsqlCommand.Parameters.AddWithValue("@sex", adminOpr.Sex);
                        npgsqlCommand.Parameters.AddWithValue("@phone", phone.Text);
                        npgsqlCommand.Parameters.AddWithValue("@password", password.Password);
                        npgsqlCommand.Parameters.AddWithValue("@id", id.Text);

                        result = npgsqlCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show("Modify failed!");
                }
                finally
                {
                    npgsqlConnection.Close();
                }

                if (result == 1)
                {
                    MessageBox.Show("Modify successfully!");
                }
            }
        }
       
        private void boy_Checked(object sender, RoutedEventArgs e)
        {
            boy.IsChecked = true;
            girl.IsChecked = false;
            adminOpr.Sex = "Male";

        }

        private void girl_Checked(object sender, RoutedEventArgs e)
        {
            boy.IsChecked = false;
            girl.IsChecked = true;
            adminOpr.Sex = "Femalw";

        }
    }
}
