using Autofac;
using Autofac.Extensions.DependencyInjection;
using FliGen.Common.Extensions;
using FliGen.Common.Handlers.Extensions;
using FliGen.Common.Jaeger;
using FliGen.Common.Mediator.Extensions;
using FliGen.Common.Mvc;
using FliGen.Common.RabbitMq;
using FliGen.Common.RestEase;
using FliGen.Common.SeedWork.Repository.DependencyInjection;
using FliGen.Common.Swagger;
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

			services
                .AddDbContext<ToursContext>(
                    options => options.UseSqlServer(connectionString),
                    contextLifetime: ServiceLifetime.Transient)
                .AddUnitOfWork<ToursContext>();

			services.AddControllers();

            if (_swaggerOptions.Enabled)
            {
                services.AddSwaggerDocument(config =>
                {
                    config.PostProcess = document =>
                    {
                        document.Info.Version = "v1";
                        document.Info.Title = "Tours Api";
                        document.Info.Description = "Tours service Api";
                        document.Info.TermsOfService = "None";
                    };
                });
            }

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

            if (_swaggerOptions.Enabled)
            {
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

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
