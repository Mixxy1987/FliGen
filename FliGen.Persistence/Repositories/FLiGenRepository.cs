using System.Collections.Generic;
using System.Threading.Tasks;
using Fligen.Domain.Entities;
using Fligen.Domain.Repositories;
using FliGen.Persistence.Contextes;
using Microsoft.EntityFrameworkCore;

namespace FliGen.Persistence.Repositories
{
    public class FLiGenRepository : IFLiGenRepository
    {
        private readonly FliGenContext _context;

        public FLiGenRepository(FliGenContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetPlayersAsync()
        {
            return await _context.Players.ToArrayAsync();
        }
    }
}
