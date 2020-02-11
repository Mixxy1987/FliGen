using MediatR;

namespace FliGen.Application.Commands
{
    public class AddPlayerCommand : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Rate { get; set; }
    }
}