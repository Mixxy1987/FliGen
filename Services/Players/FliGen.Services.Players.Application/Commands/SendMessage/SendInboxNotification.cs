using FliGen.Common.Messages;

namespace FliGen.Services.Players.Application.Commands.SendMessage
{
    [MessageNamespace("players")]
    public class SendInboxNotification : ICommand
    {
        public int[] PlayerIds { get; set; }
        public string Sender { get; set; }
        public string Topic { get; set; }
        public string Body { get; set; }
    }
}