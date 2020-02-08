using Fligen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Persistence.Configurations
{
    public class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            builder.ToTable("League");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasIndex(e => e.Name)
                .IsUnique();
        }
    }
}