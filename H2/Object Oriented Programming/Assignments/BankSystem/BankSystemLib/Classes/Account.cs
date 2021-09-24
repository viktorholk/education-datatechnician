using System;
using System.Collections.Generic;
namespace BankSystemLib
{

    public enum AccountTypes {
        Checking,
        Savings
    }
    public abstract class Account
    {
        public List<Transaction> Transactions { get; set;}

        public DateTime CreationDate { get; set; }
        public AccountTypes AccountType { get; set; }

        // Constructor for setting account type and define the properties
        public Account(AccountTypes accountType){
            this.Transactions = new List<Transaction>();
            this.CreationDate = DateTime.Now;
            this.AccountType = accountType;
        }

        public double GetBalance(){
            double total = 0.0;

            foreach (Transaction transaction in Transactions){
                if (transaction.TransactionType == TransactionTypes.Deposit)
                    total += transaction.Amount;
                else if (transaction.TransactionType == TransactionTypes.Withdraw)
                    total -= transaction.Amount;
            }
            return total;
        }

        public void CreateTransaction(TransactionTypes type, double amount){
            Transactions.Add(new Transaction(type, amount));
        }

        public void CreateTransaction(TransactionTypes type, double amount, User receiver){

        }

    }
}
