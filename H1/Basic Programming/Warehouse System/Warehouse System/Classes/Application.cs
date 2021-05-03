using System;
using System.Collections.Generic;
using System.Text;
using Warehouse_System.Classes.SQL;
using Warehouse_System.Classes.Warehouse;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Warehouse_System.Classes
{
    class Application
    {
        private readonly ConsoleColor InfoColor    = ConsoleColor.Yellow;
        private readonly ConsoleColor SuccessColor      = ConsoleColor.Green;
        private readonly ConsoleColor ErrorColor        = ConsoleColor.Red;

        public enum StatusCodes
        {
            INFO,
            SUCCESS,
            ERROR
        }
        public void SetStatus(StatusCodes code, string message)
        {

            // Get current cursor so we can revert
            int previousLeft = Console.CursorLeft;
            int previousTop = Console.CursorTop;

            // Set the color of the code
            switch (code)
            {
                case StatusCodes.INFO:
                    Console.ForegroundColor = InfoColor;
                    break;
                case StatusCodes.SUCCESS:
                    Console.ForegroundColor = SuccessColor;
                    break;
                case StatusCodes.ERROR:
                    Console.ForegroundColor = ErrorColor;
                    break;
                default:
                    break;
            }
            Console.SetCursorPosition(0, 0);
            // Erase the status line so we dont get fragments of other status codes
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"STATUS: {message}");
            // Reset foregroundColor
            Console.ResetColor();
            // Rewert cursor
            Console.SetCursorPosition(previousLeft, previousTop);
        }
        private void WriteColor(string message, ConsoleColor color, bool newLine = true)
        {
            Console.ForegroundColor = color;
            if (newLine)
                Console.WriteLine(message);
            else
                Console.Write(message);
            Console.ResetColor();

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
                Console.Clear();
                SetStatus(StatusCodes.INFO, "Ok");
                ConsoleKeyInfo keyboardPress;

                do
                {
                    Console.SetCursorPosition(0, 2);
                    Console.WriteLine("     -- W A R E H O U S E --");
                    Console.WriteLine("     ----- S Y S T E M -----");
                    Console.WriteLine();
                    WriteColor("     1. ", InfoColor, false);
                    Console.WriteLine("View Shelves");
                    Console.WriteLine("     -----------------------");
                    WriteColor("     ESC ", InfoColor, false);
                    Console.WriteLine(" Go Back");
                    Console.WriteLine();

                    keyboardPress = Console.ReadKey(true);



                    switch (keyboardPress.Key)
                    {
                        case ConsoleKey.D1:
                            SetStatus(StatusCodes.INFO, "Ok");

                            break;
                        case ConsoleKey.D2:
                            SetStatus(StatusCodes.SUCCESS, "AYYAYAYAY");
                            break;
                        case ConsoleKey.D3:
                            SetStatus(StatusCodes.INFO, "HHEHEHEYAYAY");
                            break;
                        default:
                            SetStatus(StatusCodes.ERROR, $"{keyboardPress.Key} is not a valid menu!");
                            break;

                    }

                } while (keyboardPress.Key != ConsoleKey.Escape);
                Console.Clear();
                Console.WriteLine("Are you sure you want to quit?");
                Console.Write("Press ");
                WriteColor("enter ", InfoColor, false);
                Console.WriteLine("to quit");

                Console.Write("Press ");
                WriteColor("any ", InfoColor, false);
                Console.WriteLine("key to go back");

                keyboardPress = Console.ReadKey(true);
                if (keyboardPress.Key == ConsoleKey.Enter) break;

            }

        }
    }
}
