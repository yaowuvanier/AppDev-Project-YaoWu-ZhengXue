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
        int bookId; // Add a bookId field
        const string conString = "Host=localhost; Port=5432; Username=postgres; Password=125521; Database=vanierAEC2023fall;  SearchPath=assignment";
        NpgsqlConnection npgsqlConnection;

        List<ReturnUtil> ltCus;
        ReturnUtil util;

        public Student_Return(User user)
        {
            this.user = user;
            InitializeComponent();
            npgsqlConnection = new NpgsqlConnection(conString);
            ltCus = new List<ReturnUtil>();
            Update();
        }

        public Student_Return(User user, int bookId) // Pass bookId to the constructor
        {
            this.user = user;
            this.bookId = bookId;
            InitializeComponent();
            npgsqlConnection = new NpgsqlConnection(conString);
            ltCus = new List<ReturnUtil>();
            Update();
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void Update()
        {
            DataTable dt = new DataTable("table1");

            try
            {
                npgsqlConnection.Open();

                string cmdStr = "SELECT * FROM BorrowReturnRecord WHERE User_Id = @userId::int ";  //AND Book_Id = @bookId::int
                using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(cmdStr, npgsqlConnection))
                {
                    npgsqlCommand.Parameters.AddWithValue("@userId", user.Id);
                   // npgsqlCommand.Parameters.AddWithValue("@bookId", bookId);
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(npgsqlCommand))
                    {
                        adapter.Fill(dt);
                    }
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

            ltCus.Clear();
            foreach (DataRow dataRow in dt.Rows)
            {
                ReturnUtil returnUtil = new ReturnUtil
                {
                    Id = dataRow["BOOK_ID"].ToString(),
                    Name = dataRow["BOOK_NAME"].ToString(),
                    BorrowTime = dataRow["BORROW_DATE"].ToString(),
                    Count = ltCus.Count + 1,
                    ReturnTime = dataRow["RETURN_DATE"].ToString()
                };
                ltCus.Add(returnUtil);
            }

            listView.ItemsSource = ltCus;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            button_renew.IsEnabled = true;
            button_return.IsEnabled = true;
            util = (ReturnUtil)listView.SelectedItem;
        }

        private void Button_Click_Renew(object sender, RoutedEventArgs e) // renewal
        {
            int result = 0;

            try
            {
                npgsqlConnection.Open();
                string[] str = util.BorrowTime.Split(' ');
                string cmdStr = "UPDATE BorrowReturnRecord SET borrow_date=@borrowTime ,return_date = @retunTime  WHERE user_id = @readerId AND bookId = @bookId "; //AND BORROW_TIME = @borrowTime
                using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(cmdStr, npgsqlConnection))
                {
                    //
                    npgsqlCommand.Parameters.AddWithValue("@readerId", user.Id);
                    npgsqlCommand.Parameters.AddWithValue("@bookId", bookId); // Include BOOK_ID
                    npgsqlCommand.Parameters.AddWithValue("@borrowTime", DateTime.Now );
                    npgsqlCommand.Parameters.AddWithValue("@retunTime", null);
                    result = npgsqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Return failed: " + ex.ToString());
            }
            finally
            {
                npgsqlConnection.Close();
            }

            listView.ItemsSource = null;
            Update();

            if (result == 1)
            {
                MessageBox.Show("Return successful!");
            }

            button_renew.IsEnabled = false;
            button_return.IsEnabled = false;
        }
        private void Button_Click_Return(object sender, RoutedEventArgs e) // return
        {
            int result = 0;

            try
            {
                npgsqlConnection.Open();
                string[] str = util.BorrowTime.Split(' ');
                string cmdStr = "UPDATE BorrowReturnRecord SET Return_Date = @returnDate WHERE User_Id = @userId AND Book_Id = @bookId";
                using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(cmdStr, npgsqlConnection))
                {
                    //npgsqlCommand.Parameters.AddWithValue("@borrowDate", user.BorrowTime);
                    npgsqlCommand.Parameters.AddWithValue("@userId",int.Parse(user.Id) );
                    npgsqlCommand.Parameters.AddWithValue("@bookId", int.Parse(util.Id));
                    npgsqlCommand.Parameters.AddWithValue("@returnDate", DateTime.Now);
                    result = npgsqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Return failed: " + ex.ToString());
            }
            finally
            {
                npgsqlConnection.Close();
            }

            listView.ItemsSource = null;
            Update();

            if (result == 1)
            {
                MessageBox.Show("Return successful!");
            }

            button_renew.IsEnabled = false;
            button_return.IsEnabled = false;
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
}