using System;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace BankSystemLib
{
    public class Registry
    {
        public static List<User> Users                  = Database.Query<User>("SELECT * FROM users");
        public static List<Account> Accounts            = Database.Query<Account>("SELECT * FROM accounts");
        public static List<Transaction> Transactions    = Database.Query<Transaction>("SELECT * FROM transactions");
        public static User AuthenticateUser(string login, string password ){
            foreach (User user in Users)
            {
                if (user.Login == login && user.Password == password)
                    return user;
            }
            return null;
        }

        public static User GetUser(int id){
            foreach (User user in Users)
            {
                if (user.Id == id)
                    return user;
            }
            return null;
        }

        public static Account GetAccount(int accountId){
            foreach (Account account in Accounts)
            {
                if (account.Id == accountId){
                    return account;
                }
            }
            return null;
        }   

       public static List<Account> GetAccounts(int userId){
           List<Account> accounts = new List<Account>();

            foreach (User user in Users)
            {
                if (user.Id == userId) {
                    foreach (Account account in Accounts)
                    {
                        if (account.User == user) {
                            accounts.Add(account);
                        }
                    }
                }
            }
            return accounts;
        }   

        public static List<Transaction> GetAccountTransactions(int accountId) {
            List<Transaction> accountTransactions = new List<Transaction>();

            foreach (Transaction transaction in Transactions)
            {
                if (transaction.Account.Id == accountId) {
                    accountTransactions.Add(transaction);
                }
            }
            return accountTransactions;
        }

    }
}
