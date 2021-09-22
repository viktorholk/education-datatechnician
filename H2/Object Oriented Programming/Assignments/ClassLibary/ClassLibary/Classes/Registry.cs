using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Registry
    {
        private List<User>  users = new List<User>();

        public List<User> GetUsers(){
            return this.users;
        }

        public void AddUser(User user)
        {
            System.Console.WriteLine($"{user} Adding user to registry");
            this.users.Add(user);
        }

        // Give the option to add multile users to the lists with user array
        public void AddUser(User[] userArray)
        {
            foreach (var user in userArray)
            {
                this.AddUser(user);
            }
        }

        public User AuthenticateUser(string login, string password)
        {
            foreach (User user in this.users)
            {
                if (user.login == login && user.password == password)
                    return user;
            }
            return null;
        }
    }
}
