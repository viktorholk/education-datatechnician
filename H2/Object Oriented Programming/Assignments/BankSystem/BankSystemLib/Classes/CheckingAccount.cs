using System;

namespace BankSystemLib
{
    public class CheckingAccount : Account
    {
        // Set the type of account to checking by calling the parent constructor
        public CheckingAccount() : base(AccountTypes.Checking){

        }

    }
}
