using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FliGen.Persistence.Contextes
{
	public class ApplicationDbContext : KeyApiAuthorizationDbContext<AppUser, AppRole, int>
	{
	    public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
	        Database.EnsureCreated();
		}

	    protected override void OnModelCreating(ModelBuilder builder)
	    {

		    base.OnModelCreating(builder);

			builder.Entity<AppUser>(entity => { entity.ToTable("User"); });
			builder.Entity<AppRole>(entity => { entity.ToTable("Role"); });
		}
    }
}
