using MediatR;

namespace FliGen.Application.Commands.League.DeleteLeague
{
    public class DeleteLeagueCommand : IRequest
    {
        public int Id { get; set; }
    }
}