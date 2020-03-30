using FliGen.Services.Leagues.Domain.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Services.Leagues.Persistence.Configurations
{
    public class LeagueTypeConfiguration : IEntityTypeConfiguration<LeagueType>
    {
        public void Configure(EntityTypeBuilder<LeagueType> builder)
        {
            builder.ToTable("LeagueType");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(e => e.Name)
                .IsUnique();
        }
    }
}