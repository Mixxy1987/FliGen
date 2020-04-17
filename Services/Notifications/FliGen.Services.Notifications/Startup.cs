using Autofac;
using Autofac.Extensions.DependencyInjection;
using FliGen.Common.Jaeger;
using FliGen.Common.Mediator.Extensions;
using FliGen.Common.Mvc;
using FliGen.Common.RabbitMq;
using FliGen.Common.RestEase;
using FliGen.Services.Notifications.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FliGen.Services.Notifications
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddJaeger();
            services.AddOpenTracing();

            services.RegisterServiceForwarder<IPlayersService>("players-service");

            var builder = new ContainerBuilder();

            ConfigureContainer(builder);
            builder.Populate(services);
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddAutoMapper();
            builder.AddRabbitMq("FliGen.Services.Leagues.Application");
            builder.AddMediator("FliGen.Services.Leagues.Application");
            builder.AddRequestLogDecorator();
            builder.AddRequestValidationDecorator();
            builder.AddSerilogService();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseErrorHandler();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseRabbitMq();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
