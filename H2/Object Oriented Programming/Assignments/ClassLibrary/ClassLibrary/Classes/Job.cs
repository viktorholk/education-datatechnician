using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace ClassLibrary
{
    interface IJobHours {
        double Hours { get; set; }

        void AddHours(double hours);
    }

    interface IJobDays {
        int Days { get; set; }

        void AddDays(int days);
    }

    public abstract class Job
    {
        private Task JobTask  { get; set; }

        public void CreateTask(Action action){
            this.JobTask = new Task(action);
        }
        public Task GetTask(){
            return this.JobTask;
        }
        public abstract double GetMonthlyPay();
    }
}
