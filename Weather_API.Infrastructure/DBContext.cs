using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Weather_API.Domain.Models;

namespace Weather_API.Infrastructure
{
    public class Web_APIDbContext : IdentityDbContext<AppUser>
    {
        public Web_APIDbContext(DbContextOptions<Web_APIDbContext> options) : base(options) { }
        public DbSet<AppUser> User { get; set; }
    }
}
