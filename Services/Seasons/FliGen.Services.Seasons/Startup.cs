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
using FliGen.Services.Seasons.Application.Services;
using FliGen.Services.Seasons.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FliGen.Services.Seasons
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
            services.AddDbContext<SeasonsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))).AddUnitOfWork<SeasonsContext>();

            _swaggerOptions = Configuration.GetOptions<SwaggerOptions>("swagger");

            services.AddControllers();

            services.AddJaeger();
            services.AddOpenTracing();

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

            services.RegisterServiceForwarder<IToursService>("tours-service");

            var builder = new ContainerBuilder();

            ConfigureContainer(builder);
            builder.Populate(services);
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddAutoMapper();
            builder.AddRabbitMq("FliGen.Services.Seasons.Application");
            builder.AddMediator("FliGen.Services.Seasons.Application");
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
