using System;
using System.Collections.Generic;

namespace Hungry_Ninja
{
    interface IConsumable
    {
        string Name {get;set;}
        int Calories {get;set;}
        bool IsSpicy {get;set;}
        bool IsSweet {get;set;}
        string GetInfo();
    }  

    class Drink : IConsumable
    {
        public string Name {get;set;}
        public int Calories {get;set;}
        public bool IsSpicy {get;set;}
        public bool IsSweet {get;set;}
        
        public string GetInfo()
        {
            return $"{Name} (Drink).  Calories: {Calories}.  Spicy?: {IsSpicy}, Sweet?: {IsSweet}";
        }

        public Drink(string name, int calories, bool spicy, bool sweet)
        {
            Name = name;
            Calories = calories;
            IsSpicy = spicy;
            IsSweet = sweet;
        }
    }   
 

    class Food : IConsumable
    {
        public string Name {get;set;}
        public int Calories {get;set;}
        public bool IsSpicy {get;set;}
        public bool IsSweet {get;set;}
        public string GetInfo()
        {
            return $"{Name} (Food).  Calories: {Calories}.  Spicy?: {IsSpicy}, Sweet?: {IsSweet}";
        }
        public Food(string name, int calories, bool spicy, bool sweet)
        {
            Name = name;
            Calories = calories;
            IsSpicy = spicy;
            IsSweet = sweet;
        }
    }   


    class Buffet
    {
        public List<IConsumable> Menu;
         
        //constructor
        public Buffet()
        {
            Menu = new List<IConsumable>()
            {
                new Food("Peanuts", 828, false, false),
                new Food("Mango", 201, false, true),
                new Food("Shrimp", 100, false, false),
                new Food("Steak", 679, false, false),
                new Food("Bell Pepper", 30, true, false),
                new Food("Orange Chicken", 490, true, true),
                new Food("Steak", 679, false, false),
                new Drink("Water", 0, false, false),
                new Drink("Lemonade", 99, false, true),
                new Drink("Mayan Hot Chocolate", 99, true, true)
            };
        }

        public IConsumable Serve()
        {
            Random food = new Random();
            int foodIndex = food.Next(Menu.Count);
            return (Menu[foodIndex]);
        }
    }

    abstract class Ninja
    {
        protected int calorieIntake;
        public List<IConsumable> ConsumptionHistory;
        public Ninja()
        {
            calorieIntake = 0;
            ConsumptionHistory = new List<IConsumable>();
        }
        public abstract bool IsFull();
        public abstract void Consume(IConsumable item);
    }

    class SweetTooth : Ninja
    {
        // provide override for IsFull (Full at 1500 Calories)
        public override bool IsFull()
        {
            if(this.calorieIntake >= 1500)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Consume(IConsumable item)
        {
            if(this.IsFull())
            {
                Console.WriteLine("Sweet tooth is full!");
            }
            else
            {
                if(item.IsSweet)
                {
                    this.calorieIntake += (item.Calories + 10);
                }
                else
                {
                    this.calorieIntake += item.Calories;
                }
                this.ConsumptionHistory.Add(item);
                Console.WriteLine(item.GetInfo());
            }
        }
    }

    class SpiceHound : Ninja
    {
        // provide override for IsFull (Full at 1200 Calories)
        public override bool IsFull()
        {
            if(this.calorieIntake >= 1200)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Consume(IConsumable item)
        {
            if(this.IsFull())
            {
                Console.WriteLine("Spice Hound is full!");
            }
            else
            {
                if(item.IsSpicy)
                {
                    this.calorieIntake += (item.Calories - 5);
                }
                else
                {
                    this.calorieIntake += item.Calories;
                }
                this.ConsumptionHistory.Add(item);
                Console.WriteLine(item.GetInfo());
            }
        }
    }




    class Program
    {
        static void Main(string[] args)
        {
            SpiceHound spicy = new SpiceHound();
            SweetTooth sweet = new SweetTooth();
            Buffet buffet1 = new Buffet();
            Console.WriteLine("Spicy is eating...");
            while(!spicy.IsFull()){
                spicy.Consume(buffet1.Serve());
            }
            Console.WriteLine($"Spicy ate {spicy.ConsumptionHistory.Count} items.");

            Console.WriteLine("Sweet is eating...");
            while(!sweet.IsFull()){
                sweet.Consume(buffet1.Serve());
            }
            Console.WriteLine($"Spicy ate {sweet.ConsumptionHistory.Count} items.");

            if(spicy.ConsumptionHistory.Count > sweet.ConsumptionHistory.Count)
            {
                Console.WriteLine("Spicy ate more!");
            }
            else if(spicy.ConsumptionHistory.Count < sweet.ConsumptionHistory.Count)
            {
                Console.WriteLine("Sweet ate more!");
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }
        }
    }
}
