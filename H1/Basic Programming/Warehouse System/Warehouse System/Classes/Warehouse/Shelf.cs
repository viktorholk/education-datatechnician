using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse_System
{
    class Shelf : SQLObject
    {
        private bool Saved = false;
        private char GetIdentifier()
        {
            if (shelves.Count > 0)
            {
                char previousIdentifier = shelves.Last().Identifier;
                return (char)(Convert.ToInt16(previousIdentifier) + 1);

            }
            // No shelves has been set yet. Return the start of the shelves with the identifier of A
            return 'A';
        }
        private void GetProducts()
        {
            Records records = Database.GetRecords(@$"SELECT * FROM products
                                                    WHERE shelf_id = {this.Id}");
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
        }

        public static List<Shelf> shelves;
        public static void LoadShelves()
        {
            Records records = Database.GetRecords("SELECT * FROM shelves");
            // Initialize the list
            shelves = new List<Shelf>();
            if (records.Count > 0)
            {
                foreach (var record in records)
                {
                    Shelf shelf = new Shelf(Convert.ToInt32(record["id"]), Convert.ToChar(record["identifier"]), record["description"], Convert.ToInt32(record["maxUnitStorageSize"]));
                    shelves.Add(shelf);
                }
            }
            else Console.WriteLine("Cannot load shelves since there is no records");
        }
        public char Identifier { get; set; }
        public string Description { get; set; }
        public int MaxUnitStorageSize { get; set; }
        public List<Product> products;

        public Shelf(string description, int maxUnitStorageSize)
        {
            this.Identifier = GetIdentifier();
            this.Description = description;
            this.MaxUnitStorageSize = maxUnitStorageSize;
        }

        public Shelf(int id, char identifier, string description, int maxUnitStorageSize)
        {
            this.Id = id;
            this.Identifier = identifier;
            this.Description = description;
            this.MaxUnitStorageSize = maxUnitStorageSize;
            // Get Products
            GetProducts();
            Saved = true;
        }

        public override void Save()
        {
            if (!Saved)
            {
                // Add it to the database
                Database.Execute($@"
                INSERT INTO shelves
                (identifier, description, maxUnitStorageSize)
                VALUES ('{Identifier}', '{Description}', {MaxUnitStorageSize})
                ");
                Console.WriteLine($"Saved shelf {this.Identifier}");
                this.Saved = true;
                // Load products from the database into the list
                LoadShelves();

            }
            else Console.WriteLine($"shelf {this.Identifier} has already been saved");
        }

        public override void Remove()
        {
            if (Saved)
            {
                // Remove it to the database
                Database.Execute(@$"
                    DELETE FROM shelves
                    WHERE id = {Id}
                ");
                Console.WriteLine($"Removed shelf {this.Identifier}");
                LoadShelves();

            }
            else Console.WriteLine($"{this.Identifier} has to be saved in the database before removal");
        }
        public void AddProduct(Product product)
        {
            if (!product.Saved)
            {
                // Add it to the database
                Database.Execute(@$"
                    INSERT INTO products 
                    (name, category_id, unitSize, unitPrice, shelf_id) 
                    VALUES ('{product.Name}', {product.Category.Id}, {product.UnitSize},{product.UnitPrice}, {this.Id})
                ");
                Console.WriteLine($"Saved product {product.Name}");
                product.Saved = true;
                // Load products from the database into the list
                GetProducts();
            }
            else Console.WriteLine($"{product.Name} is already saved");
        }
        public override string ToString()
        {
            return $"{this.Id}, {this.Identifier}, {this.Description}, {this.MaxUnitStorageSize}";
        }
    }
}
