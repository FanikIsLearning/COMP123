using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Car a = new Car("Model S", 2002, 70000.11, true);
            Car b = new Car("Model E", 2021, 40000.11, false);
            Car c = new Car("Model X", 2022, 50000.11, true);
            Car d = new Car("Model Y", 2023, 80000.11, false);

            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(c);
            Console.WriteLine(d);
        }

        class Car
        {
            int year;
            string model;
            bool isDrivable;
            double price;

            public Car(string model, int year, double price, bool isDrivable = true)
            {
                this.model = model;
                this.year = year;
                this.price = price;
                this.isDrivable = isDrivable;
            }

            public override string ToString()
            {
                return $"Car: {model}, Year: {year}, Price: {price:c2}, Drivable: {(isDrivable == true ? "Yes" : "No")}";
            }
        }
    }
}
