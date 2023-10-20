using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using Npgsql;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for Admin_Opr_Reader.xaml 
    /// </summary>
    public partial class Admin_Opr_Reader : Window
    {
        // PostgreSQL connection string
        const string conString = "Host=localhost; Port=5432; Username=postgres; Password=125521; Database=vanierAEC2023fall";
        NpgsqlConnection npgsqlConnection;

        List<Users> ltCus;
        Users util;

        AdminOpr adminOpr;
        public Admin_Opr_Reader()
        {
            InitializeComponent();
        }

        public Admin_Opr_Reader(AdminOpr adminOpr)
        {

        }
        public void Update()
        {

         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)  //adding
        {

        }

        private void User_Register_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Update();
        }

        private void listView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)  //edit
        {
  
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) //query
        {
          
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) //delete
        {
         
        }
    }

    public class Users
    {
        public int Count { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

}
