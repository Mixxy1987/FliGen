﻿using FliGen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Persistence.Configurations
{
    public class TourConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            builder.ToTable("Tour");
            builder.Property(e => e.Date)
                .IsRequired();
            builder.Property(e => e.HomeCount);
            builder.Property(e => e.GuestCount);

            builder.HasOne(e => e.Season)
                .WithMany(e => e.Tours)
                .HasForeignKey(e => e.SeasonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(e => new { e.Date, e.SeasonId });
        }
    }
}