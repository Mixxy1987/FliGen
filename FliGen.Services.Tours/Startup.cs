using Autofac;
using FliGen.Services.Tours.Application.Commands.TourCancelCommand;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using FliGen.Common.Mediator.Extensions;
using FliGen.Common.Mvc;
using Microsoft.EntityFrameworkCore;
using FliGen.Common.SeedWork.Repository.DependencyInjection;
using FliGen.Services.Tours.Persistence.Contextes;

namespace FliGen.Services.Tours
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ToursContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))).AddUnitOfWork<ToursContext>();
			services.AddControllers();
			services.AddMediatR(typeof(Startup))
				.AddMediatR(typeof(TourCancelCommand).GetTypeInfo().Assembly);
		}

		public void ConfigureContainer(ContainerBuilder builder)
		{
			builder.AddMediator("FliGen.Services.Tours.Application");
			builder.AddRequestLogDecorator();
			builder.AddRequestValidationDecorator();
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

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
