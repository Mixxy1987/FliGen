using System;
using System.Reflection;
using System.Security.Claims;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FliGen.Common.Authentication;
using FliGen.Common.RestEase;
using FliGen.Common.Jaeger;
using FliGen.Common.RabbitMq;
using FliGen.Services.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace FliGen.Services.Api
{
    public class Startup
    {
        private static readonly string[] Headers = new[] { "X-Operation", "X-Resource", "X-Total-Count" };
        public IContainer Container { get; private set; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true; // temp
            services.AddJaeger();
            services.AddOpenTracing();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors =>
                    cors.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        /*.WithExposedHeaders(Headers)*/);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {

                RequireExpirationTime = true,
                RequireSignedTokens = false,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false, //todo:: temp
                //ValidIssuer = "8d708afe-2966-40b7-918c-a39551625958",
                ValidateAudience = false,
                //ValidAudience = "https://sts.windows.net/a1d50521-9687-4e4d-a76d-ddd53ab0c668/",
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication("Bearer")
                .AddJwtBearer(o =>
                {
                    o.Authority = "https://localhost:44379";
                    o.Audience = "resourceapi";
                    o.RequireHttpsMetadata = false;
                    o.TokenValidationParameters = tokenValidationParameters;
                });

            services.RegisterServiceForwarder<IPlayersService>("players-service");
            services.RegisterServiceForwarder<ILeaguesService>("leagues-service");
            //services.RegisterServiceForwarder<ISeasonsService>("seasons-service");
            services.RegisterServiceForwarder<IToursService>("tours-service");
            services.RegisterServiceForwarder<ITeamsService>("teams-service");

            services.AddHttpContextAccessor();
            services
                .AddControllers()
                .AddNewtonsoftJson();

            var builder = new ContainerBuilder();

            builder.RegisterType<IdentityService>().As<IIdentityService>().InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddRabbitMq("FliGen.Services.Api");

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseHttpsRedirection();
            app.UseRabbitMq();
            app.UseRouting();
            app.UseCors("CorsPolicy");


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
