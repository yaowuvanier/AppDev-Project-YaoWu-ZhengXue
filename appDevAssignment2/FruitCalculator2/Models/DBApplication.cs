using Npgsql;
using System.Data;
using System.Net;

namespace FruitCalculator2RestAPI.Models
{
    public class DBApplication
    {
        
        public Response GetAllFruits(NpgsqlConnection con)
        {
            
            string Query = "Select * from fruit";

            
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);


            Response response = new Response();
           
            List<Fruit> fruits = new List<Fruit>(); 

            if (dt.Rows.Count > 0)   
            {
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Fruit fruit = new Fruit();  

                    fruit.name = (string)dt.Rows[i]["productname"]; 
                    fruit.id = (int)dt.Rows[i]["productid"];
                    fruit.amount = (int)dt.Rows[i]["amount"];
                    fruit.price = (double)dt.Rows[i]["price"];


                    fruits.Add(fruit);
                }
            }

            // Now we need to Configure the Response
            if (fruits.Count > 0)
            {
                response.statusCode = 200;
                response.messageCode = "Data Retrieved Successfully";
                response.fruit = null;
                response.fruits = fruits;
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Data failed to Retrieve or may be table is empty";
                response.fruit = null;
                response.fruits = null;
            }
            return response;
        }

        public Response GetFruitbyId(NpgsqlConnection con,int id)
        {
            Response response = new Response();
            string Query = "Select * from fruit where productid='" + id + "'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Fruit> fruits = new List<Fruit>();
            if (dt.Rows.Count > 0)
            {
                Fruit fruit = new Fruit();

                fruit.name = (string)dt.Rows[0]["productname"];
                fruit.id = (int)dt.Rows[0]["productid"];
                fruit.amount = (int)dt.Rows[0]["amount"];
                fruit.price = (double)dt.Rows[0]["price"];
                fruits.Add(fruit);
                response.statusCode = 200;
                response.messageCode = "successfully retrieved";
                response.fruit = fruit;
                response.fruits = fruits ;

            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "data couldn't found.. check the id";
                response.fruits = null;
                response.fruit = null;
            }
            return response;
            

           
        }

        public Response AddFruit(NpgsqlConnection con, Fruit fruit)
        {
            con.Open();
            Response response = new Response();
            string Query = "insert into fruit values(@productname,@productid,@amount,@price)";
            NpgsqlCommand cmd = new NpgsqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@productname", fruit.name);
            cmd.Parameters.AddWithValue("@productid", fruit.id);
            cmd.Parameters.AddWithValue("@amount", fruit.amount);
            cmd.Parameters.AddWithValue("@price", fruit.price);
            

            int i = cmd.ExecuteNonQuery();

            if (i > 0)//that means the command is exexuted successfully
            {
                response.statusCode = 200;
                response.messageCode = "successfully inserted";
                response.fruit = fruit;
                response.fruits = null;
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Insertion is not successfully";
                response.fruit = null;
                response.fruits = null;
            }
            con.Close();
            return response;

        }

        public Response UpdateFruit(NpgsqlConnection con, Fruit fruit)
        {
            con.Open();
            Response response = new Response();
            string Query = "Update fruit set productname=@name, price=@price,amount=@amount where productid=@id";
            NpgsqlCommand cmd = new NpgsqlCommand(Query, con);
           
            cmd.Parameters.AddWithValue("@price",fruit.price);
            cmd.Parameters.AddWithValue("@amount", fruit.amount);
            cmd.Parameters.AddWithValue("@name", fruit.name);
            cmd.Parameters.AddWithValue("@id",fruit.id);

            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                response.statusCode = 200;
                response.messageCode = "Updated Successfully";
                response.fruit = fruit;
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Update failed or id wasn't in correct form";
            }
            con.Close();
            return response;
        }
        public Response DeleteFruitbyId(NpgsqlConnection con, int id)
        {
            con.Open();
            Response response = new Response();
            string Query = "Delete from fruit where productid='" + id + "'";
            NpgsqlCommand cmd = new NpgsqlCommand(Query, con);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                response.statusCode = 200;
                response.messageCode = "Fruit Deleted Successfully";

            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Fruit not found! Could perform delete Ops";
            }
            con.Close();
            return response;
        }

        public Response GetProductbyName(NpgsqlConnection con, string productName)
        {
            Response response = new Response();
            string Query = "Select * from fruit where productname='" + productName +"'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Fruit> fruits = new List<Fruit>();
            if (dt.Rows.Count > 0)
            {
                Fruit fruit = new Fruit();

                fruit.name = (string)dt.Rows[0]["productname"];
                fruit.id = (int)dt.Rows[0]["productid"];
                fruit.amount = (int)dt.Rows[0]["amount"];
                fruit.price = (double)dt.Rows[0]["price"];
                fruits.Add(fruit);
                response.statusCode = 200;
                response.messageCode = "successfully retrieved";
                response.fruit = fruit;
                response.fruits = fruits;

            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "data couldn't found.. check the id";
                response.fruits = null;
                response.fruit = null;
            }
            return response;

        }

       
    }
}

