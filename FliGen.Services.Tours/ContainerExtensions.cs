using Autofac;
using AutoMapper;
using FliGen.Services.Tours.Mappings;

namespace FliGen.Services.Tours
{
    public static class ContainerExtensions
    {
        public static void AddAutoMapper(this ContainerBuilder builder)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
	            cfg.AddProfile<ToursProfile>();
            });

            builder.RegisterInstance(mapperConfig).AsSelf().SingleInstance();
            builder.RegisterInstance(mapperConfig.CreateMapper()).As<IMapper>().SingleInstance();
        }
    }
}