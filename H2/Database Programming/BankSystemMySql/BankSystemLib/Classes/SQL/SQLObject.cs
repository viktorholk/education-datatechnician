using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace BankSystemLib {

    public abstract class SQLObject {

        public int Id { get; set; }
        protected bool Saved { get; set;}

        protected virtual string TableName { get; set;}

        public SQLObject() {
            this.Saved = false;
        }

        public SQLObject(int id) {
            this.Id = id;
            this.Saved = true;
        }


        public void Save(){
            if (GetTableName() == null) return;

            PropertyInfo[] properties = this.GetType().GetProperties();

            Dictionary<string, object> columnData = new Dictionary<string, object>();

            foreach (var item in properties)
            {
                string name     = item.Name;
                object value    = item.GetValue(this);

                // When saving we dont want to do the query with ID since it will get auto incremented in the DB
                if (name == "Id") continue;

                // Check if there is a columnatribute
                object columnName = GetColumnName(name);
                if (columnName != null)
                    columnData.Add(columnName.ToString(), value);
                else
                    columnData.Add(name, value);
            }

            // If the object is already saved we want to update it
            if (!Saved) {
                // Create the query
                string columns  = string.Join(", ", columnData.Keys);
                string values   = string.Empty;
                
                // For the values, dependent on the if it is a string we need to surround it with quotes
                foreach (object value in columnData.Values)
                {
                    
                    // If it is the first value in the list we dont want to add the comma.
                    string comma = (value == columnData.Values.First()) ? "" : ", ";
                    // Parse the value and add them to the string
                    values += (comma + ValueParser(value));
                }

                string query = $"INSERT INTO {GetTableName()} ({columns}) VALUES ({values})";
                System.Console.WriteLine(query);

                // Get the new ID and save it
                int id = Database.QueryGetId(query);

                this.Id = id;
                this.Saved = true;

            } else {
                string setters = "SET ";

                foreach (var data in columnData)
                {
                    string comma = (data.Key == columnData.Keys.First()) ? "" : ", ";

                    setters += comma + $" {data.Key} = {ValueParser(data.Value)}";
                }

                string query = $"UPDATE users {setters} WHERE id = {this.Id}";
                System.Console.WriteLine(query);
                Database.Query(query);
            }
        }

        public void Delete(){
            if (GetTableName() == null) return;
            if (Saved) {
                string query = $"DELETE FROM {GetTableName()} WHERE id = {this.Id}";
                Database.Query(query);
            }
        }

        private string GetTableName(){
            TableAttribute tableAttribute = (TableAttribute) Attribute.GetCustomAttribute(this.GetType(), typeof(TableAttribute));
            if (tableAttribute != null)
                return tableAttribute.TableName;
            System.Console.WriteLine($"No table name attribute for {this.GetType()}");
            return null;
        }

        private string GetColumnName(string property){
            object[] attributes = this.GetType().GetProperty(property).GetCustomAttributes(true);

            if (attributes.Length > 0) {
                ColumnAttribute columnAttribute = (ColumnAttribute)attributes[0];
                return columnAttribute.ColumnName;
            }
            return null;
        }

        private object ValueParser(object value){
            // If the type is class we want the ID
            // If the type is enum we want the index of the enum
            // If it is string we want it in quotes
            if (value.GetType().IsSubclassOf(typeof(SQLObject))) {
                SQLObject obj = (SQLObject)value;
                return obj.Id;
            }
            if (value.GetType().BaseType == typeof(Enum)) {
                return (int)(object)value;
            }
            else if (value.GetType() == typeof(DateTime)) {
                DateTime dateTime = (DateTime)value;
                return $"'{dateTime.ToString("yyyy-MM-dd HH:mm:ss")}'";
            }
            else if (value.GetType() == typeof(String)) {
                return $"'{value}'";
            }
            return value;
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}