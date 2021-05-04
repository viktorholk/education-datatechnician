using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Warehouse_System.Classes.Application
{
    class Status : ApplicationContext
    {
        public string Message { get; set; }
        public StatusCodes Code { get; set; }

        public Status(string message, StatusCodes code)
        {
            this.Message = message;
            this.Code = code;
        }
    }
    class StatusHandler : ApplicationContext
    {
        public static List<Status> StatusLogs = new List<Status>();
        private static ConsoleColor GetColor(StatusCodes code)
        {
            return code switch
            {
                StatusCodes.INFO => InfoColor,
                StatusCodes.SUCCESS => SuccessColor,
                StatusCodes.ERROR => ErrorColor,
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
        public static void Write(string message, StatusCodes code)
        {
            // Initialize status object
            Status status = new Status(message, code);
            StatusLogs.Add(status);
            // Get current cursor so we can revert
            int previousLeft = Console.CursorLeft;
            int previousTop = Console.CursorTop;

            // Set the color of the code
            Console.ForegroundColor = GetColor(status.Code);
            Console.SetCursorPosition(0, 0);
            // Erase the status line so we dont get fragments of other status codes
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"STATUS: {status.Message}");
            // Reset foregroundColor
            Console.ResetColor();
            // Rewert cursor
            Console.SetCursorPosition(previousLeft, previousTop);
        }
    }
}
