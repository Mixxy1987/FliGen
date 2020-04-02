using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FliGen.Common.RestEase;
using FliGen.Common.Jaeger;
using FliGen.Common.RabbitMq;
using FliGen.Services.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FliGen.Services.Api
{
    public class Startup
    {
        private static readonly string[] Headers = new[] { "X-Operation", "X-Resource", "X-Total-Count" };
        public IContainer Container { get; private set; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddJaeger();
            services.AddOpenTracing();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors =>
                    cors.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders(Headers));
            });
            services.RegisterServiceForwarder<IPlayersService>("players-service");
            services.RegisterServiceForwarder<ILeaguesService>("leagues-service");
            //services.RegisterServiceForwarder<ISeasonsService>("seasons-service");
            services.RegisterServiceForwarder<IToursService>("tours-service");
            //services.RegisterServiceForwarder<ITeamsService>("teams-service");
            

            services.AddControllers();

            var builder = new ContainerBuilder();

            builder.RegisterType<IdentityService>().As<IIdentityService>().InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddRabbitMq("FliGen.Services.Api");

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseRabbitMq();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
