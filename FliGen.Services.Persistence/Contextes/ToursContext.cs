using FliGen.Services.Tours.Domain.Entities;
using FliGen.Services.Tours.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FliGen.Services.Tours.Persistence.Contextes
{
    public class ToursContext : DbContext
    {
        public DbSet<TeamRole> TeamRoles { get; set; }
        public DbSet<TourStatus> TourStatuses { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamPlayerLink> TeamPlayerLinks { get; set; }
        public DbSet<Tour> Tours { get; set; }

        public ToursContext(DbContextOptions<ToursContext> options) :base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TeamConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
