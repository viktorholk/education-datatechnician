using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace ATM_Project
{
    class Program
    {
        // Returns a bankaccount object if it authenticates the credentials with a valid bank account
        static BankAccount AuthenticateCredentials(string username, string pin)
        {
            foreach (var account in BankAccount.bankAccounts)
            {
                if (account.Username == username)
                {
                    if (account.AuthenticatePIN(pin))
                    {
                        return account;
                    }
                }
            }
            return null;
        }

        static void Main(string[] args)
        {
            // Create some dummy bankaccounts
            new BankAccount("bob", "1234", 100);
            new BankAccount("vance", "1234", 250);
            new BankAccount("ryan", "1234", 500);
            new BankAccount("vector", "1234", 750);


            // Program main loop
            while (true)
            {
                Console.Clear();
                Console.WriteLine("TechCollege Bank System");

                // Create null bank account to tell that we havent logged in yet
                BankAccount loggedInBankAccount = null;

                // Authenticate credentials
                while (loggedInBankAccount == null)
                {
                    string username, pin;
                    Console.Write("Username: ");
                    username = Console.ReadLine();

                    Console.Write("PIN: ");
                    pin = Console.ReadLine();

                    loggedInBankAccount = AuthenticateCredentials(username, pin);
                    if (loggedInBankAccount == null)
                    {
                        Logger.Log(
                            $"Failed to authenticate credentials for user: {username}",
                            "Invalid credentials, try again."
                            );
                    } else
                    {
                        Logger.Log(
                            $"Credentials sucessfully authenticated for {username}",
                            "You have logged in"
                            );
                    }
                }
                // Menu
                // Clear the console
                Console.Clear();
                // Print all menu selections
                Console.WriteLine($"Welcome back {loggedInBankAccount.Username}!");
                Console.WriteLine("1. Show Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Logout");

                // The loop where the user is currently logged in
                while (loggedInBankAccount != null)
                {
                    Console.Write(" $ ");
                    int menuSelection = IntReadline();

                    switch (menuSelection)
                    {
                        case 1:
                            // Print the balance to the user
                            Logger.Log(
                                $"{loggedInBankAccount.Username}, Balance: {loggedInBankAccount.GetBalance():c}",
                                $"Balance: {loggedInBankAccount.GetBalance():c}"
                                );
                            break;

                        case 2:
                            // Deposit money to the account

                            Console.Write("Deposit Amount: ");
                            double depositAmount = DoubleReadline();

                            loggedInBankAccount.Deposit(depositAmount);
                            Console.WriteLine($"Successfully deposited {depositAmount:c}");
                            Logger.Log(
                                $"{loggedInBankAccount.Username}, deposited {depositAmount:c}",
                                $"You have successfully deposited {depositAmount:c}");
                            break;

                        case 3:
                            Console.Write("Withdraw Amount: ");
                            double withdrawAmount = DoubleReadline();

                            if (loggedInBankAccount.Withdraw(withdrawAmount) > 0.0)
                            {
                                Logger.Log(
                                    $"{loggedInBankAccount.Username} withdrew {withdrawAmount:c}",
                                    $"Successfully withdrew {withdrawAmount:c}"
                                    );
                            }
                            else Logger.Log(
                                        $"{loggedInBankAccount.Username}, did not have enough balance to withdraw {withdrawAmount:c}",
                                        $"Insufficient balance, you have {loggedInBankAccount.GetBalance():c}"
                                        );
                            break;

                        case 4:
                            Console.WriteLine("Select an account you want to transfer to");
                            // Print all the valid bankaccounts and their selection ids
                            for (int i = 0; i < BankAccount.bankAccounts.Count; i++)
                            {
                                BankAccount account = BankAccount.bankAccounts[i];
                                // skip the logged in account so we dont get the option to transfer money to our selves
                                if (loggedInBankAccount.Equals(account)) continue;
                                // Print the id and username
                                Console.WriteLine($"{i}, {account.Username}");
                            }
                            Console.WriteLine("Select account id: ");
                            int selection = IntReadline();
                            // Check if the selection is valid 
                            if (selection < BankAccount.bankAccounts.Count)
                            {
                                BankAccount receiver = BankAccount.bankAccounts[selection];
                                Console.Write("Transfer amount: ");
                                double transferAmount = DoubleReadline();
                                // Make sure the sender has enough balance
                                if (transferAmount <= loggedInBankAccount.GetBalance())
                                {
                                    // Deposit the money and withdraw it from the loggedinuser
                                    receiver.Deposit(transferAmount);
                                    loggedInBankAccount.Withdraw(transferAmount);
                                    Logger.Log(
                                        $"{loggedInBankAccount.Username}, transferred ${transferAmount:c} to {receiver.Username}",
                                        $"Successfully transferred {transferAmount:c} to {receiver.Username}");
                                }
                                else
                                Logger.Log(
                                    $"{loggedInBankAccount.Username}, did not have enough balance to transfer {transferAmount:c} to {receiver.Username}",
                                    $"Insufficient balance, you have {loggedInBankAccount.GetBalance():c}");
                            }
                            else Console.WriteLine("Choose a valid account");

                            break;
                        case 5:
                            // Set loggedinbankaccount to null to log out
                            loggedInBankAccount = null;
                            break;
                    }
                }
            }
        }
        static double DoubleReadline()
        {
            // Get a double datatype from a console.readline string
            while (true)
            {
                string input = Console.ReadLine();
                if (double.TryParse(input, out double number))
                {
                    return number;
                }
                else
                    Console.WriteLine("Enter a valid number");
            }
        }

        static int IntReadline()
        {
            // Get a int datatype from a console.readline string
            while (true)
            {
                int number;
                string input = Console.ReadLine();
                if (Int32.TryParse(input, out number))
                {
                    return number;
                }
                else
                    Console.WriteLine("Enter a valid number");
            }
        }
    }
}
