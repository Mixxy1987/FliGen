﻿using FliGen.Services.Notifications.Application.Dto;
using FliGen.Services.Notifications.Application.Queries.Players;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FliGen.Services.Notifications.Application.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IPlayersService
    {
        [AllowAnyStatusCode]
        [Get("players")]
        Task<IEnumerable<PlayerWithRateDto>> GetAsync([Query]PlayersQuery playersQuery);
    }
}