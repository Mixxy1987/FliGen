using Autofac;
using Autofac.Extensions.DependencyInjection;
using FliGen.Common.Jaeger;
using FliGen.Common.Mediator.Extensions;
using FliGen.Common.Mvc;
using FliGen.Common.RabbitMq;
using FliGen.Common.RestEase;
using FliGen.Common.SeedWork.Repository.DependencyInjection;
using FliGen.Services.Tours.Application.Commands.PlayerRegisterOnTour;
using FliGen.Services.Tours.Application.Commands.TourBack;
using FliGen.Services.Tours.Application.Commands.TourCancel;
using FliGen.Services.Tours.Application.Commands.TourForward;
using FliGen.Services.Tours.Application.Events;
using FliGen.Services.Tours.Application.Services;
using FliGen.Services.Tours.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FliGen.Services.Tours
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
			services.AddDbContext<ToursContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))).AddUnitOfWork<ToursContext>();
			
            services.AddControllers();

            services.AddJaeger();
            services.AddOpenTracing();

			services.RegisterServiceForwarder<ILeaguesService>("leagues-service");
			services.RegisterServiceForwarder<IPlayersService>("players-service");
			services.RegisterServiceForwarder<ITeamsService>("teams-service");

			var builder = new ContainerBuilder();

            ConfigureContainer(builder);
            builder.Populate(services);
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
		}

		public void ConfigureContainer(ContainerBuilder builder)
		{
            builder.AddAutoMapper();
            builder.AddRabbitMq("FliGen.Services.Tours.Application");
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

            app.UseRabbitMq()
                .SubscribeCommand<PlayerRegisterOnTour>(onError: (c, e) =>
                    new PlayerRegisterOnTourRejected(e.Message, e.Code))
				.SubscribeCommand<TourCancel>()
                .SubscribeCommand<TourForward>()
                .SubscribeCommand<TourBack>();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
