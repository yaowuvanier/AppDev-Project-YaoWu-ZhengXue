using System.Windows;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for Administrator_Register.xaml 
    /// </summary>
    public partial class Administrator_Register : Window
    {
        public Administrator_Register()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)  // login
        {

            AdminOpr adminOpr = new AdminOpr(id.Text, password.Password);

            if(adminOpr.adminLogin())
            {
                //MessageBox.Show("login successfully");

                Administrator administrator = new Administrator(adminOpr);
                Application.Current.MainWindow = administrator;
                this.Close();
                administrator.Show();
            }
            else
            {
                MessageBox.Show("login failed");

            }
        
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)  // register
        {


            Adaim_Register adaim_Register = new Adaim_Register();
            Application.Current.MainWindow = adaim_Register;
            adaim_Register.Show();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)  // return
        {
            //Administrator_Register
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            this.Close();
            mainWindow.Show();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }
    }



}
