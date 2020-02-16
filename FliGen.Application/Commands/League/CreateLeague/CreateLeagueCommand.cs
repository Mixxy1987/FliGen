﻿using FliGen.Application.Dto;
using MediatR;

namespace FliGen.Application.Commands.League.CreateLeague
{
    public class CreateLeagueCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public LeagueType LeagueType { get; set; }
    }
}