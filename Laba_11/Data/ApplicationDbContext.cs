using Laba_11.Models;
using Microsoft.EntityFrameworkCore;

namespace Laba_11.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Bird> Birds { get; set; }
        public DbSet<EcologicalNiche> EcologicalNiches { get; set; }
        public DbSet<BirdEcologicalNiche> BirdEcologicalNiches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BirdEcologicalNiche>(x => x.HasKey(p => new { p.BirdId, p.EcologicalNicheId }));

            modelBuilder.Entity<BirdEcologicalNiche>()
                .HasOne(x => x.Bird)
                .WithMany(x => x.BirdEcologicalNiches)
                .HasForeignKey(x => x.BirdId);

            modelBuilder.Entity<BirdEcologicalNiche>()
                .HasOne(x => x.EcologicalNiche)
                .WithMany(x => x.BirdEcologicalNiches)
                .HasForeignKey(x => x.EcologicalNicheId);
        }
    }

}
