using System;
using System.Collections.Generic;
namespace AbstractsExercise
{

    interface IExperience {
        double Experience { get; set;}
        void AddExperience(double amount);
        void RemoveExperience(double amount);
        double GetExperience();

    }

    abstract class Employee{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public enum  Departments{ 
            Sales,
            Development,
            Marketing,
            Other
        }
        public Departments Department;

        public Employee(string firstName, string lastName, Departments department){
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Department = department;
        }

        public abstract void PayOutMethod();

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}, {this.Department}";
        }
    }

    class Consultant : Employee {
        public float WorkHours { get; set; }

        public Consultant(string firstName, string lastName, Departments department, float workHours) : base(firstName, lastName, department){
            this.WorkHours = workHours;
        }

        public override void PayOutMethod()
        {
            System.Console.WriteLine("PAYOUTMETHOD: Bank Transfer");
        }

        public override string ToString()
        {
            return base.ToString() + $", {this.WorkHours}";
        }
    }

    class Intern : Employee, IExperience {
        public double Experience { get; set; }

        public Intern(string firstName, string lastName, Departments department, double experience) : base(firstName, lastName, department){
            this.Experience = experience;
        }

        public override void PayOutMethod()
        {
            System.Console.WriteLine("PAYOUTMETHOD: Cold Cash");
        }

        // Experience interface
        public void AddExperience(double amount){
            this.Experience += amount;
        }

        public void RemoveExperience(double amount){
            this.Experience -= amount;
        }

        public double GetExperience(){
            return this.Experience;
        }


        public override string ToString()
        {
            return base.ToString() + $", {this.Experience}";
        }
    }

    class Trainee : Employee, IExperience {
        public double Experience{get;set;}
        public int MinimumCakeToEmployeesPerDay { get; set; }

        public Trainee(string firstName, string lastName, Departments department, int minimumCakeToEmployeesPerDay) : base(firstName, lastName, department){
            this.MinimumCakeToEmployeesPerDay = minimumCakeToEmployeesPerDay;
        }

        public override void PayOutMethod()
        {
            System.Console.WriteLine("PAYOUTMETHOD: Personnel goods");
        }

        // Experience interface
        public void AddExperience(double amount){
            this.Experience += amount;
        }

        public void RemoveExperience(double amount){
            this.Experience -= amount;
        }

        public double GetExperience(){
            return this.Experience;
        }

        public override string ToString()
        {
            return base.ToString() + $", {this.MinimumCakeToEmployeesPerDay}";
        }
    }

    class Person : IComparable {
        public string Name { get; set;}
        public int Age {get;set;}

        public enum Genders {
            Male,
            Female,
            Other
        }
        public Genders Gender { get; set; }

        public Person(string name, int age, Genders gender){
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
        }

        public int CompareTo(object obj){
            Person other = (Person) obj;

            return this.Age.CompareTo(other.Age);
        }

        public override string ToString()
        {
            return $"{this.Name, -30} {this.Age, -3} {this.Gender}";
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Consultant consultant = new Consultant("Bob", "Bobbers", Employee.Departments.Development, 15);

            System.Console.WriteLine(consultant);
            consultant.PayOutMethod();

            System.Console.WriteLine();

            Intern intern = new Intern("Henrik", "Henriksen", Employee.Departments.Sales, 10.5);

            System.Console.WriteLine(intern);
            intern.PayOutMethod();
            System.Console.WriteLine($"Experience: {intern.GetExperience()}");

            System.Console.WriteLine();

            Trainee trainee = new Trainee("Gerda", "Gerd", Employee.Departments.Other, 3);

            System.Console.WriteLine(trainee);
            trainee.PayOutMethod();
            // Add experience
            trainee.AddExperience(15.9);
            System.Console.WriteLine($"Experience: {trainee.GetExperience()}");

            System.Console.WriteLine();

            System.Console.WriteLine("IComparable:");
            Person[] people = new Person[10]{
                new Person("John", 5, Person.Genders.Male),
                new Person("Victor", 15, Person.Genders.Male),
                new Person("Julie", 14, Person.Genders.Female),
                new Person("Bob", 20, Person.Genders.Male),
                new Person("X Æ A-12", 1, Person.Genders.Male),
                new Person("Harboe Pilsner", 16, Person.Genders.Other),
                new Person("Cocio Airpods", 3, Person.Genders.Other),
                new Person("Car insurance", 21, Person.Genders.Other),
                new Person("Arla Koldskål", 20, Person.Genders.Other),
                new Person("Kebab Durum med ekstra drelle", 18, Person.Genders.Other)
            };
    
            Array.Sort(people);

            foreach (var person in people)
            {
                System.Console.WriteLine(person);
            }
        }
    }
}
