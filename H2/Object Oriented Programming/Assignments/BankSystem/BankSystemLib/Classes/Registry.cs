using System;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace BankSystemLib
{
    public class Registry
    {
        public static List<User> users = LoadUsers();

        private static List<User> LoadUsers(){
            // Load the users from the json file
            return Jsonhandler.Read<List<User>>();
        }

        public static void SaveRegistry(){
            // Write the current user list to the registry
            Jsonhandler.Write<List<User>>(Registry.users);
        }

        public Registry() { }

        public bool AddUser(User user){
            // Check if the user already exists
            if (!Registry.users.Exists(i => i.Login == user.Login)){
                Registry.users.Add(user);

                // Update the json file with the appended user
                Registry.SaveRegistry();
                return true;
            }
            return false;
        }

        public void AddUser(User[] userArray){
            foreach (User user in userArray)
                AddUser(user);
        }

        public User AuthenticateUser(string login, string password ){
            foreach (User user in Registry.users)
            {
                if (user.Login == login && user.Password == password)
                    return user;
            }
            return null;
        }
    }
}
