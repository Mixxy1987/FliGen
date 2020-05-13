using FliGen.Common.Messages;

namespace FliGen.Services.Operations.Messages.Leagues.Commands
{
    [MessageNamespace("leagues")]
    public class DeleteLeague : ICommand
    {
        public int Id { get; set; }
    }
}