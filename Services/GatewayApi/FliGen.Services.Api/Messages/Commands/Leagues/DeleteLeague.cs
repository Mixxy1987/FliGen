using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Api.Messages.Commands.Leagues
{
    [MessageNamespace("leagues")]
    public class DeleteLeague : ICommand
    {
        public int Id { get; set; }

        private DeleteLeague(){}

        [JsonConstructor]
        public DeleteLeague(int id)
        {
            Id = id;
        }
    }
}