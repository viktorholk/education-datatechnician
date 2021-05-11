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
            Console.ForegroundColor = defaultColor;
        }

        public void Run()
        {
            while (true)
            {
                ConsoleKeyInfo mainMenuKeyPress;
                StatusHandler.Write("Ok", StatusHandler.Codes.INFO);
                do
                {
                    ClearConsole();
                    PrintMenu(new Dictionary<int, string>()
                    {
                        {1, "Manage Stock" },
                        {2, "View Statistics" },
                    });                    


                    mainMenuKeyPress = Console.ReadKey(true);

                    switch (mainMenuKeyPress.Key)
                    {
                        case ConsoleKey.D1:
                            StatusHandler.Write("Ok", StatusHandler.Codes.INFO);
                            
                            ConsoleKeyInfo MenuShelfKeyPress;
                            do
                            {
                                ClearConsole();
                                PrintMenu(new Dictionary<int, string>()
                                {
                                    {1, "Add Shelf" },
                                    {2, "Edit Shelf" },
                                    {3, "Remove Shelf" },
                                    {0, "" },
                                    {4, "Add Product" },
                                    {5, "Edit Product" },
                                    {6, "Remove Product" },
                                });
                                PrintTableData(Shelf.shelves, 40, 2, true);
                                Console.SetCursorPosition(0, 0);
                                MenuShelfKeyPress = Console.ReadKey(true);

                                if (MenuShelfKeyPress.Key == ConsoleKey.D1)
                                {
                                    Console.SetCursorPosition(0, 15);
                                    Shelf shelf = CreateObject<Shelf>();

                                    if (ConfirmDialog())
                                        shelf.Save();

                                } else if (MenuShelfKeyPress.Key == ConsoleKey.D2)
                                {
                                    Console.SetCursorPosition(0, 15);
                                    Shelf shelf = GetObject<Shelf>();

                                    string description      = GetInput<string>("Description");
                                    int maxUnitStorageSize  = GetInput<int>("MaxUnitStorageSize");

                                    if (ConfirmDialog())
                                        shelf.Edit(description, maxUnitStorageSize);
                                }
                                else if (MenuShelfKeyPress.Key == ConsoleKey.D3)
                                {
                                    Console.SetCursorPosition(0, 15);
                                    Shelf shelf = GetObject<Shelf>();

                                    if (ConfirmDialog())
                                        shelf.Remove();
                                }
                                else if (MenuShelfKeyPress.Key == ConsoleKey.D4)
                                {
                                    Console.SetCursorPosition(0, 15);
                                    Shelf shelf = GetObject<Shelf>();
                                    Product product = CreateObject<Product>();

                                    if (ConfirmDialog())
                                        shelf.AddProduct(product);

                                } else if (MenuShelfKeyPress.Key == ConsoleKey.D5)
                                {
                                    Console.SetCursorPosition(0, 15);

                                    Product product = GetObject<Product>();
                                    bool edited = false;
                                    foreach (var _shelf in Shelf.shelves)
                                    {
                                        foreach (var _product in _shelf.products)
                                        {
                                            if (_product.Equals(product))
                                            {
                                                string name = GetInput<string>("Name");

                                                Console.WriteLine($"    {"Id",-4} Category Name");
                                                foreach (var _category in ProductCategory.categories)
                                                {
                                                    WriteColor($"   {_category.Id,-4}", InfoColor, false);
                                                    Console.Write($"{_category.Name}\n");
                                                }
                                                ProductCategory category = GetObject<ProductCategory>();

                                                int unitSize = GetInput<int>("Unit Size");
                                                int unitPrice = GetInput<int>("Unit Price");
                                                Shelf shelf = GetObject<Shelf>();
                                                _shelf.EditProduct(_product, name, category, unitSize, unitPrice, shelf);
                                                edited = true;
                                                break;
                                            }
                                        }
                                        if (edited) break;
                                    }
                                }

                            } while (MenuShelfKeyPress.Key != ConsoleKey.Escape);

                            break;
                        case ConsoleKey.D2:
                            StatusHandler.Write("Ok", StatusHandler.Codes.INFO);

                            break;
                        case ConsoleKey.D3:
                            StatusHandler.Write("Ok", StatusHandler.Codes.INFO);

                            break;
                        default:
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
