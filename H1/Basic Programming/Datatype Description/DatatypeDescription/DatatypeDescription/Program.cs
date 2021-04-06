using System;
using System.Collections.Generic;

namespace DatatypeDescription
{
    class Person
    {
        string Name;
        int Age;

        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            // Variables with different datatypes
            short   dShort = short.MaxValue;
            int     dInt = int.MaxValue;
            long    dLong = long.MaxValue;
            float   dFloat = float.MaxValue;
            double  dDouble = double.MaxValue;
            char    dChar = char.MaxValue;

            string[] dStringArray = {"A","B" };

            Person dPerson = new Person("Viktor", 19);
            List<Person> dPeople = new List<Person>() {
                dPerson
            };


            Console.WriteLine("{0,-20}{1,-25}{2,-16}", "DATATYPE", "MAX VALUE", "HEX");
            Console.WriteLine("{0,-20}{1,-25}{2,-20}", GetType(dShort), dShort, String.Format("{0:X}", dShort));
            Console.WriteLine("{0,-20}{1,-25}{2,-20}", GetType(dInt), dInt, String.Format("{0:X}", dInt));
            Console.WriteLine("{0,-20}{1,-25}{2,-20}", GetType(dLong), dLong, String.Format("{0:X}", dLong));
            Console.WriteLine("{0,-20}{1,-25}{2,-20}", GetType(dFloat), dFloat, "");
            Console.WriteLine("{0,-20}{1,-25}{2,-20}", GetType(dDouble), dDouble, "");
            Console.WriteLine("{0,-20}{1,-25}{2,-20}", GetType(dChar), dChar, "");
            Console.WriteLine("{0,-20}{1,-25}{2,-20}", GetType(dStringArray), dStringArray, "");
            Console.WriteLine("{0,-20}{1,-25}{2,-20}", GetType(dPerson), dPerson, "");
            Console.WriteLine("{0,-20}{1,-25}{2,-20}", GetType(dPeople), dPeople, "");
        }


        static Type GetType<T>(T i)
        {
            return typeof(T);
        }



    }
}
