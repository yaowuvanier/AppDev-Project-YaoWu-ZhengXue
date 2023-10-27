namespace FruitCalculator2RestAPI.Models
{
    public class Response
    {
        public int statusCode { get; set; }
        //response has a message
        public string messageCode { get; set; }
        //response can have only one student retrive from the database
        public Fruit fruit { get; set; }
        //response can have list of students from the database
        public List<Fruit> fruits { get; set; }
    }
}
