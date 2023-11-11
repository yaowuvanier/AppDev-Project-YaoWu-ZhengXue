namespace ReaderBorrowRestAPI.Models
{
    public class Response
    {
        public int statusCode { get; set; }
        //response has a message
        public string messageCode { get; set; }
        //response can have only one book retrive from the database
        public Book book { get; set; }
        //response can have list of books from the database
        public List<Book> books { get; set; }
    }
}
