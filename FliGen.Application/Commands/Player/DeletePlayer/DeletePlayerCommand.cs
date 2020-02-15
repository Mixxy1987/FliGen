using MediatR;

namespace FliGen.Application.Commands.Player.DeletePlayer
{
    public class DeletePlayerCommand : IRequest
    {
        public int Id { get; set; }
    }
}