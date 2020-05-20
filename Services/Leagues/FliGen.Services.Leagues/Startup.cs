using Autofac;
using Autofac.Extensions.DependencyInjection;
using Consul;
using FliGen.Common.Consul;
using FliGen.Common.Extensions;
using FliGen.Common.Handlers.Extensions;
using FliGen.Common.Jaeger;
using FliGen.Common.Mediator.Extensions;
using FliGen.Common.Mvc;
using FliGen.Common.RabbitMq;
using FliGen.Common.RestEase;
using FliGen.Common.SeedWork.Repository.DependencyInjection;
using FliGen.Common.Swagger;
using FliGen.Services.Leagues.Application.Commands.CreateLeague;
using FliGen.Services.Leagues.Application.Commands.DeleteLeague;
using FliGen.Services.Leagues.Application.Commands.JoinLeague;
using FliGen.Services.Leagues.Application.Commands.UpdateLeague;
using FliGen.Services.Leagues.Application.Commands.UpdateLeagueSettings;
using FliGen.Services.Leagues.Application.Services;
using FliGen.Services.Leagues.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using FliGen.Common.Redis;

namespace FliGen.Services.Leagues
{
    public class Startup
    {
        private SwaggerOptions _swaggerOptions;
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

            _swaggerOptions = Configuration.GetOptions<SwaggerOptions>("swagger");

            services.AddDbContext<LeaguesContext>(options => options.UseSqlServer(connectionString),
                    contextLifetime: ServiceLifetime.Transient)
                .AddUnitOfWork<LeaguesContext>();

            services.AddCustomMvc();
            services.AddControllers();
            services.AddConsul();
            services.AddJaeger();
            services.AddOpenTracing();
            services.AddRedis();

            if (_swaggerOptions.Enabled)
            {
                services.AddSwaggerDocument(config =>
                {
                    config.PostProcess = document =>
                    {
                        document.Info.Version = "v1";
                        document.Info.Title = "Seasons Api";
                        document.Info.Description = "Seasons Service Api";
                        document.Info.TermsOfService = "None";
                    };
                });
            }

            services.RegisterServiceForwarder<IPlayersService>("players-service");
            services.RegisterServiceForwarder<ISeasonsService>("seasons-service");

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
            builder.AddRequestValidationCommandHandlerDecorator();
            builder.AddRequestLogCommandHandlerDecorator();
            builder.AddSerilogService();
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IHostApplicationLifetime applicationLifetime,
            IConsulClient client)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseErrorHandler();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseServiceId();

            app.UseRabbitMq()
                .SubscribeCommand<CreateLeague>()
                .SubscribeCommand<DeleteLeague>()
                .SubscribeCommand<JoinLeague>()
                .SubscribeCommand<UpdateLeague>()
                .SubscribeCommand<UpdateLeagueSettings>();

            if (_swaggerOptions.Enabled)
            {
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                client.Agent.ServiceDeregister(consulServiceId);
                Container.Dispose();
            });
        }
    }
}
