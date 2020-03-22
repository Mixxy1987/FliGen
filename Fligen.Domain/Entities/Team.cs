using System;
using FliGen.Domain.Common;
using FliGen.Domain.Entities.Enum;

namespace FliGen.Domain.Entities
{
    public class Team : Entity
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }

        public int TeamRoleId
        {
            get
            {
                return TeamRole.Id;
            }
            set
            {
                TeamRole = Enumeration.FromValue<TeamRole>(value);
            }
        }

        public TeamRole TeamRole { get; private set; }

        public int TourId { get; set; }
        public Tour Tour { get; set; }
    }
}