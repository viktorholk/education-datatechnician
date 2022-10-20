namespace Context
{

    /// <summary>
    /// Status Class
    /// This is the object that there is going to be instantiated everytime we create a new status message
    /// We do it with objects because if we want to rewert to the latest statusmessage after we have cleared the console we can retrive the status.
    /// </summary>
    class Status
    {
        public string Message;
        public ApplicationContext.Status StatusCode;

        public string TimeString;

        /// <summary>
        /// Sets the message and statuscode of the object
        /// </summary>
        /// <param name="message">Status message</param>
        /// <param name="code">Status code</param>
        public Status(string message, ApplicationContext.Status statusCode)
        {
            this.Message = message;
            this.StatusCode = statusCode;

            this.TimeString = DateTime.Now.ToString("HH:mm:ss");
        }
    }
    /// <summary>
    /// StatusHandler abstract class
    /// This class handles the status messages whenever we want to write a new status message 
    /// It can not be instantiated since this just handles the status messages
    /// </summary>

    abstract class StatusHandler
    {
        public static List<Status> StatusLogs = new List<Status>();

        /// <summary>
        /// Write the previous status message
        /// This is used when we clear the console application for text
        /// </summary>
        public static void WritePrevious()
        {
            if (StatusLogs.Count > 0)
            {
                Status previous = StatusLogs.Last();
                Write(previous.Message, previous.StatusCode, true);
            }
        }
        /// <summary>
        /// Writes a status message to the top left of the application
        /// 
        /// </summary>
        /// <param name="message">Status message</param>
        /// <param name="code">The statuscode</param>
        public static void Write(string message, ApplicationContext.Status statusCode, bool instantiated = false)
        {

            // Get current cursor so we can revert
            int previousLeft = Console.CursorLeft;
            int previousTop = Console.CursorTop;

            // Get the previous foregroundColor so we can revert it
            ConsoleColor previousColor = Console.ForegroundColor;

            // Set the color of the code
            Console.ForegroundColor = ApplicationContext.StatusColors[statusCode];
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
                var status = new Status(message, statusCode);
                StatusLogs.Add(status);
            }
        }
    }
}

