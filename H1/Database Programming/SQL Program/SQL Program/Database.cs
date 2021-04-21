using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
namespace SQL_Program
{
    public class Records : List<Dictionary<string, string>> { }
    class Database
    {
        private static readonly SqliteConnection Instance = new SqliteConnection($"Data Source=database.db");

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
                records = new Records();

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
    }
}
