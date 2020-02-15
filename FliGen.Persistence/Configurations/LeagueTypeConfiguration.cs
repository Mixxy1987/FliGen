using FliGen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Persistence.Configurations
{
    public class LeagueTypeConfiguration : IEntityTypeConfiguration<LeagueType>
    {
        public void Configure(EntityTypeBuilder<LeagueType> builder)
        {
            builder.ToTable("LeagueType");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}