using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
namespace ATM_Project
{
    class Logger
    {
        static public void Log(string logMessage, string printMessage = "")
        {
            // Print the printMessage if there is one
            if (printMessage != "")
                Console.WriteLine(printMessage);


            string path = @"ATM-Project.log";
            // If the file doesnt already exist write a header
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine($"The log file for the ATM-Project");
                }
            }

            using (StreamWriter sw = File.AppendText(path))
            {
                // Get the date for the log
                var dateTime = DateTime.Now;
                string dateFormatted = String.Format("{0:G}", dateTime);
                sw.WriteLine($"{dateFormatted} : {logMessage}");
            }

        }
    }
}
