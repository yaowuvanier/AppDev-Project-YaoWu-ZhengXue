using System.Windows;
using Npgsql;
using System.Data;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Student_Register.xaml 
    /// </summary>
    public partial class Student_Register : Window
    {
        public Student_Register()
        {
            InitializeComponent();
        }

        private void Button_Click_login(object sender, RoutedEventArgs e) // login
        {
            User user = new User(reader_Id.Text, reader_Password.Password);
            if (user.UserLogin())
            {
                Reader reader = new Reader(user);
                Application.Current.MainWindow = reader;
                this.Close();
                reader.Show();
            }
            else
            {
                MessageBox.Show("login failed！");
            }
        }

        private void Button_Click_register(object sender, RoutedEventArgs e)  // register
        {

            User_register user_Register = new User_register();
            Application.Current.MainWindow = user_Register;
            user_Register.Show();
        }

        private void Button_Click_return(object sender, RoutedEventArgs e)  // return
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            this.Close();
            mainWindow.Show();
        }


        //modify user
        // Modify user
        public int UpdateUser(User user)
        {
            // PostgreSQL connection string
            const string conString = "Host=localhost; Port=5432; Username=postgres; Password=125521; Database=vanierAEC2023fall";
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(conString);

            NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
            string sql = "UPDATE assignment.reader SET userName = @userName, password = @password, email = @email, phoneNumber = @phoneNumber WHERE id = @id";

            npgsqlCommand.CommandText = sql;
            npgsqlCommand.Parameters.AddWithValue("@userName", user.UserName);
            npgsqlCommand.Parameters.AddWithValue("@password", user.Password);
            npgsqlCommand.Parameters.AddWithValue("@email", user.Email);
            npgsqlCommand.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
            npgsqlCommand.Parameters.AddWithValue("@id", user.Id);

            npgsqlConnection.Open();
            int result = npgsqlCommand.ExecuteNonQuery();
            npgsqlConnection.Close();

            return result;
        }



        private void Button_Click_exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }
    }
}
