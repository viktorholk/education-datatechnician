using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BankSystemLib{

    public class Database {

        private static readonly string database         = "bank_system";
        private static readonly string ConnectionString = @"server=localhost;userid=bank_system;password=supersecret;SSL MODE=None";
        private static readonly string databaseScript   = @"./BankSystemLib/SQL/database.sql";
        private static MySqlConnection Connection = new MySqlConnection(ConnectionString);
        private static bool Initialized = false;

        public static void Initialize(bool drop = false){
            if (!Initialized) {
                if (Connection.State != System.Data.ConnectionState.Open)
                    Connection.Open();

                if (drop)
                    new MySqlCommand($"DROP DATABASE IF EXISTS {database}", Connection).ExecuteNonQuery();

                // Set up the database
                new MySqlCommand($"CREATE DATABASE IF NOT EXISTS {database}; USE bank_system;", Connection).ExecuteNonQuery();

                if (drop) {
                    MySqlScript script = new MySqlScript(Connection, File.ReadAllText(databaseScript));
                    script.Execute();
                    // call the create_Defaults procedure
                    new MySqlCommand("call create_defaults()", Connection).ExecuteNonQuery();
                }

                Initialized = true;
            }
        }

        public static int Query(string query){
            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();

            try {
                using (var cmd = new MySqlCommand(query, Connection))
                    return cmd.ExecuteNonQuery();

            } catch (MySqlException exception) {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(exception.ToString());
                Console.ResetColor();
            }
            return 0;
        }

        public static int QueryGetId(string query) {
            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();

            try {
                // Create mysql transaction
                int id;
                var transaction = Connection.BeginTransaction();
                // Do de initial query and return the last inserted ID
                Query(query);
                using (var cmd = new MySqlCommand("SELECT LAST_INSERT_ID()", Connection)) {
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }

                transaction.Commit();
                return id;

            } catch (MySqlException exception) {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(exception.ToString());
                Console.ResetColor();
            }
            return -1;
        }
        public static List<T> Query<T>(string query) where T : class{
            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();

            List<T> data = new List<T>();

            try {
                using (var cmd = new MySqlCommand(query, Connection)){
                    DataTable dataTable = new DataTable();

                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        dataAdapter.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        data.Add( (T)Activator.CreateInstance(typeof(T), row.ItemArray) );
                    }
                }
                return data;

            } catch (MySqlException exception) {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(exception.ToString());
                Console.ResetColor();
            }

            return data;
        }
    }
}