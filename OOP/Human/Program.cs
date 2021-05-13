using System;

namespace Human
{
    class Human
    {
        // Fields for Human
        public string Name;
        public int Strength;
        public int Intelligence;
        public int Dexterity;
        private int health;
         
        // add a public "getter" property to access health
        public int Health
        {
            get {return this.health;}
            set {this.health = value;}
        }
         
        // Add a constructor that takes a value to set Name, and set the remaining fields to default values
        public Human(string name)
        {
            this.Name = name;
            this.Strength = 3;
            this.Intelligence = 3;
            this.Dexterity = 3;
            this.health = 100;
        }

        // Add a constructor to assign custom values to all fields
        public Human(string name, int str, int inte, int dex, int hlth )
        {
            this.Name = name;
            this.Strength = str;
            this.Intelligence = inte;
            this.Dexterity = dex;
            this.health = hlth;
        }
         
        // Build Attack method
        public virtual int Attack(Human target)
        {
            int dmg = this.Strength * 5;
            target.Health -= dmg;
            Console.WriteLine($"Dealt {dmg} damage to {target.Name} ({target.Health} HP remaining)");
            return target.Health;
        }
    }

    class Wizard : Human
    {
        public Wizard(string name) : base(name, 3, 25, 3, 50)
        {
    
        }

        public override int Attack(Human target)
        {
            int dmg = this.Intelligence * 5;
            target.Health -= dmg;
            this.Health += dmg;
            Console.WriteLine($"Dealt {dmg} damage to {target.Name} ({target.Health} HP remaining)");
            return target.Health;
        }

        public void Heal(Human target)
        {
            target.Health += 10 * this.Intelligence;
            Console.WriteLine($"Healed {10*this.Intelligence} damage to {target.Name} ({target.Health} HP remaining)");
        }
    }

    class Ninja : Human
    {
        public Ninja(string name) : base(name, 3, 3, 175, 100)
        {
            
        }
        public override int Attack(Human target)
        {
            int dmg = this.Dexterity * 5;
            Random crit = new Random();
            if(crit.Next(4) == 0)
            {
                dmg += 10;
            }
            target.Health -= dmg;
            Console.WriteLine($"Dealt {dmg} damage to {target.Name} ({target.Health} HP remaining)");
            return target.Health;
        }

        public void Steal(Human target)
        {
            target.Health -= 5;
            this.Health += 5;
            Console.WriteLine($"Stole 5 health from {target.Name} ({target.Health} HP remaining)");
        }
    }

    class Warrior : Human
    {
        public Warrior(string name) : base(name, 3, 3, 3, 200)
        {
    
        }

        public override int Attack(Human target)
        {
            base.Attack(target);
            if(target.Health <= 50){
                target.Health = 0;
            }
            return target.Health;
        }

        public void Meditate()
        {
            Console.WriteLine("Meditating...");
            if(this.Health < 200)
            {
                this.Health = 200;
                Console.WriteLine($"HP restored!");
            }
            else{
                Console.WriteLine($"But nothing happened");
            }
            
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Human assignment
            Human human1 = new Human("a");
            Human human2 = new Human("b", 3, 5, 6, 200);
            Console.WriteLine(human2.Attack(human1));

            //Wizard, Ninja, Warrior assignment
            Wizard wi1 = new Wizard("Wiz");
            Ninja n1 = new Ninja("Nin");
            Warrior wr1 = new Warrior("Wa");

            wi1.Heal(wr1);
            wi1.Heal(n1);
            wi1.Heal(wi1);
            n1.Steal(wr1);
            wr1.Meditate();
            wr1.Attack(n1);
            wi1.Attack(wr1);
            n1.Attack(wi1);
        }
    }
}
