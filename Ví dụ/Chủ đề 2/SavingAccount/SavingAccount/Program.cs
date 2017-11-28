using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingAccount
{
    class Program
    {
        static void Main(string[] args)
        {
            SavingAccount sa1 = new SavingAccount(100);
            sa1.PrintStatus();

            SavingAccount sa2 = new SavingAccount(200);
            SavingAccount.SetInterestRate(1);
            sa1.PrintStatus();
            sa2.PrintStatus();

            SavingAccount sa3 = new SavingAccount(300);
            sa3.PrintStatus();

            Console.ReadKey();
        }
    }
}
