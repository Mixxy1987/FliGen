using Autofac;
using AutoMapper;
using FliGen.Services.Teams.Mappings;

namespace FliGen.Services.Teams
{
    public static class ContainerExtensions
    {
        public static void AddAutoMapper(this ContainerBuilder builder)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
	            cfg.AddProfile<TeamsProfile>();
            });

            builder.RegisterInstance(mapperConfig).AsSelf().SingleInstance();
            builder.RegisterInstance(mapperConfig.CreateMapper()).As<IMapper>().SingleInstance();
        }
    }
}