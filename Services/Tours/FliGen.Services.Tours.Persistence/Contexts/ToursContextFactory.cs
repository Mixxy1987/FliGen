﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FliGen.Services.Tours.Persistence.Contexts
{
    public class ToursContextFactory : IDesignTimeDbContextFactory<ToursContext>
    {
        public ToursContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ToursContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=FliGen.Tours; Trusted_Connection=True; MultipleActiveResultSets=true");
            return new ToursContext(optionsBuilder.Options);
        }
    }
}
