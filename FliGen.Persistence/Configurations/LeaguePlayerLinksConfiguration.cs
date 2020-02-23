﻿using FliGen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Persistence.Configurations
{
    public class LeaguePlayerLinksConfiguration : IEntityTypeConfiguration<LeaguePlayerLink>
    {
        public void Configure(EntityTypeBuilder<LeaguePlayerLink> builder)
        {
            builder.ToTable("LeaguePlayerLinks");

            builder.HasOne(pc => pc.League)
                .WithMany(p => p.LeaguePlayerLinks)
                .HasForeignKey(pc => pc.LeagueId);

            builder.HasOne(pc => pc.Player)
                .WithMany(c => c.LeaguePlayerLinks)
                .HasForeignKey(pc => pc.PlayerId);

            builder.HasKey(e => new {e.PlayerId, e.LeagueId});
        }
    }
}