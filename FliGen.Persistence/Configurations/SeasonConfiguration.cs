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
            builder.Property(e => e.End)
                .IsRequired();
            builder.HasKey(e => new {e.Start, e.End});
        }
    }
}