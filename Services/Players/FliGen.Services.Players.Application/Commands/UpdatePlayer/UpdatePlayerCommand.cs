using MediatR;

namespace FliGen.Services.Players.Application.Commands.UpdatePlayer
{
    public class UpdatePlayerCommand : IRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int LeagueId { get; set; }
        public string Rate { get; set; }
    }
}