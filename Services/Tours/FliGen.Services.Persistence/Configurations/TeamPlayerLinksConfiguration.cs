using FliGen.Services.Tours.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Services.Tours.Persistence.Configurations
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