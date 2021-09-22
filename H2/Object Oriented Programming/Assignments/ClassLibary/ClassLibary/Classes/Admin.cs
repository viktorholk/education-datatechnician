using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Admin : User
    {

        public Admin(string login, string password) : base(login, password){
            
        }
        public void ChangeUserPassword(User user, string newPassword)
        {
            System.Console.WriteLine($"{this} Changing password from {user.password} to {newPassword}");
            user.password = newPassword;
        }
    }
}
