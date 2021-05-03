using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Warehouse_System.Classes.Application
{
    class Status : ApplicationContext
    {
        private ConsoleColor GetColor()
        {
            return this.Code switch
            {
                StatusCodes.INFO => InfoColor,
                StatusCodes.SUCCESS => SuccessColor,
                StatusCodes.ERROR => ErrorColor,
                _ => ConsoleColor.White,
            };
        }

        public static List<Status> StatusLogs = new List<Status>();

        public static void WritePrevious()
        {
            if (StatusLogs.Count > 0)
            {
                Status previous = StatusLogs.Last();
                previous.Write();

            }
        }
        public void Write()
        {
            StatusLogs.Add(this);
            // Get current cursor so we can revert
            int previousLeft = Console.CursorLeft;
            int previousTop = Console.CursorTop;

            // Set the color of the code
            Console.ForegroundColor = GetColor();
            Console.SetCursorPosition(0, 0);
            // Erase the status line so we dont get fragments of other status codes
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"STATUS: {Message}");
            // Reset foregroundColor
            Console.ResetColor();
            // Rewert cursor
            Console.SetCursorPosition(previousLeft, previousTop);
        }
        public enum StatusCodes
        {
            INFO,
            SUCCESS,
            ERROR
        }

        public StatusCodes Code;
        public string Message;


        public Status(string message, StatusCodes code)
        {
            this.Message = message;
            this.Code = code;

        }

    }
    class ApplicationContext
    {
        protected readonly ConsoleColor InfoColor = ConsoleColor.Yellow;
        protected readonly ConsoleColor SuccessColor = ConsoleColor.Green;
        protected readonly ConsoleColor ErrorColor = ConsoleColor.Red;


        protected void WriteColor(string message, ConsoleColor color, bool newLine = true)
        {
            Console.ForegroundColor = color;
            if (newLine)
                Console.WriteLine(message);
            else
                Console.Write(message);
            Console.ResetColor();

        }



    }
}
