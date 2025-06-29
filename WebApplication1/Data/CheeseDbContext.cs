using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Domain;

namespace WebApplication1.Data
{
    public class CheeseDbContext : DbContext
    {
        public CheeseDbContext(DbContextOptions<CheeseDbContext> dbContextOptions)
            : base(dbContextOptions) { }

        public DbSet<Cheese> Cheeses { get; set; }
    }
}