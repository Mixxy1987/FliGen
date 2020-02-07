using Fligen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Persistence.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Player", "FliGen");
            builder.Property(e => e.FirstName)
                .HasMaxLength(20);
            builder.Property(e => e.LastName)
                .HasMaxLength(20);
        }
    }
}