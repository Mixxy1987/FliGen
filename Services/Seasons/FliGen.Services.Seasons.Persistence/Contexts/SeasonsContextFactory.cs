using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FliGen.Services.Seasons.Persistence.Contexts
{
    public class SeasonsContextFactory : IDesignTimeDbContextFactory<SeasonsContext>
    {
        public SeasonsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SeasonsContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=FliGen.Seasons; Trusted_Connection=True; MultipleActiveResultSets=true");
            return new SeasonsContext(optionsBuilder.Options);
        }
    }
}
