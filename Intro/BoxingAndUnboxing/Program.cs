using System;
using System.Collections.Generic;

namespace BoxingAndUnboxing
{
    class Program
    {
        static void Main(string[] args)
        {
            List<object> items = new List<object>();

            items.Add(7);
            items.Add(28);
            items.Add(-1);
            items.Add(true);
            items.Add("chair");

            int count = 0;

            foreach (var item in items)
            {
                Console.WriteLine(item);
                if(item is int){
                    count = count + (int)item;
                }
            }

            Console.WriteLine(count);
        }
    }
}
