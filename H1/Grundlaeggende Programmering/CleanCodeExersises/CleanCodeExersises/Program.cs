using System;
using System.Linq;

/*
 * This project is a collection of exersises that will teach students how to write clean and DRY code.
 * These exersises are stutable for beginer to experienced programmers.
 */
namespace CleanCodeExersises
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();

            Console.WriteLine("IsLegalDrikingAge:");
            int age = 25;
            Console.WriteLine($"Dirty: {program.IsLegalDrikingAgeDirty(age)}");
            Console.WriteLine($"Clean: {program.IsLegalDrikingAgeClean(age)}");
            Console.WriteLine();

            Console.WriteLine("IsLoggedIn:");
            bool isNotloggedIn = true;
            Console.WriteLine($"Dirty: {program.IsLoggedInDirty(isNotloggedIn)}");
            Console.WriteLine($"Clean: {program.IsLoggedInClean(isNotloggedIn)}");
            Console.WriteLine();

            Console.WriteLine("Eligible:");
            Employee employee = new Employee(65, 12, true);
            Console.WriteLine($"Dirty: {program.eligibleDirty(employee)}");
            Console.WriteLine($"Clean: {program.eligibleClean(employee)}");
            Console.WriteLine();

            bool isPreordered = true;
            Console.WriteLine($"Dirty: {program.GetPriceDirty(isPreordered)}");
            Console.WriteLine($"Clean: {program.GetPriceClean(isPreordered)}");
            Console.WriteLine();


        }
        /*
         * Magic Number exercise
         */
        public bool IsLegalDrikingAgeDirty(int age)
        {
            if (age > 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /*
         * Solution to Magic Number exercise
         * Use consts to describe the numbers you are using to your programs.
         */
        public bool IsLegalDrikingAgeClean(int age)
        {
            return age > 21;
        }
        /*
         * Be positive exersise
         */
        public bool IsLoggedInDirty(bool isNotloggedIn)
        {
            if (!isNotloggedIn == true)
            {
                System.Console.WriteLine("Succesfully logged in.");
                return true;
            }
            else
            {
                System.Console.WriteLine("Failed to logged in.");
                return false;
            }
        }
        /*
         * Solution to Be positive exersise
         */
        public bool IsLoggedInClean(bool isNotloggedIn)
        {
            if (!isNotloggedIn)
            {
                Console.WriteLine("Succesfully logged in.");
                return true;
            }
            Console.WriteLine("Failed to logged in.");
            return false;
        }
        /*
         * Intermediate exercise
         * What is the question answering?
         */
        public bool eligibleDirty(Employee employee)
        {
            if (employee.Age > 55
                && employee.YearsEmployed > 10
                && employee.IsRetired == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /*
         * Solution to ohm Intermediate exercise
         * An intermediate Variable says a lot more them a long expression
         */
        public bool eligibleClean(Employee employee)
        {
            return (employee.Age > 55
                && employee.YearsEmployed > 10
                && employee.IsRetired) ? true : false;
        }
        
        /*
         * Ternary exersise
         */
        public double GetPriceDirty(bool isPreordered)
        {
            if (isPreordered == true)
            {
                return 200.00;
            } else
            {
                return 350.00;
            }
        }
        /*
         * Solution to Ternary exersise
         * Use a oneline Ternary.
         */
        public double GetPriceClean(bool isPreordered)
        {
            return isPreordered ? 200.00 : 350.00;
        }
        /*
         * Strong type exersise - not done
         */

    }
}


