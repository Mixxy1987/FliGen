using System;
using FliGen.Common.Mediator;
using FliGen.Common.Types;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace FliGen.Common.Filters
{
    public sealed class RequestValidationFilter : ExceptionFilter
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<ExceptionFilter> _logger;

        public RequestValidationFilter(IHostingEnvironment hostingEnvironment, ILogger<ExceptionFilter> logger)
            : base(hostingEnvironment, logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        public override void OnExceptionAction(ExceptionContext context)
        {
            if (context.Exception is RequestValidationException ex)
            {
                context.Result = new ObjectResult("Validation error");
                //Value = CustomError.Create(ex);
            }
            Logger.LogError(context.Exception.ToString());
        }
    }
}