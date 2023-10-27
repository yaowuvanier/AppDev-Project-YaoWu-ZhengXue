using FruitCalculatorWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
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

namespace FruitCalculatorWPF
{
    /// <summary>
    /// Interaction logic for sales.xaml
    /// </summary>
    public partial class sales : Window
    {
        HttpClient client = new HttpClient();
        public sales()
        {
            client.BaseAddress = new Uri("https://localhost:7042/Fruit/");

            client.DefaultRequestHeaders.Accept.Clear();


            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")

                );
            InitializeComponent();
        }
        private List<Product> selectedProducts = new List<Product>();
        private ObservableCollection<string> adminLog = new ObservableCollection<string>();
        public string message = "";
        public int availableQuantity = 0;
        public string availableName = null;
        public int availableId = 0;

        public double availablePrice = 0.0;
        private async void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string productname = productName.Text;
                int qty = int.Parse(quantity.Text);
               await GetAvailableQuantityFromDatabase(productname);
                //if (availableName != null) { 
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
                    message = $"{productname} amount will be ( {availableQuantity} - {qty} ) ~ {availableQuantity - qty} kg";
                    //AddMessageToAdminLog(message);
                    //adminLog.Add(message);
                    MessageBox.Show(message);


                }
                else
                {
                    MessageBox.Show("Not enough quantity available in the inventory.");
                }
                //}
                //else
                //{
                //    MessageBox.Show("Product does not exist in the database.");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task GetAvailableQuantityFromDatabase(string productName)
        {


            //var server_response = await client.GetStringAsync("GetProductbyName/" + productName);

            //Response respons_JSON = JsonConvert.DeserializeObject<Response>(server_response);

            //    availableName = respons_JSON.fruit.name;
            //    availableQuantity = respons_JSON.fruit.amount;

            //    availablePrice = respons_JSON.fruit.price;
            //    availableId = respons_JSON.fruit.id;

            var server_response = await client.GetStringAsync("GetProductbyName/" + productName);

            Response respons_JSON = JsonConvert.DeserializeObject<Response>(server_response);

            if (respons_JSON != null && respons_JSON.fruit != null)
            {
                availableQuantity = respons_JSON.fruit.amount;
                availableName = respons_JSON.fruit.name;
                availablePrice = respons_JSON.fruit.price;
                availableId = respons_JSON.fruit.id;
            }
            else
            {
                // Handle the case when the product doesn't exist or response is null
                availableQuantity = 0;
                availableName = string.Empty;
                availablePrice = 0.0;
                availableId = 0;
                MessageBox.Show("Product does not exist in the database.");
            }


        }

        private async void UpdateInventoryInDatabase(string productName, int changeAmount)
        {
            int currentQuantity = availableQuantity + changeAmount;

            Fruit fruit = new Fruit();
            fruit.price = availablePrice;
            fruit.name = availableName;
            fruit.id = availableId;
            fruit.amount = currentQuantity;
            var server_response =
             await client.PutAsJsonAsync("UpdateFruit", fruit);
            MessageBox.Show(server_response.ToString());

        }
        double pricePerKg = 0.0;
        private async void CalculateTotalSales_Click(object sender, RoutedEventArgs e)
        {
            double totalSales = 0.0;

            foreach (var product in selectedProducts)
            {
                await GetProductPriceFromDatabase(product.ProductName);
                totalSales += pricePerKg * product.Quantity;
            }

            totalSalesLabel.Content = "Total Sales: $" + totalSales;
            this.UpdateLayout(); // Ensure the UI updates before continuing
            admin ad = new admin();

            ad.Show();
            ad.show_Click(sender, e);
        }
        private async Task GetProductPriceFromDatabase(string productName)
        {
            var server_response = await client.GetStringAsync("GetProductbyName/" + productName);

            Response respons_JSON = JsonConvert.DeserializeObject<Response>(server_response);

            pricePerKg = respons_JSON.fruit.price;

        }
    }
}