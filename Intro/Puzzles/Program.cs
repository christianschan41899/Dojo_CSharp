using System;
using System.Collections.Generic;

namespace Puzzles
{
    class Program
    {
        //Random Array
        public static int[] RandomArray(){
            int[] numbers = new int[10];
            Random number = new Random();
            for(int i = 0; i < 10; i++){
                numbers[i] = number.Next(5, 26);
            } 

            int min = 26;
            int max = 4;
            int sum = 0;
            foreach(int num in numbers){
                sum = sum + num;
                if(num > max){
                    max = num;
                }

                if(num < min){
                    min = num;
                }
            }

            Console.WriteLine($"Max: {max}, Min: {min}, Sum: {sum}");

            return numbers;
        }

        //Coin Flip

        public static string TossCoin(){
            Console.WriteLine("Tossing a coin...");
            Random coin = new Random();
            //(Upper) Bound of Next is non-inclusive
            int result =  coin.Next(2);
            if(result == 0){
                return "Tails";
            }else{
                return "Heads";
            }
        }

        public static double TossMultipleCoins(int times){
            int tries = times;
            //Track no. of heads/tails
            double heads = 0;
            double tails = 0;
            //Don't want to constantly generate 'result' string variables in the for loop, so set it outside
            string result = "";
            Console.WriteLine($"Tossing a coin {tries} times...");
            while(tries != 0){
                result = TossCoin(); 
                if(result.Equals("Tails")){
                    tails++;
                }else if(result.Equals("Heads")){
                    heads++;
                }
                tries--;
            }

            //Returning the equation directly will give an int of 0, which wouldn't give the expected return.
            double headsRatio = heads/times;
            return headsRatio;
        }

        //Names
        public static List<string> Names(){
            List<string> names = new List<string>();
            names.Add("Todd");
            names.Add("Tiffany");
            names.Add("Charlie");
            names.Add("Geneva");
            names.Add("Sydney");

            //Shuffle
            List<string> shuffled = new List<string>();
            //An array of prime numbers
            int[] primes = {2, 3, 5, 7, 11};
            //When two or more are multiplied together, their result shouldn't be divisible by any other of the numbers in the array
            //Since its LCDs will be ONLY those prime numbers used
            int prime = 0;
            int multiple = 1;
            //We can code these as conditions to access an array and check against repeat condtions
            //Check for number of successfull insertions
            int success = 0;
            string name = "";
            //charlimit number. Change to see more names and verify shuffle (Do 8 for all).
            int charlimit = 5;
            //Random index accessor for array
            Random index = new Random();
            while(success != 5){
                prime = primes[index.Next(5)];
                //Check if prime is already used
                if(multiple%prime != 0){
                    //Access based on prime number recieved
                    if(prime == 2){
                        name = names[0];
                        multiple = multiple*prime;
                    }else if(prime == 3){
                        name = names[1];
                        multiple = multiple*prime;
                    }else if(prime == 5){
                        name = names[2];
                        multiple = multiple*prime;
                    }else if(prime == 7){
                        name = names[3];
                        multiple = multiple*prime;
                    }else if(prime == 11){
                        name = names[4];
                        multiple = multiple*prime;
                    }
                    
                    if(name.Length < charlimit){
                        shuffled.Add(name);
                    }
                    success++;
                }
            }

            return shuffled;

        }
        static void Main(string[] args)
        {
            Console.WriteLine(RandomArray());
            Console.WriteLine($"Ratio of Heads: {TossMultipleCoins(5)}");
            //Check to make sure the shuffle actually shuffles
            List<string> shuffleResult = new List<string>(); 
            shuffleResult = Names();
            foreach(string name in shuffleResult){
                Console.WriteLine(name);
            }
        }
    }
}
