using Autofac;
using Autofac.Extensions.DependencyInjection;
using Consul;
using FliGen.Common;
using FliGen.Common.Consul;
using FliGen.Common.Extensions;
using FliGen.Common.Jaeger;
using FliGen.Common.Mvc;
using FliGen.Common.RabbitMq;
using FliGen.Common.Redis;
using FliGen.Services.Signalr.Framework;
using FliGen.Services.Signalr.Hubs;
using FliGen.Services.Signalr.Messages.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using FliGen.Common.Authentication;

namespace FliGen.Services.Signalr
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
            services.AddCustomMvc();
            services.AddConsul();
            services.AddJaeger();
            services.AddOpenTracing();
            services.AddRedis();

            services
                .AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder => builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                    );

                    options.AddPolicy("signalr",
                        builder => builder
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .SetIsOriginAllowed(hostName => true));
                });

            AddSignalR(services);

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddJwt();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                    .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddRabbitMq("FliGen.Services.Signalr");

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        private void AddSignalR(IServiceCollection services)
        {
            var options = Configuration.GetOptions<SignalrOptions>("signalr");
            services.AddSingleton(options);
            var builder = services.AddSignalR();
            if (!options.Backplane.Equals("redis", StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }
            var redisOptions = Configuration.GetOptions<RedisOptions>("redis");
            builder.AddRedis(redisOptions.ConnectionString);
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env, 
            IHostApplicationLifetime applicationLifetime,
            SignalrOptions signalrOptions,
            IConsulClient client/*,
            IStartupInitializer startupInitializer*/)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            app.UseCors("signalr");
            app.UseStaticFiles();
            app.UseErrorHandler();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<FliGenHub>($"/{signalrOptions.Hub}");
            });
            app.UseServiceId();
            app.UseRabbitMq()
                .SubscribeEvent<OperationPending>("operations")
                .SubscribeEvent<OperationCompleted>("operations")
                .SubscribeEvent<OperationRejected>("operations");

            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() => 
            { 
                client.Agent.ServiceDeregister(consulServiceId); 
                Container.Dispose(); 
            });

            //startupInitializer.InitializeAsync();
        }
    }
}
