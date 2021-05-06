using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Warehouse_System.Classes.Application
{
    class Status : ApplicationContext
    {
        public string Message { get; set; }
        public StatusHandler.Codes Code { get; set; }

        public Status(string message, StatusHandler.Codes code)
        {
            this.Message = message;
            this.Code = code;
        }
    }
    class StatusHandler : ApplicationContext
    {
        public enum Codes
        {
            INFO,
            SUCCESS,
            ERROR
        }
        public static List<Status> StatusLogs = new List<Status>();
        private static ConsoleColor GetColor(Codes code)
        {
            return code switch
            {
                Codes.INFO => InfoColor,
                Codes.SUCCESS => SuccessColor,
                Codes.ERROR => ErrorColor,
                _ => ConsoleColor.White,
            };
        }
        public static void WritePrevious()
        {
            if (StatusLogs.Count > 0)
            {
                Status previous = StatusLogs.Last();
                Write(previous.Message, previous.Code);
            }
        }
        public static void Write(string message, Codes code)
        {
            // Initialize status object
            Status status = new Status(message, code);
            StatusLogs.Add(status);
            // Get current cursor so we can revert
            int previousLeft = Console.CursorLeft;
            int previousTop = Console.CursorTop;

            // Get the previous foregroundColor so we can revert it
            ConsoleColor previousColor = Console.ForegroundColor;

            // Set the color of the code
            Console.ForegroundColor = GetColor(status.Code);
            Console.SetCursorPosition(0, 0);
            // Erase the status line so we dont get fragments of other status codes
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"STATUS: {status.Message}");
            // Reset foregroundColor
            Console.ForegroundColor = previousColor;
            // Rewert cursor
            Console.SetCursorPosition(previousLeft, previousTop);
        }
    }
}
