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

        /*[JsonConstructor]
        public UpdatePlayer(int id, string firstName, string lastName, int leagueId, string rate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            LeagueId = leagueId;
            Rate = rate;
        }*/
    }
}