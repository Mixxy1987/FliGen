using System.Collections.Generic;
using FliGen.Domain.Entities;
using System.Threading.Tasks;
using FliGen.Domain.Common;

namespace FliGen.Domain.Repositories
{
    public interface ILeagueRepository : IRepository<League>
    {
        Task<IEnumerable<LeagueType>> GetLeagueTypesAsync();
        Task<IEnumerable<League>> GetLeaguesAsync();
        Task<League> GetByIdAsync(int id);
        Task CreateAsync(League league);
        Task DeleteByIdAsync(int id);
        Task UpdateAsync(League league);
    }
}