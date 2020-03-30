﻿using System.Collections.Generic;
using FliGen.Services.Players.Application.Dto;
using MediatR;

namespace FliGen.Services.Players.Application.Queries.Players
{
    /*
     * 1) get actual rates
     * 2) get all rates
     * 3) only in concrete leagues
     * 4) 1,2,3 for concrete players
     */
    public class PlayersQuery : IRequest<IEnumerable<PlayerWithRate>>
    {
        public int Size { get; }
        public PlayersQueryType QueryType { get; }
        public int[] PlayerIds { get; }
        public int[] LeagueIds { get; }

        public PlayersQuery(int size, PlayersQueryType queryType, int[] leagueIds, int[] playerIds)
        {
            Size = size;
            QueryType = queryType;
            LeagueIds = leagueIds;
            PlayerIds = playerIds;
        }
    }
}
