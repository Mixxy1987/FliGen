using FliGen.Common.Messages;

namespace FliGen.Services.Players.Application.Commands.DeletePlayer
{
    [MessageNamespace("players")]
    public class DeletePlayer : ICommand
    {
        public int Id { get; set; }
    }
}