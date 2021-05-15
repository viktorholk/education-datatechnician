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
    /// <summary>
    /// Application class
    /// This class handles the the menues
    /// The class only have one method which is the Run Method
    /// All the other code is from the parent class ApplicationContext
    /// </summary>
    class Application : ApplicationContext
    {
        /// <summary>
        /// Application class constructor
        /// This just sets the title of the console application on new
        /// </summary>
        /// <param name="title">The title of the console application</param>
        public Application(string title)
        {
            Console.Title = title;
            // Fetch all the data from the database into the program
            Database.Initialize();

            Console.ForegroundColor = defaultColor;
        }

        /// <summary>
        /// Run method
        /// This method runs the application which means taking the user to the menues and handles their user inputs
        /// </summary>
        public void Run()
        {
            // Program loop
            // if this loop breaks the game quits
            while (true)
            {

                ConsoleKeyInfo mainMenuKeyPress;
                // Set startup status message
                StatusHandler.Write("Ok", StatusHandler.Codes.INFO);
                // Do while loop until user presses escape button to go back
                do
                {
                    ClearConsole();
                    // Print the two menu options 
                    PrintMenu(new Dictionary<int, string>()
                    {
                        {1, "Manage Stock" },
                        {2, "View Information" },
                    });                    

                    // Wait for keypress by user
                    mainMenuKeyPress = Console.ReadKey(true);


                    switch (mainMenuKeyPress.Key)
                    {
                        // Manage stock
                        case ConsoleKey.D1:
                            StatusHandler.Write("Ok", StatusHandler.Codes.INFO);
                            
                            ConsoleKeyInfo MenuStockKeyPress;
                            do
                            {
                                // Clear the console for the new menu
                                ClearConsole();
                                // Print menu options
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
                                // Get all the data from the shelves and their products
                                PrintTableData(Shelf.shelves, 40, 2, true);
                                // Reset cursor
                                Console.SetCursorPosition(0, 0);
                                MenuStockKeyPress = Console.ReadKey(true);

                                if (MenuStockKeyPress.Key == ConsoleKey.D1)
                                {
                                    Console.SetCursorPosition(0, 15);
                                    // Create the object shelf and save it to the db
                                    Shelf shelf = CreateObject<Shelf>();

                                    if (ConfirmDialog())
                                        shelf.Save();

                                } else if (MenuStockKeyPress.Key == ConsoleKey.D2)
                                {
                                    Console.SetCursorPosition(0, 15);
                                    // Get the object by id input in the getobject method
                                    Shelf shelf = GetObject<Shelf>();
                                    // Get the new changes
                                    string description      = GetInput<string>("Description");
                                    int maxUnitStorageSize  = GetInput<int>("MaxUnitStorageSize");
                                    // Save and edit
                                    if (ConfirmDialog())
                                        shelf.Edit(description, maxUnitStorageSize);
                                }
                                else if (MenuStockKeyPress.Key == ConsoleKey.D3)
                                {
                                    Console.SetCursorPosition(0, 15);
                                    // Get the shelf by id and remove
                                    Shelf shelf = GetObject<Shelf>();

                                    if (ConfirmDialog())
                                        shelf.Remove();
                                }
                                else if (MenuStockKeyPress.Key == ConsoleKey.D4)
                                {
                                    Console.SetCursorPosition(0, 15);
                                    // Get the shelf where the product should be added
                                    Shelf shelf = GetObject<Shelf>();
                                    // Create the new product object
                                    Product product = CreateObject<Product>();
                                    // Add the prodcut to the shelf
                                    if (ConfirmDialog())
                                        shelf.AddProduct(product);

                                } else if (MenuStockKeyPress.Key == ConsoleKey.D5)
                                {
                                    Console.SetCursorPosition(0, 15);
                                    // Get the product by the id
                                    Product product = GetObject<Product>();
                                    // We make a temperary edited boolean to check if we successfully edited the product so we can break the loop
                                    bool edited = false;
                                    // We are going to find the product in the shelves and edit their instance instead of doing a copy
                                    foreach (var _shelf in Shelf.shelves)
                                    {
                                        foreach (var _product in _shelf.products)
                                        {
                                            // If it equals we have found the matching product and will now edit
                                            if (_product.Equals(product))
                                            {

                                                string name = GetInput<string>("Name");

                                                Console.WriteLine($"    {"Id",-4} Category Name");
                                                // Print valid categories
                                                foreach (var _category in ProductCategory.categories)
                                                {
                                                    WriteColor($"   {_category.Id,-4}", InfoColor, false);
                                                    Console.Write($"{_category.Name}\n");
                                                }
                                                // Get the category by id input
                                                ProductCategory category = GetObject<ProductCategory>();
                                                // User input
                                                int unitSize = GetInput<int>("Unit Size");
                                                int unitPrice = GetInput<int>("Unit Price");
                                                // Get the shelf of where the product should be listed
                                                Shelf shelf = GetObject<Shelf>();
                                                _shelf.EditProduct(_product, name, category, unitSize, unitPrice, shelf);
                                                edited = true;
                                                break;
                                            }
                                        }
                                        // If it has been edited just break for performance
                                        if (edited) break;
                                    }
                                } else if (MenuStockKeyPress.Key == ConsoleKey.D6)
                                {
                                    Console.SetCursorPosition(0, 15);
                                    // Get the product id
                                    Product product = GetObject<Product>();
                                    // Temp removed boolean to check if the product we have gotten by id matches with a product in the shelves
                                    bool removed = false;
                                    foreach (var shelf in Shelf.shelves)
                                    {
                                        foreach (var _product in shelf.products)
                                        {
                                            // We have found the product and the user gets the confirmdialog to remove the product
                                            if (_product.Equals(product))
                                            {
                                                if (ConfirmDialog())
                                                    shelf.RemoveProduct(_product);
                                                removed = true;
                                                break;
                                            }
                                        }
                                        // stop loop if already been removed
                                        if (removed) break;
                                    }
                                }

                            } while (MenuStockKeyPress.Key != ConsoleKey.Escape);

                            break;
                        case ConsoleKey.D2:
                            StatusHandler.Write("Ok", StatusHandler.Codes.INFO);

                            Console.SetCursorPosition(40, 2);
                            Console.WriteLine($"Warehouse Infomation");

                            Console.SetCursorPosition(40, Console.CursorTop);
                            Console.Write($"{"Number of shelves:", -25}");
                            WriteColor(Shelf.shelves.Count.ToString(), InfoColor, true);

                            Console.SetCursorPosition(40, Console.CursorTop);
                            Console.Write($"{"Number of products:", -25}");
                            int products = 0;
                            foreach (var shelf in Shelf.shelves)
                            {
                                products += shelf.products.Count;
                            }
                            WriteColor(products.ToString(), InfoColor, true);

                            Console.SetCursorPosition(40, Console.CursorTop + 1);
                            Console.Write($"{"Used storage:",-25}");
                            int usedStorage = 0;
                            int maxStorage = 0;
                            foreach (var shelf in Shelf.shelves)
                            {
                                maxStorage += shelf.MaxUnitStorageSize;
                                foreach (var product in shelf.products)
                                {
                                    usedStorage += product.UnitSize;
                                }
                            }
                            WriteColor($"{usedStorage} / {maxStorage}", InfoColor, true);

                            Console.SetCursorPosition(40, Console.CursorTop);
                            Console.Write($"{"Total product value:",-25}");
                            int productValue = 0;
                            foreach (var shelf in Shelf.shelves)
                            {
                                foreach (var product in shelf.products)
                                {
                                    productValue += product.UnitPrice;
                                }
                            }
                            WriteColor(productValue.ToString(), InfoColor, true);


                            Console.ReadKey(true);
                            break;
                        default:
                            break;
                    }

                } while (mainMenuKeyPress.Key != ConsoleKey.Escape);
                // If the user presses escape to the screen we want to prompt the user if they wants to quit the program
                Console.Clear();
                Console.WriteLine("Are you sure you want to quit?");
                Console.Write("Press ");
                WriteColor("enter ", InfoColor, false);
                Console.WriteLine("to quit");

                Console.Write("Press ");
                WriteColor("any ", InfoColor, false);
                Console.WriteLine("key to go back");
                // Readkey and if it is enter quit the program
                mainMenuKeyPress = Console.ReadKey(true);
                if (mainMenuKeyPress.Key == ConsoleKey.Enter) break;
            }
        }
    }
}
