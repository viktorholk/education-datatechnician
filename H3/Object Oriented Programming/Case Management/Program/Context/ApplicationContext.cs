using System.Reflection;

namespace Context
{

    public static class ApplicationContext
    {

        private static string? ApplicationName;

        private static bool ValidContext = false;

        public enum Status
        {
            Info,
            Success,
            Error
        }

        public static Dictionary<Status, ConsoleColor> StatusColors = new Dictionary<Status, ConsoleColor>(){
        {Status.Info, ConsoleColor.Yellow},
        {Status.Success, ConsoleColor.Green},
        {Status.Error, ConsoleColor.Red}
    };

        public static void Setup(string applicationName)
        {
            if (ValidContext)
                throw new Exception("Context already exists");

            ApplicationName = applicationName;

            Console.Clear();

            ValidContext = true;
        }

        public static ConsoleKey PrintMenu(string title, bool getInput = false, Dictionary<string, string>? options = null)
        {
            if (!ValidContext)
                throw new Exception("Context is not setup");

            const int menukeyPadding = 4;

            ClearScreen(0, 40, 1, 15);

            Console.SetCursorPosition(0, 1);

            //Print header 
            Console.WriteLine(ApplicationName.ToUpper());
            if (!String.IsNullOrEmpty(title))
                WriteColor($"{"",-menukeyPadding} {title}", StatusColors[Status.Info]);

            if (!(options is null))
            {

                foreach (var opt in options)
                {
                    WriteColor($"{opt.Key,-menukeyPadding} ", StatusColors[Status.Info], false);
                    Console.Write(opt.Value + "\n");
                }
            }

            // Return user input
            if (getInput)
                return GetInputKey();

            return default;
        }

        public static void WriteColor(string message, ConsoleColor color, bool newLine = true)
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


        public static void ClearScreen(int fromX, int toX, int fromY, int toY, bool debug = false)
        {
            if (toX <= fromX)
                throw new Exception($"toX({toX}) has to be greater than fromY({fromX}) ");

            if (toY <= fromY)
                throw new Exception($"toY({toY}) has to be greater than fromY({fromY}) ");

            int previousLeft = Console.CursorLeft;
            int previousTop = Console.CursorTop;


            for (int i = fromY; i <= toY - fromY; i++)
            {
                Console.SetCursorPosition(fromX, i);
                Console.WriteLine(new string(debug ? 'X' : ' ', toX - fromX));
            }

            Console.SetCursorPosition(previousLeft, previousTop);

        }


        public static T GetInput<T>(string inputType)
        {
            Type type = typeof(T);
            // Write the inputtype with color
            WriteColor($"{inputType}: ", StatusColors[Status.Info], false);
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
                    StatusHandler.Write($"Please enter a valid number", Status.Info);
                    Console.SetCursorPosition(previousLeft, previousTop);
                }

                return (T)Convert.ChangeType(number, typeof(T));
            }
            // If the type is string we simply just return the Readline, since we dont have to do anything else
            else if (type == typeof(string))
            {
                string? input = Console.ReadLine();
                return (T)Convert.ChangeType(input, typeof(T));
            }
            // Return default T, if none of the types are met
            return default;
        }

        public static ConsoleKey GetInputKey()
        {
            var input = Console.ReadKey(true);
            return input.Key;
        }




        public static void PrintTableData<T>(List<T> list, int cursorLeft = 40, int cursorTop = 0, bool recursiveFields = false)
        {

            // The properties names and their max length
            Dictionary<string, int> propertiesLength;
            propertiesLength = GetPropertiesLength(list);

            Console.SetCursorPosition(cursorLeft, cursorTop + 1);
            // Print the type of class before the data
            WriteColor($"{list.GetType().GetGenericArguments()[0].Name.ToUpper()}({list.Count})", ConsoleColor.White, false);
            // Print all of the column names in the top before we print out the data
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
            foreach (var prop in propertiesLength)
            {
                // propString with padding the length of the longest data value in the property
                string propString = $"{prop.Key.PadRight(prop.Value)} ";
                WriteColor(propString, StatusColors[Status.Info], false);
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
                    FieldInfo[] fieldInfos = item.GetType().GetFields();
                    foreach (var field in fieldInfos)
                    {
                        // We are going to skip static fields
                        if (!field.IsStatic)
                        {
                            // Only add fields that are lists
                            if (field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
                            {
                                object? value = field.GetValue(item);

                                if (value is not null)
                                    fields[field.Name] = value;

                            }
                        }
                    }

                    foreach (var field in fields)
                    {
                        // Get the field datatype
                        Type fieldType = field.Value.GetType().GetGenericArguments()[0];
                        //// If the fieldtype is product then we want to print out all of the products that there is in the list

                        if (fieldType == typeof(Case))
                        {
                            List<Case> cases = (List<Case>)field.Value;
                            if (cases.Count > 0)
                                PrintTableData(cases, cursorLeft + 4, Console.CursorTop + 1, recursiveFields: true);
                        }

                        if (fieldType == typeof(Resource))
                        {
                            List<Resource> cases = (List<Resource>)field.Value;
                            if (cases.Count > 0)
                                PrintTableData(cases, cursorLeft + 4, Console.CursorTop + 1);
                        }
                        //dynamic value = field.Value;

                        //if (value.Count > 0)
                        //    PrintTableData(value, cursorLeft + 4, Console.CursorTop + 1, recursiveFields: true);
                    }
                    Console.ResetColor();
                }
            }
            System.Console.WriteLine();
        }

        public static T SelectFromList<T>(List<T> list) where T : class
        {

            System.Console.WriteLine($"Select {typeof(T)} from the list");
            for (int i = 0; i < list.Count; i++)
            {
                ApplicationContext.WriteColor($"{i} ", ConsoleColor.Yellow, false);
                Console.WriteLine($"{list[i].ToString()}");
            }

            while (true)
            {
                var selection = ApplicationContext.GetInput<int>("Selection");
                if (selection < 0 || selection > list.Count)
                    StatusHandler.Write("Invalid selection", ApplicationContext.Status.Error);
                else
                    return list[selection];

            }

        }

        private static Dictionary<string, int> GetPropertiesLength<T>(List<T> list)
        {
            // We create the dictonary that we are going to return, with the already key item Id
            // Since this is universal for all of our SQL object, we want it to be the first property when we print out the data
            Dictionary<string, int> propertiesLength = new Dictionary<string, int>();

            // Get all properties of the object
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
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
                propertiesLength[property.Name] = fieldMaxDataLength + 2;
            }
            return propertiesLength;

        }
    }
}
