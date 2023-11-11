using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderBorrowWPF2.Models
{
    internal class Book
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Writer { get; set; }
        public double Price { get; set; }

        public int Count { get; set; }
        public int Surplus { get; set; }
    }
}
