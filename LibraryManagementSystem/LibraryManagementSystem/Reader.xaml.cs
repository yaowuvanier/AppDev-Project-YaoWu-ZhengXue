using System.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using Npgsql;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for student.xaml 
    /// </summary>
    public partial class Reader : Window
    {

        User user;
        public Reader()
        {
            InitializeComponent();                  
        }
        public Reader(User user)
        {
            InitializeComponent();
            this.user = user;
            name.Text = user.UserName;
            id.Text = user.Id;
            password.Password = user.Password;
            phone.Text = user.PhoneNumber;
            email.Text = user.Email;
   

        }

        private void Button_Click_BorrowBook(object sender, RoutedEventArgs e)  // borrow
        {
            Student_Borrow student_Borrow = new Student_Borrow(user);
            Application.Current.MainWindow = student_Borrow;
            student_Borrow.Show();
        }

        private void Button_Click_bookReturn(object sender, RoutedEventArgs e)  // return
        {
            Student_Return student_Return = new Student_Return(user);
            Application.Current.MainWindow = student_Return;
            student_Return.Show();

        }

        private void Button_Click_logout(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            this.Close();
            mainWindow.Show();
        }

        private void Button_Click_modifyInfo(object sender, RoutedEventArgs e)  //modify information
        {
            const string conString = "Host=localhost; Port=5432; Username=postgres; Password=125521; Database=vanierAEC2023fall; SearchPath=assignment";
            using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(conString))
            {
                int result = 0;
                try
                {
                    npgsqlConnection.Open();
                    using (NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand())
                    {
                        string cmdStr = "UPDATE reader SET username = @name, email=@email ,phonenumber = @phone, password = @password WHERE id = @id";

                        npgsqlCommand.CommandText = cmdStr;
                        npgsqlCommand.Parameters.AddWithValue("@name", name.Text);
                        npgsqlCommand.Parameters.AddWithValue("@phone", phone.Text);
                        npgsqlCommand.Parameters.AddWithValue("@email", email.Text);
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

    }

}
