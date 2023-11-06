using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemOne.Models
{
    public class bankCharges
    {
        private double balance;
        private int numberChecks;


        public bankCharges(double balance, int numberChecks)
        {
            this.balance = balance;
            this.numberChecks = numberChecks;
        }
        public double calculateFees()
        {
            double monthFee = 10.0;
            double checkFee = 0.0;
            if (balance < 400)
            {
                monthFee += 15.0;
            }
            if (numberChecks < 20)
            {
                checkFee = numberChecks * 0.1;
            }
            else if (numberChecks >= 20 && numberChecks <= 39)
            {
                checkFee = numberChecks * 0.08;
            }
            else if (numberChecks >= 40 && numberChecks <= 59)
            {
                checkFee = numberChecks * 0.06;
            }
            else
            {
                checkFee = numberChecks * 0.10;
            }
            return monthFee + checkFee;
        }
    }
}
