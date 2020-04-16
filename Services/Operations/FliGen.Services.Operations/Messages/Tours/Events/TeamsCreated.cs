using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Operations.Messages.Tours.Events
{
    [MessageNamespace("teams")]
    public class TeamsCreated : IEvent
    {
        public int[][] Teams { get; set; }

        [JsonConstructor]
        public TeamsCreated(int[][] teams)
        {
            Teams = teams;
        }
    }
}