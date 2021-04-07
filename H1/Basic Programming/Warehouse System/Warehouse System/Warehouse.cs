using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Warehouse_System
{
    class Shelf
    {
        public char Identifier;
        public string Description;
        public int MaxUnitStorageSize;

        // Create the shelf identifier
        // The shelf identifier is a char from A-Z, each new shelf created with increment this
        // If there is no shelves set the Shelf identifier to A
        private char GetIdentifier()
        {
            // Get all the shelves in the db
            Result shelves = SQLite.GetResults("SELECT * FROM shelves");

            // If there is mulitple shelves increment the char to the next
            if (shelves.Count > 0)
            {

                char previousIdentifier = char.Parse(shelves.Last()["identifier"]);
                return (char)(Convert.ToInt16(previousIdentifier) + 1);

            }
            // No shelves has been set yet. Return the start of the shelves with the identifier of A
            return 'A';
        }

        public Shelf(string description = "No description has been set", int unitStorageSize = 100)
        {
            this.MaxUnitStorageSize = unitStorageSize;
            this.Description = description;
            this.Identifier = GetIdentifier();
        }
    }

    class Warehouse
    {
        public static void InitializeDatabase()
        {
            SQLite.Execute(@"CREATE TABLE IF NOT EXISTS 'shelves' (
                'id'    INTEGER NOT NULL,
                'identifier'    TEXT NOT NULL UNIQUE,
                'description'   TEXT,
                'maxUnitStorageSize'    INTEGER NOT NULL DEFAULT 100,
                PRIMARY KEY('id' AUTOINCREMENT)
            )");

            SQLite.Execute(@"CREATE TABLE IF NOT EXISTS 'product_categories' (
                'id'    INTEGER NOT NULL,
                'name'  TEXT NOT NULL UNIQUE,
                PRIMARY KEY('id' AUTOINCREMENT)
            );");

            SQLite.Execute(@"CREATE TABLE IF NOT EXISTS 'products' (

                'id'    INTEGER NOT NULL,
                'name'  TEXT NOT NULL,
                'category'  INTEGER NOT NULL,
                'shelfId'   INTEGER NOT NULL,
                FOREIGN KEY('shelfId') REFERENCES 'shelves'('id'),
                FOREIGN KEY('category') REFERENCES 'product_categories'('id'),
                PRIMARY KEY('id' AUTOINCREMENT)
            );");

            SQLite.Execute(@"CREATE TABLE IF NOT EXISTS 'customers' (

                'id'    INTEGER NOT NULL,
                'firstName' TEXT NOT NULL,
                'lastName'  TEXT,
                'zipCode'   TEXT,
                PRIMARY KEY('id' AUTOINCREMENT)
            );");

            // Insert the valid product categories to the db
            foreach (string name in Enum.GetNames(typeof(Product.Categories)))
            {
                // We are taking this in a try and catch approach,
                // since the category names in the db has the constraint UNIQUE, so when we try to run it twice it will throw error
                // We can first check if the category already exists in the db, but if the amount of categories increases it will take performance.
                try
                {
                    SQLite.Execute($"INSERT INTO product_categories (name) VALUES ('{name}')");
                } catch { }
            }
        }

        public static void AddProduct(Product product, string shelfIdentifier)
        {
            // Get shelf id so it creates the reference in the db
            Result shelfResults = SQLite.GetResults($"SELECT id from shelves where identifier = '{shelfIdentifier}'");
            if (shelfResults.Count > 0)
            {
                int shelfId = Convert.ToInt32(shelfResults[0]["id"]);
                SQLite.Execute($"INSERT INTO products (name, category, shelfId) VALUES ('{product.Name}', '{(int)product.Category}', {shelfId})");
            }
            else Console.WriteLine("Can not place shelf on a shelf that doesn't exist!");
        }
        public static void AddShelf(Shelf shelf)
        {
            SQLite.Execute($"INSERT INTO shelves (identifier, description) VALUES ('{shelf.Identifier}', '{shelf.Description}')");


        }
    }
}
