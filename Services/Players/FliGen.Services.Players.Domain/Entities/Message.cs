using FliGen.Common.SeedWork;

namespace FliGen.Services.Players.Domain.Entities
{
    public class Message : Entity
    {
        public string From { get; }
        public string Topic { get; }
        public string Body { get; }

        private Message(string from, string topic, string body)
        {
            From = from;
            Topic = topic;
            Body = body;
        }

        public static Message Create(string from, string topic, string body)
        {
            return new Message(from, topic, body);
        }
    }
}