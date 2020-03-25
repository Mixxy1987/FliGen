using Autofac;
using EventBus.RabbitMQ.Standard.Configuration;
using EventBus.RabbitMQ.Standard.Options;
using FliGen.Application.Commands.Player.AddPlayer;
using FliGen.Application.Events.PlayerRegistered;
using FliGen.Common.Mediator.Extensions;
using FliGen.Common.Mvc;
using FliGen.Persistence.Contextes;
using FliGen.Web.Extensions;
using FliGen.Web.Services;
using IdentityServer4.Models;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Linq;
using System.Reflection;
using FliGen.Common.SeedWork.Repository.DependencyInjection;

namespace FliGen.Web
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
            services.AddDbContext<FliGenContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))).AddUnitOfWork<FliGenContext>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AuthConnection")));//.AddUnitOfWork<ApplicationDbContext>();

            services.AddDefaultIdentity<AppUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<AppUser, ApplicationDbContext>(options =>
                    {
                        var apiResource = options.ApiResources.First();
                        apiResource.UserClaims = new[] { "hasUsersGroup" };

                        var identityResource = new IdentityResource
                        {
                            Name = "customprofile",
                            DisplayName = "Custom profile",
                            UserClaims = new[] { "hasUsersGroup" },
                        };
                        identityResource.Properties.Add(ApplicationProfilesPropertyNames.Clients, "*");
                        options.IdentityResources.Add(identityResource);
                    }
                );

            services.AddAuthentication()
                .AddOpenIdConnect("Google", "Google",
                    o =>
                    {
                        IConfigurationSection googleAuthNSection =
                            Configuration.GetSection("Authentication:Google");
                        o.ClientId = googleAuthNSection["ClientId"];
                        o.ClientSecret = googleAuthNSection["ClientSecret"];
                        o.Authority = "https://accounts.google.com";
                        o.ResponseType = OpenIdConnectResponseType.Code;
                        o.CallbackPath = "/signin-google";
                    })
                .AddIdentityServerJwt();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ShouldHasUsersGroup", policy => { policy.RequireClaim("hasUsersGroup"); });
            });


            services.AddMediatR(typeof(Startup))
	            .AddMediatR(typeof(AddPlayerCommand).GetTypeInfo().Assembly);

            services.AddTransient<PlayerRegisteredIntegrationEventHandler>();
            services.AddTransient<IIdentityService, IdentityService>();

            var rabbitMqOptions = Configuration.GetSection("RabbitMq").Get<RabbitMqOptions>();

            services.AddRabbitMqConnection(rabbitMqOptions);
            services.AddRabbitMqRegistration(rabbitMqOptions);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddAutoMapper();
            builder.AddMediator("FliGen.Application");
            builder.AddRequestLogDecorator();
            builder.AddRequestValidationDecorator();
            builder.AddSerilogService();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<FliGenContext>();
                //context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseErrorHandler();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseIdentityServer();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

            //Event Bus
            app.SubscribeToEvents();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
