using FliGen.Services.Leagues.Application.Dto;
using MediatR;

namespace FliGen.Services.Leagues.Application.Queries.LeagueInformation
{
    public class LeagueInformationQuery : IRequest<LeagueInformationDto>
    {
        public int Id { get; set; }
    }
}
