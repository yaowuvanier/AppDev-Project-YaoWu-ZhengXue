using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Npgsql;
using Unipluss.Sign.Client.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void RunQuery_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                NpgsqlConnection connection = MainWindow.GetNqgsqlConnection();
                connection.Open();
                string query = "SELECT * FROM assignment.Product";
                DataTable dataTable = new DataTable();
                using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connection))
                {
                    dataAdapter.Fill(dataTable);
                }
                queryResult.Text = DataTableToString(dataTable);
                MessageBox.Show("Connection Established");
                connection.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private string DataTableToString(DataTable dataTable)
        {
            StringBuilder result = new StringBuilder();

            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn col in dataTable.Columns)
                {
                    result.Append(row[col].ToString()).Append("\t");
                }
                result.AppendLine();
            }

            return result.ToString();
        }


        private void addProduct_Click(object sender, RoutedEventArgs e) 
        {
            int amount;
            int.TryParse(txtAmount.Text, out amount);

             Dictionary<string, string> radioButtonValues = new Dictionary<string, string>
                {
                    { "Apple", "Apple" },
                    { "Raspberry", "Raspberry" },
                    { "Cauliflower", "Cauliflower" },
                    { "Orange", "Orange" },
                    { "Blueberry", "Blueberry" }
                };

            RadioButton selectedRadioButton = (RadioButton)sender;

            if (selectedRadioButton.IsChecked == true)
            {
                string selectedValue;
                if (radioButtonValues.TryGetValue(selectedRadioButton.Name, out selectedValue))
                {
                    try
                    {
                        NpgsqlConnection connection = MainWindow.GetNqgsqlConnection();
                        connection.Open();
                        string query = "SELECT amout FROM assignment.Product where name=@selectedValue";


                        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@selectedValue", selectedValue);

                            // Execute the query and retrieve the "amount" value.
                            var result = command.ExecuteScalar();

                            if (result != null)
                            {
                                amount += Convert.ToInt32(result);
                            }
                        }

                        string updateQuery = "UPDATE assignment.Product SET amount=@amount WHERE name=@selectedValue";
                        using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@selectedValue", selectedValue);
                            updateCommand.Parameters.AddWithValue("@amount", amount);

                            int rowsAffected = updateCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                addProductResult.Text = "Updated amount successfully.";
                                MessageBox.Show("Amount updated successfully.");
                            }
                            else
                            {
                                addProductResult.Text = "Failed to update amount.";
                            }

                            connection.Close();
                        }
                    }
                    catch (NpgsqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
        }
    }
}
