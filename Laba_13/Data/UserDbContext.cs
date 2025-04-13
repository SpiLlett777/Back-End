using Laba_13.Entities;
using Microsoft.EntityFrameworkCore;

namespace Laba_13.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
