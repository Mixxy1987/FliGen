using MediatR;

namespace FliGen.Services.Leagues.Application.Commands.DeleteLeague
{
    public class DeleteLeagueCommand : IRequest
    {
        public int Id { get; set; }
    }
}