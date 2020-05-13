using Autofac;
using Autofac.Extensions.DependencyInjection;
using Chronicle;
using FliGen.Common;
using FliGen.Common.Handlers;
using FliGen.Common.Handlers.Extensions;
using FliGen.Common.Jaeger;
using FliGen.Common.Mediator.Extensions;
using FliGen.Common.Mongo;
using FliGen.Common.Mvc;
using FliGen.Common.RabbitMq;
using FliGen.Common.Redis;
using FliGen.Services.Operations.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace FliGen.Services.Operations
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
            services.AddControllers();

            services.AddJaeger();
            services.AddOpenTracing();
            services.AddRedis();
            services.AddChronicle();
            services.AddInitializers(typeof(IMongoDbInitializer));

            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();

            builder.Populate(services);
            ConfigureContainer(builder);
           
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddRabbitMq("FliGen.Services.Operations");
            builder.AddMongo();
            builder.RegisterGeneric(typeof(GenericEventHandler<>))
                .As(typeof(IEventHandler<>));
            builder.RegisterGeneric(typeof(GenericCommandHandler<>))
                .As(typeof(ICommandHandler<>));
            builder.AddRequestLogDecorator();
            builder.AddRequestValidationDecorator();
            builder.AddRequestValidationCommandHandlerDecorator();
            builder.AddRequestLogCommandHandlerDecorator();
            builder.AddSerilogService();
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IStartupInitializer startupInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseErrorHandler();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseRabbitMq().SubscribeAllMessages();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            startupInitializer.InitializeAsync();
        }
    }
}
