﻿using FliGen.Services.Tours.Application.Dto;
using FliGen.Services.Tours.Application.Queries.LeaguesQuery;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FliGen.Services.Tours.Application.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface ILeaguesService
    {
        [AllowAnyStatusCode]
        [Get("leagues")]
        Task<IEnumerable<LeagueDto>> GetLeagues([Query]LeaguesQuery leaguesQuery);
    }
}