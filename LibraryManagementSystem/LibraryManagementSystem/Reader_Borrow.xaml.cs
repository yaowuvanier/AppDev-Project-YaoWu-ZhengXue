using System;
using System.Collections.Generic;
using System.Data;
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
using Npgsql;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Student_Borrow.xaml 
    /// </summary>
    public partial class Student_Borrow : Window
    {
        // PostgreSQL connection string
        const string conString = "Host=localhost; Port=5432; Username=postgres; Password=125521; Database=vanierAEC2023fall";
        NpgsqlConnection npgsqlConnection;

        List<BookUtil> ltCus;
        BookUtil borrowUtil;
        User user;
        public Student_Borrow()
        {
            InitializeComponent();
        }

        public Student_Borrow(User user)
        {
            InitializeComponent();
            npgsqlConnection = new NpgsqlConnection(conString);
            
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //looking for book
        {
          
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)  //borrow
        {
           

        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            button.IsEnabled = true;
            borrowUtil = (BookUtil)listView.SelectedItem;
        }
    }

    public class BookUtil
    {
        public int Count { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Writer { get; set; }
        public string Price { get; set; }
        public string PublicTime { get; set; }
        public string Number { get; set; }
        public string Surplus { get; set; }
    }
}
