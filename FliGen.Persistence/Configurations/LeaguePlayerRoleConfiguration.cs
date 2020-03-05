using FliGen.Domain.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Persistence.Configurations
{
    public class LeaguePlayerRoleConfiguration : IEntityTypeConfiguration<LeaguePlayerRole>
    {
        public void Configure(EntityTypeBuilder<LeaguePlayerRole> builder)
        {
            builder.ToTable("LeaguePlayerRole");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(e => e.Name)
                .IsUnique();
        }
    }
}