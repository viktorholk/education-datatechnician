using Microsoft.EntityFrameworkCore;
namespace RestApplication.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Person> People { get; set; }
    }
}
