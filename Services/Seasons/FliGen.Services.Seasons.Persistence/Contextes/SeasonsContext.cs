using FliGen.Services.Seasons.Domain.Entities;
using FliGen.Services.Seasons.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FliGen.Services.Seasons.Persistence.Contextes
{
    public class SeasonsContext : DbContext
    {
        public DbSet<Season> Seasons { get; set; }
        public SeasonsContext(DbContextOptions<SeasonsContext> options) :base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SeasonConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
