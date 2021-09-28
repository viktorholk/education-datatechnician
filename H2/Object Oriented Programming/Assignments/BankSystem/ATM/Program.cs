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
            // Initialize the registry
            Registry registry = new Registry();
            
            // Create a sample users
            var sampleUser = new User("sample", "password");

            sampleUser.AddAccount(new CheckingAccount(0.015));
            sampleUser.AddAccount(new SavingsAccount(0.025, 500, 250));

            sampleUser.Accounts[0].CreateTransaction(TransactionTypes.Deposit, 250);
            sampleUser.Accounts[0].CreateTransaction(TransactionTypes.Deposit, 50);
            sampleUser.Accounts[0].CreateTransaction(TransactionTypes.Withdraw, 50);
            sampleUser.Accounts[0].CreateTransaction(TransactionTypes.Deposit, 3500);

            sampleUser.Accounts[1].CreateTransaction(TransactionTypes.Deposit, 500);
 
            registry.AddUser(sampleUser);

            // Create a sample users
            var sampleUser2 = new User("sample2", "password");

            sampleUser2.AddAccount(new CheckingAccount(0.015));
            sampleUser2.AddAccount(new SavingsAccount(0.025, 250, 250));

            sampleUser2.Accounts[0].CreateTransaction(TransactionTypes.Deposit, 250);

            sampleUser2.Accounts[1].CreateTransaction(TransactionTypes.Deposit, 1500);
 
            registry.AddUser(sampleUser2);

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
                    System.Console.WriteLine("- save            ( Saves the registry of users )");
                    System.Console.WriteLine("- quit            ( Quits the program )");

                    System.Console.WriteLine();

                    if (loggedInUser != null){
                        System.Console.WriteLine("- accounts        ( Show list of available accounts )");
                        System.Console.WriteLine("- create account  ( Create a new bank account )");
                        System.Console.WriteLine("- deposit         ( Deposit money into a bank account )");
                        System.Console.WriteLine("- withdraw        ( Withdraw money from a bank account )");
                        System.Console.WriteLine("- send            ( Send money to another bank account )");
                    } else {
                        System.Console.WriteLine("Login for additonal commands.");
                    }
                } 
                else if (input == "login") {
                    if (loggedInUser == null) {
                        Console.Write($"{"LOGIN:",-10}");
                        string login = GetInput();
                        Console.Write($"{"PASSWORD:",-10}");
                        string password = GetInput();

                        loggedInUser = registry.AuthenticateUser(login, password);
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
                        if (loggedInUser.Accounts.Count > 0){
                            for (int i = 0; i < loggedInUser.Accounts.Count; i++)
                            {
                                Account account = loggedInUser.Accounts[i];
                                // Print account infomation
                                System.Console.Write(account);
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                System.Console.WriteLine($" Balance: ${account.GetBalance()}");
                                Console.ResetColor();

                                    // Print all transactions to the accounts
                                if (account.Transactions.Count == 0){
                                    System.Console.WriteLine("No current transactions");
                                } else {
                                    for (int j = 0; j < account.Transactions.Count; j++)
                                    {
                                        account.Transactions[j].Print();
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
                        if (accountTypeInput == "1")
                            loggedInUser.AddAccount(new CheckingAccount(2.6));

                            else if (accountTypeInput == "2")
                            loggedInUser.AddAccount(new SavingsAccount(10.5, 500, 250));

                        else {
                            Console.ForegroundColor = ConsoleColor.Red;
                            System.Console.WriteLine($"{accountTypeInput} is not a valid account type!");
                            Console.ResetColor();
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        System.Console.WriteLine("Successfully created an account");
                        Console.ResetColor(); 

                    } else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("You need to be logged in to use this command");
                        Console.ResetColor();
                    }
                } 
                else if (input == "deposit") {
                    if (UserIsValid(loggedInUser)) {
                        if (loggedInUser.Accounts.Count == 0){
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
                        if (loggedInUser.Accounts.Count == 0){
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
                        if (loggedInUser.Accounts.Count == 0){
                                System.Console.WriteLine("You have no current accounts");
                            }
                            System.Console.WriteLine("Select an account to send money from:");
                            Account fromAccount = SelectAccount();

                            if (fromAccount != null){

                                System.Console.WriteLine("Select an account to send money to:");

                                List<Account> userAccounts = new List<Account>();
                                // Print all user accounts
                                System.Console.WriteLine("Your accounts:");
                                foreach (Account account in loggedInUser.Accounts)
                                {
                                    if (account != fromAccount){
                                        userAccounts.Add(account);
                                        System.Console.WriteLine($"     {userAccounts.Count} - {account}");
                                    }
                                }
                                System.Console.WriteLine("User accounts:");

                                foreach (User user in registry.users)
                                {
                                    if (user != loggedInUser){
                                        System.Console.WriteLine(user);

                                        foreach (Account account in user.Accounts)
                                        {
                                            userAccounts.Add(account);
                                            System.Console.WriteLine($"     {userAccounts.Count} - {account}");
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
                                            bool created = fromAccount.CreateTransaction(TransactionTypes.Send, amount, ref fromAccount, ref receiverAccount);
                                            if (created) {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                System.Console.WriteLine($"Successfully sended ${amount} to ${receiverAccount}");
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
            for (int i = 0; i < loggedInUser.Accounts.Count; i++)
            {
                Account account = loggedInUser.Accounts[i];

                System.Console.WriteLine($"     {i + 1} - {account}");
            }
            int accountIndex;
            // Prase the input to integer
            if (int.TryParse(GetInput(), out accountIndex)){
                accountIndex -= 1;

                if (accountIndex < loggedInUser.Accounts.Count)
                    return loggedInUser.Accounts[accountIndex];
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

        static string GetInput(){
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" $ ");
            Console.ResetColor();
            // RETURN the input as lower string
            return Console.ReadLine().ToLower();
        }
    }
}
