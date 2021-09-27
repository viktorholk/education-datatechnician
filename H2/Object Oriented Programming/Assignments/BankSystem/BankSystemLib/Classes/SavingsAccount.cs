using System;
using System.Threading.Tasks;
namespace BankSystemLib
{
    public class SavingsAccount : Account, IBankInterest, IBankMinimum
    {
        public double Interest { get; set; }
        public double MinBalance { get; set;}
        public double MinDeposit { get; set;}
        public SavingsAccount(double interest, double minBalance, double minDeposit) : base(AccountTypes.Savings) {
            this.Interest = interest;
            this.MinBalance = minBalance;
            this.MinDeposit = minDeposit;

            // Start the interest task
            Task.Run(async () => {
                while (true)
                {
                    if (GetBalance() > 0){
                        // Calculate the interest
                        double interestAmount = GetBalance() * this.Interest;
                        // Wait the delay and add the interst amount to the balance
                        // System.Console.WriteLine($"{GetBalance()} - {interestAmount}");
                        CreateTransaction(TransactionTypes.Interest, interestAmount);
                        await Task.Delay(20000);
                    }
                }
            });

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

        
        public async Task PayInterest(int delay) {
            while (true){
                if (GetBalance() > 0){
                    // Calculate the interest
                    double interestAmount = GetBalance() * this.Interest;
                    // Wait the delay and add the interst amount to the balance
                    await Task.Delay(delay);
                    CreateTransaction(TransactionTypes.Interest, interestAmount);
                }
            }
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
