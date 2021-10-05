using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace BankSystemLib
{
    public enum TransactionTypes {
        Deposit = 1,
        Withdraw,
        Send,
        Receive,
        Interest
    }


    [Table("transactions")]
    public class Transaction : SQLObject
    {
        [Column("account_id")]
        public Account Account { get; set; }

        public DateTime Date { get; set; }

        [Column("transaction_type")]
        public TransactionTypes TransactionType { get; set; }

        public double Amount { get; set;}

        public Transaction(Account account, TransactionTypes transactionType, double amount){
            this.Account = account;
            this.TransactionType    = transactionType;
            this.Date = DateTime.Now;
            this.Amount = amount;
        }

        public Transaction(int id, int accountId, int transactionType, DateTime date, double amount){
            this.Id = id;
            this.Account = Registry.GetAccount(accountId);
            this.Date = date;
            this.TransactionType = (TransactionTypes)transactionType;
            this.Amount = amount;
        }

        public void Print(){
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write($"     {this.Date.ToString("HH:MM:ss")} ");
            System.Console.Write($"{this.TransactionType} ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine($"${Math.Round((double)this.Amount, 2)}");
            Console.ResetColor();
        }

        public override string ToString()
        {
            return $"{this.Date}, {this.TransactionType} {this.Amount}";
        }
    }
}
