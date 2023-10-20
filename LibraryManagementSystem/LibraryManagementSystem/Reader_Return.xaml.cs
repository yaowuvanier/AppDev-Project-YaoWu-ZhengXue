using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;

namespace LibraryManagementSystem
{
    public partial class Student_Return : Window
    {
        User user;
        const string conString = "Host=localhost; Port=5432; Username=postgres; Password=125521; Database=vanierAEC2023fall;  SearchPath=assignment";
        NpgsqlConnection npgsqlConnection;

        List<ReturnUtil> ltCus;
        ReturnUtil util;

        public Student_Return()
        {
            InitializeComponent();
        }

        public Student_Return(User user)
        {
            this.user = user;
            InitializeComponent();
            npgsqlConnection = new NpgsqlConnection(conString);
            ltCus = new List<ReturnUtil>();

            Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void Update()
        {
           
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
  
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // renew
        {
          
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // return
        {
           
        }
    }

    class ReturnUtil
    {
        public int Count { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string BorrowTime { get; set; }
        public string ReturnTime { get; set; }
    }
}
