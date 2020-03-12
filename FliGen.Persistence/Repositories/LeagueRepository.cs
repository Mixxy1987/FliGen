using System;
using FliGen.Domain.Common;
using FliGen.Domain.Entities;
using FliGen.Domain.Entities.Enum;
using FliGen.Domain.Repositories;
using FliGen.Persistence.Contextes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FliGen.Persistence.Repositories
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly FliGenContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public LeagueRepository(FliGenContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LeagueType>> GetLeagueTypesAsync()
        {
            return await _context.LeagueTypes.ToArrayAsync();
        }

        public async Task<IEnumerable<League>> GetLeaguesAsync()
        {
            return await _context.Leagues.ToArrayAsync();
        }

        public Task<League> GetByIdAsync(int id)
        {
            return GetLeagueInternalAsync(id);
        }

        public async Task<League> GetByIdOrThrowAsync(int id)
        {
	        return await GetLeagueInternalAsync(id) ??
	               throw new InvalidDataException("Invalid league id - no such league");
        }

        public async Task CreateAsync(League league)
        {
            await _context.Leagues.AddAsync(league);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            _context.Remove(_context.Leagues.Single(x => x.Id == id));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(League league)
        {
            _context.Leagues.Update(league);
            await _context.SaveChangesAsync();
        }

        public async Task JoinLeagueAsync(LeaguePlayerLink link)
        {
	        _context.LeaguePlayerLinks.Add(link);
	        await _context.SaveChangesAsync();
        }
        
        private Task<League> GetLeagueInternalAsync(int id)
        {
	        return _context.Leagues.Include(x => x.LeaguePlayerLinks).SingleAsync(x => x.Id == id);
        }
    }
}