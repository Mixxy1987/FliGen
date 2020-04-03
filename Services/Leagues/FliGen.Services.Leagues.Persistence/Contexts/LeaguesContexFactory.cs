using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FliGen.Services.Leagues.Persistence.Contexts
{
    public class LeaguesContextFactory : IDesignTimeDbContextFactory<LeaguesContext>
    {
        public LeaguesContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LeaguesContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=FliGen.Leagues; Trusted_Connection=True; MultipleActiveResultSets=true");
            return new LeaguesContext(optionsBuilder.Options);
        }
    }
}
