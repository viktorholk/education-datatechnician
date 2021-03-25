using System;

namespace SortingAlgorithm
{
    public class Sort
    {


        public void QuickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                // Pivot
                int pi = Partition(arr, low, high);

                QuickSort(arr, low, pi - 1);
                QuickSort(arr, pi + 1, high);
            }
        }

        private int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];

            int i = low - 1;

            for (int j = low; j <= high - 1; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }
            Swap(arr, i + 1, high);
            return (i + 1);
        }

        private void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }


    public class Program
    {
        static void Main(string[] args)
        {
            Sort sort = new Sort();
            // Integer sort
            int[] iArr = { 16, 7, 3, 9, 12, 4,  22 };
            int n = iArr.Length;

            PrintIntArray(iArr);
            sort.QuickSort(iArr, 0, n - 1);
            PrintIntArray(iArr);

            
        }

        static void PrintIntArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i]}{ (i != (arr.Length - 1) ? ", " : "")} ");
            }
            Console.Write("\n");
        }




    }
}
