using System;

namespace Overloading
{
    class MyClass {

        public int Add(int i, int j){
            return i + j;
        }

        public int Add(int[] numbers){
            int result = 0;
            foreach (int number in numbers){
                result += number;
            }
            return result;
        }
    }

    class Person {
        public string FullName { get; set; }
        public Person(string firstName, string lastName){
            this.FullName = firstName + " " + lastName;
        }

        public Person(string fullName){
            this.FullName = fullName;
        }

        public override string ToString()
        {
            return this.FullName;
        }
    }

    class Math {
        public int Add(int i, int j){
            return i + j;
        }
        public float Add(float i, float j){
            return (float)i + (float)j;
        }
        public int Add(string i, string j){
            return Convert.ToInt32(i) + Convert.ToInt32(j);
        }
        public int Minus(int i, int j){
            return i - j;
        }
        public float Minus(float i, float j){
            return i - j;
        }
        public int Minus(string i, string j){
            return Convert.ToInt32(i) - Convert.ToInt32(j);
        }
        public int Divide(int i, int j){
            return i / j;
        }
        public float Divide(float i, float j){
            return i / j;
        }
        public int Divide(string i, string j){
            return Convert.ToInt32(i) / Convert.ToInt32(j);
        }
        public int Multiply(int i, int j){
            return i * j;
        }
        public float Multiply(float i, float j){
            return i * j;
        }
        public int Multiply(string i, string j){
            return Convert.ToInt32(i) * Convert.ToInt32(j);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            MyClass i = new MyClass();
            Console.WriteLine(i.Add(5, 5));
            Console.WriteLine(i.Add(new int[]{1,2,3,4,5,6,7,8,9}));

            Person person1 = new Person("Bob", "Bobbersen");
            Person person2 = new Person("Henrik Henriksen");

            System.Console.WriteLine(person1);
            System.Console.WriteLine(person2);

            System.Console.WriteLine();

            Math MathObj = new Math();
            System.Console.WriteLine("ADD");
            System.Console.WriteLine(MathObj.Add(10, 5));
            System.Console.WriteLine(MathObj.Add((float)6.5, (float)7.5));
            System.Console.WriteLine(MathObj.Add("8", "16"));
            System.Console.WriteLine();
            System.Console.WriteLine("MINUS");
            System.Console.WriteLine(MathObj.Minus(10, 5));
            System.Console.WriteLine(MathObj.Minus((float)6.5, (float)7.5));
            System.Console.WriteLine(MathObj.Minus("8", "16"));
            System.Console.WriteLine();
            System.Console.WriteLine("MULTIPLY");
            System.Console.WriteLine(MathObj.Multiply(10, 5));
            System.Console.WriteLine(MathObj.Multiply((float)6.5, (float)7.5));
            System.Console.WriteLine(MathObj.Multiply("8", "16"));
            System.Console.WriteLine();
            System.Console.WriteLine("DIVISION");
            System.Console.WriteLine(MathObj.Divide(10, 5));
            System.Console.WriteLine(MathObj.Divide((float)6.5, (float)7.5));
            System.Console.WriteLine(MathObj.Divide("32", "16"));
        }
    }
}
