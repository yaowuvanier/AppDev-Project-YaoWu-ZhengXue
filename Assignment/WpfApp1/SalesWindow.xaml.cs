using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unipluss.Sign.ExternalContract.Entities;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for SalesWindow.xaml
    /// </summary>
    public partial class SalesWindow : Window
    {
        public SalesWindow()
        {
            InitializeComponent();

            List<Product> products = new List<Product>
            {
                new Product { ProductName = "Apple", AmountKg = 0, PricePerKg = 2.10 },
                new Product { ProductName = "Orange", AmountKg = 0, PricePerKg = 2.49 },
                new Product { ProductName = "Raspberry", AmountKg = 0, PricePerKg = 2.35 },
                new Product { ProductName = "Blueberry", AmountKg = 0, PricePerKg = 1.45 },
                new Product { ProductName = "Cauliflower", AmountKg = 0, PricePerKg = 2.22 },
            };

            dataGrid.ItemsSource = products;
        }

        public object Windows { get; private set; }

        private void CalculateSalesButton_Click(object sender, EventArgs e)
        {
            var products = (List<Product>)dataGrid.ItemsSource;
            double totalSales = products.Sum(p => p.AmountKg * p.PricePerKg);
            totalSalesLabel.Content = totalSales.ToString("C2");

            products = (List<Product>)dataGrid.ItemsSource;
   

            try
            {
                using (NpgsqlConnection connection = MainWindow.GetNqgsqlConnection())
                {
                    connection.Open();

                    foreach (Product product in products)
                    {
                        if  (product != null && product.AmountKg != 0) 
                        {
                            string name = product.ProductName;
                            // Retrieve the current "amount" value from the database
                            string selectQuery = "SELECT amount FROM assignment.Product WHERE name = @name";
                            using (NpgsqlCommand selectCommand = new NpgsqlCommand(selectQuery, connection))
                            {
                                selectCommand.Parameters.AddWithValue("@productName", name);
                                int currentAmount = Convert.ToInt32(selectCommand.ExecuteScalar());

                                // Calculate the new amount
                                int newAmount = currentAmount - product.AmountKg;

                                // Update the "amount" in the table
                                string updateQuery = "UPDATE assignment.Product SET amount = @newAmount WHERE name = @name";

                                using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@newAmount", newAmount);
                                    updateCommand.Parameters.AddWithValue("@productName", name);

                                    int rowsAffected = updateCommand.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        Console.WriteLine($"Updated product: {product.ProductName}, New Amount: {newAmount}");
                                    }
                                }
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                // Handle any exceptions here (e.g., log, display, or throw).
                Console.WriteLine(ex.Message);
            }
        }
    }

}
