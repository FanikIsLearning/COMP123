using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecentangleDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //declare a BadRecmtagle variable
            Rectangle small;
            Rectangle big;
            //Initialize the variable
            small = new Rectangle(10, 6);
            //print the variable
            big = new Rectangle(15, 20);

            //set the length and width
      
            //print the variable
            Console.WriteLine(small);
            Console.WriteLine(big);

            //change the dimensions of the aobve rectangle
            big.SetLengthWidth(8, 5);
            Console.WriteLine($"New object: {big})");
        }

        //static void BadRecntangleDemo()
    }

    //defining a Rectangle Class
    class Rectangle
    {
        int length; //data attribute (field)
        int width; //data attribute(field)
        //constructor
        public Rectangle(int len, int wid)
        {
            length = len;
            width = wid;
        }

        public void SetLengthWidth(int len, int wid)
        {
            length = len;
            width = wid;
        }
        //override string!!
        public override string ToString()
        {
            return $"Length:{length}, width:{width}";
        }
    }

    //defining a BadRectangle Class
    class BadRectangle
    {
        public int length; //data attribute (field)
        public int width; //data attribute(field)
        //constructor
     
    }
}
