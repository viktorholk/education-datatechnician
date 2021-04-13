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
            Records shelves = SQLite.GetRecords("SELECT * FROM shelves");

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
            // Shelves Table
            SQLite.Execute(@"CREATE TABLE IF NOT EXISTS 'shelves' (
                'id'                    INTEGER NOT NULL,
                'identifier'            TEXT NOT NULL UNIQUE,
                'description'           TEXT,
                'maxUnitStorageSize'    INTEGER NOT NULL DEFAULT 100,
                PRIMARY KEY('id' AUTOINCREMENT)
            )");
            // Product categories table
            SQLite.Execute(@"CREATE TABLE IF NOT EXISTS 'product_categories' (
                'id'    INTEGER NOT NULL,
                'name'  TEXT NOT NULL UNIQUE,
                PRIMARY KEY('id' AUTOINCREMENT)
            );");
            // Product table
            SQLite.Execute(@"CREATE TABLE IF NOT EXISTS 'products' (

                'id'        INTEGER NOT NULL,
                'name'      TEXT NOT NULL,
                'category'  INTEGER NOT NULL,
                'unitSize'  INTEGER NOT NULL DEFAULT 1,
                'shelfId'   INTEGER NOT NULL,
                FOREIGN KEY('shelfId') REFERENCES 'shelves'('id') ON DELETE CASCADE,
                FOREIGN KEY('category') REFERENCES 'product_categories'('id') ON DELETE CASCADE,
                PRIMARY KEY('id' AUTOINCREMENT)
            );");
            // Customer table
            SQLite.Execute(@"CREATE TABLE IF NOT EXISTS 'customers' (

                'id'    INTEGER NOT NULL,
                'firstName' TEXT NOT NULL,
                'lastName'  TEXT,
                'zipCode'   TEXT,
                PRIMARY KEY('id' AUTOINCREMENT)
            );");
            // Orders table
            SQLite.Execute(@"CREATE TABLE 'orders' (

                'id'    INTEGER NOT NULL,
                'customer_id'   INTEGER NOT NULL,
                'date'  INTEGER NOT NULL,
                FOREIGN KEY('customer_id') REFERENCES 'customers'('id') ON DELETE CASCADE,
                PRIMARY KEY('id' AUTOINCREMENT)
            ); ");
            // Order Lines table
            SQLite.Execute(@"REATE TABLE 'orderlines'(
                'id'    INTEGER NOT NULL,
                'product_id'    INTEGER NOT NULL,
                FOREIGN KEY('product_id') REFERENCES 'products'('id') ON DELETE CASCADE,
                PRIMARY KEY('id' AUTOINCREMENT)
            ); ");

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

        /// <summary>
        /// The Addproduct method adds the product to a valid shelf in the warehouse
        /// It will get the shelf object from the database from the given shelfIdentifier parameter
        /// It will check if there is storage space for the product and if there is, it will add it to the shelf.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="shelfIdentifier"></param>
        /// <returns>
        /// Returns 1 for success
        /// Returns 0 for storage error
        /// </returns>
        public static int AddProduct(Product product, string shelfIdentifier)
        {
            // Get shelf id so it creates the reference in the db
            var query = SQLite.GetRecords($"SELECT id, maxUnitStorageSize from shelves where identifier = '{shelfIdentifier}'");

            if (query.Count > 0)
            {
                var shelf = query[0];
                int shelfId = Convert.ToInt32(shelf["id"]);
                int shelfMaxUnitStorageSize = Convert.ToInt32(shelf["maxUnitStorageSize"]);
                int shelfCurrentStorageSize = 0;
                //// Get all the products that are on the shelf to get the sum of
                query = SQLite.GetRecords($"SELECT sum(unitSize) as sum from products where shelfId = {shelfId}");
                if (query[0]["sum"] != "NULL")
                    shelfCurrentStorageSize = Convert.ToInt32(query[0]["sum"]);

                if ((product.UnitSize + shelfCurrentStorageSize) <= shelfMaxUnitStorageSize)
                {
                    return SQLite.Execute($"INSERT INTO products (name, category, unitSize ,shelfId) VALUES ('{product.Name}','{(int)product.Category}', {product.UnitSize}, {shelfId})");
                }
            }
            else Console.WriteLine("No shelves has been set up in the warehouse!");

            return 0;
        }
        public static void AddShelf(Shelf shelf)
        {
            SQLite.Execute($"INSERT INTO shelves (identifier, description, maxUnitStorageSize) VALUES ('{shelf.Identifier}', '{shelf.Description}', {shelf.MaxUnitStorageSize})");
        }

        public static Records GetProducts()
        {
            return SQLite.GetRecords(@"
                SELECT products.id, products.name, product_categories.name as category, shelves.identifier as shelf
                from products
                    INNER JOIN shelves
                    ON products.shelfId=shelves.id
                    INNER JOIN product_categories
                    ON products.category = product_categories.id
                ORDER BY product_categories.name ASC");
        }

        public static Records GetShelves()
        {
            return SQLite.GetRecords(@"
                SELECT shelves.id, shelves.identifier, shelves.description, shelves.maxUnitStorageSize as maxStorage,
                (SELECT sum(products.unitSize) from products where products.shelfId = shelves.id) as usedStorage,
                (SELECT count(*) from products where products.shelfId = shelves.id) as products
                FROM shelves
                ");
        }

        public static void PrettyPrintRecords(Records records)
        {
            //The length of the pretty print of the columns
            const int columnIdLength = 6;
            const int columnDataLength = 18;
            // Take the first result to gather the column fields
            if (records.Count > 0)
            {
                foreach (var record in records[0])
                {
                    // If the field is ID, then we wont take as much space for the print
                    if (record.Key == "id")
                    {
                        Console.Write($"{record.Key,-columnIdLength}");
                    }
                    else {

                        Console.Write($"{record.Key,-columnDataLength}");
                    }
                }
                Console.WriteLine();
                // Print all of the rows
                foreach (var record in records)
                {
                    foreach (KeyValuePair<string, string> data in record)
                    {
                        if (data.Key == "id")
                        {
                            Console.Write($"{data.Value,-columnIdLength}");
                        }
                        else {
                            var value = data.Value;
                            if (value.Length > columnDataLength)
                            {
                                const string endPrefix = "...";
                                value = value.Substring(0, (columnDataLength - endPrefix.Length) - 1) + endPrefix;
                            }
                            Console.Write($"{value,-columnDataLength}");
                        }
                        
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            else Console.WriteLine("Records is empty");

        }
    }
}


