using FliGen.Services.Leagues.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Services.Leagues.Persistence.Configurations
{
    public class LeagueSettingConfiguration : IEntityTypeConfiguration<LeagueSettings>
    {
        public void Configure(EntityTypeBuilder<LeagueSettings> builder)
        {
            builder.ToTable("LeagueSettings");
            builder.Property(e => e.RequireConfirmation)
                .IsRequired();
            builder.Property(e => e.Visibility)
                .IsRequired();

            builder.HasOne(e => e.League)
                .WithOne(e => e.LeagueSettings)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}