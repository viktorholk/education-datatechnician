using System;
using System.Threading.Tasks;
namespace BankSystemLib
{
    public class CheckingAccount : Account, IBankInterest
    {
        public double Interest { get; set;}

        public Task InterestTask { get; set; }

        // Set the type of account to checking by calling the parent constructor
        public CheckingAccount(double interest) : base(AccountTypes.Checking){
            this.Interest = interest;

            // Start the interest task
            this.InterestTask = new Task(async () => {
                while (true)
                {
                    if (GetBalance() > 0){
                        // Calculate the interest
                        double interestAmount = GetBalance() * this.Interest;
                        // Wait the delay and add the interst amount to the balance
                        // System.Console.WriteLine($"{GetBalance()} - {interestAmount}");
                        CreateTransaction(TransactionTypes.Interest, interestAmount);
                        await Task.Delay(10000);
                    }
                }
            });

            this.InterestTask.Start();
        }

        public override bool CreateTransaction(TransactionTypes transactionType, double amount){
            if (transactionType == TransactionTypes.Withdraw) {
                if (amount > GetBalance()){
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Insufficient balance");
                    Console.ResetColor();
                    return false;
                }
            }

            this.Transactions.Add(new Transaction(transactionType, amount));

            return true;
        }


        public override bool CreateTransaction(TransactionTypes transactionType, double amount, ref Account sender, ref Account receiver){
            if (transactionType == TransactionTypes.Send) {
                if (amount > GetBalance()){
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Insufficient balance");
                    Console.ResetColor();
                    return false;
                }
                this.Transactions.Add(new Transaction(TransactionTypes.Send, amount, sender, receiver));
                receiver.Transactions.Add(new Transaction(TransactionTypes.Receive, amount, sender, receiver));
                return true;
            }
            return false;
        }
    }
}
