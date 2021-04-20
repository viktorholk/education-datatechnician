using System;
using System.Collections.Generic;

namespace Warehouse_System
{
    class Program
    {
        static List<Product> products = new List<Product>();
        static void Main(string[] args)
        {
            var records = Database.GetRecords("SELECT * FROM products");
            foreach (var row in records)
            {
                foreach (KeyValuePair<string,string> data in row)
                {
                    products.Add(new Product(data["]))
                }
            }
        }


    }
}
