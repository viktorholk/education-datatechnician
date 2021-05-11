using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse_System.Classes.SQL;
using Warehouse_System.Classes.Application;
namespace Warehouse_System.Classes.Warehouse
{
    /// <summary>
    /// Shelf class
    /// This class holds the fields for the Shelf information from the database
    /// you can create a new shelf object and add it to the database and assign products to it
    /// </summary>
    class Shelf : SQLObject
    {
        /// <summary>
        /// Gets an uniquie identifer from the amount of shelves that there is, and increments the char for the new shelf
        /// </summary>
        /// <returns>Char identifier</returns>
        private char CreateIdentifer()
        {
            if (shelves.Count > 0)
            {
                char previousIdentifier = shelves.Last().Identifier;
                return (char)(Convert.ToInt16(previousIdentifier) + 1);

            }
            // No shelves has been set yet. Return the start of the shelves with the identifier of A
            return 'A';
        }
        private void LoadShelves()
        {
            // Get all products associated to the shelf and add it to he list
            Database.Load(Shelf.shelves, $"SELECT * FROM shelves");
        }
        private void LoadProducts()
        {
            // Get all products associated to the shelf and add it to he list
            Database.Load(this.products, $"SELECT * FROM products WHERE shelf_id = {this.Id}");
        }
        public static List<Shelf> shelves = new List<Shelf>();

        public char Identifier { get; set; }
        public string Description{ get; set; }
        public int MaxUnitStorageSize { get; set; }
        public List<Product> products = new List<Product>();

        public Shelf(string description, int maxUnitStorageSize)
        {
            this.Identifier         = CreateIdentifer();
            this.Description        = description;
            this.MaxUnitStorageSize = maxUnitStorageSize;
        }
        public Shelf(string id, string identifier, string description, string maxUnitStorageSize)
        {
            this.Id                 = Convert.ToInt32(id);
            this.Identifier         = Convert.ToChar(identifier);
            this.Description        = description;
            this.MaxUnitStorageSize = Convert.ToInt32(maxUnitStorageSize);
            this.Saved = true;
            LoadProducts();
        }

        public  void Save()
        {
            base.Insert($@" INSERT INTO shelves
                            (identifier, description, maxUnitStorageSize)
                            VALUES ('{this.Identifier}','{this.Description}',{this.MaxUnitStorageSize})");

            StatusHandler.Write($"Saved shelf {this.Identifier}", StatusHandler.Codes.SUCCESS);
            LoadShelves();

        }
        public  void Remove()
        {
            // Delete all products first
            foreach (var product in products)
            {
                Database.Execute($"DELETE FROM products WHERE id = {product.Id}");
            }

            base.Delete($@" DELETE FROM shelves
                            WHERE id = {this.Id}");
            StatusHandler.Write($"Deleted shelf {this.Identifier}", StatusHandler.Codes.SUCCESS);
            LoadShelves();
        }
        public void Edit(string description, int maxUnitStorageSize)
        {
            base.Update($@" UPDATE shelves
                            SET description = '{description}', maxUnitStorageSize = {maxUnitStorageSize}
                            WHERE id = {this.Id}");
            StatusHandler.Write($"Edited shelf {this.Identifier}", StatusHandler.Codes.SUCCESS);
            LoadShelves();
        }

        public void AddProduct(Product product)
        {
            if (!product.Saved)
            {
                product.Save(this.Id);
                LoadProducts();
            }
        }
        public void EditProduct(Product product, string name, ProductCategory category, int unitSize, int unitPrice, Shelf shelf)
        {
            if (this.products.Contains(product))
            {
                product.Edit(name, category, unitSize, unitPrice, shelf);
                LoadProducts();

            }
            else
                StatusHandler.Write($"{product}, does not exist in the shelf' product list", StatusHandler.Codes.ERROR);

        }
        public void RemoveProduct(Product product)
        {
            if (this.products.Contains(product))
            {
                product.Remove();
                LoadProducts();
                StatusHandler.Write($"Removed product {product.Name}", StatusHandler.Codes.SUCCESS);

            }
            else
                StatusHandler.Write($"{product}, does not exist in the shelf' product list", StatusHandler.Codes.ERROR);

        }
        public override string ToString()
        {
            return $"{this.Id}, {this.Identifier}, {this.Description}, {this.MaxUnitStorageSize}";
        }
    }
}
