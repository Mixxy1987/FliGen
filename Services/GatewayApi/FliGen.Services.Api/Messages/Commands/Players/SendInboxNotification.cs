using FliGen.Common.Messages;

namespace FliGen.Services.Api.Messages.Commands.Players
{
    [MessageNamespace("players")]
    public class SendInboxNotification : ICommand
    {
        public int[] PlayerId { get; set; }
        public string From { get; set; }
        public string Topic { get; set; }
        public string Body { get; set; }
    }
}