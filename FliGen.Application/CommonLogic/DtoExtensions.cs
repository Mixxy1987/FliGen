using System.Collections.Generic;
using System.Linq;
using FliGen.Application.Dto;
using FliGen.Domain.Entities;
using League = FliGen.Application.Dto.League;

namespace FliGen.Application.CommonLogic
{
	public static class DtoExtensions
	{
		public static void EnrichByPlayerLeagueJoinStatus(this List<League> leagues, IEnumerable<LeaguePlayerLink> links)
		{
			foreach (var link in links)
			{
				League league = leagues.Single(x => x.Id == link.LeagueId);

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