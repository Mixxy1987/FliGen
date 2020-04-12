using FliGen.Services.Teams.Domain.Entities;
using FliGen.Services.Teams.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FliGen.Services.Teams.Persistence.Contexts
{
    public class TeamsContext : DbContext
    {
        public DbSet<TeamRole> TeamRoles { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamPlayerLink> TeamPlayerLinks { get; set; }
        public DbSet<TemporalTeamPlayerLink> TemporalTeamPlayerLinks { get; set; }

        public TeamsContext(DbContextOptions<TeamsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TeamConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
