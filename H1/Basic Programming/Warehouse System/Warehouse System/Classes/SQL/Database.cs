using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SQLite;
using Warehouse_System.Classes.Warehouse;
namespace Warehouse_System.Classes.SQL
{
    /// <summary>
    /// Database class
    /// This class handles everything that we have to use whenever we want to communicate to the SQLite database
    /// </summary>
    class Database
    {
        private static readonly SQLiteConnection Instance = new SQLiteConnection($"Data Source=warehouse.db");
        public static void Load<T>(List<T> list, string query) where T : class
        {
            list.Clear();
            foreach (DataRow row in GetDataTable(query).Rows)
            {
                T instance = (T)Activator.CreateInstance(typeof(T), row.ItemArray);
                list.Add(instance);
            }
        }
        public static int Execute(string query)
        {
            if (Instance.State != ConnectionState.Open)
                Instance.Open();

            var cmd = Instance.CreateCommand();
            cmd.CommandText = query;
            return cmd.ExecuteNonQuery();
        }
        public static int ExecuteAndGetId(string query)
        {
            if (Instance.State != ConnectionState.Open)
                Instance.Open();

            SQLiteTransaction transaction;
            transaction = Instance.BeginTransaction();
            Execute(query);
            int id = Convert.ToInt32(GetDataTable("SELECT last_insert_rowid() as id").Rows[0]["id"]);
            transaction.Commit();
            return id;
        }
        public static DataTable GetDataTable(string query)
        {
            if (Instance.State != ConnectionState.Open)
                Instance.Open();

            DataTable entries = new DataTable();
            var cmd = Instance.CreateCommand();
            cmd.CommandText = query;

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        entries.Columns.Add(new DataColumn(reader.GetName(i)));
                    }

                    int j = 0;
                    while (reader.Read())
                    {
                        DataRow row = entries.NewRow();
                        entries.Rows.Add(row);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            entries.Rows[j][i] = reader.GetValue(i);
                        }
                        j++;
                    }
                }
            }
            return entries;
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

            // Add data from the database to the lists
            Load(ProductCategory.categories, "SELECT * FROM product_categories");
            Load(Shelf.shelves, "SELECT * FROM shelves");
        }

    }
}
