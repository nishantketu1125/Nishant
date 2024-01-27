using API.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppUserData : DbContext
    {
        public AppUserData(DbContextOptions options) : base(options)
        {

        }
        public DbSet<AppUser> Users { get; set; }
    }
}

