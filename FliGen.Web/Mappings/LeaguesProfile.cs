using AutoMapper;

namespace FliGen.Web.Mappings
{
	public class LeaguesProfile : Profile
	{
		public LeaguesProfile()
		{
			CreateMap<Domain.Entities.League, Application.Dto.League>()
				.ForMember(x => x.Id, o => o.MapFrom(s => s.Id))
				.ForMember(x => x.Name, o => o.MapFrom(s => s.Name))
				.ForMember(x => x.Description, o => o.MapFrom(s => s.Description))
			    .ForPath(x => x.LeagueType.Name, o => o.MapFrom(s => s.Type.Name));

            CreateMap<Domain.Entities.LeagueSettings, Application.Dto.LeagueSettings>()
                .ForMember(x => x.RequireConfirmation, o => o.MapFrom(s => s.RequireConfirmation))
                .ForMember(x => x.Visibility, o => o.MapFrom(s => s.Visibility));

            CreateMap<Domain.Entities.League, Application.Dto.LeagueInformation>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Name))
                .ForMember(x => x.Description, o => o.MapFrom(s => s.Description))
                .ForPath(x => x.LeagueType.Name, o => o.MapFrom(s => s.Type.Name));
		}
	}
}