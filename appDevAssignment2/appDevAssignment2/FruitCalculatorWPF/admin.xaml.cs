
using FruitCalculatorWPF.Models;
using Newtonsoft.Json;
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

namespace FruitCalculatorWPF
{
    /// <summary>
    /// Interaction logic for admin.xaml
    /// </summary>
    public partial class admin : Window
    {
        HttpClient client = new HttpClient();
        public admin()
        {
            client.BaseAddress = new Uri("https://localhost:7042/Fruit/");

            client.DefaultRequestHeaders.Accept.Clear();

            
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")

                );
            InitializeComponent();
        }

        public async void show_Click(object sender, RoutedEventArgs e)
        {
            var server_response = await client.GetStringAsync("GetAllFruits");

            Response respons_JSON = JsonConvert.DeserializeObject<Response>(server_response);
            dataGird.ItemsSource = respons_JSON.fruits;
           
            DataContext = this;
        }

        private async void select_Click(object sender, RoutedEventArgs e)
        {
            var server_response = await client.GetStringAsync("GetFruitbyId/"+int.Parse(id.Text));

            Response respons_JSON = JsonConvert.DeserializeObject<Response>(server_response);
            dataGird.ItemsSource = respons_JSON.fruits;

            DataContext = this;
            //name.Text = respons_JSON.fruit.name;
            //amount.Text=respons_JSON.fruit.amount.ToString();

        }

        private async void insert_Click(object sender, RoutedEventArgs e)
        {
            Fruit fruit = new Fruit();
            fruit.price = double.Parse(price.Text);
            fruit.name = name.Text;
            fruit.id = int.Parse(id.Text);
            fruit.amount=int.Parse(amount.Text);
            var server_response =
             await client.PostAsJsonAsync("AddFruit", fruit);
            MessageBox.Show(server_response.ToString());
        }

        
        private async void update_Click(object sender, RoutedEventArgs e)
        {
            Fruit fruit= new Fruit();
            fruit.price = double.Parse(price.Text);
            fruit.name = name.Text;
            fruit.id = int.Parse(id.Text);
            fruit.amount = int.Parse(amount.Text);
            var server_response =
             await client.PutAsJsonAsync("UpdateFruit", fruit);
            MessageBox.Show(server_response.ToString());

        }

        private async void delete_Click(object sender, RoutedEventArgs e)
        {
            var response_JSON =
                await client.DeleteAsync("DeleteFruitbyId/" + int.Parse(id.Text));
            MessageBox.Show(response_JSON.StatusCode.ToString());
            MessageBox.Show(response_JSON.RequestMessage.ToString());
        }
    }
}
