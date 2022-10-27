using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Document>()
                .HasOne<Client>(i => i.Client)
                .WithMany(i => i.Documents)
                .HasForeignKey(i => i.ClientId);


            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 1,
                    Username = "Viktor"
                },
                new Client
                {
                    Id = 2,
                    Username = "Hugo"
                }
                );
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}
