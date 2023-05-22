using System;
using System.Collections.Generic;

namespace Inheritance_Shapes
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //although Shape is an abstract is can be used as a reference type
            //any child class of Shape is also a Shape
            List<Shape> shapes = new List<Shape>();

            shapes.Add(new Square("s1", 2));
            shapes.Add(new Rectangle("r1", 2, 3));
            shapes.Add(new Circle("c1", 2));
            shapes.Add(new Triangle("t1", 4, 6));
            shapes.Add(new Ellipse("e1", 2, 3));
            shapes.Add(new Diamond("d1", 2, 3));

            shapes.Add(new Square("s2", 5));
            shapes.Add(new Rectangle("r2", 5, 4));
            shapes.Add(new Circle("c2", 1));
            shapes.Add(new Triangle("t2", 7, 8));

            foreach (var s in shapes)
            {
                Console.WriteLine(s);
            }
        }

        public abstract class Shape
        {
            // Properties
            public string Name { get; private set; }
            public abstract double Area { get; }

            // Constructor
            public Shape(string name)
            {
                Name = name;
            }

            // Methods
            public override string ToString()
            {
                return $"Name: {Name}, Area: {Area:n2}\n-------------------------";
            }
        }

        public class Square : Shape
        {
            public double Length { get; protected set; }
            public override double Area => Math.Pow(Length, 2);

            public Square(string name, double length) : base(name)
            {
                Length = length;
            }
        }

        public class Circle : Square
        {
            public override double Area => Math.PI * Math.Pow(Length, 2);

            public Circle(string name, double length) : base(name, length)
            {
            }
        }

        public class Rectangle : Shape
        {
            public double Width { get; protected set; }
            public double Height { get; protected set; }
            public override double Area => Width * Height;

            public Rectangle(string name, double height, double width) : base(name)
            {
                Height = height;
                Width = width;
            }
        }

        public class Ellipse : Rectangle
        {
            public override double Area => Math.PI * Width * Height;

            public Ellipse(string name, double height, double width) : base(name, height, width)
            {
            }
        }

        public class Triangle : Rectangle
        {
            public override double Area => Width * Height * 0.5;

            public Triangle(string name, double height, double width) : base(name, height, width)
            {
            }
        }

        public class Diamond : Rectangle
        {
            public override double Area => Width * Height * 0.5;

            public Diamond(string name, double height, double width) : base(name, height, width) { }
        }
    }

}
