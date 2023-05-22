using Time;

namespace Time
{
    public enum TimeFormat
    {
        Mil,
        Hour12,
        Hour24,
            hour11
    }

    class Program
    {
        static void Main(string[] args)
        {
            //To test the constructor and the ToString method
            //create a list to store the objects
            List<Time> times = new List<Time>()
            {
                new Time(9, 35),
                new Time(18, 5),
                new Time(20, 500),
                new Time(10),
                new Time()
            };


            //display all the objects
            TimeFormat format = TimeFormat.Hour12;
            Console.WriteLine($"\n\nTime format is {format}");
            foreach (Time t in times)
            {
                Console.WriteLine(t);
            }

            //change the format of the output
            format = TimeFormat.Mil;
            Console.WriteLine($"\n\nSetting time format to {format}");
            //SetFormat(TimeFormat) is a class member, so you need the type to access it
            Time.SetFormat(format);
            //again display all the objects
            foreach (Time t in times)
            {
                Console.WriteLine(t);
            }

            //change the format of the output
            format = TimeFormat.Hour24;
            Console.WriteLine($"\n\nSetting time format to {format}");
            //SetFormat(TimeFormat) is a class member, so you need the type to access it
            Time.SetFormat(format);
            foreach (Time t in times)
            {
                Console.WriteLine(t);
            }
        }

    }

    public class Time
    {
        private static TimeFormat TIME_FORMAT = TimeFormat.Hour12;
        public int Hour { get; }
        public int Minute { get; }

        public Time(int hour = 0, int minute = 0)
        {
            Hour = hour >= 0 && hour <= 24 ? hour : 0;
            Minute = minute >= 0 && minute <= 60 ? minute : 0;
        }

        public override string ToString()
        {
            string result = "";
            switch (TIME_FORMAT)
            {
                case TimeFormat.Mil:
                    result = $"{Hour:D2}{Minute:D2}";
                    break;
                case TimeFormat.Hour24:
                    result = $"{Hour:D2}:{Minute:D2}";
                    break;
                /*case TimeFormat.Hour12:
                    int hour = Hour == 0 || Hour == 12 ? 00 : Hour % 12;
                    string period = Hour < 12 ? "AM" : "PM";
                    result = $"{hour:D1}:{Minute:D2} {period}";*/

                case TimeFormat.Hour12:
                    int hr;
                    string time;
                    if(Hour > 12)
                    {
                        time = "PM";
                    }
                    else
                    {
                        time = "AM";
                    }

                    if (Hour == 0 || Hour == 12)
                    {
                        hr = 00;
                    }
                    else
                    {
                        hr = Hour % 12;
                    }
                    result = $"{hr:D1}:{Minute:D2} {time}";
                    break;
            }
            return result;
        }

        public static void SetFormat(TimeFormat timeFormat)
        {
            TIME_FORMAT = timeFormat;
        }
    }


}





