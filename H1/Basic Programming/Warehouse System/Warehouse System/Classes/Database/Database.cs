using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace Warehouse_System
{
    public class Records : List<Dictionary<string,string>> {
        public string TableName;
    }
    class Database
    {
        private static readonly SqliteConnection Instance = new SqliteConnection($"Data Source=warehouse.db");

        public static int Execute(string query)
        {
            Instance.Open();

            var cmd = Instance.CreateCommand();
            cmd.CommandText = query;
            return cmd.ExecuteNonQuery();
        }
        public static Records GetRecords(string query)
        {
            Instance.Open();

            Records records = null;

            var cmd = Instance.CreateCommand();
            cmd.CommandText = query;

            using (var reader = cmd.ExecuteReader())
            {
                records = new Records
                {
                    TableName = reader.GetSchemaTable().Rows[0]["BaseTableName"].ToString()
                };
                while (reader.Read())
                {
                    var record = new Dictionary<string, string>();

                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        record[reader.GetName(i)] = reader.IsDBNull(i) ? "NULL" : reader.GetString(i);
                    }
                    records.Add(record);
                }
            }
            return records;
        }

        public static void Initialize()
        {
            // Shelves Table
            Execute(@"CREATE TABLE IF NOT EXISTS 'shelves' (
                'id'                    INTEGER NOT NULL,
                'identifier'            TEXT NOT NULL UNIQUE,
                'description'           TEXT,
                'maxUnitStorageSize'    INTEGER NOT NULL DEFAULT 100,
                PRIMARY KEY('id' AUTOINCREMENT)
            )");
            // Product categories table
            Execute(@"CREATE TABLE IF NOT EXISTS 'product_categories' (
                'id'    INTEGER NOT NULL,
                'name'  TEXT NOT NULL UNIQUE,
                PRIMARY KEY('id' AUTOINCREMENT)
            );");
            // Product table
            Execute(@"CREATE TABLE IF NOT EXISTS 'products' (

                'id'        INTEGER NOT NULL,
                'name'      TEXT NOT NULL,
                'category_id'  INTEGER NOT NULL,
                'unitSize'  INTEGER NOT NULL DEFAULT 1,
                'unitPrice' INTEGER NOT NULL DEFAULT 0,
                'shelf_id'   INTEGER NOT NULL,
                FOREIGN KEY('shelf_id') REFERENCES 'shelves'('id') ON DELETE CASCADE,
                FOREIGN KEY('category_id') REFERENCES 'product_categories'('id') ON DELETE CASCADE,
                PRIMARY KEY('id' AUTOINCREMENT)
            );");
            // Customer table
            Execute(@"CREATE TABLE IF NOT EXISTS 'customers' (
                'id'    INTEGER NOT NULL,
                'firstName' TEXT NOT NULL,
                'lastName'  TEXT,
	            'address'	TEXT NOT NULL,
                'zipCode'   TEXT,
                PRIMARY KEY('id' AUTOINCREMENT)
            );");
            // Orders table
            Execute(@"CREATE TABLE IF NOT EXISTS 'orders' (

                'id'    INTEGER NOT NULL,
                'customer_id'   INTEGER NOT NULL,
                'date'  TEXT NOT NULL,
                FOREIGN KEY('customer_id') REFERENCES 'customers'('id') ON DELETE CASCADE,
                PRIMARY KEY('id' AUTOINCREMENT)
            ); ");
            // Order Lines table
            Execute(@"CREATE TABLE IF NOT EXISTS 'orderlines'(
                'id'    INTEGER NOT NULL,
                'order_id' INTEGER NOT NULL,
                'product_id'    INTEGER NOT NULL,
                FOREIGN KEY('product_id') REFERENCES 'products'('id') ON DELETE CASCADE,
                FOREIGN KEY('order_id') REFERENCES 'orders'('id') ON DELETE CASCADE,
                PRIMARY KEY('id' AUTOINCREMENT)
            ); ");

            LoadData();
        }
        public static void LoadData()
        {
            ProductCategory.LoadCategories();
            Product.LoadProducts();
            Shelf.LoadShelves();
            Customer.LoadCustomers();
        }

        public static void PrettyPrint(Records records)
        {
            Records tableInfo = GetRecords($"pragma table_info('{records.TableName}')");
            foreach (var item in tableInfo)
            {
                foreach (var i in item.Values)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
 }
