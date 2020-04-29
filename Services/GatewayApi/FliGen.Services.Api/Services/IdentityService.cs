using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace FliGen.Services.Api.Services
{
	public class IdentityService : IIdentityService
	{
		private readonly IHttpContextAccessor _context;

		public IdentityService(IHttpContextAccessor context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public string GetUserIdentity()
        {
			return _context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
		}
	}
}