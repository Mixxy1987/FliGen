using FliGen.Common.Messages;

namespace FliGen.Services.Players.Application.Commands.AddPlayer
{
    [MessageNamespace("players")]
    public class AddPlayer : ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Rate { get; set; }
    }
}