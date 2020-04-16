using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Operations.Messages.Tours.Events
{
    [MessageNamespace("teams")]
    public class GenerateTeamsRejected : IRejectedEvent
    {
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public GenerateTeamsRejected(string reason, string code)
        {
            Reason = reason;
            Code = code;
        }
    }
}