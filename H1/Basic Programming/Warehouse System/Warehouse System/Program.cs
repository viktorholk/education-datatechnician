using System;
using System.Collections.Generic;

namespace Warehouse_System
{
    class Program
    {
        static void Main(string[] args)
        {
            Warehouse.InitializeDatabase();



            //Shelf _ = new Shelf("test2");

            //Warehouse.AddShelf(_);

            Product product = new Product("USB", Product.Categories.Electronics, 25);

            Warehouse.AddProduct(product, "C");
        }
    }
}
