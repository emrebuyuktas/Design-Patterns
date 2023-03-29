using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommandPattern.Models
{
    public class AppIdenitytDbContext : IdentityDbContext<AppUser>
    {
        public AppIdenitytDbContext(DbContextOptions<AppIdenitytDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
