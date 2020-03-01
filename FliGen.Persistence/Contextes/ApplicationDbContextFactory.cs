using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace FliGen.Persistence.Contextes
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AuthDb;Trusted_Connection=True;MultipleActiveResultSets=true"/*,
                sql => sql.MigrationsAssembly(typeof(ApplicationDbContextFactory).GetTypeInfo().Assembly.GetName().Name)*/);

            var operationalStoreOptions = Options.Create(new OperationalStoreOptions());

            return new ApplicationDbContext(optionsBuilder.Options, operationalStoreOptions);
        }
    }
}
