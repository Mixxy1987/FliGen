using FliGen.Services.Leagues.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Services.Leagues.Persistence.Configurations
{
    public class LeaguePlayerLinksConfiguration : IEntityTypeConfiguration<LeaguePlayerLink>
    {
        public void Configure(EntityTypeBuilder<LeaguePlayerLink> builder)
        {
            builder.ToTable("LeaguePlayerLinks");

            builder.HasOne(pc => pc.League)
                .WithMany(p => p.LeaguePlayerLinks)
                .HasForeignKey(pc => pc.LeagueId);

            builder.Property(e => e.CreationTime)
                .IsRequired();
            builder.Property(e => e.JoinTime);
            builder.Property(e => e.LeaveTime);
            builder.Property(e => e.Actual)
                .IsRequired();

            builder.HasKey(e => new {e.PlayerId, e.LeagueId, e.CreationTime});

            builder.Property(e => e.LeaguePlayerRoleId)
                .IsRequired();
        }
    }
}