using MediatR;

namespace FliGen.Services.Leagues.Application.Queries.LeagueSettings
{
    public class LeagueSettingsQuery : IRequest<Dto.LeagueSettings>
    {
        public int LeagueId { get; }

        public LeagueSettingsQuery(int leagueId)
        {
            LeagueId = leagueId;
        }
    }
}