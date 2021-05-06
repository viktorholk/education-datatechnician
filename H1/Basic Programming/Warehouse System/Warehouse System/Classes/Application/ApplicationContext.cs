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
        protected static readonly ConsoleColor defaultColor = ConsoleColor.White;
        protected static readonly ConsoleColor highlightColor = ConsoleColor.Gray;
        protected static readonly ConsoleColor InfoColor = ConsoleColor.Yellow;
        protected static readonly ConsoleColor SuccessColor = ConsoleColor.Green;
        protected static readonly ConsoleColor ErrorColor = ConsoleColor.Red;


        protected void ClearConsole()
        {
            Console.Clear();
            StatusHandler.WritePrevious();
        }
        protected void WriteColor(string message, ConsoleColor color, bool newLine = true)
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            if (newLine)
                Console.WriteLine(message);
            else
                Console.Write(message);
            Console.ForegroundColor = previousColor;

        }

        protected int GetIntegerInput(bool keepCursor = false, bool clear = false)
        {
            int previousLeft = Console.CursorLeft;
            int previousTop = Console.CursorTop;
            int number;
            string input = "";
            while (true)
            {

                try
                {
                    input = Console.ReadLine();
                    number = Convert.ToInt32(input);
                    break;
                }
                catch 
                {
                    StatusHandler.Write($"'{input}' is not an integer", StatusHandler.Codes.ERROR);
                    if (keepCursor)
                        Console.SetCursorPosition(previousLeft, previousTop);
                }
                finally
                {
                    if (clear)
                        ClearConsolePosition(previousLeft, previousTop, input.Length, 1);
                }

            }
            return number;
        }

        protected string GetStringinput(bool clear = false)
        {
            int previousLeft = Console.CursorLeft;
            int previousTop = Console.CursorTop;
            string input = Console.ReadLine();

            if (clear)
                ClearConsolePosition(previousLeft, previousTop, input.Length, 1);
            return input;
        }
        private T SelectFromList<T>(List<T> list, int id) where T : class
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
            return null;
        }

        protected T GetObject<T>() where T : class
        {
            Type type = typeof(T);
            Console.SetCursorPosition(0, 15);
            WriteColor("    Id: ", InfoColor, false);
            int id = GetIntegerInput(true, true);

            if (type == typeof(Shelf))
            {
                return SelectFromList(Shelf.shelves, id) as T;
            }
            return null;
        }
        protected T CreateObject<T>() where T : class
        {
            Type type = typeof(T);

            Console.SetCursorPosition(0, 15);
            if (type == typeof(Shelf))
            {
                WriteColor("    Description: ", InfoColor, false);
                string description = GetStringinput(false);

                WriteColor("    MaxUnitStorageSize: ", InfoColor, false);
                int maxUnitStorageSize = GetIntegerInput(true, true);
                return (T)Activator.CreateInstance(typeof(Shelf), description, maxUnitStorageSize);
            }
            return default;
        }
        protected T MenuGetObject<T>(T t)
        {
            return t;
        }
        protected bool ConfirmDialog()
        {
            int previousLeft = Console.CursorLeft;
            int previousTop = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop + 2);
            WriteColor("Confirm Action", InfoColor, true);
            WriteColor("Enter ", InfoColor, false);
            Console.Write(" to confirm");
            Console.WriteLine();
            WriteColor("Any key", InfoColor, false);
            Console.Write(" to cancel");
            ConsoleKeyInfo keypress = Console.ReadKey(true);
            Console.SetCursorPosition(previousLeft, previousTop);
            if (keypress.Key == ConsoleKey.Enter)
                return true;
            StatusHandler.Write($"Cancelled action", StatusHandler.Codes.INFO);
            return false;
        }
        protected void ClearConsolePosition(int cursorLeft, int cursorTop, int width, int height)
        {
            int previousLeft = cursorLeft;
            int previousTop = cursorTop;
            Console.SetCursorPosition(cursorLeft, cursorTop);

            for (int i = 0; i < height; i++)
            {
                Console.WriteLine(new string(' ', width));
                Console.SetCursorPosition(cursorLeft, Console.CursorTop);
            }
            Console.SetCursorPosition(previousLeft, previousTop);
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
            WriteColor($"{list.GetType().GetGenericArguments()[0].Name.ToUpper()}(s)", highlightColor, false);
            // Print the id field first
            // Print the rest of the propertiesLength
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
            foreach (var prop in propertiesLength)
            {
                string propString = $"{prop.Key.PadRight(prop.Value)} ";
                WriteColor(propString, InfoColor, false);
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

        protected void PrintMenu(Dictionary<int, string> options)
        {
            const int menukeyPadding = 4;
            Console.SetCursorPosition(0, 2);
            // Clear the menu first
            for (int i = 0; i < 25; i++)
            {
                Console.WriteLine("                            ");
            }
            Console.SetCursorPosition(0, 2);

            //Print header 
            Console.WriteLine("     -- W A R E H O U S E --");
            Console.WriteLine("     ----- S Y S T E M -----");
            // Print options with whitespaces, so it overwrites each time we call the method
            foreach (var opt in options)
            {
                // Create a space 
                if (opt.Key == 0)
                {
                    Console.WriteLine();
                    continue;
                }
                WriteColor($"     {opt.Key,-menukeyPadding} ", InfoColor, false);
                Console.Write(opt.Value + "\n");
            }
            // print footer
            Console.WriteLine("     ----------------------");
            WriteColor($"     {"ESC",-menukeyPadding} ", InfoColor, false);
            Console.Write("Go Back\n");
        }


    }
}
