using System;
using System.Collections.Generic;
using System.Data.Common;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace WpfApp1
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
        public static void establishConnect()
        {
            try 
            {
                NpgsqlConnection con = GetNqgsqlConnection();
                con.Open();
                MessageBox.Show("Connection Established");
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static NpgsqlConnection GetNqgsqlConnection()
        {
            const string conString = "Host=localhost; Port=5432; Username=postgres; Password=125521; Database=vanierAEC2023fall";
            return new NpgsqlConnection(conString);
        }

        private void RunAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.ShowDialog();
;       }

        private void RunSales_Click(object sender, RoutedEventArgs e) 
        {
            SalesWindow salesWindow = new SalesWindow();
            salesWindow.ShowDialog();
        }
    }


}
