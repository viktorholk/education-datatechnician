using System;
using System.Collections.Generic;
using System.Text;
using Warehouse_System.Classes.SQL;
using Warehouse_System.Classes.Warehouse;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Warehouse_System.Classes.Application
{
    class Application : ApplicationContext
    {
        private void ClearConsole()
        {
            Console.Clear();
            Status.WritePrevious();
        }
        private int GetIntegerInput()
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

        private T SelectFromList<T>(List<T> list, int id) where T : class
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
        private void PrintTableData<T>(List<T> list, int cursorLeft = 40, int cursorTop = 2, bool recursiveFields = false)
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


        public Application(string title, (int, int) dimensions)
        {
            Console.Title           = title;
            //Console.SetWindowSize(dimensions.Item1, dimensions.Item2);
            //// Removes the scrollbars
            //Console.SetBufferSize(dimensions.Item1, dimensions.Item2);
            Database.Initialize();
        }

        public void Run()
        {
            while (true)
            {

                ConsoleKeyInfo mainMenuKeyPress;
                do
                {
                    ClearConsole();
                    new Status("Ok", Status.StatusCodes.INFO).Write();
                    Console.SetCursorPosition(0, 2);
                    Console.WriteLine("     -- W A R E H O U S E --");
                    Console.WriteLine("     ----- S Y S T E M -----");
                    Console.WriteLine();
                    WriteColor("     1 ", InfoColor, false);
                    Console.WriteLine("View Shelves");
                    WriteColor("     2 ", InfoColor, false);
                    Console.WriteLine("View Products");
                    Console.WriteLine("     -----------------------");
                    WriteColor("     ESC ", InfoColor, false);
                    Console.WriteLine(" Go Back");
                    Console.WriteLine();

                    mainMenuKeyPress = Console.ReadKey(true);

                    switch (mainMenuKeyPress.Key)
                    {
                        case ConsoleKey.D1:
                            new Status("Shelf view", Status.StatusCodes.INFO).Write();
                            ConsoleKeyInfo MenuShelfKeyPress;
                            do
                            {
                                PrintTableData(Shelf.shelves, 40, 1, true);
                                MenuShelfKeyPress = Console.ReadKey(true);

                            } while (MenuShelfKeyPress.Key != ConsoleKey.Escape);

                            break;
                        case ConsoleKey.D2:

                            break;
                        case ConsoleKey.D3:
                            break;
                        default:
                            new Status($"{mainMenuKeyPress.Key} is not a valid menu!", Status.StatusCodes.ERROR).Write();
                            break;

                    }

                } while (mainMenuKeyPress.Key != ConsoleKey.Escape);
                Console.Clear();
                Console.WriteLine("Are you sure you want to quit?");
                Console.Write("Press ");
                WriteColor("enter ", InfoColor, false);
                Console.WriteLine("to quit");

                Console.Write("Press ");
                WriteColor("any ", InfoColor, false);
                Console.WriteLine("key to go back");

                mainMenuKeyPress = Console.ReadKey(true);
                if (mainMenuKeyPress.Key == ConsoleKey.Enter) break;

            }

        }
    }
}
