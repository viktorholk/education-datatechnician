using System;
using System.Collections.Generic;
namespace MapExercise
{
    class Program
    {
        static int[] Map (int[] array, Func<int, int> del){
            for (int i = 0; i < array.Length; i++){
                array[i] = del(array[i]);
            }
            return array;
        }

        static int[] Filter(int[] array, Func<int, bool> del){
            List<int> filteredList = new List<int>();

            for (int i = 0; i < array.Length; i++)
            {
                // Check if the delegate is true
                if (del(array[i]))
                    filteredList.Add(array[i]);
            }
            return filteredList.ToArray();
        }

        static void Main(string[] args)
        {
            System.Console.WriteLine("Mapping");
            // Declare the array and the delegate
            int[]           array = new int[]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Func<int,int>   del = x => (int)Math.Pow(2, x);
            // Map the array
            // array is a reference type so we can just pass it
            Map(array, del);

            for (int i = 0; i < array.Length; i++)
            {
                System.Console.WriteLine(array[i]);
            }
            System.Console.WriteLine();
            System.Console.WriteLine("Filter");
            int[] filterArray = Filter(new int[]{ 1, 2, 3, 4, 5}, x=> x > 3);

            for (int i = 0; i < filterArray.Length; i++)
            {
                System.Console.WriteLine(filterArray[i]);
            }
        }
    }
}
