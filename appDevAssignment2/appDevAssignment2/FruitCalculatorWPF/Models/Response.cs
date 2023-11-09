using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitCalculatorWPF.Models
{
    internal class Response
    {
        public int statusCode { get; set; }
       
        public string messageCode { get; set; }
        
        public Fruit fruit { get; set; }
        
        public List<Fruit> fruits { get; set; }

    }
}
