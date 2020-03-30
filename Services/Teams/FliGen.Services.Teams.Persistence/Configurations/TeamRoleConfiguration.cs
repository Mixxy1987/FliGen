using FliGen.Services.Teams.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Services.Teams.Persistence.Configurations
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