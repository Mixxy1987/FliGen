using FliGen.Services.Players.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Services.Players.Persistence.Configurations
{
    public class PlayerRateConfiguration : IEntityTypeConfiguration<PlayerRate>
    {
        public void Configure(EntityTypeBuilder<PlayerRate> builder)
        {
            builder.ToTable("PlayerRate");
            builder.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(3);
            builder.Property(e => e.Date)
                .IsRequired();

            builder.HasOne(e => e.Player)
                .WithMany(e => e.Rates)
                .HasForeignKey(e => e.PlayerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.LeagueId)
                .IsRequired();
        }
    }
}