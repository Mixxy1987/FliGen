using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FliGen.Common.Mvc
{
	public static class Extensions
	{
        public static IMvcCoreBuilder AddCustomMvc(this IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                services.Configure<AppOptions>(configuration.GetSection("app"));
            }

            services.AddSingleton<IServiceId, ServiceId>();
            services.AddTransient<IStartupInitializer, StartupInitializer>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services
                .AddMvcCore();
            //.AddJsonFormatters()
            //.AddDataAnnotations()
            //.AddApiExplorer()
            //.AddDefaultJsonOptions()
            //.AddAuthorization();
        }

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }

        public static IServiceCollection AddInitializers(this IServiceCollection services, params Type[] initializers)
            => initializers == null
                ? services
                : services.AddTransient<IStartupInitializer, StartupInitializer>(c =>
                {
                    var startupInitializer = new StartupInitializer();
                    var validInitializers = initializers.Where(t => typeof(IInitializer).IsAssignableFrom(t));
                    foreach (var initializer in validInitializers)
                    {
                        startupInitializer.AddInitializer(c.GetService(initializer) as IInitializer);
                    }

                    return startupInitializer;
                });
    }
}