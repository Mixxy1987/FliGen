﻿using FliGen.Common.Messages;
using FliGen.Services.Api.Models.Leagues;
using Newtonsoft.Json;

namespace FliGen.Services.Api.Messages.Commands.Leagues
{
    [MessageNamespace("leagues")]
    public class UpdateLeague : ICommand
    {
        public int LeagueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LeagueType LeagueType { get; set; }

        private UpdateLeague() { }

        [JsonConstructor]
        public UpdateLeague(int leagueId, string name, string description, LeagueType leagueType)
        {
            LeagueId = leagueId;
            Name = name;
            Description = description;
            LeagueType = leagueType;
        }
    }
}