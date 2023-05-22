using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Date
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Date test = new Date();
            Date first = new Date(2024, 13, 33);// 2025 feb 2
            Date second = new Date(2022, 5, 25);
            Date third = new Date(11, 2, 29);
            Date fourth = new Date(2024, 2, 29);
            Date fifth = new Date(2024, 2, 10);


            //Enhancement: checking validator
            Console.WriteLine(test);
            Console.WriteLine(first);
            Console.WriteLine(second);
            Console.WriteLine(third);
            Console.WriteLine(fourth);
            Console.WriteLine(fifth);
            Console.WriteLine("-------------");

            //using 2.public void Add(int days)
            test.Add(31);
            Console.WriteLine(test);
            Console.WriteLine("-------------");

            //using 3.public void Add(int days, int months)
            second.Add(12, 11);
            fifth.Add(12, 31); // 2025 mar 13
            Console.WriteLine(second);
            Console.WriteLine(fifth);
            Console.WriteLine("-------------");

            //public 4.void Add(Date other)
            fourth.Add(third);
            Console.WriteLine(fourth);
            Console.WriteLine(third);

        }

        class Date
        {
            int year;
            int month;
            int day;
            Date date;


            public Date(int year = 2022, int month = 1, int day = 1)
            {

                this.year = year;
                this.month = month;
                this.day = day;
                Normalize();
            }

            public void Add(int days)
            {
                this.day += days;
                Normalize();
            }

            //priority months then days
            public void Add(int months, int days)
            {
                this.day += days;
                this.month += months;
                Normalize();
            }


            public void Add(Date other)
            {
                this.date = other;
                year += other.year;
                month += other.month;
                day += other.day;
                Normalize();
            }


            private void Normalize()
            {
                if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
                {
                    if (month <= 12)
                    {
                        if (month == 2 && day > 29)
                        {
                            day -= 29;
                            month += 1;
                        }
                        else if ((month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) && (day > 31))
                        {
                            day -= 31;
                            month += 1;
                        }
                        else if ((month == 4 || month == 6 || month == 9 || month == 11) && (day > 30))
                        {
                            day -= 30;
                            month += 1;
                        }
                    }
                    else if (month > 12)
                    {
                        year += 1;
                        month -= 12;
                        if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
                        {
                            if (month == 2 && day > 29)
                            {
                                day -= 29;
                                month += 1;
                            }
                            else if ((month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) && (day > 31))
                            {
                                day -= 31;
                                month += 1;
                            }
                            else if ((month == 4 || month == 6 || month == 9 || month == 11) && (day > 30))
                            {
                                day -= 30;
                                month += 1;
                            }
                        }
                        else
                        {
                            if (month == 2 && day > 28)
                            {
                                day -= 28;
                                month += 1;
                            }
                            else if ((month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) && (day > 31))
                            {
                                day -= 31;
                                month += 1;
                            }
                            else if ((month == 4 || month == 6 || month == 9 || month == 11) && (day > 30))
                            {
                                day -= 30;
                                month += 1;
                            }
                        }
                            
                    }
                }
                else
                {
                    if (month <= 12)
                    {
                        if (month == 2 && day > 28)
                        {
                            day -= 28;
                            month += 1;
                        }
                        else if ((month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) && (day > 31))
                        {
                            day -= 31;
                            month += 1;
                        }
                        else if ((month == 4 || month == 6 || month == 9 || month == 11) && (day > 30))
                        {
                            day -= 30;
                            month += 1;
                        }
                    }
                    else if (month > 12)
                    {
                        year += 1;
                        month -= 12;
                        if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
                        {
                            if (month == 2 && day > 29)
                            {
                                day -= 29;
                                month += 1;
                            }
                            else if ((month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) && (day > 31))
                            {
                                day -= 31;
                                month += 1;
                            }
                            else if ((month == 4 || month == 6 || month == 9 || month == 11) && (day > 30))
                            {
                                day -= 30;
                                month += 1;
                            }
                        }
                        else
                        {
                            if (month == 2 && day > 28)
                            {
                                day -= 28;
                                month += 1;
                            }
                            else if ((month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) && (day > 31))
                            {
                                day -= 31;
                                month += 1;
                            }
                            else if ((month == 4 || month == 6 || month == 9 || month == 11) && (day > 30))
                            {
                                day -= 30;
                                month += 1;
                            }
                        }
                    }
                }
            }

            public override string ToString()
            {
                string m = "";
                string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
                m = months[month - 1];
                return $" {year} {m} {day}";
            }

        }
    }
}
