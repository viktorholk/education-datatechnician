using Microsoft.EntityFrameworkCore;
using API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Setup database
var connectionString = builder.Configuration.GetConnectionString("sqlite");
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlite(connectionString);
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
