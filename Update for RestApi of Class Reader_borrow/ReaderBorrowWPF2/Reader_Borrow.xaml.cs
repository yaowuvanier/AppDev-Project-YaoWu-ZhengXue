using Newtonsoft.Json;
using ReaderBorrowWPF2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace ReaderBorrowWPF2
{
    /// <summary>
    /// Interaction logic for Reader_Borrow.xaml
    /// </summary>
    public partial class Reader_Borrow : Window
    {   
      HttpClient client = new HttpClient();
        public Reader_Borrow()
        {
            client.BaseAddress = new Uri("https://localhost:7089/Book/");

            client.DefaultRequestHeaders.Accept.Clear();


            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")

                );
            InitializeComponent();
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {


            var server_response = await client.GetStringAsync("GetBookbyName/" + name.Text);


            Response respons_JSON = JsonConvert.DeserializeObject<Response>(server_response);

            dataGrid.ItemsSource = respons_JSON.books;



        }
        private async void BorrowButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Assuming you have the book ID available for borrowing
                int bookIdToBorrow = 1; // Replace with the actual book ID

                // Send a POST request to the API to borrow the book
                HttpResponseMessage response = await client.PostAsync($"BorrowBook/1/{bookIdToBorrow}", null);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Book borrowed successfully!");
                }
                else
                {
                    MessageBox.Show($"Failed to borrow the book. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error borrowing the book: {ex.Message}");
            }
        }
    }
}
