using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingAccount
{
    class SavingAccount
    {
        double currBalance;
        static double currInterestRate;

        public SavingAccount(double balance)
        {
            currBalance = balance;
        }

        // Cau tu static - de khoi tao gia tri cho cac bien thanh phan static
        static SavingAccount()
        {
            currInterestRate = 0.04;
        }

        // Ham static de gan gia tri cho bien thanh phan static
        public static void SetInterestRate(double newRate)
        {
            currInterestRate = newRate;
        }

        public void PrintStatus()
        {
            Console.WriteLine("{0}-{1}", currBalance, currInterestRate);
        }
    }
}
