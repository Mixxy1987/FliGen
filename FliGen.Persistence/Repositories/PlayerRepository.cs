using FliGen.Domain.Entities;
using FliGen.Domain.Repositories;
using FliGen.Persistence.Contextes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FliGen.Domain.Common;

namespace FliGen.Persistence.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly FliGenContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public PlayerRepository(FliGenContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetAsync()
        {
            return await _context.Players.Include(x => x.Rates).ToArrayAsync();
        }

        public async Task AddAsync(Player player)
        {
            var entityEntry = await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }

        public Task RemoveAsync(Player player)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Player player)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Player> FindPlayers(Func<Player, bool> predicate)
        {
            return _context.Players.Include(x => x.Rates).Where(predicate);
        }
    }
}