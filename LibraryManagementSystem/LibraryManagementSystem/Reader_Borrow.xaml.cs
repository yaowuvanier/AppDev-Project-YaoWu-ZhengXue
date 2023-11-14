using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;

namespace LibraryManagementSystem
{
    public partial class Student_Borrow : Window
    {
        const string conString = "Host=localhost; Port=5432; Username=postgres; Password=125521; Database=vanierAEC2023fall; SearchPath=assignment";
        NpgsqlConnection npgsqlConnection;

        List<BookUtil> ltCus;
        BookUtil borrowUtil;
        User user;

        public Student_Borrow(User user)
        {
            InitializeComponent();
            npgsqlConnection = new NpgsqlConnection(conString);
            ltCus = new List<BookUtil>();
            button.IsEnabled = false;
            this.user = user;
            UpdateBookList();
        }

        public void UpdateBookList()
        {
            DataTable dt = new DataTable("table1");

            try
            {
                npgsqlConnection.Open();
                string cmdStr = "SELECT * FROM book";

                using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(cmdStr, npgsqlConnection))
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(npgsqlCommand))
                {
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                npgsqlConnection.Close();
            }

            listView.ItemsSource = null;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // query
        {
            DataTable dt = new DataTable("table1");

            try
            {
                npgsqlConnection.Open();
                string cmdStr = "SELECT * FROM book WHERE book_name = @name";

                using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(cmdStr, npgsqlConnection))
                {
                    npgsqlCommand.Parameters.AddWithValue("@name", name.Text);

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(npgsqlCommand))
                    {
                        adapter.Fill(dt);
                    }
                }
                listView.ItemsSource = null;
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
                MessageBox.Show("Query failed: " + ex.ToString());
            }
            finally
            {
                npgsqlConnection.Close();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)  // borrow
        {
            if (Convert.ToInt32(borrowUtil.Surplus) <= 0)
            {
                MessageBox.Show("All the books have been borrowed");
            }
            else
            {
                int result = 0;

                try
                {
                    npgsqlConnection.Open();
                    string cmdStr = "INSERT INTO BorrowReturnRecord (User_Id, Book_Id, Book_Name, Borrow_Date) " +
                                   "VALUES (@userId, @bookId, @bookName, @borrowDate)";

                    using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(cmdStr, npgsqlConnection))
                    {
                        int userId = int.Parse(user.Id);
                        int bookId = int.Parse(borrowUtil.Id); // Ensure that Book_Id matches the INT type in the table
                        string bookName = borrowUtil.Name;
                        DateTime borrowDate = DateTime.Now; // Borrow_Date should match the TIMESTAMP type in the table

                        npgsqlCommand.Parameters.AddWithValue("@userId", userId);
                        npgsqlCommand.Parameters.AddWithValue("@bookId", bookId);
                        npgsqlCommand.Parameters.AddWithValue("@bookName", bookName);
                        npgsqlCommand.Parameters.AddWithValue("@borrowDate", borrowDate);

                        result = npgsqlCommand.ExecuteNonQuery();
                    }

                    UpdateBookList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Borrowing failed: " + ex.ToString());
                }
                finally
                {
                    npgsqlConnection.Close();
                }

                if (result == 1)
                {
                    MessageBox.Show("Borrowing successfully!");
                }

                button.IsEnabled = false;
            }
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
