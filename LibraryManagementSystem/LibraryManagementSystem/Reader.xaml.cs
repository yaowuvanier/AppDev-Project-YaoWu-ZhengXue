using System.Windows;
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

        private void Button_Click(object sender, RoutedEventArgs e)  // borrow
        {
            Student_Borrow student_Borrow = new Student_Borrow(user);
            Application.Current.MainWindow = student_Borrow;
            student_Borrow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)  // return
        {
            Student_Return student_Return = new Student_Return(user);
            Application.Current.MainWindow = student_Return;
            student_Return.Show();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            this.Close();
            mainWindow.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)  //modify information
        {
            // PostgreSQL connection string
            const string conString = "Host=localhost; Port=5432; Username=postgres; Password=125521; Database=vanierAEC2023fall";

            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(conString);
           
        }




    }

}
