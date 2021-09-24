using System;
using System.Threading.Tasks;
namespace BankSystemLib
{
    public enum TransactionTypes {
        Withdraw,
        Deposit
    }


    public class Transaction
    {
        public DateTime Date { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public Task Job { get; set; }

        public double Amount { get; set;}

        public Transaction(TransactionTypes transactionType, double amount){
            this.Date = DateTime.Now;
            this.TransactionType = transactionType;
            // this.Sender = sender;
            this.Amount = amount;



        }

        private void CreateTaskJob(){
            switch (this.TransactionType)
            {
                case TransactionTypes.Deposit:

                    break;

                case TransactionTypes.Withdraw:
                        // If there is a receiver we want to withdraw the money to that user account
                    break;

                default:
                    break;
            }
        }






    }
}
