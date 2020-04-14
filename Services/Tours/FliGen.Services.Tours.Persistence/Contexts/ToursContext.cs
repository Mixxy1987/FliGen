﻿using FliGen.Services.Tours.Domain.Entities;
using FliGen.Services.Tours.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FliGen.Services.Tours.Persistence.Contexts
{
    public class ToursContext : DbContext
    {
        public DbSet<TourStatus> TourStatuses { get; set; }
        public DbSet<Tour> Tours { get; set; }

        public ToursContext(DbContextOptions<ToursContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            //optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); // not working
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TourConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
