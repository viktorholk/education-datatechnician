using System;
using System.Collections.Generic;
namespace Warehouse_System
{
    class Program
    {

        static void PrintData()
        {
            Console.WriteLine("CATEGORIES");

            foreach (var item in ProductCategory.categories)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("SHELVES");

            foreach (var item in Shelf.shelves)
            {
                Console.WriteLine(item);
                foreach (var p in item.products)
                {
                    Console.WriteLine($"    {p}");
                }
            }
            Console.WriteLine("CUSTOMERS");
            foreach (var item in Customer.customers)
            {
                Console.WriteLine(item);
                Console.WriteLine("ORDERS");
                foreach (var p in item.orders)
                {
                    Console.WriteLine($"    {p}");
                    Console.WriteLine("ORDERLINES");
                    foreach (var orderline in p.Orderlines)
                    {
                        Console.WriteLine($"        {orderline}");
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            Database.LoadData();
            Customer customer = new Customer("Ass", "Perkins", "perkins street", 3500);
            customer.Save();
            PrintData();
        }
    }
}
