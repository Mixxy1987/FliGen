using FliGen.Domain.Common;
using FliGen.Domain.Entities;
using FliGen.Domain.Entities.Enum;
using FliGen.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Persistence.Contextes
{
    public class FliGenContext : DbContext, IUnitOfWork
    {
        public DbSet<League> Leagues { get; set; }
        public DbSet<LeagueType> LeagueTypes { get; set; }
        public DbSet<LeagueSeasonLink> LeagueSeasonLinks { get; set; }
        public DbSet<LeaguePlayerLink> LeaguePlayerLinks { get; set; }
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
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlayerConfiguration).Assembly);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            //await _mediator.DispatchDomainEventsAsync(this); //todo:: ?

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
