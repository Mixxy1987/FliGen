using Fligen.Domain.Entities;
using FliGen.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FliGen.Persistence.Contextes
{
    public class FliGenContext : DbContext
    {
        public DbSet<League> Leagues { get; set; }
        public DbSet<LeagueSeasonLink> LeagueSeasonLinks { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerRate> PlayerRates { get; set; }
        public DbSet<PlayerRatePlayerLink> PlayerRatePlayerLinks { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<SeasonTourLink> SeasonTourLinks { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamPlayerLink> TeamPlayerLinks { get; set; }
        public DbSet<Tour> Tours { get; set; }

        public FliGenContext(DbContextOptions<FliGenContext> options) :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlayerConfiguration).Assembly);
        }
    }
}
