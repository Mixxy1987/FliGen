﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FliGen.Persistence.Contexts
{
    public class FliGenContextFactory : IDesignTimeDbContextFactory<FliGenContext>
    {
        public FliGenContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FliGenContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=FliGen; Trusted_Connection=True; MultipleActiveResultSets=true");
            return new FliGenContext(optionsBuilder.Options);
        }
    }
}