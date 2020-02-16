using System;
using MediatR;

namespace FliGen.Application.Commands.Player.AddPlayer
{
    public class AddPlayerCommand : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Rate { get; set; }

        internal string DotToComma()
        {
            throw new NotImplementedException();
        }
    }
}