using Microsoft.AspNetCore.Builder;

namespace FliGen.Common.Mvc
{
	public static class Extensions
	{
		public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
			=> builder.UseMiddleware<ErrorHandlerMiddleware>();
	}
}