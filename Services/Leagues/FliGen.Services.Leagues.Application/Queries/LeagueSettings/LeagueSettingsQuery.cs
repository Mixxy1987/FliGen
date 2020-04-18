using MediatR;

namespace FliGen.Services.Leagues.Application.Queries.LeagueSettings
{
    public class LeagueSettingsQuery : IRequest<Dto.LeagueSettings>
    {
        public int LeagueId { get; set; }
    }
}