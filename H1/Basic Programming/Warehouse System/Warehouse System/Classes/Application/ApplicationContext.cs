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
        private Dictionary<string, int> GetPropertiesLength<T>(List<T> list)
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
        protected T GetInput<T>(string inputType)
        {
            Type type = typeof(T);
            WriteColor($"   {inputType}:", InfoColor, false);

            int previousLeft = Console.CursorLeft;
            int previousTop = Console.CursorTop;
            if (type == typeof(int))
            {
                int number;
                while (!int.TryParse(Console.ReadLine(), out number))
                {
                    StatusHandler.Write($"Please enter a valid number", StatusHandler.Codes.ERROR);
                    Console.SetCursorPosition(previousLeft, previousTop);
                }

                return (T)Convert.ChangeType(number, typeof(T));
            } else if (type == typeof(string))
            {
                string input = Console.ReadLine();
                return (T)Convert.ChangeType(input, typeof(T));
            }
            return default;

        }
        protected T CreateObject<T>()
        {
            Type type = typeof(T);

            if (type == typeof(Shelf))
            {
                string description = GetInput<string>("Description");
                int maxUnitStorageSize = GetInput<int>("MaxUnitStorageSize");

                return (T)Activator.CreateInstance(type, description, maxUnitStorageSize);

            } else if (type == typeof(Product))
            {
                string name = GetInput<string>("Name");

                Console.WriteLine($"    {"Id", -4} Category Name");
                foreach (var _category in ProductCategory.categories)
                {
                    WriteColor($"   {_category.Id,-4}", InfoColor, false);
                    Console.Write($"{_category.Name}\n");
                }
                ProductCategory category = GetObject<ProductCategory>();
                int unitSize = GetInput<int>("Unit Size");
                int unitPrice = GetInput<int>("Unit Price");

                return (T)Activator.CreateInstance(type, name, category, unitSize, unitPrice);

            }
            return default;
        }
        protected T GetObject<T>()
        {
            int previousLeft = Console.CursorLeft;
            int previousTop = Console.CursorTop;
            Type type = typeof(T);

            while (true)
            {
                // Get the id of the object
                int id = GetInput<int>($"{type.Name} Id");

                if (type == typeof(Shelf))
                {
                    Shelf shelf = SelectFromList(Shelf.shelves, id);
                    if (shelf != null)
                        return (T)Convert.ChangeType(shelf, typeof(T));
                    else
                    {
                        StatusHandler.Write($"No shelf has given id {id}", StatusHandler.Codes.ERROR);
                        Console.SetCursorPosition(previousLeft, previousTop);

                    }

                } else if (type == typeof(ProductCategory)){

                    ProductCategory category = SelectFromList(ProductCategory.categories, id);
                    if (category != null)
                        return (T)Convert.ChangeType(category, typeof(T));
                    else
                    {
                        StatusHandler.Write($"No category has given id {id}", StatusHandler.Codes.ERROR);
                        Console.SetCursorPosition(previousLeft, previousTop);

                    }

                } else if (type == typeof(Product))
                {
                    Product product = null;
                    foreach (var shelf in Shelf.shelves)
                    {
                        foreach (var _product in shelf.products)
                        {
                            if (_product.Id == id)
                            {
                                product = _product;
                            }
                        }
                    }
                    if (product != null)
                    {
                        return (T)Convert.ChangeType(product, typeof(T));
                    }
                    else
                    {
                        StatusHandler.Write($"No product has given id {id}", StatusHandler.Codes.ERROR);
                        Console.SetCursorPosition(previousLeft, previousTop);
                    }
                }
            }
        }
        protected void ClearConsole()
        {
            Console.Clear();
            StatusHandler.WritePrevious();
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
        protected void PrintTableData<T>(List<T> list, int cursorLeft = 40, int cursorTop = 2, bool recursiveFields = false)
        {
            // The properties names and their max length
            Dictionary<string, int> propertiesLength;
            propertiesLength = GetPropertiesLength(list);
            foreach (var item in propertiesLength)
            {
                Console.WriteLine(item.Key);
            }
            Console.ReadLine();
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
    
        
    }
}
