using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}
