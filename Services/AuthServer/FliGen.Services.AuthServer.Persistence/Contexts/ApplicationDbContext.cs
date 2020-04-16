using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FliGen.Services.AuthServer.Persistence.Contexts
{
	public class ApplicationDbContext : ApiAuthorizationDbContext<AppUser>
	{
	    public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
	        Database.EnsureCreated();
		}
    }
}
