using Context;

public class Application
{
    public List<User> Users;
    public List<Product> Products;

    private bool running;

    private User? currentUser;

    public Application()
    {
        this.Users = new List<User>();
        this.Products = new List<Product>();
        this.running = true;
    }

    public void Run()
    {
        //this.currentUser = this.Users[1];
        while (this.running)
        {
            Console.Clear();
            StatusHandler.WritePrevious();
            while (this.currentUser is null)
            {
                ApplicationContext.PrintMenu("Login", false);
                string username = ApplicationContext.GetInput<string>("Username");
                string password = ApplicationContext.GetInput<string>("Password");

                var user = Authenticate(username, password);

                // If the user is not null set the current user an return to main manu
                if (!(user is null))
                {
                    this.currentUser = user;
                }
                else
                {
                    Console.WriteLine("Wrong username or password!");
                    Thread.Sleep(800);

                }
            }

            ConsoleKey input;

            if (this.currentUser.IsAdmin())
            {
                input = ApplicationContext.PrintMenu("Home Menu (Logged in as admin)", true, new Dictionary<string, string>(){
                        {"1", "Show overview"},
                        {"2", "Create new Product"},
                        {"3", "Create new Case"},
                        {"4", "Create new Resource"},
                        {"5", "Update existing Resource"},
                        {"6", "Logout"},
                        {"7", "Quit"}
                    });


                if (input == ConsoleKey.D1)
                {
                    while (true)
                    {
                        ApplicationContext.PrintTableData(Products, recursiveFields: true);
                        input = ApplicationContext.PrintMenu("Show Overview", true, new Dictionary<string, string>(){
                            {"1", "Go back"}
                        });

                        if (input == ConsoleKey.D1) break;
                    }
                }
                else if (input == ConsoleKey.D2)
                {
                    ApplicationContext.PrintMenu("Create new Product");

                    string name = ApplicationContext.GetInput<string>("Name");
                    string description = ApplicationContext.GetInput<string>("Description");

                    CreateProduct(name, description);

                }
                else if (input == ConsoleKey.D3)
                {

                    ApplicationContext.PrintMenu("Create new Case");

                    Product product = ApplicationContext.SelectFromList(Products);
                    Console.WriteLine("Choose a case type");
                    ApplicationContext.WriteColor("0.", ConsoleColor.Yellow, false);
                    Console.WriteLine("Feature");
                    ApplicationContext.WriteColor("1.", ConsoleColor.Yellow, false);
                    Console.WriteLine("Bug");
                    int caseSelection = ApplicationContext.GetInput<int>("Selection");

                    string description = ApplicationContext.GetInput<string>("Description");

                    product.CreateCase(this.currentUser, (Case.CaseType)caseSelection, description);

                }
                else if (input == ConsoleKey.D4)
                {
                    ApplicationContext.PrintMenu("Create new Resource");

                    List<Case> cases = new List<Case>();
                    foreach (var product in Products)
                    {
                        foreach (var _case in product.Cases)
                        {
                            cases.Add(_case);
                        }
                    }

                    Case __case = ApplicationContext.SelectFromList<Case>(cases);
                    string description = ApplicationContext.GetInput<string>("Description");
                    __case.CreateResource(description);

                }
                else if (input == ConsoleKey.D5)
                {
                    ApplicationContext.PrintMenu("Update existing resource");

                    List<Resource> resources = new List<Resource>();
                    foreach (var product in Products)
                    {
                        foreach (var _case in product.Cases)
                        {
                            foreach (var resource in _case.Resources)
                            {
                                resources.Add(resource);
                            }
                        }

                    }

                    Resource selectedResource = ApplicationContext.SelectFromList(resources);


                    while (true)
                    {
                        Console.Clear();

                        input = ApplicationContext.PrintMenu($"Selected Resource: {selectedResource.ToString()}", true, new Dictionary<string, string>(){
                            {"1", "Add Hours"},
                            {"2", "Update Status"},
                            {"3", "Go Back"},
                        });

                        if (input == ConsoleKey.D1)
                        {
                            var hours = ApplicationContext.GetInput<int>("Hours");
                            selectedResource.AddHours(hours);
                        }
                        else if (input == ConsoleKey.D2)
                        {
                            ApplicationContext.WriteColor("0.", ConsoleColor.Yellow, false);
                            Console.WriteLine("Pending");
                            ApplicationContext.WriteColor("1.", ConsoleColor.Yellow, false);
                            Console.WriteLine("Ongoing");
                            ApplicationContext.WriteColor("2.", ConsoleColor.Yellow, false);
                            Console.WriteLine("Completed");

                            var selection = ApplicationContext.GetInput<int>("Selection");
                            selectedResource.UpdateStatus((Resource.ResourceStatus)selection);
                        }

                        else if (input == ConsoleKey.D3) break;
                    }

                }
                else if (input == ConsoleKey.D6)
                {
                    this.currentUser = null;
                }
                else if (input == ConsoleKey.D7)
                {
                    this.running = false;

                }

            }
            else
            {
                input = ApplicationContext.PrintMenu("Home Menu", true, new Dictionary<string, string>(){
                        {"1", "My Cases"},
                        {"2", "Logout"},
                        {"3", "Quit"}
                    });

                if (input == ConsoleKey.D1)
                {
                    while (true)
                    {
                        List<Case> cases = new List<Case>();

                        foreach (var product in Products)
                        {
                            foreach (var _case in product.Cases)
                            {
                                if (_case.Creator == this.currentUser)
                                {
                                    cases.Add(_case);
                                }
                            }
                        }

                        if (cases.Count > 0)
                        {
                            ApplicationContext.PrintTableData(cases, recursiveFields: true);
                        }
                        else
                        {
                            Console.SetCursorPosition(40, 1);
                            Console.WriteLine("No cases found");
                        }

                        input = ApplicationContext.PrintMenu("My Cases", true, new Dictionary<string, string>(){
                        {"1", "Create new Case"},
                        {"2", "Go Back"}
                    });
                        if (input == ConsoleKey.D1)
                        {
                            Console.Clear();
                            ApplicationContext.PrintMenu("Create new Case");

                            Product product = ApplicationContext.SelectFromList(Products);
                            Console.WriteLine("Choose a case type");
                            ApplicationContext.WriteColor("0.", ConsoleColor.Yellow, false);
                            Console.WriteLine("Feature");
                            ApplicationContext.WriteColor("1.", ConsoleColor.Yellow, false);
                            Console.WriteLine("Bug");
                            int caseSelection = ApplicationContext.GetInput<int>("Selection");

                            string description = ApplicationContext.GetInput<string>("Description");

                            product.CreateCase(this.currentUser, (Case.CaseType)caseSelection, description);


                        }
                        else if (input == ConsoleKey.D2) break;

                    }

                }
                else if (input == ConsoleKey.D2)
                {
                    this.currentUser = null;
                }
                else if (input == ConsoleKey.D3)
                {
                    this.running = false;

                }
            }
        }
    }


    public User? Authenticate(string username, string password)
    {
        foreach (User user in this.Users)
        {
            if (user.Username == username && user.Password == password)
                return user;
        }

        return null;
    }

    public User CreateUser(string name, string password, User.UserType type = User.UserType.customer)
    {
        var user = new User(name, password, type);
        this.Users.Add(user);
        return user;
    }


    public Product CreateProduct(string name, string description)
    {
        var product = new Product(name, description);
        this.Products.Add(product);
        StatusHandler.Write("Successfully created new product", ApplicationContext.Status.Success);
        return product;
    }
}
