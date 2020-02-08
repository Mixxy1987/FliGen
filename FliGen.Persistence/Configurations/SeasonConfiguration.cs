using Fligen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Persistence.Configurations
{
    public class SeasonConfiguration : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.ToTable("Season");
            builder.Property(e => e.Start)
                .IsRequired();
            builder.Property(e => e.Finish)
                .IsRequired();
            
            builder.HasOne(e => e.League)
                .WithMany(e => e.Seasons)
                .HasForeignKey(e => e.LeagueId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}