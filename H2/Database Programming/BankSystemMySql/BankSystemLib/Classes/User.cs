using System;
using System.Collections.Generic;
namespace BankSystemLib
{

    [Table("users")]
    public class User : SQLObject
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }

        public User(string login, string password, bool admin = false){
            this.Login      = login;
            this.Password   = password;
            this.Admin      = admin;

        }
        public User(int id, string login, string password, bool admin) : base(id){
            this.Login      = login;
            this.Password   = password;
            this.Admin      = admin; 
        }
        public void AddAccount(Account account) {
            if (!Admin)
                account.Save();
            else
                System.Console.WriteLine("Admins can not have accounts");
        }
    }
}
