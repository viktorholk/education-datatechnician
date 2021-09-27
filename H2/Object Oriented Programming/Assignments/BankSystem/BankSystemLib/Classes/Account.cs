using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BankSystemLib
{
    public interface IBankInterest{
        double Interest { get; set; }
    }

    public interface IBankMinimum {
        double MinBalance { get; set; }
        double MinDeposit { get; set; }
    }

    public enum AccountTypes {
        Checking,
        Savings
    }
    public abstract class Account
    {
        public string AccountNumber { get; set;}

        public List<Transaction> Transactions = new List<Transaction>();

        public AccountTypes AccountType { get; set; }

        // Constructor for setting account type and define the properties
        public Account(AccountTypes accountType){
            this.Transactions = new List<Transaction>();
            this.AccountType = accountType;

            // The registation number is different from the account types
            string registrationNumber = "";
            if (AccountType == AccountTypes.Checking)
                registrationNumber = "1000";
            else if (AccountType == AccountTypes.Savings)
                registrationNumber = "2000";
            
            // Generate the random bank number
            Random random = new Random();
            string accountNumber = random.Next(10000000, 99999999).ToString();

            // Set the account number
            this.AccountNumber = $"{registrationNumber} {accountNumber}";
        }

        public double GetBalance(){
            double total = 0.0;

            foreach (Transaction transaction in Transactions){
                switch (transaction.TransactionType) {
                    case TransactionTypes.Deposit:
                        total += transaction.Amount;
                        break;

                    case TransactionTypes.Withdraw:
                        total -= transaction.Amount;
                        break;

                    case TransactionTypes.Send:
                        total -= transaction.Amount;
                        break;

                    case TransactionTypes.Receive:
                        total += transaction.Amount;
                        break;

                    case TransactionTypes.Interest:
                        total += transaction.Amount;
                        break;
                }
            }
            return Math.Round(total, 2);
        }

        public override string ToString()
        {
            return $"{this.AccountType} - {this.AccountNumber}";
        }

        public abstract bool CreateTransaction(TransactionTypes transactionType, double amount);
        public abstract bool CreateTransaction(TransactionTypes transactionType, double amount,  ref Account receiver, ref Account sender);
    }
}
