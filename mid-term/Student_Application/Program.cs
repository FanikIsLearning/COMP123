using System;
using System.Collections.Generic;
using System.Text.Json;
using static Student_Application.Program;

namespace Student_Application
{
    public enum StudentProgram
    {
        GP,
        AI,
        HIT,
        SET,
        SETn
    }

    public enum StudentStatus
    {
        Fulltime,
        Graduated,
        Suspended
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating a course");
            //write the statements to create a course - 1 marks
            //object and print it
            Course course = new Course("COMP100", 4);
            Console.WriteLine(course);

            Console.WriteLine("Creating a student");
            //write the statements to instantiate the - 1 marks
            //Student class with the above course
            //object and display the resulting object
            Student jakeNesovic = new Student("Jake Nesovic", StudentStatus.Graduated, course, StudentProgram.SET);
            Console.WriteLine(jakeNesovic);


            Console.WriteLine("\n\nAdding a few courses to the student object");
            //write the statements to add 3 courses to - 2 marks
            //the current student and then print it
            jakeNesovic.Add(new Course("EMPS100", 3));
            jakeNesovic.Add(new Course("COMP120", 4));
            jakeNesovic.Add(new Course("COMP213", 4));
            Console.WriteLine(jakeNesovic);

            string startswith = "COMP";
            Console.WriteLine($"\n\nDisplaying all {startswith} courses for {jakeNesovic.Name}");
            //write the statements to filter out only - 1 marks
            //the required courses from the Courses collection
            //Hint: use the StartsWith() of the string class
            List<Course> filteredCourses = jakeNesovic.Courses.FindAll(courses => courses.Code.StartsWith(startswith));
            foreach (Course startWithCourses in filteredCourses)
            {
                Console.WriteLine(startWithCourses);
            }


            Course comp100 = jakeNesovic.Courses[0];
            jakeNesovic.Drop(comp100);
            Console.WriteLine($"\n\nDropping \"{comp100}\"");
            //write the statements to drop the first - 1 marks
            //course from the student object and display the resulting object
            Console.WriteLine(jakeNesovic);



            StudentProgram program = StudentProgram.GP;
            Console.WriteLine($"\n\nChanging to \"{program}\"");
            //write the statements to change the program - 1 marks
            //and display the resulting object
            jakeNesovic.ChangeProgram(program);
            Console.WriteLine(jakeNesovic);

            Console.WriteLine($"\n\nCreating a collection of 4 students");
            //write the statements to create a collection - 2 marks
            //of 4 students
            List<Student> students = new List<Student>();
            Course id = new Course("ID: 301249275", 4);
            students.Add(jakeNesovic);
            students.Add(new Student("HOI KIT FAN", StudentStatus.Fulltime, id, StudentProgram.SET));
            students.Add(new Student("Ivan", StudentStatus.Suspended, course, StudentProgram.SET));
            students.Add(new Student("Selene", StudentStatus.Graduated, course, StudentProgram.GP));
            foreach (Student s in students)
            {
                Console.WriteLine(s);
            }

            string filename = "students.json";
            Console.WriteLine($"\n\nSerializing the students collection to the file {filename}");
            //write the statements to save the - 1 marks
            //current collection to a file
            string json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filename, json);
            //json stored in bin folder
        }

        public struct Course
        {
            public string Code { get; }
            public int Hours { get; }

            public Course(string code, int hours)
            {
                Code = code;
                Hours = hours;
            }

            public override string ToString()
            {
                return $"{Code} {Hours}hrs";
            }
        }

        public interface IStudent
        {
            void Add(Course course);
            void Drop(Course course);
            void ChangeProgram(StudentProgram program);
        }

        class Student : IStudent
        {
            private static int ID = 1_000;

            public string Name { get; }
            public string Id { get; }
            public StudentStatus Status { get; }
            public StudentProgram Program { get; private set; }
            public List<Course> Courses { get; }

            public Student(string name, StudentStatus status, Course course, StudentProgram program = StudentProgram.SET)
            {
                Name = name;
                Status = status;
                Courses = new List<Course>();
                Add(course);
                Program = program;
                Id = ID.ToString();
                ID++;
            }

            public override string ToString()
            {
                string courseList = "";

                if (Courses.Count == 1)
                {
                    courseList += Courses[0].ToString();
                }
                else
                {
                    foreach (Course course in Courses)
                    {
                        courseList += course.ToString() + ", ";
                    }

                    courseList = courseList.Remove(courseList.Length - 2);
                }

                return $"{Id} ({Program}) {Name} {Status} \n Courses: {courseList}";
            }

            public void Add(Course course)
            {
                Courses.Add(course);
            }

            public void Drop(Course course)
            {
                Courses.Remove(course);
            }

            public void ChangeProgram(StudentProgram program)
            {
                Program = program;
            }
        }
    }
}
