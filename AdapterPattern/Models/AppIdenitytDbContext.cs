using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdapterPattern.Models
{
    public class AppIdenitytDbContext : IdentityDbContext<AppUser>
    {
        public AppIdenitytDbContext(DbContextOptions<AppIdenitytDbContext> options) : base(options)
        {

        }
    }
}
