using AutoMapper;

namespace FliGen.Services.Players.Mappings
{
    public class PlayersProfile : Profile
    {
        public PlayersProfile()
        {
            CreateMap<Domain.Entities.PlayerRate, Application.Dto.PlayerLeagueRate>()
                .ForMember(plr => plr.Date, x => x.MapFrom(pr => pr.Date.ToString("yyyy-MM-dd")))
                .ForMember(plr => plr.LeagueId, x => x.MapFrom(pr => pr.LeagueId))
                .ForMember(plr => plr.PlayerId, x => x.MapFrom(pr => pr.PlayerId))
                .ForMember(plr => plr.Rate, x => x.MapFrom(pr => pr.Value));

            CreateMap<Domain.Entities.Player, Application.Dto.PlayerWithRate>()
                .ForMember(pwr => pwr.Id, x => x.MapFrom(p => p.Id))
                .ForMember(pwr => pwr.FirstName, x => x.MapFrom(p => p.FirstName))
                .ForMember(pwr => pwr.LastName, x => x.MapFrom(p => p.LastName))
                .ForMember(pwr => pwr.PlayerLeagueRates, x => x.MapFrom(p => p.Rates));
        }
    }
}