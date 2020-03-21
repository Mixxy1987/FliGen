using MediatR;

namespace FliGen.Application.Queries.League.LeagueInformation
{
    public class LeagueInformationQuery : IRequest<Dto.LeagueInformation>
    {
        public int LeagueId { get; }

        public LeagueInformationQuery(int leagueId)
        {
            LeagueId = leagueId;
        }
    }
}