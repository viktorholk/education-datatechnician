using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse_System
{
    class Product
    {
        public static List<Product> products;
        public static void LoadProducts()
        {
            Records records = Database.GetRecords("SELECT * FROM products");
            // Initialize the list
            products = new List<Product>();
            if (records.Count > 0)
            {
                foreach (var record in records)
                {
                    Product product = new Product(Convert.ToInt32(record["id"]), record["name"], Convert.ToInt32(record["category_id"]), Convert.ToInt32(record["unitSize"]), Convert.ToInt32(record["unitPrice"]));
                    products.Add(product);
                }
            }
            else Console.WriteLine("Cannot load shelves since there is no records");
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int UnitSize { get; set; }

        public ProductCategory Category;
        public bool Saved = false;
        public Product(string name, ProductCategory category, int unitSize, int unitPrice )
        {
            this.Name = name;
            this.UnitSize = unitSize;
            this.UnitPrice = unitPrice;
            this.Category = category;

        }
        public Product(int id, string name, int categoryId, int unitSize, int unitPrice)
        {
            this.Id = id;
            this.Name = name;
            this.UnitSize = unitSize;
            this.UnitPrice = unitPrice;
            this.Category = ProductCategory.categories.Single(c => c.Id == categoryId);
            this.UnitSize = unitSize;
            Saved = true;
        }
        public void Remove()
        {
            if (Saved)
            {
                // Remove it to the database
                Database.Execute(@$"
                    DELETE FROM products
                    WHERE id = {Id}
                ");
                Console.WriteLine($"Removed product {this.Name}");
            }
            else Console.WriteLine($"{this.Name} has to be saved in the database before removal");
        }

        public override string ToString()
        {
            return $"{this.Id}, {this.Name}, {this.Category.Name}, {this.UnitSize}";
        }
    }
}
