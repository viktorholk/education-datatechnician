using System;
using System.Collections.Generic;
using System.Text;
using Warehouse_System.Classes.SQL;

namespace Warehouse_System.Classes.Warehouse
{
    class ProductCategory : SQLObject
    {
        /// <summary>
        /// LoadCategories method
        /// This is used when we make changes to the database and want to keep the application up to the correct amount of categories
        /// </summary>
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
        /// <summary>
        /// Save the Product Category into the database
        /// </summary>
        public void Save()
        {
            // Call the parent insert method which also handles the Saved variable
            base.Insert($@" INSERT INTO product_categories
                            (name)
                            VALUES ('{this.Name}')");
            LoadCategories();
            
        }
        /// <summary>
        /// Removes the product from the database
        /// </summary>
        public void Remove()
        {
            base.Delete($@" DELETE FROM product_categories
                            WHERE id = {this.Id}");
            LoadCategories();
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
