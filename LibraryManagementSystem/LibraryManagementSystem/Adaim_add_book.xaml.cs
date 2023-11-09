
using System;
using System.Windows;
using System.Windows.Controls;
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
        string cmdStr;
        bool flag;

        public Adaim_add_book()
        {
            InitializeComponent();
            npgsqlConnection = new NpgsqlConnection(conString);
            flag = true;
        }

        public Adaim_add_book(BookUtil bookUtil)
        {
            InitializeComponent();
            npgsqlConnection = new NpgsqlConnection(conString);

            id.Text = bookUtil.Id;
            name.Text = bookUtil.Name;
            writer.Text = bookUtil.Writer;
            publishTime.Text = bookUtil.PublicTime;
            price.Text = bookUtil.Price;
            number.Text = bookUtil.Number;
            surplus.Text = bookUtil.Surplus;
            id.IsEnabled = false;
            flag = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (flag)
            {
                cmdStr = @"INSERT INTO book (book_id, book_name, book_writer, publish_time, book_price, book_count, book_surplus) " +
                         "VALUES (@id, @name, @writer, @publishTime, @price, @number, @surplus)";
            }
            else
            {
                string[] str = publishTime.Text.Split(' ');
                cmdStr = @"UPDATE book " +
                         "SET book_name = @name, book_writer = @writer, publish_time = @publishTime, book_price = @price, " +
                         "book_count = @number, book_surplus = @surplus " +
                         "WHERE book_id = @id";
            }

            int result = 0;
            try
            {
                npgsqlConnection.Open();
                using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(cmdStr, npgsqlConnection))

                {
                    int bookId = int.Parse(id.Text);
                    npgsqlCommand.Parameters.AddWithValue("@id", bookId);
                    npgsqlCommand.Parameters.AddWithValue("@name", name.Text);
                    npgsqlCommand.Parameters.AddWithValue("@writer", writer.Text);
                    npgsqlCommand.Parameters.AddWithValue("@publishTime", DateTime.Parse(publishTime.Text));

                    decimal priceValue = decimal.Parse(price.Text);
                    npgsqlCommand.Parameters.AddWithValue("@price", priceValue);

                    int bookCount;
                    int bookRemain;
                    if (number.Text == "" || surplus.Text == "")
                    {
                        bookCount = 100;
                        bookRemain = 50;
                    }
                    else 
                    {
                         bookCount = int.Parse(number.Text);
                         bookRemain = int.Parse(surplus.Text);
                    }

                    npgsqlCommand.Parameters.AddWithValue("@number", bookCount);
                    npgsqlCommand.Parameters.AddWithValue("@surplus", bookRemain);

                    result = npgsqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                if (cmdStr.StartsWith("UPDATE"))
                    MessageBox.Show("Update failed!");
                else
                    MessageBox.Show("Insertion failed!");
            }
            finally
            {
                npgsqlConnection.Close();
            }
            if (result == 1)
            {
                if (cmdStr.StartsWith("UPDATE"))
                    MessageBox.Show("Update successful!");
                else
                    MessageBox.Show("Insertion successful!");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
