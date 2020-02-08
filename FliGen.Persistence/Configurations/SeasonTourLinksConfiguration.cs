using Fligen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Persistence.Configurations
{
    public class SeasonTourLinksConfiguration : IEntityTypeConfiguration<SeasonTourLink>
    {
        public void Configure(EntityTypeBuilder<SeasonTourLink> builder)
        {
            builder.ToTable("SeasonTourLinks");
            builder.Property(e => e.SeasonId)
                .IsRequired();
            builder.Property(e => e.TourId)
                .IsRequired();
            builder.HasKey(e => new { e.SeasonId, e.TourId });
        }
    }
}