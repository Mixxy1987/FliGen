﻿using AutoMapper;

namespace FliGen.Services.Leagues.Mappings
{
    public class LeaguesProfile : Profile
    {
        public LeaguesProfile()
        {
            CreateMap<Domain.Entities.League, Application.Dto.LeagueDto>()
                .ForMember(x => x.Id, l => l.MapFrom(s => s.Id))
                .ForMember(x => x.Name, l => l.MapFrom(s => s.Name))
                .ForMember(x => x.Description, l => l.MapFrom(s => s.Description))
                .ForPath(x => x.LeagueType.Name, l => l.MapFrom(s => s.Type.Name));

            CreateMap<Domain.Entities.League, Application.Dto.LeagueInformationDto>()
                .ForMember(x => x.Id, l => l.MapFrom(s => s.Id))
                .ForMember(x => x.Name, l => l.MapFrom(s => s.Name))
                .ForMember(x => x.Description, l => l.MapFrom(s => s.Description))
                .ForPath(x => x.LeagueType.Name, l => l.MapFrom(s => s.Type.Name));

            CreateMap<Domain.Entities.LeagueSettings, Application.Dto.LeagueSettings>()
                .ForMember(x => x.RequireConfirmation, ls => ls.MapFrom(s => s.RequireConfirmation))
                .ForMember(x => x.Visibility, ls => ls.MapFrom(s => s.Visibility))
                .ForMember(x => x.PlayersInTeam, ls => ls.MapFrom(s => s.PlayersInTeam))
                .ForMember(x => x.TeamsInTour, ls => ls.MapFrom(s => s.TeamsInTour));

            CreateMap<Domain.Entities.Enum.LeagueType, Application.Dto.LeagueType>()
                .ForMember(x => x.Name, ls => ls.MapFrom(s => s.Name));
        }
    }
}