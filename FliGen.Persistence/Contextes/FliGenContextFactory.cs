using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FliGen.Persistence.Contextes
{
    public class FliGenContextFactory : IDesignTimeDbContextFactory<FliGenContext>
    {
        public FliGenContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FliGenContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Fligen;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new FliGenContext(optionsBuilder.Options);
        }
    }
}
