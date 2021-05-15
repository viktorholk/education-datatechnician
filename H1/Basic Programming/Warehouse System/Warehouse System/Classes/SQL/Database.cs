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
        /// <summary>
        /// The instance of the database connection that we aren't going to overwrite so it stays readonly
        /// It sets the connection to the SQLite file
        /// </summary>
        private static readonly SQLiteConnection Instance = new SQLiteConnection($"Data Source=warehouse.db");
        /// <summary>
        /// Load data from the database into a list
        /// Overwrites the data from the database into the list so we will always have a list with the correct amount of rows
        /// </summary>
        /// <typeparam name="T">The type of class</typeparam>
        /// <param name="list">The list we are going to add data to</param>
        /// <param name="query">Database query to get the rows</param>
        public static void Load<T>(List<T> list, string query) where T : class
        {
            list.Clear();
            foreach (DataRow row in GetDataTable(query).Rows)
            {
                // Create an instance of the T class and add it
                T instance = (T)Activator.CreateInstance(typeof(T), row.ItemArray);
                list.Add(instance);
            }
        }
        /// <summary>
        /// Execute method
        /// This is primarily used when we want to SELECT, DELETE or UPDATE from the database
        /// since we dont want any particular information back other than if the query was successfull
        /// </summary>
        /// <param name="query">Database query</param>
        /// <returns>Integer whether or not the query was successfull</returns>
        public static int Execute(string query)
        {
            // If the instance haven't been opened yet, open it
            if (Instance.State != ConnectionState.Open)
                Instance.Open();
            // Create the command
            var cmd = Instance.CreateCommand();
            // Set the commandtext to be the query parameter
            cmd.CommandText = query;
            // Executethequery and return the count of rows inserted/updated
            return cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// Execute a query and return the last inserted row id
        /// This is used when we want to save an object in the database, but also we have saved in the database we would like to set the Id of the object
        /// This work with sql transaction so we makes sure that we dont get the id of another rowid
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Count of rows inserted/updated</returns>
        public static int ExecuteAndGetId(string query)
        {
            // If the instance haven't been opened yet, open it
            if (Instance.State != ConnectionState.Open)
                Instance.Open();
            // Create the transaction
            SQLiteTransaction transaction;
            transaction = Instance.BeginTransaction();
            // Execute the queries
            Execute(query);
            int id = Convert.ToInt32(GetDataTable("SELECT last_insert_rowid() as id").Rows[0]["id"]);
            // Commit the transaction and return the id
            transaction.Commit();
            return id;
        }
        /// <summary>
        /// GetDataTable method
        /// Gets all of the rows from the executed query into a datatable we can iterate through
        /// </summary>
        /// <param name="query">Database query</param>
        /// <returns>Datatable of the rows</returns>
        public static DataTable GetDataTable(string query)
        {
            // If the instance haven't been opened yet, open it
            if (Instance.State != ConnectionState.Open)
                Instance.Open();
            // Datatable entries
            DataTable entries = new DataTable();
            // Create the command with the query
            var cmd = Instance.CreateCommand();
            cmd.CommandText = query;
            // Read through all the columns and rows
            using (var reader = cmd.ExecuteReader())
            {
                // First check if the reader isn't empty and there is rows
                if (reader.HasRows)
                {
                    // Add the columns first
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        entries.Columns.Add(new DataColumn(reader.GetName(i)));
                    }

                    // Add the row data to the datatable
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
        /// <summary>
        /// Initialize method
        /// This method executes all the tables into the SQLite file, if they already haven't been created
        /// The method also adds the data from the database into the
        /// ProductCategories
        /// Shelves
        /// </summary>
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
