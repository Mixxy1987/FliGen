using System.Collections.Generic;
using FliGen.Common.SeedWork;
using FliGen.Common.Types;
using FliGen.Services.Leagues.Domain.Entities.Enum;

namespace FliGen.Services.Leagues.Domain.Entities
{
	public class League : Entity, IAggregateRoot
	{
		public string Name { get; }
		public string Description { get; }

		public LeagueSettings LeagueSettings { get; }
		public int LeagueTypeId 
		{
			get => Type.Id;
			set => Type = Enumeration.FromValue<LeagueType>(value);
		}

		public LeagueType Type { get; private set; }

		public virtual ICollection<LeaguePlayerLink> LeaguePlayerLinks { get; }

		public bool IsVisible() => LeagueSettings.Visibility;
		public bool IsRequireConfirmation() => LeagueSettings.RequireConfirmation;

		public League() {}

		private League(string name, string description, LeagueType type) : this()
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new FliGenException("cannot_create_league_with_empty_name", "Cannot create league with empty name.");
			}

			Name = name;
			Description = description;
			Type = type;
			LeagueSettings = new LeagueSettings(true, false);
		}

		private League(string name, string description, LeagueType type, LeagueSettings settings = null) : this()
		{
			Name = name;
			Description = description;
			Type = type;
			LeagueSettings = settings;
		}

		public static League Create(string name, string description, LeagueType type)
		{
			return new League(name, description, type);
		}

		public static League GetUpdated(int id, string firstName, string lastName, LeagueType type)
		{
			return new League(firstName, lastName, type, null) { Id = id };
		}
	}
}