using FliGen.Services.Teams.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Services.Teams.Persistence.Configurations
{
    public class TeamPlayerLinksConfiguration : IEntityTypeConfiguration<TeamPlayerLink>
    {
        public void Configure(EntityTypeBuilder<TeamPlayerLink> builder)
        {
            builder.ToTable("TeamPlayerLinks");
            builder.Property(e => e.PlayerId)
                .IsRequired();
            builder.Property(e => e.TeamId)
                .IsRequired();
            builder.HasKey(e => new { e.PlayerId, e.TeamId });
        }
    }
}