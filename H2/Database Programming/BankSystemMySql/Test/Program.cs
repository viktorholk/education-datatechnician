using System;
using System.Linq;
using System.Collections.Generic;
using BankSystemLib;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.Initialize(true);

            User user = new User("test", "password");

            user.Save();

            Account account = new Account(user, AccountTypes.Checking);
            Account account2 = new Account(user, AccountTypes.Savings);

            account.Save();
            account2.Save();

            Transaction transaction = new Transaction(account, TransactionTypes.Deposit, 500);

            transaction.Save();

            // Console.ReadKey();
            // Account account = new Account(0, AccountTypes.Checking);
            // account.Save();

            // Database.Query<User>("SELECT * FROM users").ForEach(i => {
            //     System.Console.WriteLine(i);
            // });
            // System.Console.WriteLine();
            // Database.Query<Transaction>("SELECT * FROM transactions").ForEach(i => {
            //     System.Console.WriteLine(i);
            // });
            // System.Console.WriteLine();
            // Database.Query<Account>("SELECT * FROM accounts").ForEach(i => {
            //     System.Console.WriteLine(i);
            // });

            // System.Console.WriteLine(Database.QueryGetId("INSERT INTO users (login, password) VALUES ('AAAA', 'BBBB')")); 

        }
    }
}
