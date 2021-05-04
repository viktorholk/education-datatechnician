using System;
using System.Text;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Warehouse_System.Classes.SQL;
using Warehouse_System.Classes.Warehouse;

namespace Warehouse_System.Classes.Application
{
    class Application : ApplicationContext
    {
        public Application(string title, int width, int height)
        {
            Console.Title = title;
            //Console.SetWindowSize(width, height);
            //// Removes the scrollbars
            //Console.SetBufferSize(dimensions.Item1, dimensions.Item2);
            Database.Initialize();
        }

        public void Run()
        {
            while (true)
            {
                ConsoleKeyInfo mainMenuKeyPress;
                StatusHandler.Write("Ok", StatusCodes.INFO);
                do
                {
                    ClearConsole();

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
                            StatusHandler.Write($"{mainMenuKeyPress.Key} is not a valid menu!", StatusCodes.ERROR);
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
