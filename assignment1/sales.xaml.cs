using Npgsql;
using System;
using System.Collections.Generic;
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
using static assignment1.sales;
using System.Collections.ObjectModel;

namespace assignment1
{
    /// <summary>
    /// Interaction logic for sales.xaml
    /// </summary>
    public partial class sales : Window
    {
        //private admin adminInstance;
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
        public class Product
        {
            public string ProductName { get; set; }
            public double Quantity { get; set; }
        }
        private List<Product> selectedProducts = new List<Product>();
        private ObservableCollection<string> adminLog = new ObservableCollection<string>();
        public sales()
        {
            InitializeComponent();
        }
        //private void AddMessageToAdminLog(string message)
        //{

        //   adminLog.Add(message);
        // }
        public string message = "";
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string productname = productName.Text;
                double qty = double.Parse(quantity.Text);
                double availableQuantity = GetAvailableQuantityFromDatabase(productname);

                if (availableQuantity >= qty)
                {
                    // Add the selected product to the list
                    selectedProducts.Add(new Product { ProductName = productname, Quantity = qty });

                    // Update the ListBox
                    selectedProductsListBox.ItemsSource = null;
                    selectedProductsListBox.ItemsSource = selectedProducts;

                    // Clear the input fields
                    productName.Text = "";
                    quantity.Text = "";
                    UpdateInventoryInDatabase(productname, -qty);
                    // Add a message to indicate the change in quantity
                     message =$"{productname} amount will be ( {availableQuantity} - {qty} ) ~ {availableQuantity - qty} kg";
                    //AddMessageToAdminLog(message);
                    //adminLog.Add(message);
                    MessageBox.Show(message);
                    

                }
                else
                {
                    MessageBox.Show("Not enough quantity available in the inventory.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private double GetAvailableQuantityFromDatabase(string productName)
        {
            double availableQuantity = 0.0; // Default value if not found or an error occurs

            try
            {
                // Establish a connection to your PostgreSQL database
                using (NpgsqlConnection con = new NpgsqlConnection(get_ConnectionString()))
                {
                    con.Open();

                    // Define your SQL query to fetch the available quantity based on the product name
                    string query = "SELECT amount FROM fruit WHERE productname = @productName";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@productName", productName);

                        // Execute the query and retrieve the available quantity
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            availableQuantity = Convert.ToDouble(result);
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                // Handle any database connection or query errors here
                MessageBox.Show(ex.Message);
            }

            return availableQuantity;
        }
        private void UpdateInventoryInDatabase(string productName, double changeAmount)
        {
            try
            {
                double currentQuantity = GetAvailableQuantityFromDatabase(productName);
                // Establish a connection to your PostgreSQL database
                using (NpgsqlConnection con = new NpgsqlConnection(get_ConnectionString()))
                {
                    con.Open();

                    // Define your SQL query to update the inventory quantity based on the product name
                    string query = "UPDATE fruit SET amount = amount + @changeAmount WHERE productname = @productName";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@productName", productName);
                        cmd.Parameters.AddWithValue("@changeAmount", changeAmount);

                        // Execute the query to update the inventory
                        cmd.ExecuteNonQuery();
                    }
                }
                
            }
            catch (NpgsqlException ex)
            {
                // Handle any database connection or query errors here
                MessageBox.Show(ex.Message);
            }
        }
        
       
        private void CalculateTotalSales_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double totalSales = 0.0;

                foreach (var product in selectedProducts)
                {
                    double pricePerKg = GetProductPriceFromDatabase(product.ProductName);
                    totalSales += pricePerKg * product.Quantity;
                }

                totalSalesLabel.Content = "Total Sales: $" + totalSales;
                //adminLog.ForEach(log => Console.WriteLine(log));
                /*foreach (var product in adminLog)
                {
                    selectedProductsListBox2.ItemsSource = null;
                    selectedProductsListBox2.ItemsSource = product;
                }*/
                admin ad = new admin();
                
                ad.Show();
                ad.show_Click(sender, e);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            //adminInstance.RefreshAdminLog();
        }
        private double GetProductPriceFromDatabase(string productName)
        {
            double productPrice = 0.0; // Default price if not found or an error occurs

            try
            {
                // Establish a connection to your PostgreSQL database
                using (NpgsqlConnection con = new NpgsqlConnection(get_ConnectionString()))
                {
                    con.Open();

                    // Define your SQL query to fetch the product price based on the product name
                    string query = "SELECT price FROM fruit WHERE productname = @productName";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@productName", productName);

                        // Execute the query and retrieve the price
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            productPrice = Convert.ToDouble(result);
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                // Handle any database connection or query errors here
                MessageBox.Show(ex.Message);
            }

            return productPrice;
        }
    }
}
