using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class CheeseAuthDbContext : IdentityDbContext
    {
        public CheeseAuthDbContext(DbContextOptions<CheeseAuthDbContext> dbContextOptions)
            : base(dbContextOptions) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var adminId = "4b4261c3-20c9-4118-93aa-e89fc1bc4df6";
            var userId = "688cb143-bfd9-479c-a22a-833780e4f5b0";

            var roles = new List<IdentityRole>
            {
                new IdentityRole { Id = adminId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = userId, Name = "User", NormalizedName = "USER" }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}