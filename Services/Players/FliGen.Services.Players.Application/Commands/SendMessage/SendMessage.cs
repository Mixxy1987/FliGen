using FliGen.Common.Messages;

namespace FliGen.Services.Players.Application.Commands.SendMessage
{
    [MessageNamespace("players")]
    public class SendMessage : ICommand
    {
        public int? PlayerId { get; set; }
        public string From { get; set; }
        public string Topic { get; set; }
        public string Body { get; set; }
    }
}