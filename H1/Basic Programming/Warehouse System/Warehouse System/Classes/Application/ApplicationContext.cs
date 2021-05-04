using System;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Warehouse_System.Classes.Warehouse;

namespace Warehouse_System.Classes.Application
{
    class ApplicationContext
    {
        protected static readonly ConsoleColor InfoColor = ConsoleColor.Yellow;
        protected static readonly ConsoleColor SuccessColor = ConsoleColor.Green;
        protected static readonly ConsoleColor ErrorColor = ConsoleColor.Red;

        public enum StatusCodes
        {
            INFO,
            SUCCESS,
            ERROR
        }
        protected void ClearConsole()
        {
            Console.Clear();
            StatusHandler.WritePrevious();
        }
        protected void WriteColor(string message, ConsoleColor color, bool newLine = true)
        {
            Console.ForegroundColor = color;
            if (newLine)
                Console.WriteLine(message);
            else
                Console.Write(message);
            Console.ResetColor();

        }

        protected int GetIntegerInput()
        {
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out int _integer))
                {
                    return _integer;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number");
                }
            }
        }
        protected T SelectFromList<T>(List<T> list, int id) where T : class
        {
            while (true)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                foreach (var item in list)
                {
                    int genericObjectId = Convert.ToInt32(properties.Single(i => i.Name == "Id").GetValue(item));
                    if (genericObjectId == id)
                    {
                        return item;
                    }

                }
                Console.WriteLine("Please enter a valid Id");
            }
        }

        protected Dictionary<string, int> GetPropertiesLength<T>(List<T> list)
        {
            string[] hiddenProps = new string[]
            {
                "Saved"
            };

            Dictionary<string, int> propertiesLength = new Dictionary<string, int>
            {
                // With this line we manipulate the Id field to be first everytime
                ["Id"] = 0
            };
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                if (hiddenProps.Contains(property.Name)) continue;
                int fieldMaxDataLength = property.Name.Length;

                foreach (var item in list)
                {
                    PropertyInfo _property = item.GetType().GetProperty(property.Name);
                    if (property.Equals(_property))
                    {
                        int fieldDataLength = _property.GetValue(item).ToString().Length;
                        if (fieldDataLength > fieldMaxDataLength)
                        {
                            fieldMaxDataLength = fieldDataLength;
                        }
                    }

                }
                propertiesLength[property.Name] = fieldMaxDataLength;
            }
            return propertiesLength;
        }

        protected void PrintTableData<T>(List<T> list, int cursorLeft = 40, int cursorTop = 2, bool recursiveFields = false)
        {
            // The properties names and their max length
            Dictionary<string, int> propertiesLength;
            propertiesLength = GetPropertiesLength(list);

            Console.SetCursorPosition(cursorLeft, cursorTop);
            WriteColor($"{list.GetType().GetGenericArguments()[0].Name.ToUpper()}(s)", InfoColor, false);
            // Print the id field first
            // Print the rest of the propertiesLength
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
            foreach (var prop in propertiesLength)
            {
                string propString = $"{prop.Key.PadRight(prop.Value)} ";
                if (prop.Key == "Id")
                    WriteColor(propString, InfoColor, false);
                else
                    Console.Write(propString);
            }
            foreach (var item in list)
            {
                Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
                foreach (var prop in propertiesLength)
                {
                    PropertyInfo property = item.GetType().GetProperty(prop.Key);
                    var value = property.GetValue(item).ToString();
                    string dataString = $"{value.PadRight(prop.Value)} ";
                    if (property.Name == "Id")
                        WriteColor(dataString, InfoColor, false);
                    else
                        Console.Write(dataString);
                }
                if (recursiveFields)
                {
                    // We are now going to check for fields for each item 
                    // if perhaps the shelf class has a list called products, we want to print those too
                    Dictionary<string, object> fields = new Dictionary<string, object>();
                    FieldInfo[] _fields = item.GetType().GetFields();
                    foreach (var field in _fields)
                    {
                        if (!field.IsStatic)
                        {
                            object value = field.GetValue(item);
                            fields[field.Name] = value;
                        }
                    }

                    foreach (var field in fields)
                    {
                        Type fieldType = field.Value.GetType().GetGenericArguments()[0];

                        if (fieldType == typeof(Product))
                        {
                            List<Product> products = (List<Product>)field.Value;
                            if (products.Count > 0)
                                PrintTableData((List<Product>)field.Value, 50, Console.CursorTop + 1);
                        }
                    }
                }
            }
        }
    }
}
