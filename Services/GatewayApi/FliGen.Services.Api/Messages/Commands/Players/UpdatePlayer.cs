using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Api.Messages.Commands.Players
{
    [MessageNamespace("players")]
    public class UpdatePlayer : ICommand
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int LeagueId { get; set; }
        public string Rate { get; set; }
    }
}