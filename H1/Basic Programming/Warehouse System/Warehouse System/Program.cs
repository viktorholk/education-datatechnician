using System;
using System.Collections.Generic;
namespace Warehouse_System
{
    class Program
    {

        static void PrintData()
        {
            foreach (var item in ProductCategory.categories)
            {
                Console.WriteLine(item);
            }

            foreach (var item in Shelf.shelves)
            {
                Console.WriteLine(item);
                foreach (var p in item.products)
                {
                    Console.WriteLine($"    {p}");
                }
            }
        }
        static void Main(string[] args)
        {
            Database.LoadData();
            PrintData();
        }
    }
}
