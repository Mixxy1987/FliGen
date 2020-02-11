using FliGen.Domain.Entities;
using FliGen.Domain.Repositories;
using FliGen.Persistence.Contextes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FliGen.Persistence.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly FliGenContext _context;

        public PlayerRepository(FliGenContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetPlayersWithRatesAsync()
        {
            return await _context.Players.Include(x => x.Rates).ToArrayAsync();
        }

        public async Task AddPlayer(Player player)
        {
            var entityEntry = await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }

        public Task RemovePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public Task<Player> GetPlayerById(int playerId)
        {
            return _context.Players.FirstOrDefaultAsync(x => x.Id == playerId);
        }

        public Task<Player> GetPlayerId(int playerId)
        {
            return _context.Players.FirstOrDefaultAsync(x => x.Id == playerId);
        }

        public IEnumerable<Player> FindPlayers(Func<Player, bool> predicate)
        {
            return _context.Players.Include(x => x.Rates).Where(predicate);
        }
    }
}