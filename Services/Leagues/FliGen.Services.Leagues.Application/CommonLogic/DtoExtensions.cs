using FliGen.Services.Leagues.Application.Dto.Enum;
using FliGen.Services.Leagues.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FliGen.Services.Leagues.Application.CommonLogic
{
	public static class DtoExtensions
	{
		public static void EnrichByPlayerLeagueJoinStatus(this List<Dto.League> leagues, IEnumerable<LeaguePlayerLink> links)
		{
			foreach (var link in links)
			{
				Dto.League league = leagues.Single(x => x.Id == link.LeagueId);

				if (link.JoinTime == null)
				{
					league.PlayerLeagueJoinStatus = PlayerLeagueJoinStatus.Waiting;
				}
				else if (link.LeaveTime == null)
				{
					league.PlayerLeagueJoinStatus = PlayerLeagueJoinStatus.Joined;
				}
			}
		}
	}
}