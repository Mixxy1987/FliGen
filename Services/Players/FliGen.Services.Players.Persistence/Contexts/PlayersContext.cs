using FliGen.Services.Players.Domain.Entities;
using FliGen.Services.Players.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FliGen.Services.Players.Persistence.Contexts
{
    public class PlayersContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerRate> PlayerRates { get; set; }

        public PlayersContext(DbContextOptions<PlayersContext> options) :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlayerConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
