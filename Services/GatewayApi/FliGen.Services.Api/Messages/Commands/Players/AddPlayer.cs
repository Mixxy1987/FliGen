using FliGen.Common.Messages;

namespace FliGen.Services.Api.Messages.Commands.Players
{
    [MessageNamespace("players")]
    public class AddPlayer : ICommand
    {
        public string ExternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Rate { get; set; }
    }
}