using FliGen.Common.Messages;
using MediatR;

namespace FliGen.Services.Leagues.Application.Commands.DeleteLeague
{
    [MessageNamespace("leagues")]
    public class DeleteLeague : ICommand
    {
        public int Id { get; set; }
    }
}