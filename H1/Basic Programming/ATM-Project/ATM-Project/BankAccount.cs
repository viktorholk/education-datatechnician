using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_Project
{
    class BankAccount
    {
        public string Username;
        private double Balance;
        private string PIN;

        public static List<BankAccount> bankAccounts = new List<BankAccount>();

        public BankAccount(string username, string pin, double balance = 0.0)
        {
            this.Username = username;
            this.PIN = pin;
            this.Balance = balance;

            BankAccount.bankAccounts.Add(this);
        }

        public bool AuthenticatePIN(string pin)
        {
            if (pin == this.PIN)
            {
                return true;
            }
            return false;
        }
        
        public double GetBalance()
        {
            return Balance;
        }   

        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                this.Balance += amount;
            } else
            {
                throw new Exception("Can not deposit a negative amount");
            }

        }
        // Withdraw returns a double since we want to know whether the operation was sucessfull
        // If it returns 0.0 it was NOT successfull
        public double Withdraw(double amount)
        {
            if (amount <= this.Balance)
            {
                this.Balance -= amount;
                return amount;
            }
            return 0.0;
        }

        public void Transfer(BankAccount bankAccount, double amount)
        {
            if (amount <= this.Balance)
            {
                this.Balance -= amount;
                bankAccount.Deposit(amount);
            }
        }
    }
}
