using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Notifications.Application.Commands
{
    [MessageNamespace("players")]
    public class SendInboxNotification
    {
        public string From { get; }
        public string Topic { get; }
        public string Body { get; }

        [JsonConstructor]
        public SendInboxNotification(string from, string topic, string body)
        {
            From = from;
            Topic = topic;
            Body = body;
        }
    }
}