using System;
using FliGen.Domain.Common;
using FliGen.Domain.Entities.Enum;

namespace FliGen.Domain.Entities
{
    public class LeaguePlayerLink
    {
        public int LeagueId { get; set; }
        public League League { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public DateTime JoinTime { get; set; }
        public DateTime? LeaveTime { get; set; }

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
    }
}