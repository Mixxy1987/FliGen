using Fligen.Domain.Entities;
using FliGen.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FliGen.Persistence.Contextes
{
    public class FliGenContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public FliGenContext(DbContextOptions<FliGenContext> options) :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
        }
    }
}
