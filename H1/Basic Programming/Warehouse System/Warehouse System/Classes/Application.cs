using System;
using System.Collections.Generic;
using System.Text;
using Warehouse_System.Classes.SQL;
using System.Data;
namespace Warehouse_System.Classes
{
    class Application
    {
        private readonly ConsoleColor DefaultColor = ConsoleColor.White;
        private readonly ConsoleColor HighlightColor = ConsoleColor.Yellow;
        private readonly ConsoleColor SuccessColor = ConsoleColor.Green;
        private readonly ConsoleColor ErrorColor = ConsoleColor.Red;


        private readonly string[] header = new string[]
        {
            "WAREHOUSE SYSTEM",
            "By viktorholk"
        };
        private void PrintHeader()
        {
            Console.Clear();
            CenterWrite(header);
        }
        private void CenterWrite(string[] strings)
        {
            for (int i = 0; i < strings.Length; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth - strings[i].Length) / 2, Console.CursorTop);
                Console.WriteLine(strings[i]);
            }
        }
        private void ColorWrite(string text, ConsoleColor type, ConsoleColor color, bool newLine = true)
        {
            ConsoleColor previousForeground = Console.ForegroundColor;
            ConsoleColor previousBackground = Console.BackgroundColor;

            if (type.Equals(Console.ForegroundColor))
                Console.ForegroundColor = color;
            else if (type.Equals(Console.BackgroundColor))
                Console.BackgroundColor = color;

            // Write the text
            if (newLine)
                Console.WriteLine(text);
            else
                Console.Write(text);

            // Reset the colors to the previous
            Console.ForegroundColor = previousForeground;
            Console.BackgroundColor = previousBackground;
        }
        private string[] FormatDataTable(string query)
        {
            // All the lines that we are going to return
            List<string> stringLines = new List<string>();

            // The records of the datatable
            DataTable table = Database.GetDataTable(query);

            // First we are going to make a dictonary that contains the column names and their max length, for formatting
            Dictionary<string, int> columns = new Dictionary<string, int>();
            string columnsString = "";

            // Go through all the columns and for each column we will get the longest data length in the rows for the max length
            foreach (var column in table.Columns)
            {
                string columnName = column.ToString();
                int maxLength = 0;
                foreach (DataRow row in table.Rows)
                {
                    if (row[columnName].ToString().Length > maxLength)
                        maxLength = row[columnName].ToString().Length;
                }
                columns[columnName] = maxLength;
                columnsString += $"{columnName.PadRight(maxLength)} ";
            }
            stringLines.Add(columnsString);

            foreach (DataRow row in table.Rows)
            {
                
            }

            return stringLines.ToArray();
        }
        public Application(string title, (int, int) dimensions)
        {
            Console.Title           = title;
            Console.SetWindowSize(dimensions.Item1, dimensions.Item2);
            //// Removes the scrollbars
            //Console.SetBufferSize(dimensions.Item1, dimensions.Item2);
        }

        public void Run()
        {
            while (true)
            {
                ConsoleKeyInfo keyboardPress;
                PrintHeader();
                // print main menu
                CenterWrite(new string[]
                {
                    "Navigate through the menus with keypresses",
                    "Go back by pressing ESCAPE",
                    "",
                    "1. Show list of shelves",
                    "2. Show list of products"
                });
                
                do
                {
                    keyboardPress = Console.ReadKey(true);

                    switch (keyboardPress.Key)
                    {
                        case ConsoleKey.D1:
                            PrintHeader();
                            CenterWrite(Database.GetDataTable("SELECT * FROM shelves").Print);
                            break;
                        default: break;

                    }


                } while (keyboardPress.Key != ConsoleKey.Escape);
                Console.Clear();
                Console.WriteLine("Are you sure you want to quit?");
                Console.WriteLine("Press enter to quit");
                keyboardPress = Console.ReadKey(true);
                if (keyboardPress.Key == ConsoleKey.Enter) break;
            }
        }
    }
}
