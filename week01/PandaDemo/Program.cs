using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanadaDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //instantiate class
            Panda joe = new Panda("Joe Biden", 72);
            Console.WriteLine(joe);
            Panda donald = new Panda("Donald Trump", 71);
            Console.WriteLine(donald);

            //Marry
            joe.Marry(donald);
            Console.WriteLine(joe);
            Console.WriteLine(donald);

            Panda barack = new Panda("barack", 58);
            Panda michelle = new Panda("michelle", 49, barack);
            Console.WriteLine(barack);
            Console.WriteLine(michelle);
        }

    
    }

    class Panda
    {
        string name;
        int age;
        Panda spouse;
        //data members -> name, age, spouse
        //action members -> constryctir, tostring, marry
        public Panda(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public Panda(string name, int age, Panda other)
        {
            this.name = name;
            this.age = age;
            Marry(other);
        }

        public void Marry (Panda other)
        {
            spouse = other;
            other.spouse = this.spouse;
        }

        public override string ToString()
        {
            string isMarried;
            if (spouse == null)
            {
                isMarried = "not";
            }
            else
            {
                isMarried = "";
            }
            //return $"I am {name} and I am {age}yrs and I am {isMarried}married";
            return $"I am {name} and I am {age}yrs and I am {(spouse == null ? "not": "")}married";
        }
    }

 
}
