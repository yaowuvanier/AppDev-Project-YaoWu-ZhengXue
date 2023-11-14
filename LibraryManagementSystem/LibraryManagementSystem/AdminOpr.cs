using Npgsql;
using System;
using System.Windows;

namespace LibraryManagementSystem
{
    public class AdminOpr
    {
        private string id;          
        private string adminName;  
        private string password;    
        private string phone;
        private int age;
        private string sex;

        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public string AdminName
        {
            get { return adminName; }
            set { adminName = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public AdminOpr()
        {

        }
        public AdminOpr(string id, string password)
        {
            this.id = id;
            this.password = password;
        }
        public AdminOpr(string id, string adminName, string password)
        {
            this.id = id;
            this.adminName = adminName;
            this.password = password;
        }

        // Connection string for PostgreSQL
        const string conString = "Host=localhost; Port=5432; Username=postgres; Password=125521; Database=vanierAEC2023fall; SearchPath=assignment";
        NpgsqlConnection postgresConnection;

        // Administrator login
        public Boolean adminLogin()
        {
            bool flag = false;
            if (postgresConnection == null)
                postgresConnection = new NpgsqlConnection(conString);
            try
            {
                postgresConnection.Open();
                string sql = "SELECT manager_name, age, sex, telephone FROM assignment.manager WHERE manager_id=@id AND password=@password";
                using (var cmd = new NpgsqlCommand(sql, postgresConnection))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("password", Password);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            flag = true;
                            adminName = reader["manager_name"].ToString();
                            age = (int)reader["age"]; //type convert still has some problems  //Convert.ToInt32(reader["age"]);  
                            phone = reader["telephone"].ToString();
                            sex = reader["sex"].ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occurred: " + e.ToString(), "Error");
            }
            finally
            {
                postgresConnection.Close();
            }
            return flag;
        }

      
        public bool sameAdmin(string id)
        {
            bool flag = false;

            if (postgresConnection == null)
                postgresConnection = new NpgsqlConnection(conString);

            try
            {
                postgresConnection.Open();

                string sql = "SELECT COUNT(*) FROM manager WHERE manager_id = @id";
                using (var cmd = new NpgsqlCommand(sql, postgresConnection))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        flag = true; // User already registered
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occurred: " + e.ToString(), "Error");
            }
            finally
            {
                postgresConnection.Close();
            }

            return flag;
        }

        // Register an administrator
        public int adminRegister(AdminOpr adminOpr)
        {
            Console.WriteLine("enter register main block, insert into database");
            MessageBox.Show("enter register main block, insert into database");

            int query = 0;
            if (postgresConnection == null)
                postgresConnection = new NpgsqlConnection(conString);
            try
            {
                postgresConnection.Open();
                string sql = @"INSERT INTO assignment.manager (manager_name, manager_id, age, sex, telephone, password) 
                              VALUES (@adminName, @id, @age, @sex, @phone, @password)";
                using (var cmd = new NpgsqlCommand(sql, postgresConnection))
                {
                    cmd.Parameters.AddWithValue("adminName", adminOpr.adminName);
                    cmd.Parameters.AddWithValue("id", adminOpr.id);
                    cmd.Parameters.AddWithValue("age", adminOpr.age);
                    cmd.Parameters.AddWithValue("sex", adminOpr.sex);
                    cmd.Parameters.AddWithValue("phone", adminOpr.phone);
                    cmd.Parameters.AddWithValue("password", adminOpr.password);
                    query = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                postgresConnection.Close();
            }
            return query;
        }

        public int userAdd1(int a)
        {
            return a + 1;
        }
    }
}