using FliGen.Services.Teams.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Services.Teams.Persistence.Configurations
{
    public class TemporalTeamPlayerLinksConfiguration : IEntityTypeConfiguration<TemporalTeamPlayerLink>
    {
        public void Configure(EntityTypeBuilder<TemporalTeamPlayerLink> builder)
        {
            builder.ToTable("TemporalTeamPlayerLinks");
            builder.Property(e => e.LeagueId)
                .IsRequired();
            builder.Property(e => e.TourId)
                .IsRequired();
            builder.Property(e => e.PlayerId)
                .IsRequired();
            builder.Property(e => e.TeamId)
                .IsRequired();
            builder.HasKey(e => new { e.PlayerId, e.TeamId });
        }
    }
}