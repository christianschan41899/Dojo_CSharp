using System;

namespace Fundamentals_I
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            int counter = 1;
            while(counter != 256){
                Console.WriteLine(counter);
                counter++;
            }

            for(int i = 1; i <= 100; i++){
                //if divisible by 3 OR divisible by 5 AND NOT divisible by 3 AND divisible by 5
                if((i%3 == 0 || i%5 == 0) && !(i%3 == 0 && i%5 == 0)){
                    Console.WriteLine(i);
                }
            }

            //Text version
            for(int i = 1; i <= 100; i++){
                //if divisible by 3 OR divisible by 5 AND NOT divisible by 3 AND divisible by 5
                if(i%3 == 0 && i%5 == 0){
                    Console.WriteLine("FizzBuzz");
                }
                else if(i%5 == 0){
                    Console.WriteLine("Buzz");
                }
                else if(i%3 == 0){
                    Console.WriteLine("Fizz");
                }
            }
        }
    }
}
