using FliGen.Common.SeedWork;

namespace FliGen.Services.Players.Domain.Entities
{
    public class Message : Entity
    {
        public string From { get; set; }
        public string Topic { get; set; }
        public string Body { get; set; }
    }
}