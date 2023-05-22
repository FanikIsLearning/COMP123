using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<Student> students = new List<Student> {
  new Student{ First="Xavier", Last="Thomas", Id=111, Scores=new List<int>{97,92 ,81 , 55, 60 } },
  new Student{ First="Lyoid", Last="Lopes", Id=112, Scores=new List<int>{96, 88, 86, 77, 55 } },
  new Student{ First="Navdeep", Last="Singh", Id=113, Scores=new List<int>{92, 97, 65, 89,  45} },
  new Student{ First="Lyle", Last="Spurrell", Id=114, Scores=new List<int>{90, 95, 75, 78, 83 } },
  new Student{ First="Viktor", Last="Salnichenko", Id=115, Scores=new List<int>{87, 96, 65, 34,  90} },
  new Student{ First="Sukhpratap", Last="Singh", Id=116, Scores=new List<int>{90, 87, 56, 98, 78 } },
  new Student{ First="Dannel", Last="Alon", Id=117, Scores=new List<int>{90, 84,59 ,67 , 98 } },
  new Student{ First="Francis", Last="Acheampong", Id=118, Scores=new List<int>{89, 56, 56, 67, 87 } },
  new Student{ First="Mahamod", Last="Masleh", Id=119, Scores=new List<int>{67, 78,46 , 78, 98 } },
  new Student{ First="John", Last="Calma", Id=120, Scores=new List<int>{89, 76, 78, 67,  87} },
  new Student{ First="Sarina", Last="Luu", Id=121, Scores=new List<int>{67, 67, 87, 74,  95} },
  new Student{ First="Valerie", Last="Chan", Id=122, Scores=new List<int>{87, 69, 95, 67,  49} },
  new Student{ First="Tej", Last="Singh", Id=127, Scores=new List<int>{90, 87, 96, 98, 78 } },
  new Student{ First="Mabel", Last="Tang", Id=123, Scores=new List<int>{87, 78,59 , 75,  67} },
  new Student{ First="Rishav", Last="Giri", Id=124, Scores=new List<int>{65, 87, 58, 92,  68} },
  new Student{ First="Toni", Last="Lea", Id=125, Scores=new List<int>{78, 97, 83, 83,  87} },
  new Student{ First="Melanie", Last="March", Id=126, Scores=new List<int>{89, 79, 80, 95, 97 } }
};

    static List<Person> persons = new List<Person>()
{
  new Person(){ Age = 72, Asset = 7.0, Country="South Africa", IsFemale=false, Name="Nicky Oppenheimer"},
  new Person(){ Age = 67, Asset = 7.6, Country="India", IsFemale=true, Name="Savitri Jindal"},
  new Person(){ Age = 81, Asset = 3.1, Country="India", IsFemale=true, Name="Indu Jain"},
  new Person(){ Age = 70, Asset = 2.5, Country="India", IsFemale=true, Name="Vinod Gupta"},
  new Person(){ Age = 77, Asset = 27.0, Country = "US",IsFemale = true,Name = "Jacqueline Mars"},
  new Person(){ Age = 76, Asset = 25.2, Country = "Italy", IsFemale = true, Name = "Maria Franca Fissolo"},
  new Person(){ Age = 55, Asset = 20.4, Country = "Germany", IsFemale = true, Name = "Susanne Klatten"},
  new Person(){ Age = 53, Asset = 20.0, Country = "US",IsFemale = true,Name = "Laurene Jobs"},
  new Person(){ Age = 60, Asset = 12.5, Country = "Nigeria", IsFemale=false, Name="Aliko Dangote" },
  new Person(){ Age = 76, Asset = 10.9, Country = "Ethiopia", IsFemale=false, Name="Mohammed Al Amoudi"},
  new Person(){ Age = 60, Asset = 30.7, Country = "Canada", IsFemale=false, Name="David Thomson" },
  new Person(){ Age = 76, Asset = 11.4, Country = "Canada", IsFemale=false, Name="Galen Weston"},
  new Person(){ Age = 60, Asset = 22.3, Country = "India", IsFemale=false, Name="Mukesh Ambani"},
  new Person(){ Age = 50, Asset = 17.5, Country = "India", IsFemale=false, Name="Dilip Shanghvi"},
  new Person(){ Age = 83, Asset = 30.4, Country = "US", IsFemale=false, Name="Sheldon Adelson"},
  new Person(){ Age = 78, Asset = 30.0, Country = "Brazil", IsFemale=false, Name="Jorge Lemann"},
  new Person(){ Age = 62, Asset = 18.4, Country = "Russia", IsFemale=false, Name="Leonid Mikhelson"},
  new Person(){ Age = 51, Asset = 17.5, Country = "Russia", IsFemale=false, Name="Alexey Mordashov"},
  new Person(){ Age = 89, Asset = 31.2, Country = "Hong Kong", IsFemale=false, Name="Li Ka-shing"},
  new Person(){ Age = 62, Asset = 31.2, Country = "China", IsFemale=false, Name="Wang Jianlin"},
  new Person(){ Age = 67, Asset = 33.8, Country = "US", IsFemale=true, Name="Alice Walton" },
  new Person(){ Age = 60, Asset = 34.0, Country = "US", IsFemale=false, Name="Jim Walton"},
  new Person(){ Age = 72, Asset = 34.1, Country = "US", IsFemale=false, Name="Rob Walton"},
  new Person(){ Age = 94, Asset = 39.5, Country = "France", IsFemale=true, Name="Liliane Bettencourt"},
  new Person(){ Age = 43, Asset = 39.8, Country = "US", IsFemale=false, Name="Sergey Brin"},
  new Person(){ Age = 43, Asset = 39.6, Country = "US", IsFemale=false, Name="Larry Page"},
  new Person(){ Age = 68, Asset = 41.5, Country = "France", IsFemale=false, Name="Bernard Arnault"},
  new Person(){ Age = 75, Asset = 47.5, Country = "US", IsFemale=false, Name="Michael Bloomberg"},
  new Person(){ Age = 77, Asset = 48.3, Country = "US", IsFemale=false, Name="David Koch"},
  new Person(){ Age = 81, Asset = 48.3, Country = "US", IsFemale=false, Name="Charles Koch"},
  new Person(){ Age = 72, Asset = 52.2, Country = "US", IsFemale=false, Name="Larry Ellison"},
  new Person(){ Age = 77, Asset = 54.5, Country = "Mexico", IsFemale=false, Name="Carlos Slim Helu"},
  new Person(){ Age = 33, Asset = 56.0, Country = "US", IsFemale=false, Name="Mark Zuckerberg"},
  new Person(){ Age = 81, Asset = 71.3, Country = "Spain", IsFemale=false, Name="Amancio Ortega"},
  new Person(){ Age = 53, Asset = 72.8, Country = "US", IsFemale=false, Name="Jeff Bezos" },
  new Person(){ Age = 85, Asset = 75.6, Country = "US", IsFemale=false, Name="Warren Buffet" },
  new Person(){ Age = 60, Asset = 86.0, Country = "US", IsFemale=false, Name="Bill Gates"}
};

    static List<Fruit> fruits = new List<Fruit>()
{
  new Fruit(){ Name="Mango", Origin="Guyana", Price=5.67},
  new Fruit(){ Name="Kiwi", Origin="New Zeeland", Price=1.34},
  new Fruit(){ Name="Dragon Fruit", Origin="China", Price=3.45},
  new Fruit(){ Name="Avacado", Origin="Mexico", Price=2.56},
  new Fruit(){ Name="Banana", Origin="Ecuador", Price=0.25},
  new Fruit(){ Name="Persimon", Origin="Spain", Price=1.36 },
  new Fruit(){ Name="Blueberry", Origin="Canada", Price=0.19 },
  new Fruit(){ Name="Strawberry", Origin="Russia", Price=0.45 },
  new Fruit(){ Name="Avocado", Origin="Mexico", Price=0.45 }
};


    public class Student
    {
        public string First { get; set; }
        public string Last { get; set; }
        public int Id { get; set; }
        public List<int> Scores;
        public override string ToString()
        {
            return string.Format("{0} {1} - {2} ", First, Last, Id);
        }
    }
    class Fruit
    {
        public string Name { get; set; }
        public string Origin { get; set; }
        public double Price { get; set; }
        public override string ToString()
        {
            return string.Format("{0} @{1:c} ({2})", Name, Price, Origin.Substring(0, 2).ToUpper());
        }
    }
    class Person
    {
        public string Name { get; set; }
        public double Asset { get; set; }
        public bool IsFemale { get; set; }
        public string Country { get; set; }
        public int Age { get; set; }
        public override string ToString()
        {
            return string.Format("{0} {1}B {2} {3} {4}yrs", Name, Asset, IsFemale ? "F" : "M", Country, Age);
        }
    }


    class PetOwner
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }


    class Pet
    {
        public string Name { get; set; }
        public PetOwner Owner { get; set; }
    }


    // Include the class definitions and data source from the provided code

    static void Main(string[] args)
    {
        Console.WriteLine("1. Select all the persons with assets of over 50B dollars:");
        var query1 = from p in persons where p.Asset > 50 select p;
        foreach (var person in query1)
        {
            Console.WriteLine(person);
        }

        Console.WriteLine("\n2. Select all non-US citizens:");
        var query2 = from p in persons where p.Country != "US" select p;
        foreach (var person in query2)
        {
            Console.WriteLine(person);
        }

        Console.WriteLine("\n3. Select the name of all the females from India:");
        var query3 = from p in persons where p.IsFemale && p.Country == "India" select p.Name;
        foreach (var name in query3)
        {
            Console.WriteLine(name);
        }

        Console.WriteLine("\n4. Select all persons whose first name is less than five letters long:");
        var query4 = from p in persons where p.Name.Split()[0].Length < 5 select p;
        foreach (var person in query4)
        {
            Console.WriteLine(person);
        }

        Console.WriteLine("\n5. Sort the collection by assets:");
        var query5 = from p in persons orderby p.Asset descending select new { p.Name, p.Asset };
        foreach (var item in query5)
        {
            Console.WriteLine($"{item.Name} - {item.Asset}B");
        }

        Console.WriteLine("\n6. Group the collection by country:");
        var query6 = from p in persons group p by p.Country;
        foreach (var group in query6)
        {
            Console.WriteLine($"Country: {group.Key}");
            foreach (var person in group)
            {
                Console.WriteLine(person);
            }
            Console.WriteLine();
        }

        Console.WriteLine("\n7. Sort the above grouping:");
        var query7 = from g in query6 orderby g.Key select g;
        foreach (var group in query7)
        {
            Console.WriteLine($"Country: {group.Key}");
            foreach (var person in group)
            {
                Console.WriteLine(person);
            }
            Console.WriteLine();
        }

        Console.WriteLine("\n8. Make up three queries of your own:");
        Console.WriteLine("a. Choose the 5 least wealthy individuals:");
        var query8a = (from p in persons orderby p.Asset ascending select p).Take(5);
        foreach (var person in query8a)
        {
            Console.WriteLine(person);
        }

        Console.WriteLine("\nb. Select the top 3 richest females:");
        var query8b = (from p in persons where p.IsFemale orderby p.Asset descending select p).Take(3);
        foreach (var person in query8b)
        {
            Console.WriteLine(person);
        }

        Console.WriteLine("\nc. Choose individuals whose first names start with 'M':");
        var query8c = from p in persons where p.Name.StartsWith("M") select p;
        foreach (var person in query8c)
        {
            Console.WriteLine(person);
        }
    }
}
