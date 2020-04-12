using FliGen.Common.Messages;
using Newtonsoft.Json;
using System;

namespace FliGen.Services.Teams.Application.Events
{
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