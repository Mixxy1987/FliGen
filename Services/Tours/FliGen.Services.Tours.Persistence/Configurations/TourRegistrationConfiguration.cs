using FliGen.Services.Tours.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Services.Tours.Persistence.Configurations
{
    public class TourRegistrationConfiguration : IEntityTypeConfiguration<TourRegistration>
    {
        public void Configure(EntityTypeBuilder<TourRegistration> builder)
        {
            builder.ToTable("TourRegistration");

            builder.Property(e => e.TourId)
                .IsRequired();
            builder.Property(e => e.PlayerId)
                .IsRequired();
            builder.Property(e => e.RegistrationDate)
                .IsRequired();
        }
    }
}