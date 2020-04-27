using Autofac;
using Autofac.Extensions.DependencyInjection;
using FliGen.Common.Handlers.Extensions;
using FliGen.Common.Jaeger;
using FliGen.Common.Mediator.Extensions;
using FliGen.Common.Mvc;
using FliGen.Common.RabbitMq;
using FliGen.Common.RestEase;
using FliGen.Common.SeedWork.Repository.DependencyInjection;
using FliGen.Services.Notifications.Application.Events.TeamsCreated;
using FliGen.Services.Notifications.Application.Events.TourRegistrationOpened;
using FliGen.Services.Notifications.Application.Services;
using FliGen.Services.Notifications.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

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
            string connectionString = Configuration["TestConnection"] ??
                                      Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<NotificationsContext>(options =>
                        options.UseSqlServer(connectionString),
                    contextLifetime: ServiceLifetime.Transient)
                .AddUnitOfWork<NotificationsContext>();

            services.AddControllers();

            services.AddJaeger();
            services.AddOpenTracing();

            services.RegisterServiceForwarder<IPlayersService>("players-service");
            services.RegisterServiceForwarder<ILeaguesService>("leagues-service");
            services.RegisterServiceForwarder<IToursService>("tours-service");

            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();

            ConfigureContainer(builder);
            builder.Populate(services);
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddAutoMapper();
            builder.AddRabbitMq("FliGen.Services.Notifications.Application");
            builder.AddMediator("FliGen.Services.Notifications.Application");
            builder.AddRequestLogDecorator();
            builder.AddRequestValidationDecorator();
            builder.AddRequestValidationCommandHandlerDecorator();
            builder.AddRequestLogCommandHandlerDecorator();
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

            app.UseRabbitMq()
                .SubscribeEvent<TourRegistrationOpened>("tours")
                .SubscribeEvent<TeamsCreated>("teams");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
