using System;
using System.Collections.Generic;
using System.Text;
using Warehouse_System.Classes.SQL;

namespace Warehouse_System.Classes.Warehouse
{
    class ProductCategory : SQLObject
    {
        private void LoadCategories() {
            Database.Load(ProductCategory.categories, "SELECT * FROM product_categories");
        }

        public static List<ProductCategory> categories = new List<ProductCategory>();
        public string Name { get; set; }
        public ProductCategory(string name)
        {
            this.Name   = name;
        }

        public ProductCategory(string id, string name)
        {
            this.Id     = Convert.ToInt32(id);
            this.Name   = name;
            base.Saved  = true;
        }
        public void Save()
        {
            base.Insert($@" INSERT INTO product_categories
                            (name)
                            VALUES ('{this.Name}')");
            Console.WriteLine($"Saved category {this.Name}");
            LoadCategories();
            
        }
        public void Remove()
        {
            base.Delete($@" DELETE FROM product_categories
                            WHERE id = {this.Id}");
            Console.WriteLine($"Deleted category {this.Name}");
            LoadCategories();
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
