using Microsoft.AspNetCore.Mvc;
using Npgsql;
using ReaderBorrowRestAPI.Models;

namespace ReaderBorrowRestAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BookController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetBookbyName/{name}")]
        public Response GetBookbyName(string name)
        {
            Response response = new Response(); 
            NpgsqlConnection con=new NpgsqlConnection(_configuration.GetConnectionString("bookConnection"));
            DBApplication dBA= new DBApplication();
            response=dBA.GetBookbyName(con, name);
            return response;
        }
        [HttpPost]
        [Route("BorrowBook/{userId}/{bookId}")]
        public Response BorrowBook(int userId, int bookId)
        {
            Response response = new Response();
            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("bookConnection"));
            DBApplication dBA = new DBApplication();
            response = dBA.BorrowBook(con, userId, bookId);
            return response;
        }
    }
}
