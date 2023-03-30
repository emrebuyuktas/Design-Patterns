using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ObserverPattern.Models
{
    public class AppIdenitytDbContext : IdentityDbContext<AppUser>
    {
        public AppIdenitytDbContext(DbContextOptions<AppIdenitytDbContext> options) : base(options)
        {

        }
    }
}
