using Autofac;
using Autofac.Extensions.DependencyInjection;
using FliGen.Common.Handlers.Extensions;
using FliGen.Common.Jaeger;
using FliGen.Common.Mediator.Extensions;
using FliGen.Common.Mvc;
using FliGen.Common.RabbitMq;
using FliGen.Common.RestEase;
using FliGen.Common.SeedWork.Repository.DependencyInjection;
using FliGen.Services.Teams.Application.Commands.GenerateTeams;
using FliGen.Services.Teams.Application.Events;
using FliGen.Services.Teams.Application.Services;
using FliGen.Services.Teams.Application.Services.GenerateTeams;
using FliGen.Services.Teams.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FliGen.Services.Teams
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<TeamsContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
                    contextLifetime: ServiceLifetime.Transient)
                .AddUnitOfWork<TeamsContext>();

            services.AddControllers();

            services.AddJaeger();
            services.AddOpenTracing();

            services.RegisterServiceForwarder<ILeaguesService>("leagues-service");
            services.RegisterServiceForwarder<IPlayersService>("players-service");

            var builder = new ContainerBuilder();
            builder.RegisterType<GenerateTeamsServiceFactory>()
                .As<IGenerateTeamsServiceFactory>()
                .InstancePerDependency();

            ConfigureContainer(builder);
            builder.Populate(services);
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddAutoMapper();
            builder.AddRabbitMq("FliGen.Services.Teams.Application");
            builder.AddMediator("FliGen.Services.Teams.Application");
            builder.AddRequestLogDecorator();
            builder.AddRequestValidationDecorator();
            builder.AddRequestValidationCommandHandlerDecorator();
            builder.AddRequestLogCommandHandlerDecorator();
            builder.AddSerilogService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                .SubscribeCommand<GenerateTeams>(onError: (c, e) =>
                    new GenerateTeamsRejected(e.Message, e.Code));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
