using FliGen.Services.Leagues.Domain.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Services.Leagues.Persistence.Configurations
{
    public class LeaguePlayerPriorityConfiguration : IEntityTypeConfiguration<LeaguePlayerPriority>
    {
        public void Configure(EntityTypeBuilder<LeaguePlayerPriority> builder)
        {
            builder.ToTable("LeaguePlayerPriority");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(e => e.Name)
                .IsUnique();
        }
    }
}