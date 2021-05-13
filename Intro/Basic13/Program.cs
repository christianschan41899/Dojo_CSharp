using System;

namespace Basic13
{
    class Program
    {
        public static void PrintNumbers()
        {
            for(int i = 1; i < 256; i++)
            {
                Console.WriteLine(i);
            }
        }

        public static void PrintOdds()
        {
            for(int i = 1; i < 256; i++)
            {
                if(i%2 != 0)
                {
                Console.WriteLine(i);
                }
            }
        }

        public static void PrintSum()
        {
            int sum = 0;
            for(int i = 0; i < 256; i++)
            {
                sum = sum + i;
                Console.WriteLine($"New number: {i} Sum: {sum}");
            }
        }

        public static void LoopArray(int[] numbers)
        {
            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }
        }

        public static int FindMax(int[] numbers)
        {
            int max = -100000;
            foreach (int number in numbers)
            {
                if(number>max){
                    max = number;
                }
            }
            return max;
        }

        public static void GetAverage(int[] numbers)
        {
            float average = 0;
            int count = 0;
            foreach (int number in numbers)
            {
                average = average + number;
                count++;
            }

            average = average/count;
            Console.Write($"The average is {average}");
        }

        public static int[] OddArray()
        {
            int[] newArray = new int[128];
            int index = 0;
            for(int i = 1; i < 256; i++)
            {
                if(i%2 != 0)
                {
                    newArray[index] = i;
                    index++;
                }
            }
            return newArray;
        }

        public static int GreaterThanY(int[] numbers, int y)
        {
            int count = 0;
            foreach (int number in numbers)
            {
                if(number > y)
                {
                    count++;
                }
            }
            return count;
        }

        public static void SquareArrayValues(int[] numbers)
        {
            for(int i = 0; i<numbers.Length; i++)
            {
                if(numbers[i] < 0){
                    numbers[i] = numbers[i] * numbers[i];
                }
            }
            Console.WriteLine("Squared array values!");
        }

        public static void EliminateNegatives(int[] numbers)
        {
            for(int i = 0; i<numbers.Length; i++)
            {
                if(numbers[i] < 0){
                    numbers[i] = 0;
                }
            }
            Console.WriteLine("Negatives removed!");
        }

        public static void MinMaxAverage(int[] numbers)
        {
            int max = -100000;
            int min = 100000;
            int average = 0;
            int count = 0;
            foreach (int number in numbers)
            {
                if(number>max){
                    max = number;
                }

                if(number<min){
                    min = number;
                }

                average = average + number;
                count++;
            }
            Console.WriteLine($"Max: {max}, Min: {min}, Avg: {average}");
        }
        
        public static void ShiftValues(int[] numbers)
        {
            for(int i = 0; i < numbers.Length-1; i++){
                Console.WriteLine($"{numbers[i]} to {numbers[i+1]}");
                numbers[i] = numbers[i+1];
            }
            numbers[numbers.Length-1] = 0;

            Console.WriteLine("Array shifted left!");
        }

        public static object[] NumToString(int[] numbers)
        {
            object[] newArray = new object[numbers.Length];
            int index = 0;
            foreach (int number in numbers)
            {
                if(number<0){
                    newArray[index] = "Dojo";
                }
                else{
                    newArray[index] = number;
                }
            }

            return newArray;
        }



        static void Main(string[] args)
        {
            PrintNumbers();
            Console.WriteLine("");
            PrintOdds();
            Console.WriteLine("");
            PrintSum();
            int[] numbers = {-3, 2, 0, 4, -9, 10};
            Console.WriteLine("");
            LoopArray(numbers);
            Console.WriteLine("");
            Console.WriteLine(FindMax(numbers));
            Console.WriteLine("");
            GetAverage(numbers);
            Console.WriteLine("");
            Console.WriteLine(OddArray());
            Console.WriteLine("");
            Console.WriteLine(GreaterThanY(numbers, 0));
            Console.WriteLine("");
            SquareArrayValues(numbers);
            Console.WriteLine("");
            int[] numbers2 = {-3, 2, 0, 4, -9, 10};
            EliminateNegatives(numbers2);
            Console.WriteLine("");
            int[] numbers3 = {-3, 2, 0, 4, -9, 10};
            MinMaxAverage(numbers3);
            Console.WriteLine("");
            ShiftValues(numbers3);
            Console.WriteLine("");
            int[] numbers4 = {-3, 2, 0, 4, -9, 10};
            Console.WriteLine(NumToString(numbers4));
            

        }
    }
}
