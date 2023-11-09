using FruitCalculator2RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace FruitCalculator2RestAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FruitController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public FruitController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetAllFruits")]
        public Response GetAllFruits()
        {

            Response response = new Response();

            NpgsqlConnection con =
                new NpgsqlConnection(_configuration.GetConnectionString("fruitConnection"));

            DBApplication dbA = new DBApplication();
            response = dbA.GetAllFruits(con);


            return response;
        }
        
        [HttpGet]
        [Route("GetFruitbyId/{id}")]
        public Response GetFruitbyId(int id)
        {
            
            Response response = new Response();
            
            NpgsqlConnection con =
                new NpgsqlConnection(_configuration.GetConnectionString("fruitConnection"));
            
            DBApplication dbA = new DBApplication();
            
            response = dbA.GetFruitbyId(con, id);
            
            return response;

        }

        [HttpPost]
        [Route("AddFruit")]
        public Response AddStudent(Fruit fruit)
        {
            
            Response response = new Response();
           
            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("fruitConnection"));
            
            DBApplication dbA = new DBApplication();
         
            response = dbA.AddFruit(con, fruit);
            return response;
        }

        [HttpPut]
        [Route("UpdateFruit")]
        public Response UpdateFruit(Fruit fruit)
        {
            Response response = new Response();
            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("fruitConnection"));
            DBApplication dbA = new DBApplication();
            response = dbA.UpdateFruit(con, fruit);
            return response;
        }

        [HttpDelete]
        [Route("DeleteFruitbyId/{id}")]
        public Response DeleteFruitbyId(int id)
        {
            Response response = new Response();
            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("fruitConnection"));
            DBApplication dbA = new DBApplication();
            response = dbA.DeleteFruitbyId(con, id);
            return response;

        }

        [HttpGet]
        [Route("GetProductbyName/{productName}")]
        public Response GetProductbyName(string productName)
        {

            Response response = new Response();

            NpgsqlConnection con =
                new NpgsqlConnection(_configuration.GetConnectionString("fruitConnection"));

            DBApplication dbA = new DBApplication();

            response = dbA.GetProductbyName(con, productName);

            return response;

        }

        
    }
}
