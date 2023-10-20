using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for Search_Books.xaml 
    /// </summary>
    public partial class Admain_Opr_Books : Window
    {
        // PostgreSQL connection string
        const string conString = "Host=localhost; Port=5432; Username=postgres; Password=125521; Database=vanierAEC2023fall; SearchPath=assignment";
        NpgsqlConnection npgsqlConnection;


        List<BookUtil> ltCus;
        BookUtil bookUtil;
        public Admain_Opr_Books()
        {

        }
        public void Update()
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)  //edit
        {

        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //query
        {
         
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)  //add 
        {

        }

        private void Adaim_add_book_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Update();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) //DELETE
        {

        }
    }
}
