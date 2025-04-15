using Laba_7.Entities;
using Microsoft.EntityFrameworkCore;

namespace Laba_7.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
