using System;
using System.Linq;
using System.Collections.Generic;
using BankSystemLib;
namespace ATM
{
    class Program
    {

        static User loggedInUser = null;

        static void Main(string[] args)
        {
            Database.Initialize();


            // Print welcome message
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.WriteLine("> HOLK BANKSYSTEM");
            System.Console.WriteLine("> TYPE 'help' for commands.");
            Console.ResetColor();

            string input = "";

            while (input != "quit"){
                input = GetInput();

                // If statemets since we then can put the code in blocks and can use the same variable declarations

                if (input == "help"){
                    System.Console.WriteLine("List of commands:");
                    System.Console.WriteLine("- login           ( Authenticate a valid bank user )");
                    System.Console.WriteLine("- logout          ( Logout an authenticated bank user )");
                    System.Console.WriteLine("- quit            ( Quits the program )");

                    System.Console.WriteLine();

                    if (loggedInUser != null && !loggedInUser.Admin){
                        System.Console.WriteLine("- accounts        ( Show list of available accounts )");
                        System.Console.WriteLine("- create account  ( Create a new bank account )");
                        System.Console.WriteLine("- deposit         ( Deposit money into a bank account )");
                        System.Console.WriteLine("- withdraw        ( Withdraw money from a bank account )");
                        System.Console.WriteLine("- send            ( Send money to another bank account )");
                    } else if (loggedInUser.Admin) {
                        System.Console.WriteLine("- sql             ( Query the database with SQL )");
                    } else
                        System.Console.WriteLine("Login for additonal commands.");
                } 
                else if (input == "login") {
                    if (loggedInUser == null) {
                        Console.Write($"{"LOGIN:",-10}");
                        string login = GetInput();
                        Console.Write($"{"PASSWORD:",-10}");
                        string password = GetInput();

                        loggedInUser = Registry.AuthenticateUser(login, password);
                        Console.ForegroundColor = (loggedInUser == null) ? ConsoleColor.Red : ConsoleColor.Green;
                        if (loggedInUser == null)
                            System.Console.WriteLine("Invalid credentials");
                        else 
                            System.Console.WriteLine("Successfully logged in");

                        Console.ResetColor();

                    } else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("You need to log out first");
                        Console.ResetColor();
                    }
                }
                else if (input == "logout"){
                    loggedInUser = null;
                    Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("Successfully logged out");
                    Console.ResetColor();
                } 
                else if (input == "accounts") {
                    if (UserIsValid(loggedInUser)) {
                        // Print all accounts to the user
                        if (Registry.GetAccounts(loggedInUser.Id).Count > 0){
                            for (int i = 0; i < Registry.GetAccounts(loggedInUser.Id).Count; i++)
                            {
                                Account account = Registry.GetAccounts(loggedInUser.Id)[i];
                                // Print account infomation
                                System.Console.Write(account);
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                System.Console.WriteLine($" Balance: ${account.GetBalance()}");
                                Console.ResetColor();

                                    // Print all transactions to the accounts
                                if (Registry.GetAccountTransactions(account.Id).Count == 0){
                                    System.Console.WriteLine("No current transactions");
                                } else {
                                    for (int j = 0; j < Registry.GetAccountTransactions(account.Id).Count; j++)
                                    {
                                        Registry.GetAccountTransactions(account.Id)[j].Print();
                                    }
                                }
                                System.Console.WriteLine();
                            }
                        } else System.Console.WriteLine("There is currently no accounts to the user.");
                    } else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("You need to be logged in to use this command");
                        Console.ResetColor();
                    }

                } 
                else if (input == "create account") {
                    if (UserIsValid(loggedInUser)) {
                        
                        System.Console.WriteLine("Select account type:");
                        System.Console.WriteLine("1 - Checking Account");
                        System.Console.WriteLine("2 - Savings Account");
                        string accountTypeInput = GetInput();

                        if (!loggedInUser.Admin) {
                            if (accountTypeInput == "1")
                                loggedInUser.AddAccount(new Account(loggedInUser , AccountTypes.Checking));

                            else if (accountTypeInput == "2")
                            loggedInUser.AddAccount(new Account(loggedInUser , AccountTypes.Savings));
                            Console.ForegroundColor = ConsoleColor.Green;
                            System.Console.WriteLine("Successfully created an account");
                            Console.ResetColor(); 
                        }
                        else {
                            Console.ForegroundColor = ConsoleColor.Red;
                            System.Console.WriteLine($"{accountTypeInput} is not a valid account type!");
                            Console.ResetColor();
                        }
                    } else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("You need to be logged in to use this command");
                        Console.ResetColor();
                    }
                } 
                else if (input == "deposit") {
                    if (UserIsValid(loggedInUser)) {
                        if (Registry.GetAccounts(loggedInUser.Id).Count == 0){
                            System.Console.WriteLine("You have no current accounts");
                        }

                        System.Console.WriteLine("Select an account");
                        Account account = SelectAccount();

                        if (account != null){

                            int amount;
                            System.Console.Write("AMOUNT:");
                            bool validAmount = int.TryParse(GetInput(), out amount);
                            if (!validAmount){
                                Console.ForegroundColor = ConsoleColor.Red;
                                System.Console.WriteLine("Please enter a valid deposit amount");
                                Console.ResetColor();
                            }
                            // Select the account from the input

                            // Create the transaction
                            bool created = account.CreateTransaction(TransactionTypes.Deposit, amount);
                            if (created){
                                Console.ForegroundColor = ConsoleColor.Green;
                                System.Console.WriteLine($"Successfully deposited ${amount}");
                                Console.ResetColor();

                            } else {
                                Console.ForegroundColor = ConsoleColor.Red;
                                System.Console.WriteLine($"Something went wrong creating the transaction");
                                Console.ResetColor();
                            }
                        }

                    } else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("You need to be logged in to use this command");
                        Console.ResetColor();
                    }
                }
                else if (input == "withdraw") {
                    if (UserIsValid(loggedInUser)) {
                        if (Registry.GetAccounts(loggedInUser.Id).Count == 0){
                            System.Console.WriteLine("You have no current accounts");
                        }

                        System.Console.WriteLine("Select an account");
                        Account account = SelectAccount();

                        if (account != null){

                            int amount;
                            System.Console.Write("AMOUNT:");
                            bool validAmount = int.TryParse(GetInput(), out amount);
                            if (!validAmount){
                                Console.ForegroundColor = ConsoleColor.Red;
                                System.Console.WriteLine("Please enter a valid withdraw amount");
                                Console.ResetColor();
                            }
                            // Select the account from the input

                            // Create the transaction
                            bool created = account.CreateTransaction(TransactionTypes.Withdraw, amount);
                            if (created) {
                                Console.ForegroundColor = ConsoleColor.Green;
                                System.Console.WriteLine($"Successfully withdrew ${amount}");
                                Console.ResetColor();

                            } else {
                                Console.ForegroundColor = ConsoleColor.Red;
                                System.Console.WriteLine($"Something went wrong creating the transaction");
                                Console.ResetColor();
                            }
                        }
                    } else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("You need to be logged in to use this command");
                        Console.ResetColor();
                    }
                }
                else if (input == "send") {
                    if (UserIsValid(loggedInUser)) {
                        if (Registry.GetAccounts(loggedInUser.Id).Count == 0){
                                System.Console.WriteLine("You have no current accounts");
                            }
                            System.Console.WriteLine("Select an account to send money from:");
                            Account fromAccount = SelectAccount();

                            if (fromAccount != null){

                                System.Console.WriteLine("Select an account to send money to:");

                                List<Account> userAccounts = new List<Account>();
                                // Print all user accounts
                                System.Console.WriteLine("Your accounts:");
                                foreach (Account account in Registry.GetAccounts(loggedInUser.Id))
                                {
                                    if (account != fromAccount){
                                        userAccounts.Add(account);
                                        Console.ForegroundColor = ConsoleColor.White;
                                        System.Console.WriteLine($"     {userAccounts.Count} - {account}");
                                        Console.ResetColor();
                                    }
                                }

                                foreach (User user in Registry.Users)
                                {
                                    if (user != loggedInUser && user.Admin != true){
                                        System.Console.WriteLine(user.Login + ":");

                                        foreach (Account account in Registry.GetAccounts(user.Id))
                                        {
                                            userAccounts.Add(account);
                                            Console.ForegroundColor = ConsoleColor.White;
                                            System.Console.WriteLine($"     {userAccounts.Count} - {account}");
                                            Console.ResetColor();
                                        }
                                    }
                                }

                                int receiverIndex;


                                if (int.TryParse(GetInput(), out receiverIndex)){
                                    receiverIndex -= 1;
                                    if (receiverIndex < userAccounts.Count) {
                                        Account receiverAccount = userAccounts[receiverIndex];


                                        int amount;
                                        System.Console.Write("AMOUNT:");
                                        bool validAmount = int.TryParse(GetInput(), out amount);
                                        if (!validAmount){
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            System.Console.WriteLine("Please enter a valid deposit amount");
                                            Console.ResetColor();
                                        } else {
                                            // Select the account from the input

                                            // Create the transaction
                                            bool created = fromAccount.CreateTransaction(TransactionTypes.Send, amount, receiverAccount.Id);
                                            if (created) {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                System.Console.WriteLine($"Successfully sended ${amount} to ${receiverAccount}");
                                                Console.ResetColor();
                                            } else {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                System.Console.WriteLine($"Something went wrong creating the transaction");
                                                Console.ResetColor();
                                            }
                                        }
                                    } else {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        System.Console.WriteLine("There is currently no other user accounts");
                                        Console.ResetColor();
                                    }

                                } else {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    System.Console.WriteLine("Invalid user input");
                                    Console.ResetColor();
                                }

                    } else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("You need to be logged in to use this command");
                        Console.ResetColor();
                    }
                }
                else if (input == "quit") {
                    System.Console.WriteLine("Goodbye!");
                }
                else {
                    if (input.Length > 0)
                        System.Console.WriteLine($"{input} is not a valid command!"); 
                }
            }
                else if (input == "sql") {
                    if (loggedInUser.Admin) {
                        string sqlInput = "";
                        System.Console.WriteLine("Type '.views' to see a list of available views");
                        System.Console.WriteLine("Type '.quit' to quit the sql prompt");

                        while (sqlInput != ".quit" ) {

                            sqlInput = GetInput(">");

                            if (sqlInput == ".views") {
                                System.Console.WriteLine("Available views:");
                                System.Console.WriteLine("- system_flags                ( Get all flagged users and the reason )");
                                System.Console.WriteLine("- user_accounts               ( Get all accounts for each user )");
                                System.Console.WriteLine("- user_transactions           ( Get all transactions to each account )");
                                System.Console.WriteLine("- account_transactions_count  ( Show count of transactions on each account )");
                            } else {
                                // Run the query
                                var records = Database.QueryRecords(sqlInput);

                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Database.PrettyPrintRecords(records);
                                Console.ResetColor();
                            }
                        }
                    } else {
                        System.Console.WriteLine("You need to be an admin to use this command");
                    }
                }
            }

        }

        static bool UserIsValid(User user){
            if (user == null){
                System.Console.WriteLine("You need to login to use this command!");
                return false;
            }
            return true;
        }

        static Account SelectAccount(){
            for (int i = 0; i < Registry.GetAccounts(loggedInUser.Id).Count; i++)
            {
                Account account = Registry.GetAccounts(loggedInUser.Id)[i];

                
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine($"     {i + 1} - {account}");
            Console.ResetColor();
            
            }
            int accountIndex;
            // Prase the input to integer
            if (int.TryParse(GetInput(), out accountIndex)){
                accountIndex -= 1;

                if (accountIndex < Registry.GetAccounts(loggedInUser.Id).Count)
                    return Registry.GetAccounts(loggedInUser.Id)[accountIndex];
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Invalid account selection");
                    Console.ResetColor();
                }
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Invalid user input");
                Console.ResetColor();
            }
            return null;
        }

        static string GetInput(string prefix = "$"){
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($" {prefix} ");
            Console.ResetColor();
            // RETURN the input as lower string
            return Console.ReadLine().ToLower();
        }
    }
}
