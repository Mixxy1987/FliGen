using FliGen.Services.Players.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Services.Players.Persistence.Configurations
{
    public class PlayerMessageLinkConfiguration : IEntityTypeConfiguration<PlayerMessageLink>
    {
        public void Configure(EntityTypeBuilder<PlayerMessageLink> builder)
        {
            builder.ToTable("PlayerMessageLink");

            builder.Property(e => e.PlayerId)
                .IsRequired();

            builder.Property(e => e.MessageId)
                .IsRequired();

            builder.Property(e => e.MessageTypeId)
                .IsRequired();

            builder.Property(e => e.Read)
                .IsRequired();

            builder.HasKey(e => new { e.PlayerId, e.MessageId });

            builder.Ignore(x => x.MessageType);
        }
    }
}