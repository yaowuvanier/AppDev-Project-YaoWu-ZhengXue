using Npgsql;
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

namespace assignment1
{
    /// <summary>
    /// Interaction logic for admin.xaml
    /// </summary>
    public partial class admin : Window
    {
        private sales salesInstance;
        public admin()
        {
            InitializeComponent();
           
           // RefreshAdminLog();
        }
        /*public admin(sales salesInstance)
        {
            InitializeComponent();
            this.salesInstance = salesInstance;
            
            // RefreshAdminLog();
        }*/
        public static NpgsqlConnection con;
        public static NpgsqlCommand cmd;
        private void establishConnect()
        {

            try
            {
                con = new NpgsqlConnection(get_ConnectionString());
                MessageBox.Show("Connection Established");
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string get_ConnectionString()
        {
            /**
             * need to pass five values
             */
            string host = "Host=localhost;";
            string port = "Port=5432;";
            string dbName = "Database=vanierAEC Fall2023;";
            string userName = "Username=postgres;";
            string password = "Password=1234;";

            string connectionString = string.Format("{0}{1}{2}{3}{4}", host, port, dbName, userName, password);
            return connectionString;
        }

        private void insert_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                establishConnect();
                con.Open();
                string Query = "insert into fruit values(@productName,@productId,@amount,@price) ";

                cmd = new NpgsqlCommand(Query, con);//dynamic memory allocation of the command
                                                    //4.1 add define values for the variables in the query
                cmd.Parameters.AddWithValue("@productName", name.Text);
                cmd.Parameters.AddWithValue("@productId", int.Parse(id.Text));
                cmd.Parameters.AddWithValue("@amount", int.Parse(amount.Text));
                cmd.Parameters.AddWithValue("@price", double.Parse(price.Text));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Item created successfully");
                con.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void select_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                establishConnect();
                con.Open();
                string Query = "select * from fruit where productid=@id";
                cmd = new NpgsqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@id", int.Parse(id.Text));
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGird.ItemsSource = dt.AsDataView();
                DataContext = da;
                con.Close();

            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void show_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                establishConnect();
                con.Open();
                string Query = "Select * from fruit";
                cmd = new NpgsqlCommand(Query, con);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGird.ItemsSource = dt.AsDataView();
                DataContext = da;
                con.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                establishConnect();
                con.Open();

                string Query = "UPDATE fruit SET productname = @productName, amount = @amount, price = @price WHERE productid = @productId";

                cmd = new NpgsqlCommand(Query, con);

                cmd.Parameters.AddWithValue("@productName", name.Text);
                cmd.Parameters.AddWithValue("@productId", int.Parse(id.Text));
                cmd.Parameters.AddWithValue("@amount", int.Parse(amount.Text));
                cmd.Parameters.AddWithValue("@price", double.Parse(price.Text));

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Item updated successfully");
                }
                else
                {
                    MessageBox.Show("No records were updated.");
                }

                con.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                establishConnect();
                con.Open();

                string Query = "DELETE FROM fruit WHERE productid = @productId";

                cmd = new NpgsqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@productId", int.Parse(id.Text));

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Item deleted successfully");
                }
                else
                {
                    MessageBox.Show("No records were deleted.");
                }

                con.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /*private void RefreshAdminLog()
        {
            if (salesInstance != null && salesInstance.adminLog != null)
            {
                
                adminLogListBox.ItemsSource = salesInstance.adminLog;
            }
            else
            {
                
                adminLogListBox.ItemsSource = null;
            }
        }*/
    }
}
