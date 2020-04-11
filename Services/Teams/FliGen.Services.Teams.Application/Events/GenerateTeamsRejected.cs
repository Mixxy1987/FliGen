using FliGen.Common.Messages;
using Newtonsoft.Json;
using System;

namespace FliGen.Services.Teams.Application.Events
{
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