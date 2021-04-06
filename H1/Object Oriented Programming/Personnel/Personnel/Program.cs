using System;
using System.Collections.Generic;

namespace Personnel
{
    class ClassRoom
    {
        public static List<ClassRoom> classRooms = new List<ClassRoom>();

        public Teacher Teacher;
        public List<Student> students = new List<Student>();

        public string Name;

        public ClassRoom(Teacher teacher, string name)
        {
            this.Teacher = teacher;
            this.Name = name;

            classRooms.Add(this);
        }

        public override string ToString()
        {
            return $"Classroom: {this.Name}";
        }
    }

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

        public Personnel(PersonnelTypes type, string username, string password)
        {
            // Set personnel id
            id = personnels.Count + 1;
            // assign parameters
            this.username = username;
            this.password = password;
            this.type = type;

            // Add to personnel list
            personnels.Add(this);
            Console.WriteLine("yes");
        }

        public Personnel( string username, string password )
        {
            // Set personnel id
            id = personnels.Count + 1;
            this.username = username;
            this.password = password;

            // Add to personnel list
            personnels.Add(this);
        }

        public override string ToString()
        {
            return $"{this.type}[id={this.id}, username={this.username}] ";
        }

        public static Personnel Authenticate( string username, string password )
        {
            foreach (var personnel in personnels)
            {
                if (personnel.username == username
                    && personnel.password == password)
                    return personnel;
            }
            return null;
        }
    }

    class Student : Personnel
    {
        public Student( string username, string password ) : base ( username, password )
        {
            type = Personnel.PersonnelTypes.Student;
        }

        public ClassRoom AssignClassRoom(ClassRoom room)
        {
            room.students.Add(this);
            return room;
        }

    }

    class Teacher : Personnel
    {
        public Teacher(string username, string password) : base(username, password)
        {
            type = Personnel.PersonnelTypes.Teacher;
        }
        
        private ClassRoom GetClassRoom()
        {
            foreach (var room in ClassRoom.classRooms)
            {
                if (room.Teacher == this)
                    return room;
            }
            return null;
        }

        public  void PrintStudents()
        {
            ClassRoom room = GetClassRoom();
            Console.WriteLine($"You are the teacher of classroom: {room.Name}");
            foreach (var student in room.students)
            {
                Console.WriteLine(student);
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create the teacher(s)
            Teacher teacher1 = new Teacher("bob", "123");
            Teacher teacher2 = new Teacher("Kent", "123");
            // Create the classroom(s)
            ClassRoom classA1= new ClassRoom(teacher1, "A1");
            ClassRoom classA2= new ClassRoom(teacher2, "A2");
            // Assign teacher to classroom(s)
            new ClassRoom(teacher2, "A2");
            // Create the student(s)
            new Student("elev", "123").AssignClassRoom(classA1);
            new Student("elev2", "123").AssignClassRoom(classA1);
            new Student("elev3", "123").AssignClassRoom(classA1);
            new Student("elev4", "123").AssignClassRoom(classA1);
            new Student("elev5", "123").AssignClassRoom(classA2);
            new Student("elev6", "123").AssignClassRoom(classA2);
            new Student("elev7", "123").AssignClassRoom(classA2);
            new Student("elev8", "123").AssignClassRoom(classA2);


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

                user = Personnel.Authenticate(username, password);
                if (user != null)
                {
                    break;
                }
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
                            Console.WriteLine("You are in the following classrooms:");
                            foreach (var room in ClassRoom.classRooms)
                            {
                                if (room.students.Contains((Student)user))
                                {
                                    Console.WriteLine(room);
                                }
                            }
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
                            // cast to teacher class
                            var _ = (Teacher)user;
                            _.PrintStudents();

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

    }
}
