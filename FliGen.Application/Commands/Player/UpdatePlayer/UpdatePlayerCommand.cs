using MediatR;

namespace FliGen.Application.Commands.Player.UpdatePlayer
{
    public class UpdatePlayerCommand : IRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Rate { get; set; }
    }
}