using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace FliGen.Services.Api.Services
{
	public class IdentityService : IIdentityService
	{
		//private readonly IHttpContextAccessor _context;

		public IdentityService(/*IHttpContextAccessor context*/)
		{
			//_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public string GetUserIdentity()
        {
            return "038132fd-ade3-4946-a555-efa6686ac8d5"; //todo:: ?
			//return _context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
		}
	}
}