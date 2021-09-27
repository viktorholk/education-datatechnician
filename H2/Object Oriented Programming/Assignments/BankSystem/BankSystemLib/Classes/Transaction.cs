using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace BankSystemLib
{
    public enum TransactionTypes {
        Withdraw,
        Deposit,
        Send,
        Receive,
        Interest
    }


    public class Transaction
    {
        public Account Sender { get; set; }

        public Account Receiver { get; set; }

        public DateTime Date { get; set; }

        public TransactionTypes TransactionType { get; set; }

        public double Amount { get; set;}


        public Transaction(TransactionTypes transactionType, double amount){
            this.Date = DateTime.Now;
            this.TransactionType = transactionType;
            this.Amount = amount;

            // Create the transaction job
        }

        public Transaction(TransactionTypes transactionType, double amount, Account sender, Account receiver) : this(transactionType, amount){
            this.Sender = sender;
            this.Receiver = receiver;
        }
        public void Print(){
            System.Console.Write($"     {this.Date.ToString("HH:MM:ss")} ");

            if (this.TransactionType == TransactionTypes.Send || this.TransactionType == TransactionTypes.Receive){
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.Write($"{((this.Sender == null) ? "null" : this.Sender) } ");
                Console.ResetColor();
                System.Console.Write(" -> ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.Write($"{((this.Receiver == null) ? "null" : this.Receiver)}, ");
                Console.ResetColor();
                System.Console.Write($"{this.TransactionType} ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.WriteLine($"${Math.Round((double)this.Amount, 2)}");
                Console.ResetColor();

            } else {
                System.Console.Write($"{this.TransactionType} ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.WriteLine($"${Math.Round((double)this.Amount, 2)}");
                Console.ResetColor();
            }

        }
    }
}
