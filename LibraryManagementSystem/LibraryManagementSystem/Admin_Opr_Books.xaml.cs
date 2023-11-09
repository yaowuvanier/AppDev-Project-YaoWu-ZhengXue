using System;
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
            InitializeComponent();
            npgsqlConnection = new NpgsqlConnection(conString);
            ltCus = new List<BookUtil>();
            Update();
            bookUtil = new BookUtil();
        }

        public void Update()
        {
            if (npgsqlConnection == null)
                npgsqlConnection = new NpgsqlConnection(conString);

            DataTable dt = new DataTable("table1");

            try
            {
                npgsqlConnection.Open();
                string cmdStr = @"select * from book";

                NpgsqlCommand command = new NpgsqlCommand(cmdStr, npgsqlConnection);
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                adapter.Fill(dt);
                adapter.Dispose();

                npgsqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            listView.ItemsSource = null;
            ltCus.Clear();

            foreach (DataRow dataRow in dt.Rows)
            {
                BookUtil util = new BookUtil
                {
                    Count = ltCus.Count + 1,
                    Id = dataRow["book_id"].ToString(),
                    Name = dataRow["book_name"].ToString(),
                    Writer = dataRow["book_writer"].ToString(),
                    PublicTime = dataRow["publish_time"].ToString(),
                    Price = dataRow["book_price"].ToString(),
                    Number = dataRow["book_count"].ToString(),
                    Surplus = dataRow["book_surplus"].ToString()
                };
                ltCus.Add(util);
            }

            listView.ItemsSource = ltCus;
        }

        // Other methods (Button_Click, listView_SelectionChanged, Button_Click_1, Button_Click_2, Button_Click_3, Adaim_add_book_IsVisibleChanged, Button_Click_4) should be modified in a similar way as shown in the Update method.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)  //edit
        {
            Adaim_add_book adaim_add_book = new Adaim_add_book(bookUtil);
            Application.Current.MainWindow = adaim_add_book;
            adaim_add_book.IsVisibleChanged += Adaim_add_book_IsVisibleChanged;
            adaim_add_book.Show();
            button1.IsEnabled = false;
            button2.IsEnabled = false;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            button1.IsEnabled = true;
            button2.IsEnabled = true;
            bookUtil = (BookUtil)listView.SelectedItem;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e) //query
        {
            if (name.Text == "")
            {
                Update();
            }
            else
            {
                DataTable dt = new DataTable("table1");

                try
                {
                    npgsqlConnection.Open();
                    string cmdStr = "SELECT * FROM book WHERE book_name = @name";

                    using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(cmdStr, npgsqlConnection))
                    {
                        npgsqlCommand.Parameters.AddWithValue("@name", name.Text);

                        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(npgsqlCommand);
                        adapter.Fill(dt);
                    }
                    listView.ItemsSource = null;
                    npgsqlConnection.Close();
                    ltCus.Clear();

                    foreach (DataRow dataRow in dt.Rows)
                    {
                        BookUtil bookUtil = new BookUtil
                        {
                            Id = dataRow["book_id"].ToString(),
                            Name = dataRow["book_name"].ToString(),
                            Writer = dataRow["book_writer"].ToString(),
                            Price = dataRow["book_price"].ToString(),
                            Count = ltCus.Count + 1,
                            Number = dataRow["book_count"].ToString(),
                            PublicTime = dataRow["publish_time"].ToString(),
                            Surplus = dataRow["book_surplus"].ToString()
                        };

                        ltCus.Add(bookUtil);
                    }
                    listView.ItemsSource = ltCus;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("query failed: " + ex.ToString());
                }
            }
            button1.IsEnabled = false;
            button2.IsEnabled = false;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)  
        {
            Adaim_add_book adaim_add_book = new Adaim_add_book();
            Application.Current.MainWindow = adaim_add_book;
            adaim_add_book.IsVisibleChanged += Adaim_add_book_IsVisibleChanged;
            adaim_add_book.Show();
            button1.IsEnabled = false;
            button2.IsEnabled = false;
        }

        private void Adaim_add_book_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Update();
        }
        // ...

        private void Button_Click_4(object sender, RoutedEventArgs e) // Delete
        {
            int result = 0;

            try
            {
                npgsqlConnection.Open();
                NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
                string cmdStr = @"DELETE FROM book WHERE book_id = @id";

                npgsqlCommand.CommandText = cmdStr;
                npgsqlCommand.Parameters.AddWithValue("@id", bookUtil.Id);

                result = npgsqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Delete failed!");
            }
            finally
            {
                npgsqlConnection.Close();
            }

            listView.ItemsSource = null;
            Update();

            if (result == 1)
            {
                MessageBox.Show("Delete successful!");
            }

            button1.IsEnabled = false;
            button2.IsEnabled = false;
        }
    }
}
