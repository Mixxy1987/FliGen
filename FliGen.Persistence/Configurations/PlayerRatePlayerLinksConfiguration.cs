using Fligen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Persistence.Configurations
{
    public class PlayerRatePlayerLinksConfiguration : IEntityTypeConfiguration<PlayerRatePlayerLink>
    {
        public void Configure(EntityTypeBuilder<PlayerRatePlayerLink> builder)
        {
            builder.ToTable("PlayerRatePlayerLinks");
            builder.Property(e => e.PlayerId)
                .IsRequired();
            builder.Property(e => e.PlayerRateId)
                .IsRequired();
            builder.HasKey(e => new { e.PlayerId, e.PlayerRateId });
        }
    }
}