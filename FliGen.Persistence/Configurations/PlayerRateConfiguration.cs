using Fligen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Persistence.Configurations
{
    public class PlayerRateConfiguration : IEntityTypeConfiguration<PlayerRate>
    {
        public void Configure(EntityTypeBuilder<PlayerRate> builder)
        {
            builder.ToTable("PlayerRate");
            builder.Property(e => e.Rate)
                .IsRequired()
                .HasMaxLength(3);
            builder.Property(e => e.Date)
                .IsRequired();
        }
    }
}