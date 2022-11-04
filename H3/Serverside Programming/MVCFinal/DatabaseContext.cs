using Microsoft.EntityFrameworkCore;
using MVCFinal.Models;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Cases)
            .WithOne(c => c.Product);

        modelBuilder.Entity<Case>()
            .HasOne(c => c.Client);

        modelBuilder.Entity<Case>()
            .HasMany(c => c.Resources)
            .WithOne(r => r.Case);

        modelBuilder.Entity<Client>().HasData(
            new Client { Id = 1, Username = "Viktor" },
            new Client { Id = 2, Username = "Sigurd" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Sample Product", Description = "This is just a sample product" },
            new Product { Id = 2, Name = "The Cooler Product", Description = "Cool product man" }
        );

        modelBuilder.Entity<Case>().HasData(
            new Case { Id = 1, Type = CaseType.Bug, Description = "Uncool the cool product please", ProductId = 2, ClientId = 1 },
            new Case { Id = 2, Type = CaseType.Feature, Description = "Make this sample product into a real one", ProductId = 1, ClientId = 2 }
        );

        modelBuilder.Entity<ResourceTask>().HasData(
            new ResourceTask { Id = 1, Status = Status.Completed, Description = "Uncool the product with barbeque tongs", CaseId = 1 },
            new ResourceTask { Id = 2, Status = Status.Ongoing, Description = "Finalizations", CaseId = 1 },
            new ResourceTask { Id = 3, Status = Status.Pending, Description = "Do the Magic Mike", CaseId = 2 }
        );

    }


    public DbSet<MVCFinal.Models.Client>? Client { get; set; }

    public DbSet<MVCFinal.Models.Case>? Case { get; set; }

    public DbSet<MVCFinal.Models.Product>? Product { get; set; }

    public DbSet<MVCFinal.Models.ResourceTask>? ResourceTask { get; set; }
}
