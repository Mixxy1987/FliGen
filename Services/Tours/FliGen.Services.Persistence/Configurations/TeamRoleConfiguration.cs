using FliGen.Services.Tours.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Services.Tours.Persistence.Configurations
{
    public class TeamRoleConfiguration : IEntityTypeConfiguration<TeamRole>
    {
        public void Configure(EntityTypeBuilder<TeamRole> builder)
        {
            builder.ToTable("TeamRole");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(e => e.Name)
                .IsUnique();
        }
    }
}