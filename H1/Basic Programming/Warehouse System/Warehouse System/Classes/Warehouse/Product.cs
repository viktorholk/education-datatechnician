using System;
using System.Collections.Generic;
using System.Text;
using Warehouse_System.Classes.SQL;
using System.Linq;
using Warehouse_System.Classes.Application;
namespace Warehouse_System.Classes.Warehouse
{
    class Product : SQLObject
    {
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int UnitSize { get; set; }
        public ProductCategory Category { get; set; }

        /// <summary>
        /// New bool Saved
        /// This bool is used in the Shelf class, when we want to check whether or not the Product has been saved yet
        /// It is by default protected by the SQLObject inheritance but we need this exception to make it work through the Shelf class
        /// </summary>
        public new bool Saved
        {
            get         { return base.Saved; }
            private set { base.Saved = value; }
        }

        /// <summary>
        /// Product constructor used to create a new instance
        /// 
        /// </summary>
        /// <param name="name">Name of product</param>
        /// <param name="category">Product category </param>
        /// <param name="unitSize">The size of the product</param>
        /// <param name="unitPrice">The price of the product</param>
        public Product(string name, ProductCategory category, int unitSize, int unitPrice)
        {
            this.Name = name;
            this.UnitSize = unitSize;
            this.UnitPrice = unitPrice;
            this.Category = category;
        }
        /// <summary>
        /// Product constructor used to create a new instance from the database
        /// </summary>
        /// <param name="name">Name of product</param>
        /// <param name="category">Product category </param>
        /// <param name="unitSize">The size of the product</param>
        /// <param name="unitPrice">The price of the product</param>
        public Product(string id, string name, string categoryId, string unitSize, string unitPrice, string shelfId)
        {
            this.Id = Convert.ToInt32(id);
            this.Name = name;
            // Set the category from the list of categories where there is a matching category from the categoryId param
            this.Category = ProductCategory.categories.Single(i => i.Id == Convert.ToInt32(categoryId));
            this.UnitSize = Convert.ToInt32(unitSize);
            this.UnitPrice = Convert.ToInt32(unitPrice);
            Saved = true;
        }

        /// <summary>
        /// Save the product to the database
        /// </summary>
        /// <param name="shelfId">The id of the shelf the product is going to be added to</param>
        public void Save(int shelfId)
        {
            // Call the base insert which also will handle the Saved variable
            base.Insert($@" INSERT INTO products
                            (name, category_id, unitSize, unitPrice, shelf_id)
                            VALUES ('{this.Name}',{this.Category.Id},{this.UnitSize}, {this.UnitPrice}, {shelfId})");
            // Write a status
            StatusHandler.Write($"Saved product {this.Name}", StatusHandler.Codes.SUCCESS);
        }
        /// <summary>
        /// Removes the product from the database
        /// </summary>
        public void Remove()
        {
            // Call the base Delete which also will handle the Saved variable

            base.Delete($@" DELETE FROM products
                            WHERE id = {this.Id}");
            // Write a status
            StatusHandler.Write($"Deleted product {this.Name}", StatusHandler.Codes.SUCCESS);
        }
        /// <summary>
        /// Edits the product from the database
        /// </summary>
        public void Edit(string name, ProductCategory category, int unitSize, int unitPrice, Shelf shelf)
        {
            base.Update($@"UPDATE products
                            SET name = '{name}', category_id = {category.Id}, unitSize = {unitSize}, unitPrice = {unitPrice}, shelf_id = {shelf.Id}
                            WHERE id = {this.Id}");
            // Write a status
            StatusHandler.Write($"Edited product {this.Name}", StatusHandler.Codes.SUCCESS);

        }

        public override string ToString()
        {
            return $"{this.Id}, {this.Name}, {this.Category.Name}, {this.UnitSize}";
        }
    }
}
