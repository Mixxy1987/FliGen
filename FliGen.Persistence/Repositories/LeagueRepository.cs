using FliGen.Domain.Common;
using FliGen.Domain.Entities;
using FliGen.Domain.Entities.Enum;
using FliGen.Domain.Repositories;
using FliGen.Persistence.Contextes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            return _context.Leagues.SingleAsync(x => x.Id == id);
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
    }
}