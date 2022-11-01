using Microsoft.EntityFrameworkCore;
using MVCAppEntityFramework.Models;

public class DatabaseContext : DbContext
{

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookModel>().HasData(
            new BookModel { Id = 1, ISBN = "978-3-16-148410-0", Title = "Holk Smash 4", Author = "Viktor", Publisher = "freePublishNet", Description = "This book is about cooking.", Category = "Cooking", TotalPages = 180, Price = 299.9 },
            new BookModel { Id = 2, ISBN = "978-3-16-148410-0", Title = "How to build a sinking boat", Author = "SAS", Publisher = "SAS Publishers", Description = "This book is about airplanes", Category = "Airplanes", TotalPages = 10, Price = 100 },
            new BookModel { Id = 3, ISBN = "978-3-16-148410-0", Title = "Keep Drinking Alcohol", Author = "Homeless Ben", Publisher = "Homeless Ben", Description = "How drinking keeps me sane", Category = "Aerodynamics", TotalPages = 200, Price = 599.9 },
            new BookModel { Id = 4, ISBN = "978-3-16-148410-0", Title = "Why HTML is a programming language", Author = "Richard", Publisher = "StackOverflow", Description = "No fact checks", Category = "Comedy", TotalPages = 180, Price = 299.9 }
        );
    }

    public DbSet<MVCAppEntityFramework.Models.BookModel>? BookModel { get; set; }

}
