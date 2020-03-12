using FliGen.Domain.Entities;
using FliGen.Domain.Repositories;
using FliGen.Persistence.Contextes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            _context.Remove(_context.Players.Single(x => x.Id == id));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Player player)
        {
	        _context.Players.Update(player);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateByParametersAsync(Player player)
        {
	        Player foundPlayer = await _context.Players.AsNoTracking().Include(x => x.Rates).
		        SingleOrDefaultAsync(x =>
			        x.FirstName == player.FirstName &&
			        x.LastName == player.LastName);

            if (foundPlayer == null)
	        {
		        await AddAsync(player);
	        }
	        else
	        {
                player = Player.GetUpdated(player, foundPlayer.Id);
		        await UpdateAsync(player);
	        }
        }

        public IEnumerable<Player> FindPlayers(Func<Player, bool> predicate)
        {
	        return _context.Players.Include(x => x.Rates).Where(predicate);
        }

        public Task<Player> GetByExternalId(string externalId)
        {
	        return GetPlayerInternalAsync(externalId);
        }

        public async Task<Player> GetByExternalIdOrThrowAsync(string externalId)
        {
	        return await GetPlayerInternalAsync(externalId) ??
	               throw new InvalidDataException("Invalid players externalId - no such player");
        }


        private Task<Player> GetPlayerInternalAsync(string externalId)
        {
	        return _context.Players.Include(x => x.LeaguePlayerLinks).SingleAsync(x => x.ExternalId == externalId);
        }
    }
}