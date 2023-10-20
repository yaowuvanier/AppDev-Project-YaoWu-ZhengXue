
using System;
using System.Windows;
using Npgsql;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for Adaim_add_book.xaml
    /// </summary>
    public partial class Adaim_add_book : Window
    {


        // PostgreSQL connection string
        const string conString = "Host=localhost; Port=5432; Username=postgres; Password=125521; Database=vanierAEC2023fall; SearchPath=assignment";
        NpgsqlConnection npgsqlConnection;

        public Adaim_add_book()
        {


        }
        public Adaim_add_book(BookUtil bookUtil)
        {

           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
