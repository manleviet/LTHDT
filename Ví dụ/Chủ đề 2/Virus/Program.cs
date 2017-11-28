using System;

namespace virus
{
    class Program
    {
        static void Main(string[] args)
        {
            Patient p = new Patient(0.5f, 10);

            Console.WriteLine("The patient have {0} virus cells.", p.getNumVirusCells());
    
            p.TakeDrug();

            p.SimulateStep();

            Console.WriteLine("After take drug, the patient have {0} virus cells.", p.getNumVirusCells());
            Console.ReadKey();
        }
    }
}
