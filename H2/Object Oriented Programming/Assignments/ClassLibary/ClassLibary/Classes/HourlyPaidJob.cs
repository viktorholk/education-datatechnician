using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace ClassLibrary
{
    public class HourlyPaidJob : Job, IJobHours
    {
        public double Hours { get; set; }
        private double hourlyPay;

        public HourlyPaidJob(double hourlyPay)
        {
            this.hourlyPay = hourlyPay;
        }
        
        public void AddHours(double hours){
            this.Hours += hours;
        }

        public override double GetMonthlyPay()
        {
            return this.Hours * this.hourlyPay;
        }
    }
}
