using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagementSystem
{
    // User class
    public class User
    {
        private string id;               
        private string userName;        
        private string password;        
        private string email;           
        private string phoneNumber;     
         
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }

        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }

        }

        // PostgreSQL connection string
        const string conString = "Host=localhost; Port=5432; Username=postgres; Password=125521; Database=vanierAEC2023fall; SearchPath=assignment";
        NpgsqlConnection npgsqlConnection;
        public User()
        {
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(conString);

        }
        public User(string id)
        {
            this.id = id;
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(conString);

        }
        /* public User(string userName)
         {
             this.userName = userName;

         }*/
        public User(string id, string password)
        {
            this.id = id;
            this.password = password;


        }
        public User(string userName, string password, string phoneNumber)
        {
            this.userName = userName;
            this.password = password;
            this.phoneNumber = phoneNumber;
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(conString);

        }


        /*public User(string id, string userName, string password)
        {
            this.id = id;
            this.userName = userName;
            this.password = password;
        }*/
        public User(string userName, string password, string email, string phoneNumber)
        {
            this.userName = userName;
            this.password = password;
            this.email = email;
            this.phoneNumber = phoneNumber;
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(conString);
        }
        public User(string id, string userName, string password, string email, string phoneNumber)
        {
            this.id = id;
            this.userName = userName;
            this.password = password;
            this.email = email;
            this.phoneNumber = phoneNumber;
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(conString);
        }


        //Used to display logged in user information


        //User login verification
        public bool UserLogin()
        {
            bool flag = false;
            npgsqlConnection = new NpgsqlConnection(conString);
            try
            {
                
                npgsqlConnection.Open();
                string sql = "SELECT username, phonenumber, email FROM reader WHERE id=@id AND password=@password";

                using (var cmd = new NpgsqlCommand(sql, npgsqlConnection))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("password", Password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            flag = true;
                            userName = reader["reader_name"].ToString();
                            email = reader["email"].ToString();
                            phoneNumber = reader["telephone"].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("An error occurred: " + ex.Message);

                // You can also log the exception details to a log file or other means.
                // For example, if you have a logging library like log4net, you can use it to log the exception.
                // log.Error("An error occurred in UserLogin method: " + ex.ToString());

                // You can re-throw the exception if you want it to be caught further up the call stack.
                // throw;

                // Depending on your application and security considerations, you might want to handle different exceptions differently.
                // For example, you can catch specific exceptions, like NpgsqlException, to handle database-related errors separately.
                // catch (NpgsqlException dbException)
                // {
                //     Console.WriteLine("Database error: " + dbException.Message);
                // }

                // Handle the exception based on the specific error or re-throw it if necessary.
            }

            finally
            {
                npgsqlConnection.Close();
            }
            return flag;
        }

        // Determine whether the user has been registered
        public bool SameUser(string id)
        {
            bool flag;
            npgsqlConnection.Open();

            string sql = "SELECT COUNT(*) FROM reader WHERE reader_id=@id";

            using (var cmd = new NpgsqlCommand(sql, npgsqlConnection))
            {
                cmd.Parameters.AddWithValue("id", id);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                flag = count == 0;
            }

            npgsqlConnection.Close();

            return flag;
        }

        // User register
        public int Register(User user)
        {
            int result = 0;

            try
            {
                npgsqlConnection.Open();

                string sql = @"INSERT INTO assignment.reader (username, id, phonenumber, email, password)
                              VALUES (@userName, @id, @phoneNumber, @email, @password)";

                using (var cmd = new NpgsqlCommand(sql, npgsqlConnection))
                {
                    cmd.Parameters.AddWithValue("userName", user.UserName);
                    cmd.Parameters.AddWithValue("id", user.Id);
                    cmd.Parameters.AddWithValue("phoneNumber", user.PhoneNumber);
                    cmd.Parameters.AddWithValue("email", user.Email);
                    cmd.Parameters.AddWithValue("password", user.Password);

                    result = cmd.ExecuteNonQuery();
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

            return result;
        }

        // Modify user
        public int UpdateUser(User user)
        {
            if (npgsqlConnection == null)
                npgsqlConnection = new NpgsqlConnection(conString);

            npgsqlConnection.Open();

            string sql = @"UPDATE reader SET username=@userName, password=@password, email=@email, phoneNumber=@phoneNumber WHERE id=@id";

            int result = 0;

            using (var cmd = new NpgsqlCommand(sql, npgsqlConnection))
            {
                cmd.Parameters.AddWithValue("userName", user.UserName);
                cmd.Parameters.AddWithValue("id", user.Id);
                cmd.Parameters.AddWithValue("password", user.Password);
                cmd.Parameters.AddWithValue("email", user.Email);
                cmd.Parameters.AddWithValue("phoneNumber", user.PhoneNumber);

                result = cmd.ExecuteNonQuery();
            }

            npgsqlConnection.Close();

            return result;
        }
    }
}