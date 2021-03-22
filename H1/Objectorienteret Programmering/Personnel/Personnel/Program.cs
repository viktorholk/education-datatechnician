using System;
using System.Collections.Generic;

namespace Personnel
{
    class Personnel
    {
        static public List<Personnel> personnels = new List<Personnel>();
        public enum PersonnelTypes
        {
            Student,
            Teacher
        }
        public PersonnelTypes type;
        public int id;
        public string username;
        public string password;

        public override string ToString()
        {
            return $"{this.id}, {this.type}, {this.username}";
        }
    }

    class Student : Personnel
    {


        public int classId;
        public Student(string username, string password, int classId)
        {
            // Get student id
            id = personnels.Count + 1;
            this.classId = classId;
            // Assign the type
            type = PersonnelTypes.Student;

            this.username = username;
            this.password = password;

            personnels.Add(this);

        }
    }

    class Teacher : Personnel
    {
        int educateClassId;

        public Teacher(string username, string password, int educateClassId)
        {
            // Get teacher id
            id = personnels.Count + 1;

            this.educateClassId = educateClassId;
            // type of personel
            type = PersonnelTypes.Teacher;

            this.username = username;
            this.password = password;

            personnels.Add(this);

        }

        public void PrintStudentClass()
        {
            foreach (var personnel in personnels)
            {
                if (personnel.type == PersonnelTypes.Student)
                {
                    // Cast the personnel object to student object
                    Student student = (Student)personnel;
                    // Check if student is in class
                    if (educateClassId == student.classId)
                    {
                        Console.WriteLine(student);
                    }
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Define already registered users
            new Teacher("teacher1", "123", 1);
            new Student("student1", "123", 1);
            new Student("student2", "123", 1);
            new Student("student3", "123", 1);
            new Student("student4", "123", 1);

            new Teacher("teacher2", "123", 2);
            new Student("student5", "123", 2);
            new Student("student6", "123", 2);
            new Student("student7", "123", 2);

            Console.WriteLine("Personnel Login");
            Personnel user;
            string username;
            string password;
            while (true)
            {
                // Get username
                Console.Write("USERNAME: ");
                username = Console.ReadLine();
            
                // Get password
                Console.Write("PASSWORD: ");
                password = Console.ReadLine();

                user = LoginUser(username, password);
                if (user != null) break;
                else Console.WriteLine("Incorrect username or password");
            }

            // welcome message
            Console.WriteLine($"Welcome back {user.type} {user.username}");
            
            // Conditions if user is either student or teahcer
            if (user.type == Personnel.PersonnelTypes.Student)
            {
                // Menu
                Console.WriteLine("1. Print Student Infomation");
                Console.WriteLine("2. Quit");
                while (true)
                {
                    int input = MenuInput();
                    if (input == 2) break;
                    switch (input)
                    {
                        case 1:
                            Console.WriteLine(user);
                            break;
                        default:
                            Console.WriteLine("Not a valid option.");
                            break;
                    }
                }
            } 
            else if (user.type == Personnel.PersonnelTypes.Teacher)
            {
                // Menu
                Console.WriteLine("1. Print Teacher Infomation");
                Console.WriteLine("2. Print Students In Class");
                Console.WriteLine("3. Quit");
                while (true)
                {
                    int input = MenuInput();
                    if (input == 3) break;
                    switch (input)
                    {
                        case 1:
                            Console.WriteLine(user);
                            break;
                        case 2:
                            // cast to teacher object
                            Teacher teacher = (Teacher)user;
                            teacher.PrintStudentClass();
                            break;
                        default:
                            Console.WriteLine("Not a valid option.");
                            break;
                    }
                }
            }
        }

        static int MenuInput()
        {
            int userInput;
            while (!int.TryParse(Console.ReadLine(), out userInput))
            {
                Console.WriteLine("Not a valid number!");
            }
            return userInput;
        }

        static Personnel LoginUser ( string username, string password )
        {
            foreach (var personnel in Personnel.personnels)
            {
                if (personnel.username == username
                    && personnel.password == password)
                    return personnel;
            }
            return null;
        }

    }
}
