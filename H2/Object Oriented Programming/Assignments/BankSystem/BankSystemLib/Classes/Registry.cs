using System;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace BankSystemLib
{
    public class Registry
    {
        public List<User> users;
        
        public Registry() {
            this.users = new List<User>();
         }

        public bool AddUser(User user){
            // Check if the user already exists
            if (!this.users.Exists(i => i.Login == user.Login)){
                this.users.Add(user);
                return true;
            }
            return false;
        }

        public void AddUser(User[] userArray){
            foreach (User user in userArray)
                AddUser(user);
        }

        public User AuthenticateUser(string login, string password ){
            foreach (User user in this.users)
            {
                if (user.Login == login && user.Password == password)
                    return user;
            }
            return null;
        }
    }
}
