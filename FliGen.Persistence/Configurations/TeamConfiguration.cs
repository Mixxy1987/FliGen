using FliGen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Persistence.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Team");
            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.TeamRoleId)
                .IsRequired();

            builder.Ignore(x => x.TeamRole);

            builder.Property(e => e.PlayersCount)
                .IsRequired();

            builder.HasOne(e => e.Tour)
                .WithMany(e => e.Teams)
                .HasForeignKey(e => e.TourId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(e => new { e.TourId, e.TeamRoleId });
        }
    }
}