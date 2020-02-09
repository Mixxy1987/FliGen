using System.Collections.Generic;
using System.Threading.Tasks;
using FliGen.Domain.Entities;
using FliGen.Domain.Repositories;
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

        public async Task<IEnumerable<Player>> GetPlayersWithRatesAsync()
        {
            return await _context.Players.Include(x => x.Rates).ToArrayAsync();
        }

        public async Task AddPlayer(Player player)
        {
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }
    }
}
