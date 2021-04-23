using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse_System
{
    class ProductCategory
    {
        private bool Saved = false;

        public static List<ProductCategory> categories;

        public static void LoadCategories()
        {
            Records records = Database.GetRecords("SELECT * FROM product_categories");
            // Initialize the list
            categories = new List<ProductCategory>();
            if (records.Count > 0)
            {
                foreach (var record in records)
                {
                    ProductCategory productCategory = new ProductCategory(Convert.ToInt32(record["id"]), record["name"]);
                    categories.Add(productCategory);
                }
            }
            else Console.WriteLine("Cannot load product categories since there is no records");
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductCategory(string name)
        {
            this.Name = name;
        }

        public ProductCategory(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            Saved = true;
        }

        public void Save()
        {
            if (!Saved)
            {
                // Add it to the database
                Database.Execute(@$"
                    INSERT INTO product_categories 
                    (name) 
                    VALUES ('{Name}')
                ");
                Console.WriteLine($"Saved product category {this.Name}");
                this.Saved = true;
                // Load products from the database into the list
                LoadCategories();

            }
            else Console.WriteLine($"{this.Name} category has already been saved");
        }

        public void Remove()
        {
            if (Saved)
            {
                // Remove it to the database
                Database.Execute(@$"
                    DELETE FROM product_categories
                    WHERE id = {Id}
                ");
                Console.WriteLine($"Removed product category {this.Name}");
                LoadCategories();

            }
            else Console.WriteLine($"{this.Name} category has to be saved in the database before removal");
        }



        public override string ToString()
        {
            return $"{this.Id}, {this.Name}";
        }
    }
}
