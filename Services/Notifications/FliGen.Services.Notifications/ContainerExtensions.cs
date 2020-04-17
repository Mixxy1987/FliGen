using Autofac;
using AutoMapper;

namespace FliGen.Services.Notifications
{
    public static class ContainerExtensions
    {
        public static void AddAutoMapper(this ContainerBuilder builder)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
            });

            builder.RegisterInstance(mapperConfig).AsSelf().SingleInstance();
            builder.RegisterInstance(mapperConfig.CreateMapper()).As<IMapper>().SingleInstance();
        }
    }
}