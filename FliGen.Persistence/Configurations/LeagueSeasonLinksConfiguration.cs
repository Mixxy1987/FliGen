using FliGen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Persistence.Configurations
{
    public class LeagueSeasonLinksConfiguration : IEntityTypeConfiguration<LeagueSeasonLink>
    {
        public void Configure(EntityTypeBuilder<LeagueSeasonLink> builder)
        {
            builder.ToTable("LeagueSeasonLinks");
            builder.Property(e => e.LeagueId)
                .IsRequired();
            builder.Property(e => e.SeasonId)
                .IsRequired();
            builder.HasKey(e => new {e.SeasonId, e.LeagueId});
        }
    }
}