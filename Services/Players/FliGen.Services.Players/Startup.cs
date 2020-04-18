using Autofac;
using Autofac.Extensions.DependencyInjection;
using FliGen.Common.Mediator.Extensions;
using FliGen.Common.Mvc;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository.DependencyInjection;
using FliGen.Services.Players.Application.Commands.AddPlayer;
using FliGen.Services.Players.Application.Commands.DeletePlayer;
using FliGen.Services.Players.Application.Commands.SendMessage;
using FliGen.Services.Players.Application.Commands.UpdatePlayer;
using FliGen.Services.Players.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

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
			builder.AddRabbitMq("FliGen.Services.Players.Application");
			builder.AddMediator("FliGen.Services.Players.Application");
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

            app.UseRabbitMq()
                .SubscribeCommand<UpdatePlayer>()
                .SubscribeCommand<AddPlayer>()
                .SubscribeCommand<DeletePlayer>()
                .SubscribeCommand<SendMessage>();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
