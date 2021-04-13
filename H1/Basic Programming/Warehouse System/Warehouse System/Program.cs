using System;
using System.Collections.Generic;

namespace Warehouse_System
{
    class Program
    {
        static void AddSampleDatabaseRecords()
        {

            // Shelves
            Warehouse.AddShelf(new Shelf("Electronics Shelf", 250));
            Warehouse.AddShelf(new Shelf("Electronics Shelf 2", 75));
            Warehouse.AddShelf(new Shelf("Agriculture Shelf", 750));
            Warehouse.AddShelf(new Shelf("Agriculture Shelf 2", 500));
            Warehouse.AddShelf(new Shelf("Convenience Shelf", 200));
            Warehouse.AddShelf(new Shelf("Convenience Shelf 2", 150));
            // Products
            Warehouse.AddProduct(new Product("Lenovo Laptop 128GB SSD", Product.Categories.Electronics, 200), "A");
            Warehouse.AddProduct(new Product("Mobile charger", Product.Categories.Electronics, 25), "A");
            Warehouse.AddProduct(new Product("USB Mouse", Product.Categories.Electronics, 25), "A");
            Warehouse.AddProduct(new Product("USB 128GB", Product.Categories.Electronics, 2), "B");
            Warehouse.AddProduct(new Product("USB 128GB", Product.Categories.Electronics, 2), "B");
            Warehouse.AddProduct(new Product("USB 128GB", Product.Categories.Electronics, 2), "B");
            Warehouse.AddProduct(new Product("USB 64GB", Product.Categories.Electronics, 2), "B");
            Warehouse.AddProduct(new Product("USB 64GB", Product.Categories.Electronics, 2), "B");
            Warehouse.AddProduct(new Product("USB 64GB", Product.Categories.Electronics, 2), "B");
            Warehouse.AddProduct(new Product("USB 64GB", Product.Categories.Electronics, 2), "B");
            Warehouse.AddProduct(new Product("USB 32GB", Product.Categories.Electronics, 2), "B");
            Warehouse.AddProduct(new Product("USB 32GB", Product.Categories.Electronics, 2), "B");
            Warehouse.AddProduct(new Product("USB 32GB", Product.Categories.Electronics, 2), "B");
            Warehouse.AddProduct(new Product("USB 32GB", Product.Categories.Electronics, 2), "B");
            Warehouse.AddProduct(new Product("USB 32GB", Product.Categories.Electronics, 2), "B");
            Warehouse.AddProduct(new Product("USB 32GB", Product.Categories.Electronics, 2), "B");
            Warehouse.AddProduct(new Product("USB 32GB", Product.Categories.Electronics, 2), "B");


            Warehouse.AddProduct(new Product("Cultivator 2000", Product.Categories.Agriculture, 75), "C");
            Warehouse.AddProduct(new Product("Cultivator 2000", Product.Categories.Agriculture, 75), "C");
            Warehouse.AddProduct(new Product("Rotovator xb-12", Product.Categories.Agriculture, 250), "C");


            Warehouse.AddProduct(new Product("Cabbage", Product.Categories.Convenience, 10), "E");
            Warehouse.AddProduct(new Product("Cabbage", Product.Categories.Convenience, 10), "E");
            Warehouse.AddProduct(new Product("Cabbage", Product.Categories.Convenience, 10), "E");
            Warehouse.AddProduct(new Product("Cabbage", Product.Categories.Convenience, 10), "E");
            Warehouse.AddProduct(new Product("Cabbage", Product.Categories.Convenience, 10), "E");
            Warehouse.AddProduct(new Product("Cabbage", Product.Categories.Convenience, 10), "E");
            Warehouse.AddProduct(new Product("Cabbage", Product.Categories.Convenience, 10), "E");
            Warehouse.AddProduct(new Product("Fruit loops", Product.Categories.Convenience, 1), "E");
            Warehouse.AddProduct(new Product("Fruit loops", Product.Categories.Convenience, 1), "E");
            Warehouse.AddProduct(new Product("Fruit loops", Product.Categories.Convenience, 1), "E");
            Warehouse.AddProduct(new Product("Fruit loops", Product.Categories.Convenience, 1), "E");
            Warehouse.AddProduct(new Product("Bio Applepack(24)", Product.Categories.Convenience, 40), "E");
            Warehouse.AddProduct(new Product("Bio Applepack(24)", Product.Categories.Convenience, 40), "E");
        }
        static void Main(string[] args)
        {
            Warehouse.InitializeDatabase();


            Warehouse.PrettyPrintRecords(Warehouse.GetProducts());
            Warehouse.PrettyPrintRecords(Warehouse.GetShelves());
        }


    }
}
