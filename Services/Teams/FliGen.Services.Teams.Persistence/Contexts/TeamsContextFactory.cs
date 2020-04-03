using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FliGen.Services.Teams.Persistence.Contexts
{
    public class TeamsContextFactory : IDesignTimeDbContextFactory<TeamsContext>
    {
        public TeamsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TeamsContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=FliGen.Teams; Trusted_Connection=True; MultipleActiveResultSets=true");
            return new TeamsContext(optionsBuilder.Options);
        }
    }
}
