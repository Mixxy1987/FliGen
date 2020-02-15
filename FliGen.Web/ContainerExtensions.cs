using Autofac;
using AutoMapper;
using FliGen.Common.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FliGen.Web
{
    public static class ContainerExtensions
    {
        public static void AddExceptionFilters(this IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            });
        }

        public static void AddRequestValidationExceptionFilters(this IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            });
        }

        public static void AddLogger(this ContainerBuilder builder, IConfiguration configuration)
        {
            //todo::
        }

        public static void AddAutoMapper(this ContainerBuilder builder)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                //todo::
            });

            builder.RegisterInstance(mapperConfig).AsSelf().SingleInstance();
            builder.RegisterInstance(mapperConfig).As<IMapper>().SingleInstance();
        }

        public static void AddModules(this ContainerBuilder builder)
        {
            //todo::
        }
    }
}