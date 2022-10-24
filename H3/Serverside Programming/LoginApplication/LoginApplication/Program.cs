var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/authenticate", async (context) =>
{
    string username = context.Request.Query["username"].ToString() ?? "";
    string password = context.Request.Query["password"].ToString() ?? "";

    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
    {
        await context.Response.WriteAsync("Missing username or password parameters");
        return;
    }


    var availableLogins = new Dictionary<string, string>()
    {
        {"sample", "password" },
        {"admin", "admin" },
    };

    bool validLogin = false;
    foreach(var login in availableLogins)
    {
        if (login.Key == username && login.Value == password)
            validLogin = true;
    }

    if (validLogin)
        context.Response.Redirect("/content.html");
    else
        context.Response.Redirect("/401.html");
});

app.UseMiddleware<LoginApplication.DebugMiddleware>();
app.UseStaticFiles();
app.Run();

