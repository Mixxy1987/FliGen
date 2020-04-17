using FliGen.Common.Messages;

namespace FliGen.Services.Api.Messages.Commands.Players
{
    [MessageNamespace("players")]
    public class DeletePlayer : ICommand
    {
        public int Id { get; set; }
    }
}