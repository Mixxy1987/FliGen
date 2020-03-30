using Autofac;
using AutoMapper;
using FliGen.Services.Players.Mappings;

namespace FliGen.Services.Players
{
    public static class ContainerExtensions
    {
        public static void AddAutoMapper(this ContainerBuilder builder)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
	            cfg.AddProfile<PlayersProfile>();
            });

            builder.RegisterInstance(mapperConfig).AsSelf().SingleInstance();
            builder.RegisterInstance(mapperConfig.CreateMapper()).As<IMapper>().SingleInstance();
        }
    }
}