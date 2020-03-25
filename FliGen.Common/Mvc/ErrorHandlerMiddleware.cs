using System;
using System.Net;
using System.Threading.Tasks;
using FliGen.Common.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FliGen.Common.Mvc
{
	public class ErrorHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ErrorHandlerMiddleware> _logger;

		public ErrorHandlerMiddleware(RequestDelegate next,
			ILogger<ErrorHandlerMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
				await HandleErrorAsync(context, exception);
			}
		}

		private static Task HandleErrorAsync(HttpContext context, Exception exception)
		{
			var errorCode = "error";
			string message = exception.Message;
			switch (exception)
			{
				case FliGenException e:
					errorCode = e.Code;
					message = e.Message;
					break;
			}

			var payload = JsonConvert.SerializeObject(new
				{
					code = errorCode,
					message
				}
			);
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

			return context.Response.WriteAsync(payload);
		}
	}
}