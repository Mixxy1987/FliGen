using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FliGen.Services.Players.Persistence.Contexts
{
    public class FliGenContextFactory : IDesignTimeDbContextFactory<PlayersContext>
    {
        public PlayersContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PlayersContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=FliGen.Players; Trusted_Connection=True; MultipleActiveResultSets=true");
            return new PlayersContext(optionsBuilder.Options);
        }
    }
}
