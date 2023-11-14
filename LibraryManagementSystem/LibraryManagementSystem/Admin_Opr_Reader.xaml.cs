using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using Npgsql;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for Admin_Opr_Reader.xaml 
    /// </summary>
    public partial class Admin_Opr_Reader : Window
    {
        // PostgreSQL connection string
        const string conString = "Host=localhost; Port=5432; Username=postgres; Password=125521; Database=vanierAEC2023fall; SearchPath=assignment";
        NpgsqlConnection npgsqlConnection;

        List<Users> ltCus;
        Users util;

        AdminOpr adminOpr;

        public Admin_Opr_Reader()
        {
            InitializeComponent();
            npgsqlConnection = new NpgsqlConnection(conString);
        }

        public Admin_Opr_Reader(AdminOpr adminOpr)
        {
            this.adminOpr = adminOpr;
            InitializeComponent();
            npgsqlConnection = new NpgsqlConnection(conString);
            ltCus = new List<Users>();
            Update();
        }

        public void Update()
        {
            if (npgsqlConnection == null)
                npgsqlConnection = new NpgsqlConnection(conString);

            DataTable dt = new DataTable("table1");

            try
            {
                npgsqlConnection.Open();
                string cmdStr = "SELECT * FROM reader";

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
                Users util = new Users
                {
                    Id = dataRow["id"].ToString(),
                    Name = dataRow["username"].ToString(),
                    Phone = dataRow["phonenumber"].ToString(),
                    Email = dataRow["email"].ToString(),
                    Password = dataRow["password"].ToString()
                };
                ltCus.Add(util);
            }
            listView.ItemsSource = ltCus;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)  //modify
        {
            User_register user_Register = new User_register();
            Application.Current.MainWindow = user_Register;
            user_Register.IsVisibleChanged += User_Register_IsVisibleChanged;
            user_Register.Show();
            button1.IsEnabled = false;
            button2.IsEnabled = false;
        }

        private void User_Register_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Update();
        }

        private void listView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            util = (Users)listView.SelectedItem;
            button1.IsEnabled = true;
            button2.IsEnabled = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)  //edit
        {
            User_register user_Register = new User_register(util);
            Application.Current.MainWindow = user_Register;
            user_Register.Title = "Modify User";
            user_Register.IsVisibleChanged += User_Register_IsVisibleChanged;
            user_Register.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) //query
        {
            DataTable dt = new DataTable("table1");

            try
            {
                npgsqlConnection.Open();
                string cmdStr = "SELECT * FROM reader WHERE username = @name";

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
                    Users util = new Users
                    {
                        Id = dataRow["id"].ToString(),
                        Name = dataRow["username"].ToString(),
                        Phone = dataRow["phonenumber"].ToString(),
                        Email = dataRow["email"].ToString(),
                        Password = dataRow["password"].ToString()
                    };
                    ltCus.Add(util);
                }
                listView.ItemsSource = ltCus;
            }
            catch (Exception ex)
            {
                MessageBox.Show("query failed: " + ex.ToString());
            }
            finally
            {
                npgsqlConnection.Close();
            }

            button1.IsEnabled = false;
            button2.IsEnabled = false;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) //delete
        {
            int result = 0;

            try
            {
                npgsqlConnection.Open();
                string cmdStr = "DELETE FROM reader WHERE id = @id";

                using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(cmdStr, npgsqlConnection))
                {
                    npgsqlCommand.Parameters.AddWithValue("@id", util.Id);
                    result = npgsqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("delete failed: " + ex.ToString());
            }
            finally
            {
                npgsqlConnection.Close();
            }

            listView.ItemsSource = null;
            Update();

            if (result == 1)
            {
                MessageBox.Show("delete successful！");
            }

            button1.IsEnabled = false;
            button2.IsEnabled = false;
        }
    }

    public class Users
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

}
