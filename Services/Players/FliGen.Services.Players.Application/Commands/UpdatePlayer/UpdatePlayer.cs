using FliGen.Common.Messages;

namespace FliGen.Services.Players.Application.Commands.UpdatePlayer
{
    [MessageNamespace("players")]
    public class UpdatePlayer: ICommand
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int LeagueId { get; set; }
        public string Rate { get; set; }
    }
}