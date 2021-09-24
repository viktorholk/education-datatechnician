using System;
using System.Linq;
using BankSystemLib;
namespace ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            Registry registry = new Registry();

            registry.AddUser(new User("sample", "1234"));
            registry.AddUser(new User("other_sample", "4321"));

            var sample_user = Registry.users[0];

            var account = new CheckingAccount();

            sample_user.AddAccount(account);

            sample_user.Accounts[0].CreateTransaction(TransactionTypes.Deposit, 1000);
            sample_user.Accounts[0].CreateTransaction(TransactionTypes.Deposit, 500);
            sample_user.Accounts[0].CreateTransaction(TransactionTypes.Deposit, 2000);

            System.Console.WriteLine(sample_user.Accounts[0].GetBalance());

            System.Console.WriteLine(Registry.users.Count);
            Registry.users.ForEach(i => System.Console.WriteLine(i));


            Registry.SaveRegistry();
        }
    }
}
