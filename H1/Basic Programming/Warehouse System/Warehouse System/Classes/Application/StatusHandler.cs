using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Warehouse_System.Classes.Application
{
    /// <summary>
    /// Status Class
    /// This is the object that there is going to be instantiated everytime we create a new status message
    /// We do it with objects because if we want to rewert to the latest statusmessage after we have cleared the console we can retrive the status.
    /// </summary>
    class Status : ApplicationContext
    {
        public string Message { get; set; }
        public StatusHandler.Codes Code { get; set; }

        private string timeString;
        public string TimeString
        {
            get
            {
                return timeString;
            }
            private set
            {
                this.timeString = value;
            }
        }

        /// <summary>
        /// Sets the message and statuscode of the object
        /// </summary>
        /// <param name="message">Status message</param>
        /// <param name="code">Status code</param>
        public Status(string message, StatusHandler.Codes code)
        {
            this.Message = message;
            this.Code = code;

            this.TimeString = DateTime.Now.ToString("HH:mm:ss");
            StatusHandler.StatusLogs.Add(this);
        }
    }
    /// <summary>
    /// StatusHandler abstract class
    /// This class handles the status messages whenever we want to write a new status message 
    /// It can not be instantiated since this just handles the status messages
    /// </summary>
    abstract class StatusHandler : ApplicationContext
    {
        public enum Codes
        {
            INFO,
            SUCCESS,
            ERROR
        }
        public static List<Status> StatusLogs = new List<Status>();
        /// <summary>
        /// Get the console color from the StatusCode provided
        /// </summary>
        /// <param name="code">StatusHandler Code</param>
        /// <returns>ConsoleColor</returns>
        public static ConsoleColor GetColor(Codes code)
        {

            return code switch
            {
                Codes.INFO => InfoColor,
                Codes.SUCCESS => SuccessColor,
                Codes.ERROR => ErrorColor,
                _ => ConsoleColor.White,
            };
        }
        /// <summary>
        /// Write the previous status message
        /// This is used when we clear the console application for text
        /// </summary>
        public static void WritePrevious()
        {
            if (StatusLogs.Count > 0)
            {
                Status previous = StatusLogs.Last();
                Write(previous.Message, previous.Code, true);
            }
        }
        /// <summary>
        /// Writes a status message to the top left of the application
        /// 
        /// </summary>
        /// <param name="message">Status message</param>
        /// <param name="code">The statuscode</param>
        public static void Write(string message, Codes code, bool instantiated = false)
        {

            // Get current cursor so we can revert
            int previousLeft = Console.CursorLeft;
            int previousTop = Console.CursorTop;

            // Get the previous foregroundColor so we can revert it
            ConsoleColor previousColor = Console.ForegroundColor;

            // Set the color of the code
            Console.ForegroundColor = GetColor(code);
            Console.SetCursorPosition(0, 0);
            // Erase the status line so we dont get fragments of other status codes
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"STATUS: {message}");
            // Reset foregroundColor
            Console.ForegroundColor = previousColor;
            // Rewert cursor
            Console.SetCursorPosition(previousLeft, previousTop);

            // Initialize status object
            if (!instantiated)
            {
                new Status(message, code);
            }
        }
    }
}
