using FliGen.Services.Leagues.Application.Dto;
using MediatR;

namespace FliGen.Services.Leagues.Application.Commands.CreateLeague
{
    public class CreateLeagueCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public LeagueType LeagueType { get; set; }
    }
}