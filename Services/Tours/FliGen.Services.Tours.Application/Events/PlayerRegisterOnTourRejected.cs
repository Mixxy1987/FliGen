using FliGen.Common.Messages;
using Newtonsoft.Json;
using System;

namespace FliGen.Services.Tours.Application.Events
{
    public class PlayerRegisterOnTourRejected : IRejectedEvent
    {
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public PlayerRegisterOnTourRejected(string reason, string code)
        {
            Reason = reason;
            Code = code;
        }
    }
}