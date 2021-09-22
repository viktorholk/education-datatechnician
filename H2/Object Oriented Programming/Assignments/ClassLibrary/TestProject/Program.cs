using System;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Registry registry = new Registry();

            registry.AddUser(new User[]{
                new Admin("admin",  "password"),
                new Admin("admin2", "password"),
                new User("user1", "password"),
                new User("user3", "password"),
                new User("Test User", "password2"),
            });

            System.Console.WriteLine($"REGISTRY: USERS: {registry.GetUsers().Count(i => i.IsAdmin() == false)}, ADMINS: {registry.GetUsers().Count(i => i.IsAdmin() == true)}");
            // Print all available users
            registry.GetUsers().ForEach(i => System.Console.WriteLine(i));
            System.Console.WriteLine();

            // Create some sample jobs
            HourlyPaidJob hourlyPaidJob = new HourlyPaidJob(200);
            // Create a task for the job
            // Work for 8 hours
            hourlyPaidJob.CreateTask(async () =>  {
                for (int i = 1; i <= 8; i++)
                {
                    System.Console.WriteLine($"      HourlyPaidJob Hour: {i}");
                    await Task.Delay(250);
                    // Add an hour to the job
                    hourlyPaidJob.AddHours(1);
                }
                // Print the Monthly Payout
                System.Console.WriteLine($"      GetMonthlyPay: {hourlyPaidJob.GetMonthlyPay()}");
            });
            // 1600 pay each day
            DailyPaidJob dailyPaidJob = new DailyPaidJob(1600);
            // Create a task for the job
            // Work for 20 days
            dailyPaidJob.CreateTask(async () => {
                for (int i = 0; i < 20; i++)
                {
                    System.Console.WriteLine($"         DailyPaidJob Day: {i}");
                    await Task.Delay(1000);
                    // Add an hour to the job
                    dailyPaidJob.AddDays(1);
                }
                // Print the Monthly Payout
                System.Console.WriteLine($"         GetMonthlyPay: {dailyPaidJob.GetMonthlyPay()}");
            });



            // Get the testuser from the authentication from the registry
            var user_1 = registry.AuthenticateUser("user1", "password");
            if (user_1 != null){
                // Give the user a job
                user_1.SetJob(hourlyPaidJob);

                // Start the user job
                user_1.StartJob();
            }

            // Create a new user
            // Pass the dailypaid job to the constructor instead
            registry.AddUser(new User("user2", "password", dailyPaidJob));
            // Authenticate another user
            var user_2 = registry.AuthenticateUser("user2", "password");
            if (user_2 != null){
                // Start the user job
                user_2.StartJob();
            }


            // Demostrate the chaning of a user password with a admin user

            // Create the user and add it to the registry
            var sampleUser = new User("sample_password_user", "secretpassword");
            registry.AddUser(sampleUser);
            System.Console.WriteLine($"{sampleUser} OLD PASSWORD: {sampleUser.password}");
            // Login into an admin account and change the user password

            var admin = (Admin)registry.AuthenticateUser("admin", "password");
            System.Console.WriteLine(admin);
            if (admin != null){
                // Change the password
                admin.ChangeUserPassword(sampleUser, "$$$$ADMIN CHANGED PASSWORD$$$$");

            }
            System.Console.WriteLine($"{sampleUser} NEW PASSWORD: {sampleUser.password}");

            // Wait for application
            Console.ReadLine();
        }

    }
}
