using System;
using System.Collections.Generic;

namespace Collections_Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            //Three basic arrays
            int[] array1;
            array1 = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            string[] array2;
            array2 = new string[] {"Tim", "Martin", "Nikki", "Sarah"};

            bool[] array3;
            array3 = new bool[] {true, false, true, false, true, false, true, false, true, false}; 

            //List of Flavors

            List<string> IceCreamFlavors = new List<string>();
            IceCreamFlavors.Add("Chocolate");
            IceCreamFlavors.Add("Mint");
            IceCreamFlavors.Add("Vanilla");
            IceCreamFlavors.Add("Strawberry");
            IceCreamFlavors.Add("Cookie Dough");

            Console.WriteLine($"There are currently {IceCreamFlavors.Count} ice cream flavors.");

            IceCreamFlavors.RemoveAt(2);

            Console.WriteLine($"There are now {IceCreamFlavors.Count} ice cream flavors.");

            //User Dictionary

            Dictionary<string,string> userInfo = new Dictionary<string,string>();
            Random index = new Random();

            foreach (string name in array2)
            {
                userInfo.Add(name, IceCreamFlavors[index.Next(IceCreamFlavors.Count)]);
            }
        }
    }
}
