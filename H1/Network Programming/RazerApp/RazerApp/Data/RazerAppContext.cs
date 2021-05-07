using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazerApp.Models;

namespace RazerApp.Data
{
    public class RazerAppContext : DbContext
    {
        public RazerAppContext (DbContextOptions<RazerAppContext> options)
            : base(options)
        {
        }

        public DbSet<RazerApp.Models.Movie> Movie { get; set; }
    }
}
