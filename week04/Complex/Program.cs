using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Complex c0 = new Complex(-2, 3);
            Complex c1 = new Complex(-2, 3);
            Complex c2 = new Complex(1, -2);

            Console.WriteLine($"{c0}");
            Console.WriteLine(c1);
            Console.WriteLine(c2);
            Console.WriteLine("------------------------------------------------------------");

            //+ – You will overload the plus operator to add the two numbers.Copy the code below into your type declaration to overload the + operator.
            Console.WriteLine($"{c1} + {c2} = {c1 + c2}");
            //– You will also overload the minus operator. Examine the code above and then try to implement this operator.
            Console.WriteLine($"{c1} - {c2} = {c1 - c2}");
            Console.WriteLine("------------------------------------------------------------");

            Complex c3 = c1 + c2;

            Console.WriteLine($"{c3} in polar form is {c3.Modulus:f2}cis({c3.Argument:f2})");
            Console.WriteLine("------------------------------------------------------------");

            //==  – You will also overload the equal-equal operator. Examine the code above and then try to implement this operator. What should the return type of the method be?
            Console.WriteLine($"{c0} {(c0 == c1 ? "=" : "!=")} {c1}");
            Console.WriteLine($"{c0} {(c0 == c2 ? "=" : "!=")} {c2}");
            Console.WriteLine("------------------------------------------------------------");

            //1.	Try to overload the * operator. Multiplication of two complex numbers is defined by the following relation: <a, b> * <c, d> = <ac-bd, ad+bc>. 
            Console.WriteLine($"{c1} * {c2} = {c1 * c2}");
            Console.WriteLine("------------------------------------------------------------");
            //2.	Try to overload the unary - operator. This operator simply changes the sign of the operand: -<a, b> = <-a, -b>. 
            Console.WriteLine($"{-(c0)}");
            Console.WriteLine($"{-(c1)}");
            Console.WriteLine($"{-(c2)}");
        }

        public class Complex
        {
            public int Real { get; }
            public int Imaginary { get; }
            public double Modulus => Math.Sqrt(Real * Real + Imaginary * Imaginary);
            public double Argument => Math.Atan2(Imaginary, Real);
            public static Complex Zero => new Complex(0, 0);

            public Complex(int real = 0, int imaginary = 0)
            {
                Real = real;
                Imaginary = imaginary;
            }

            public override string ToString()
            {
                return $"({Real}, {Imaginary})";
            }

            public static Complex operator +(Complex lhs, Complex rhs)
            {
                int real = lhs.Real + rhs.Real;
                int imaginary = lhs.Imaginary + rhs.Imaginary;
                return new Complex(real, imaginary);
            }

            public static Complex operator -(Complex lhs, Complex rhs)
            {
                int real = lhs.Real - rhs.Real;
                int imaginary = lhs.Imaginary - rhs.Imaginary;
                return new Complex(real, imaginary);
            }

            public static bool operator ==(Complex lhs, Complex rhs)
            {
                return lhs.Real == rhs.Real && lhs.Imaginary == rhs.Imaginary;
            }

            public static bool operator !=(Complex lhs, Complex rhs)
            {
                return !(lhs == rhs);
            }

            public static Complex operator *(Complex lhs, Complex rhs)
            {
                int real = lhs.Real * rhs.Real;
                int imaginary = lhs.Imaginary * rhs.Imaginary;
                return new Complex(real, imaginary);
            }

            public static Complex operator -(Complex c)
            {
                return new Complex(-c.Real, -c.Imaginary);
            }
        }

    }
}
