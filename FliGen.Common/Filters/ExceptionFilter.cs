using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace FliGen.Common.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        protected readonly IHostingEnvironment HostingEnvironment;
        protected readonly ILogger<ExceptionFilter> Logger;

        public ExceptionFilter(IHostingEnvironment hostingEnvironment, ILogger<ExceptionFilter> logger)
        {
            HostingEnvironment = hostingEnvironment;
            Logger = logger;
        }

        public virtual void OnExceptionAction(ExceptionContext context)
        {
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnExceptionAction(context);
            
            if (context.Result != null)
                return Task.CompletedTask;

            context.Result = new ObjectResult("Internal server error.")
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Value = HostingEnvironment.IsDevelopment() ? context.Exception.ToString() : context.Exception.Message
            };
            Logger.LogError("Request handling error: ", context.Exception);
            return Task.CompletedTask;
        }
    }
}