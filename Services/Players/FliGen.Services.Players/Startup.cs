using Autofac;
using Autofac.Extensions.DependencyInjection;
using FliGen.Common.Mediator.Extensions;
using FliGen.Common.Mvc;
using FliGen.Common.SeedWork.Repository.DependencyInjection;
using FliGen.Services.Players.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using FliGen.Common.RabbitMq;
using FliGen.Services.Players.Application.Commands.UpdatePlayer;

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
			services.AddDbContext<PlayersContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))).AddUnitOfWork<PlayersContext>();
			
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

            app.UseRabbitMq().SubscribeCommand<UpdatePlayer>();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
