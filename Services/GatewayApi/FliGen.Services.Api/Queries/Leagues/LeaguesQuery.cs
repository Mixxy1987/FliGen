using System;
using FliGen.Common.Types;

namespace FliGen.Services.Api.Queries.Leagues
{
    public class LeaguesQuery : PagedQuery
    {
        public string PlayerExternalId { get; }
        public int[] LeaguesId { get; }
        public int[] PlayersId { get; }

        public LeaguesQuery(
            string playerExternalId,
            int[] leaguesId,
            int[] playersId,
            int? size,
            int? page):base(size, page)
        {
            PlayerExternalId = playerExternalId;
            LeaguesId = leaguesId;
            PlayersId = playersId;
        }
    }
}