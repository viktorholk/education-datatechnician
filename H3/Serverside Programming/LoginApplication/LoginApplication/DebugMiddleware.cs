using System;
namespace LoginApplication
{
    public class DebugMiddleware
    {
        private RequestDelegate _next;

        public DebugMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;

            var ipAddress = context.Request.HttpContext.Connection.LocalIpAddress;

            var port = context.Request.HttpContext.Connection.LocalPort;


            DateTime dateTime = DateTime.Now;

            using (StreamWriter writer = new StreamWriter("log.txt", append: true))
            {
                await writer.WriteAsync($"{dateTime.ToString("g")} - {path.ToString()} - {(ipAddress.ToString() == "::1" ? "localhost" : ipAddress.ToString())}:{port.ToString()}\n");
            }

            await _next(context);

        }
    }
}

