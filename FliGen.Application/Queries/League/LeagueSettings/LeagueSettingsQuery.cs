using MediatR;

namespace FliGen.Application.Queries.League.LeagueSettings
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