using System;
using System.Linq;
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

            CreateMap<Domain.Entities.Tour, Application.Dto.Tour>()
                .ForMember(x => x.Date, ls => ls.MapFrom(s => s.Date.ToString("yyyy-MM-dd")))
                .ForMember(x => x.HomeCount, ls => ls.MapFrom(s => s.HomeCount))
                .ForMember(x => x.GuestCount, ls => ls.MapFrom(s => s.GuestCount));

            CreateMap<Domain.Entities.Season, Application.Dto.Season>()
                .ForMember(x => x.Start, ls => ls.MapFrom(s => s.Start.ToString("yyyy-MM-dd")))
                .ForMember(x => x.ToursPlayed, ls => ls.MapFrom(s => s.Tours.Count(t => t.Date < DateTime.Now)))
                .ForMember(x => x.LastTour, ls => ls.MapFrom(s => s.Tours
                        .Where(t => t.Date < DateTime.Now)
                        .OrderBy(t => t.Date)
                        .Last())
                    );

            CreateMap<Domain.Entities.League, Application.Dto.LeagueInformation>()
                .ForMember(x => x.Name, o => o.MapFrom(l => l.Name))
                .ForMember(x => x.Description, o => o.MapFrom(l => l.Description))
                .ForMember(x => x.SeasonsCount, o => o.MapFrom(l => l.Seasons.Count))
                .ForMember(x => x.ToursCount, o => o.MapFrom(l => l.Seasons.SelectMany(s => s.Tours).Count()))
                .ForMember(x => x.CurrentSeason, o => o.MapFrom(l => l.Seasons.OrderBy(s => s.Start).Last()))
                .ForPath(x => x.LeagueType.Name, o => o.MapFrom(s => s.Type.Name));
        }
    }
}