using Autofac;
using Autofac.Extensions.DependencyInjection;
using Consul;
using FliGen.Common.Consul;
using FliGen.Common.Handlers.Extensions;
using FliGen.Common.Mediator.Extensions;
using FliGen.Common.Mvc;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository.DependencyInjection;
using FliGen.Services.Players.Application.Commands.AddPlayer;
using FliGen.Services.Players.Application.Commands.DeletePlayer;
using FliGen.Services.Players.Application.Commands.InboxNotification;
using FliGen.Services.Players.Application.Commands.UpdatePlayer;
using FliGen.Services.Players.Application.Events.PlayerRegistered;
using FliGen.Services.Players.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using FliGen.Common.Jaeger;
using FliGen.Common.Redis;

namespace FliGen.Services.Players
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
            string connectionString = Configuration["TestConnection"] ??   //todo:: think how do it better!
                                      Configuration.GetConnectionString("DefaultConnection");

			services
                .AddDbContext<PlayersContext>(options =>
                    options.UseSqlServer(connectionString))
                .AddUnitOfWork<PlayersContext>();

			services.AddControllers();
            services.AddCustomMvc();
			services.AddConsul();
            services.AddJaeger();
            services.AddOpenTracing();
            services.AddRedis();

			var builder = new ContainerBuilder();
			ConfigureContainer(builder);
			builder.Populate(services);
			Container = builder.Build();

			return new AutofacServiceProvider(Container);
		}

		public void ConfigureContainer(ContainerBuilder builder)
		{
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
			builder.AddAutoMapper();
			builder.AddRabbitMq("FliGen.Services.Players.Application");
			builder.AddMediator("FliGen.Services.Players.Application");
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
                .SubscribeCommand<UpdatePlayer>()
                .SubscribeCommand<AddPlayer>()
                .SubscribeCommand<DeletePlayer>()
                .SubscribeCommand<InboxNotification>()
                .SubscribeEvent<PlayerRegistered>("web");

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
