using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BankSystemLib
{
    public enum AccountTypes {
        Checking = 1,
        Savings
    }
    [Table("accounts")]
    public class Account : SQLObject
    {
        [Column("user_id")]
        public User User { get; set; }
        [Column("account_number")]
        public string AccountNumber { get; set;}
        [Column("account_type")]
        public AccountTypes AccountType { get; set; }

        // Constructor for setting account type and define the properties
        public Account(User user, AccountTypes accountType){
            this.User = user;
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

        public Account(int id, int userId, string accountNumber, int accountType) : base(id) {
            this.Id = id;
            this.User = Registry.GetUser(userId);
            this.AccountNumber = accountNumber;
            this.AccountType = (AccountTypes)accountType;
        }


        public double GetBalance() {
            double total = 0.0;

            List<Transaction> transactions = Registry.GetAccountTransactions(this.Id);

            for (int i = 0; i < transactions.Count; i++)
            {
                TransactionTypes type   = transactions[i].TransactionType;
                double amount           = transactions[i].Amount;

                switch (type) {
                    case TransactionTypes.Deposit:
                        total += amount;
                        break;

                    case TransactionTypes.Withdraw:
                        total -= amount;
                        break;

                    case TransactionTypes.Send:
                        total -= amount;
                        break;

                    case TransactionTypes.Receive:
                        total += amount;
                        break;

                    case TransactionTypes.Interest:
                        total += amount;
                        break;
                }
            }
            return Math.Round(total, 2);
        }


        public bool CreateTransaction(TransactionTypes transactionType, double amount, int receiverAccountId = -1){
            if (transactionType == TransactionTypes.Withdraw || transactionType == TransactionTypes.Send) {
                if (amount > GetBalance()){
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Insufficient balance");
                    Console.ResetColor();
                    return false;
                }
            }

            Transaction transaction = new Transaction(this, transactionType, amount);
            transaction.Save();

            // If there is a receiverAccountId then we will create an transacion on the other account
            if (receiverAccountId > 0) {
                Transaction receiverTransaction = new Transaction(Registry.GetAccount(receiverAccountId), TransactionTypes.Receive, amount);
                receiverTransaction.Save();
            }

            return true;
        }

        public override string ToString()
        {
            return $"{this.AccountType} - {this.AccountNumber}";
        }
    }
}
