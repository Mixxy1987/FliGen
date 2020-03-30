using FliGen.Services.Leagues.Domain.Entities;
using FliGen.Services.Leagues.Domain.Entities.Enum;
using FliGen.Services.Leagues.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FliGen.Services.Leagues.Persistence.Contextes
{
    public class LeaguesContext : DbContext
    {
        public DbSet<LeaguePlayerRole> LeaguePlayerRoles { get; set; }
        public DbSet<LeagueType> LeagueTypes { get; set; }

        public DbSet<League> Leagues { get; set; }
        public DbSet<LeaguePlayerLink> LeaguePlayerLinks { get; set; }
        public DbSet<LeagueSettings> LeagueSettings { get; set; }
        public LeaguesContext(DbContextOptions<LeaguesContext> options) :base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeagueConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
