using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class User
    {
        public string  login;
        public string  password;
        private Job     job;

        public User(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        public User(string login, string password, Job job)
        {
            this.login = login;
            this.password = password;
            this.job = job;
        }

        public void StartJob(){
            System.Console.WriteLine($"{this} Starting job");
            this.job.GetTask().Start();
        }

        public void SetJob(Job job){
            this.job = job;
        }

        public bool IsAdmin()
        {
            return this is Admin;
        }


        public override string ToString()
        {
            return base.ToString() + " " + login;
        }
    }
}
