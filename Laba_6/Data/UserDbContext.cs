using Laba_6.Entities;
using Microsoft.EntityFrameworkCore;

namespace Laba_6.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
