using System.Windows;
using Npgsql;
namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml 
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_ReaderLogin(object sender, RoutedEventArgs e)  // student login endpoint
        {
            Student_Register student_Register = new Student_Register();
            Application.Current.MainWindow = student_Register;
            this.Close();
            student_Register.Show();
        }

        private void Button_Click_adminLogin(object sender, RoutedEventArgs e)   // administrator login endpoint
        {
            Administrator_Register administrator_Register = new Administrator_Register();
            Application.Current.MainWindow = administrator_Register;
            this.Close();
            administrator_Register.Show();
        }

        private void Button_Click_logout(object sender, RoutedEventArgs e)  // exit
        {
            Application.Current.Shutdown();
        }
    }
}
