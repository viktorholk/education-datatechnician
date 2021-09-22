using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace ClassLibrary
{
    public class DailyPaidJob : Job, IJobDays
    {
        public int Days {get; set;}
        private double dailyPay;

        public DailyPaidJob(double dailyPay)
        {
            this.dailyPay = dailyPay;
        }

        public void AddDays(int days){
            this.Days += days;
        }

        public override double GetMonthlyPay()
        {
            
            return this.Days * this.dailyPay; 
        }
    }
}
