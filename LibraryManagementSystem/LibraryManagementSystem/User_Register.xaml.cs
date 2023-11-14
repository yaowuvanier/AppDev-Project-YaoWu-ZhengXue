using System.Windows;
using Npgsql;

namespace LibraryManagementSystem
{
    /// <summary>
    /// user_register.xaml 
    /// </summary>
    public partial class User_register : Window
    {
        User user;
        Users users;

        bool flag;
        public User_register()
        {
            user = new User();
            users = new Users();
            flag = true;
            InitializeComponent();
          
        }

        public User_register(Users users)
        {
            flag = false;
            this.users = users;
            user = new User();
            InitializeComponent();
            button1.Content = "Modify";
            name.Text = users.Name;
            id.Text = users.Id;
            phone.Text = users.Phone;
            email.Text = users.Email;
            password.Text = users.Password;
            password1.Text = users.Password;

            id.IsEnabled = false;

            
        }

        public void Insert() //register
        {
            string accountId;

            do
            {
                accountId = id.Text; // Get the value from the TextBox

                if (string.IsNullOrEmpty(accountId))
                {
                    MessageBox.Show("Account ID cannot be empty.");
                    // Optionally prompt the user to enter a value again.
                }
            }
            while (string.IsNullOrEmpty(accountId));

            if (user.SameUser(id.Text))
            {
                if (!password.Text.Equals(password1.Text))
                {
                    MessageBox.Show("The passwords entered twice are not equal, please re-enter");
                }
                else
                {
                    user.UserName = name.Text;
                    user.Id = id.Text;
                    user.PhoneNumber = phone.Text;
                    user.Email = email.Text;
                    user.Password = password.Text;

                    if (user.Register(user) > 0)
                    {
                        MessageBox.Show("register successfully");
                    }
                    else
                    {
                        MessageBox.Show("register failed");
                    }
                }
            }
            else
            {
                MessageBox.Show("This user already exists, please change the account！");

            }
        }


        public void Update()
        {
            if (password.Text.Equals(password1.Text))
            {
                // PostgreSQL connection string
                const string conString = "Host=localhost; Port=5432; Username=postgres; Password=125521; Database=vanierAEC2023fall; SearchPath=assignment";
                NpgsqlConnection npgsqlConnection = new NpgsqlConnection(conString);

                string cmdStr = "UPDATE assignment.reader SET reader_name = @name,  telephone = @phone, email = @email, password = @password WHERE reader_id = @id";

                int result = 0;
                try
                {
                    npgsqlConnection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(cmdStr, npgsqlConnection))
                    {
                        command.Parameters.AddWithValue("name", name.Text);
                        command.Parameters.AddWithValue("phone", phone.Text);
                        command.Parameters.AddWithValue("email", email.Text);
                        command.Parameters.AddWithValue("password", password.Text);
                        command.Parameters.AddWithValue("id", id.Text);

                        result = command.ExecuteNonQuery();
                    }
                }
                catch
                {
                    MessageBox.Show("modify failed!");
                }
                finally
                {
                    npgsqlConnection.Close();
                }
                if (result == 1)
                {
                    MessageBox.Show("modify successfully!");
                }
            }
            else
            {
                MessageBox.Show("Passwords are inconsistent");
            }
        }

        private void Button_Click_register(object sender, RoutedEventArgs e)  //register
        {
            if(flag)
            {
                Insert();
            }
            else
            {
                Update();
            }

        }
        

        private void Button_Click_return(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

   
    }
}
