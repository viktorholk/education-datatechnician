using System;
using System.Collections.Generic;
using GPOpgaver;


namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 5;
            int y = 10;
            Opgaver.Interchange(ref x, ref y);
            Console.WriteLine($"INTERCHANGE: {x}, {y}");

            Console.WriteLine();

            int a = 2;
            int b = 4;
            int c = 6;
            Console.Write($"INTERCHANGETRIPLE: ({a}, {b}, {c}) = ");
            Opgaver.InterchangeTriple(ref a, ref b, ref c);
            Console.WriteLine($"({a}, {b}, {c})");

            Console.WriteLine();

            string[] palindromeChecks = {"racecar", "radar", "rotator", "tenet", "kayak", "ass" };
            foreach (string s in palindromeChecks)
            {
                bool isPalindrome = Opgaver.IsPalindrome(s);
                if (isPalindrome)
                    Console.WriteLine($"{s} is a palindrome!");
                else
                    Console.WriteLine($"{s} is NOT a palindrome!");
            }

            Console.WriteLine();

            int[] integerArray = { 1, 3, 4, 5, 6, 8, 9, 11 };
            Console.WriteLine(string.Join(", ", integerArray));
            Console.WriteLine("LinearSearch:");
            Console.WriteLine($"STEPS FOR 11: {Opgaver.StepsInLinearSearch(11, integerArray)}");
            Console.WriteLine($"STEPS FOR 5: {Opgaver.StepsInLinearSearch(5, integerArray)}");
            Console.WriteLine("BinarySearch:");
            Console.WriteLine($"STEPS FOR 3: {Opgaver.StepsInBinarySearch(integerArray, 0, integerArray.Length - 1, 3)}");
            Console.WriteLine($"STEPS FOR 5: {Opgaver.StepsInBinarySearch(integerArray, 0, integerArray.Length - 1, 5)}");

            Console.WriteLine();

            List<int> sortedList = new List<int> { 1, 2, 3, 5, 6, 8, 9 };
            Console.WriteLine(string.Join(", ", sortedList));
            Console.WriteLine($"4 PLACE AT POS: {Opgaver.InsertSortedList(sortedList, 4)}");
            Console.WriteLine($"7 PLACE AT POS: {Opgaver.InsertSortedList(sortedList, 7)}");


            Console.WriteLine();

            int num = 2;
            int length = 64;
            Console.WriteLine($"NUM:{num}. LENGTH:{length}, MULTIPLES: {string.Join(", ", Opgaver.ArrayOfMultiples(num, length))}");


            Console.WriteLine();

            int power = 2;
            int min = 50;
            int max = 100;
            Console.WriteLine($"POWER:{power}, MIN:{min}, MAX:{max}");
            Console.WriteLine($"COUNT: {Opgaver.PowerRanger(power, min, max)}");

            Console.WriteLine();

            int n = 7;
            Console.WriteLine($"FACTORIAL OF {n} = {Opgaver.Factorial(n)}");

            Console.WriteLine();

            string txt = "Boat000015";
            Console.WriteLine(Opgaver.IncrementString(txt));

            Console.ReadLine();

        }
    }
}
