using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
namespace Warehouse_System
{

    public class Result : List<Dictionary<string, string>> { }
    class SQLite
    {

        private static readonly SqliteConnection Instance = new SqliteConnection($"Data Source=warehouse.db");

        public static int Execute(string query)
        {
            Instance.Open();

            var cmd = Instance.CreateCommand();
            cmd.CommandText = query;
            return cmd.ExecuteNonQuery();
        }
        public static Result GetResults(string query)
        {
            Instance.Open();

            Result results = null;

            var cmd = Instance.CreateCommand();
            cmd.CommandText = query;

            using (var reader = cmd.ExecuteReader())
            {
                results = new Result();
                
                while (reader.Read())
                {
                    var record = new Dictionary<string, string>();

                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        record[reader.GetName(i)] = reader.IsDBNull(i) ? "NULL" : reader.GetString(i);
                    }
                    results.Add(record);
                }
            }
            return results;
        }
    }
}
