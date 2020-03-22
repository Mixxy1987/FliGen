﻿using System.Linq;
using AutoMapper;

namespace FliGen.Web.Mappings
{
	public class LeaguesProfile : Profile
	{
		public LeaguesProfile()
		{
			CreateMap<Domain.Entities.League, Application.Dto.League>()
				.ForMember(x => x.Id, l => l.MapFrom(s => s.Id))
				.ForMember(x => x.Name, l => l.MapFrom(s => s.Name))
				.ForMember(x => x.Description, l => l.MapFrom(s => s.Description))
			    .ForPath(x => x.LeagueType.Name, l => l.MapFrom(s => s.Type.Name));

            CreateMap<Domain.Entities.LeagueSettings, Application.Dto.LeagueSettings>()
                .ForMember(x => x.RequireConfirmation, ls => ls.MapFrom(s => s.RequireConfirmation))
                .ForMember(x => x.Visibility, ls => ls.MapFrom(s => s.Visibility));

            CreateMap<Domain.Entities.League, Application.Dto.LeagueInformation>()
                .ForMember(x => x.Name, o => o.MapFrom(l => l.Name))
                .ForMember(x => x.Description, o => o.MapFrom(l => l.Description))
                .ForMember(x => x.SeasonsCount, o => o.MapFrom(l => l.Seasons.Count))
                .ForMember(x => x.ToursCount, o => o.MapFrom(l => l.Seasons.SelectMany(s => s.Tours).Count()))
				.ForPath(x => x.LeagueType.Name, o => o.MapFrom(s => s.Type.Name));
		}
	}
}