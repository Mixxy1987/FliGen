using System;
using FliGen.Domain.Common;
using FliGen.Domain.Entities.Enum;

namespace FliGen.Domain.Entities
{
    public class LeaguePlayerLink
    {
        public int LeagueId { get; }
        public League League { get; }
        public int PlayerId { get; }
        public Player Player { get; }
        public DateTime CreationTime { get; }
        public DateTime? JoinTime { get; }
        public DateTime? LeaveTime { get; }

        public int LeaguePlayerRoleId
        {
            get
            {
                return Role.Id;
            }
            set
            {
                Role = Enumeration.FromValue<LeaguePlayerRole>(value);
            }
        }

        public LeaguePlayerRole Role { get; private set; }

        protected LeaguePlayerLink()
        {
        }

        private LeaguePlayerLink(
	        int leagueId, 
	        int playerId,
	        int roleId,
	        DateTime? joinTime = null,
	        DateTime? leaveTime = null)
        {
	        LeagueId = leagueId;
	        PlayerId = playerId;
	        LeaguePlayerRoleId = roleId;
            JoinTime = joinTime;
	        LeaveTime = leaveTime;
        }

        public static LeaguePlayerLink CreateWaitingLink(int leagueId, int playerId)
        {
	        return new LeaguePlayerLink(leagueId, playerId, LeaguePlayerRole.User.Id);
        }

        public static LeaguePlayerLink CreateJoinedLink(int leagueId, int playerId)
        {
	        return new LeaguePlayerLink(
                leagueId,
                playerId,
                LeaguePlayerRole.User.Id,
		        DateTime.Now);
        }

        public static LeaguePlayerLink UpdateToJoinedLink(LeaguePlayerLink link)
        {
	        return new LeaguePlayerLink(
		        link.LeagueId,
		        link.PlayerId,
		        link.LeaguePlayerRoleId,
		        DateTime.Now);
        }

        public static LeaguePlayerLink UpdateToLeftLink(LeaguePlayerLink link)
        {
	        return new LeaguePlayerLink(
		        link.LeagueId,
		        link.PlayerId,
		        link.LeaguePlayerRoleId,
		        link.JoinTime, 
		        DateTime.Now);
        }
    }
}