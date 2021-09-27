using System;
using System.Collections.Generic;
namespace BankSystemLib
{
    public class User
    {
        public List<Account> Accounts;

        public string Login { get; set; }
        public string Password { get; set; }
        public User(string login, string password){
            this.Accounts   = new List<Account>();
            this.Login      = login;
            this.Password   = password;

        }
        public void AddAccount(Account account){
            this.Accounts.Add(account);
        }


        public override string ToString()
        {
            return $"{this.Login}";
        }


    }
}
