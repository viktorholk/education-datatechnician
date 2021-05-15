using System;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Warehouse_System.Classes.Warehouse;

namespace Warehouse_System.Classes.Application
{
    /// <summary>
    /// ApplicationContext Class
    /// This class handles all of the context that is going on in the Application
    /// It has methods for printing colors to the screen 
    /// and handling generic object handling whether we want to get or create an object
    /// 
    /// The class is the parent class of both Application and StatusHandler
    /// StatusHandler class needs the class as a parent to know the different colorcodes that we use
    /// </summary>
    public class ApplicationContext
    {
        // The different colorcodes that we use in the application

        protected static readonly ConsoleColor defaultColor = ConsoleColor.White;
        protected static readonly ConsoleColor highlightColor = ConsoleColor.Gray;
        protected static readonly ConsoleColor InfoColor = ConsoleColor.Yellow;
        protected static readonly ConsoleColor SuccessColor = ConsoleColor.Green;
        protected static readonly ConsoleColor ErrorColor = ConsoleColor.Red;

        /// <summary>
        /// SelectFromList method
        /// With this we check for the property "Id" of the list<object> passed into the method
        /// we compare the Ids and if they equals we will return the object
        /// </summary>
        /// <typeparam name="T">Generic class</typeparam>
        /// <param name="list">The list we will look through</param>
        /// <param name="id">The id of the object we want to find</param>
        /// <returns>T object</returns>
        private     T SelectFromList<T>(List<T> list, int id) where T : class
        {
            // All of the properties given from the T class
            PropertyInfo[] properties = typeof(T).GetProperties();
            //Go through all of the items in the list
            // if the property "Id" equals to the id parameter, return the item in the list
            // else return null
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
        /// <summary>
        /// GetInput method
        /// This is a generic method that takes in a <datatype> and handles how we are going to handle the input
        /// if its an integer the method makes sure to return an integer from the console.readline string
        /// if its a string it just return the string
        /// </summary>
        /// <typeparam name="T">Datatype</typeparam>
        /// <param name="inputType">What message should we prompt the user?</param>
        /// <returns>Returns the datatype giving in T</returns>
        protected   T GetInput<T>(string inputType)
        {
            Type type = typeof(T);
            // Write the inputtype with color
            WriteColor($"   {inputType}:", InfoColor, false);
            // Save the previous cursor
            int previousLeft = Console.CursorLeft;
            int previousTop = Console.CursorTop;

            // If the type is integer
            // we are going to tryparse it, until it returns true and we will convert the type to int and return it
            if (type == typeof(int))
            {
                int number;
                while (!int.TryParse(Console.ReadLine(), out number))
                {
                    StatusHandler.Write($"Please enter a valid number", StatusHandler.Codes.ERROR);
                    Console.SetCursorPosition(previousLeft, previousTop);
                }

                return (T)Convert.ChangeType(number, typeof(T));
            }
            // If the type is string we simply just return the Readline, since we dont have to do anything else
            else if (type == typeof(string))
            {
                string input = Console.ReadLine();
                return (T)Convert.ChangeType(input, typeof(T));
            }
            // Return default T, if none of the types are met
            return default;

        }
        /// <summary>
        /// CreateObject method
        /// This creates a object from what type of class we have passed into the method
        /// It will go through the input of different parameters that the class constructors needs to instantiate
        /// it will return an instanitated object of the T
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <returns>Object</returns>
        protected   T CreateObject<T>()
        {
            // Store the type of the Object provided
            Type type = typeof(T);


            if (type == typeof(Shelf))
            {
                // Get the input and create the instance
                string description = GetInput<string>("Description");
                int maxUnitStorageSize = GetInput<int>("MaxUnitStorageSize");

                return (T)Activator.CreateInstance(type, description, maxUnitStorageSize);

            }
            else if (type == typeof(Product))
            {
                // Get the input and create the instance

                string name = GetInput<string>("Name");

                Console.WriteLine($"    {"Id",-4} Category Name");
                // Print all valid categories so the user can see which to pick
                foreach (var _category in ProductCategory.categories)
                {
                    WriteColor($"   {_category.Id,-4}", InfoColor, false);
                    Console.Write($"{_category.Name}\n");
                }
                // Create the category object
                ProductCategory category = GetObject<ProductCategory>();
                int unitSize = GetInput<int>("Unit Size");
                int unitPrice = GetInput<int>("Unit Price");
                // return instance
                return (T)Activator.CreateInstance(type, name, category, unitSize, unitPrice);

            }
            return default;
        }
        /// <summary>
        /// GetObject method
        /// This method returns an existing object from the corrosponding type list
        /// it will go through the different types, since we are going to differnet lists depending on the class type
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <returns>Object</returns>
        protected   T GetObject<T>()
        {
            // Save previous cursor
            int previousLeft = Console.CursorLeft;
            int previousTop = Console.CursorTop;
            // Get the typeof T
            Type type = typeof(T);

            // We are going to do this in a while true, 
            // since the GetInput can pass whenever a valid integer has been given
            // But we will still make sure that the int id also matches a valid object from the list
            // So we will keep prompting the user to type in a correct id
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

                }
                else if (type == typeof(ProductCategory))
                {

                    ProductCategory category = SelectFromList(ProductCategory.categories, id);
                    if (category != null)
                        return (T)Convert.ChangeType(category, typeof(T));
                    else
                    {
                        StatusHandler.Write($"No category has given id {id}", StatusHandler.Codes.ERROR);
                        Console.SetCursorPosition(previousLeft, previousTop);

                    }

                }
                else if (type == typeof(Product))
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
        /// <summary>
        /// GetPropertiesLength method
        /// This method takes in a list of objects
        /// and goes through each properties and compares the length of the value 
        /// to other objects to get the longest value spread over the list
        /// It is used when we need to format the tables with the correct data lengths
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="list">The list with the objects</param>
        /// <returns>Dictionary string, int, the key is the name of the prop and the integer is the maximum length of the values</returns>
        protected   Dictionary<string, int> GetPropertiesLength<T>(List<T> list)
        {
            // We dont want to print out the Saved property
            string[] hiddenProps = new string[]
            {
                "Saved"
            };

            // We create the dictonary that we are going to return, with the already key item Id
            // Since this is universal for all of our SQL object, we want it to be the first property when we print out the data
            Dictionary<string, int> propertiesLength = new Dictionary<string, int>
            {
                // With this line we manipulate the Id field to be first everytime
                ["Id"] = 0
            };
            // Get all properties of the object
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                // If the hiddenprops contains the propertyname skip
                if (hiddenProps.Contains(property.Name)) continue;
                // We set the maxDataLength to be the property.name.length by default for formatting purposes
                int fieldMaxDataLength = property.Name.Length;

                // Go through all of the objects and match the property to see how long the longest value is
                foreach (var item in list)
                {
                    // Get the property from the item
                    PropertyInfo _property = item.GetType().GetProperty(property.Name);

                    if (property.Equals(_property))
                    {
                        // Compare the dataLength and if it is longer than our previous max length we will overwrite it
                        int fieldDataLength = _property.GetValue(item).ToString().Length;
                        if (fieldDataLength > fieldMaxDataLength)
                        {
                            fieldMaxDataLength = fieldDataLength;
                        }
                    }

                }
                // Set the key and value in the dictonary with the name of the property and the value of the max length
                propertiesLength[property.Name] = fieldMaxDataLength;
            }
            return propertiesLength;
        }
        /// <summary>
        /// ConfirmDialog method
        /// This method can be called whether or not we want to make the user of the application sure of his decision on his actions
        /// that can be the user wants to save, edit or delete an object.
        /// </summary>
        /// <returns>Boolean whether or not the user wants to confirm action</returns>
        protected   bool ConfirmDialog()
        {
            // Save previous cursor
            int previousLeft = Console.CursorLeft;
            int previousTop = Console.CursorTop;
            // Set the cursor below our latest print
            Console.SetCursorPosition(0, Console.CursorTop + 2);
            // Print message
            WriteColor("Confirm Action", InfoColor, true);
            WriteColor("Enter ", InfoColor, false);
            Console.Write(" to confirm");

            Console.WriteLine();

            WriteColor("Any key", InfoColor, false);
            Console.Write(" to cancel");

            // Wait for keypress, and if it is enter we will return true, else false.
            ConsoleKeyInfo keypress = Console.ReadKey(true);
            Console.SetCursorPosition(previousLeft, previousTop);
            if (keypress.Key == ConsoleKey.Enter)
                return true;

            StatusHandler.Write($"Cancelled action", StatusHandler.Codes.INFO);
            return false;
        }
        /// <summary>
        /// ClearConsole method
        /// Clears the console and sets the previous statusmessage
        /// </summary>
        protected   void ClearConsole()
        {
            Console.Clear();
            StatusHandler.WritePrevious();
        }
        /// <summary>
        /// PrintMenu method
        /// This makes it easier when we want to print a new menu
        /// </summary>
        /// <param name="options">
        /// A dictonary<string,int> 
        /// is passed and the int is the key that is supposed to be press
        /// the string is the title of the menu
        /// 
        /// </param>
        protected   void PrintMenu(Dictionary<int, string> options)
        {
            const int menukeyPadding = 4;
            // Where to print the menu
            Console.SetCursorPosition(0, 2);
            // Clear the menu first, 
            for (int i = 0; i < 25; i++)
            {
                Console.WriteLine("                            ");
            }

            Console.SetCursorPosition(0, 2);

            //Print header 
            Console.WriteLine("     -- W A R E H O U S E --");
            Console.WriteLine("     ----- S Y S T E M -----");

            foreach (var opt in options)
            {
                // Create a space  if the key is 0
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
        /// <summary>
        /// WriteColor method
        /// We use this method whenever we want to print color to the application
        /// </summary>
        /// <param name="message">What to print</param>
        /// <param name="color">What color to print in</param>
        /// <param name="newLine">Whether or not we want to write it on a new line</param>
        protected   void WriteColor(string message, ConsoleColor color, bool newLine = true)
        {
            // Store the previous color
            ConsoleColor previousColor = Console.ForegroundColor;
            // Set the color from the parameter
            Console.ForegroundColor = color;
            // Print newline if true
            if (newLine)
                Console.WriteLine(message);
            else
                Console.Write(message);
            // reset to previous color
            Console.ForegroundColor = previousColor;

        }
        /// <summary>
        /// PrintTableData generic method
        /// This method prints the data in the list that is provivded in the parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">List of objects</param>
        /// <param name="cursorLeft">Left positiion to print </param>
        /// <param name="cursorTop">Top positition to print</param>
        /// <param name="recursiveFields">boolean whether or not recursive is allowed</param>
        protected   void PrintTableData<T>(List<T> list, int cursorLeft = 40, int cursorTop = 2, bool recursiveFields = false)
        {
            // The properties names and their max length
            Dictionary<string, int> propertiesLength;
            propertiesLength = GetPropertiesLength(list);

            Console.SetCursorPosition(cursorLeft, cursorTop);
            // Print the type of class before the data
            WriteColor($"{list.GetType().GetGenericArguments()[0].Name.ToUpper()}(s)", highlightColor, false);
            // Print all of the column names in the top before we print out the data
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
            foreach (var prop in propertiesLength)
            {
                // propString with padding the length of the longest data value in the property
                string propString = $"{prop.Key.PadRight(prop.Value)} ";
                WriteColor(propString, InfoColor, false);
            }
            // Go through all of the data in the list provided in the method
            foreach (var item in list)
            {
                Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
                // Go through all the props givin and get their value
                foreach (var prop in propertiesLength)
                {
                    // Get the property of the item
                    PropertyInfo property = item.GetType().GetProperty(prop.Key);
                    // Get the value tostring
                    var value = property.GetValue(item).ToString();
                    // Add padding to the data
                    string dataString = $"{value.PadRight(prop.Value)} ";
                    // If the property name is Id we want to print it with color
                    if (property.Name == "Id")
                        WriteColor(dataString, InfoColor, false);
                    else
                        Console.Write(dataString);
                }
                // If recursiveFields we are going to loop through the lists in the class
                // This makes it possible to show all of the products for each shelf
                // Because we loop through the shelf class and they each have a product list we can iterate through
                if (recursiveFields)
                {
                    // We are now going to check for fields for each item 
                    // if perhaps the shelf class has a list called products, we want to print those too
                    // Since fields can have different object types we are going to add the fieldinfo to a dictonary
                    Dictionary<string, object> fields = new Dictionary<string, object>();
                    FieldInfo[] _fields = item.GetType().GetFields();
                    foreach (var field in _fields)
                    {
                        // We are going to skip static fields
                        if (!field.IsStatic)
                        {
                            object value = field.GetValue(item);
                            fields[field.Name] = value;
                        }
                    }

                    foreach (var field in fields)
                    {
                        // Get the field datatype
                        Type fieldType = field.Value.GetType().GetGenericArguments()[0];
                        // If the fieldtype is product then we want to print out all of the products that there is in the list
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
