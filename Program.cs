using System;
using System.Collections.Generic;

namespace payMet_shipInfo_Bantilan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Payment Method ---");
            Console.WriteLine("a. Gcash Payment");
            Console.WriteLine("b. Credit Card");
            Console.WriteLine("c. Cash on Delivery");

            Console.Write("Enter your choice (a/b/c): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "a":
                    Console.WriteLine("You chose Gcash Payment.");
                    break;
                case "b":
                    Console.WriteLine("You chose Credit Card Payment.");
                    break;
                case "c":
                    Console.WriteLine("You chose Cash on Delivery.");
                    break;
            }
             
        }
    }
}
