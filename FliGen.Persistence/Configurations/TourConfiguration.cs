using FliGen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Persistence.Configurations
{
    public class TourConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            builder.ToTable("Tour");
            builder.Property(e => e.TourDate)
                .IsRequired();
            builder.Property(e => e.HomeTeamId)
                .IsRequired();
            builder.Property(e => e.GuestTeamId)
                .IsRequired();
            builder.Property(e => e.HomeCount)
                .IsRequired();
            builder.Property(e => e.GuestCount)
                .IsRequired();

            builder.HasOne(e => e.Season)
                .WithMany(e => e.Tours)
                .HasForeignKey(e => e.SeasonId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}