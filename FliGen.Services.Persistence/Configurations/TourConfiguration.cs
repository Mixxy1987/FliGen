using FliGen.Services.Tours.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Services.Tours.Persistence.Configurations
{
    public class TourConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            builder.ToTable("Tour");
            builder.Property(e => e.Date)
                .IsRequired();
            builder.Property(e => e.HomeCount);
            builder.Property(e => e.GuestCount);
            builder.Property(e => e.SeasonId);

            builder.HasIndex(e => new { e.Date, e.SeasonId });

            builder.Property(e => e.TourStatusId)
                .IsRequired();

            builder.Ignore(x => x.TourStatus);
        }
    }
}