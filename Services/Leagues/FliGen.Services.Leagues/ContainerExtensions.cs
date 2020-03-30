using Autofac;
using AutoMapper;
using FliGen.Services.Leagues.Mappings;

namespace FliGen.Services.Leagues
{
    public static class ContainerExtensions
    {
        public static void AddAutoMapper(this ContainerBuilder builder)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
	            cfg.AddProfile<LeaguesProfile>();
            });

            builder.RegisterInstance(mapperConfig).AsSelf().SingleInstance();
            builder.RegisterInstance(mapperConfig.CreateMapper()).As<IMapper>().SingleInstance();
        }
    }
}