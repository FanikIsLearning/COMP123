using System;
using System.Collections.Generic;
using System.Linq;
using static Data;

class Program
{
    static List<Person> persons = Data.persons;
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
        Console.WriteLine("a. Select the 5 cheapest fruits:");
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
