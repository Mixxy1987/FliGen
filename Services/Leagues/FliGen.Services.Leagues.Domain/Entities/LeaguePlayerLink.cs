using System;
using FliGen.Common.SeedWork;
using FliGen.Common.Types;
using FliGen.Services.Leagues.Domain.Entities.Enum;

namespace FliGen.Services.Leagues.Domain.Entities
{
    public class LeaguePlayerLink
    {
        public int LeagueId { get; }
        public League League { get; }
        public int PlayerId { get; }
        public DateTime CreationTime { get; }
        public DateTime? JoinTime { get; private set; }
        public DateTime? LeaveTime { get; private set; }

        public int LeaguePlayerRoleId
        {
            get => Role.Id;
            set => Enumeration.FromValue<LeaguePlayerRole>(value);
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
	        if (leagueId <= 0)
	        {
		        throw new FliGenException("invalid_leagueId", $"Invalid leagueId. - {leagueId}");
	        }

	        if (playerId <= 0)
	        {
		        throw new FliGenException("invalid_playerId", $"Invalid PlayerId. - {playerId}");
	        }

            LeagueId = leagueId;
	        PlayerId = playerId;
	        LeaguePlayerRoleId = roleId;
            CreationTime = DateTime.Now;
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

        public void UpdateToJoined()
        {
           JoinTime = DateTime.Now;
        }

        public void UpdateToLeft()
        {
            LeaveTime = DateTime.Now;
        }


        public bool InLeftStatus()
        {
	        return JoinTime != null && LeaveTime != null;
        }
        public bool InJoinedStatus()
        {
	        return JoinTime != null && LeaveTime == null;
        }

        public bool InWaitingStatus()
        {
	        return JoinTime == null && LeaveTime == null;
        }
    }
}