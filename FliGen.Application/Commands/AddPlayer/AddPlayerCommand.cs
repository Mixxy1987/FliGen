using MediatR;

namespace FliGen.Application.Commands.AddPlayer
{
    public class AddPlayerCommand : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Rate { get; set; }
    }
}