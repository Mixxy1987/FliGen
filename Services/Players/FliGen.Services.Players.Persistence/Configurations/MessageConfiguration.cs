using FliGen.Services.Players.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Services.Players.Persistence.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Message");
            
            builder.Property(e => e.From)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.Property(e => e.Body)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(e => e.Topic)
                .HasMaxLength(250);
        }
    }
}