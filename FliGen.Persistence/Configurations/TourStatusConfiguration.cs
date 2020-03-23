﻿using FliGen.Domain.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Persistence.Configurations
{
    public class TourStatusConfiguration : IEntityTypeConfiguration<TourStatus>
    {
        public void Configure(EntityTypeBuilder<TourStatus> builder)
        {
            builder.ToTable("TourStatus");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(e => e.Name)
                .IsUnique();
        }
    }
}