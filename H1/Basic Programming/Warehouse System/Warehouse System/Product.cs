using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse_System
{
    class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitSize { get; set; }

        public ProductCategory Category;
        public bool Saved = false;
        public Product(string name, ProductCategory category, int unitSize)
        {
            this.Name = name;
            this.UnitSize = unitSize;
            this.Category = category;

        }
        public Product(int id, string name, int categoryId, int unitSize)
        {
            this.Id = id;
            this.Name = name;
            this.UnitSize = unitSize;
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
