using FliGen.Domain.Common;
using FliGen.Domain.Entities;
using FliGen.Domain.Entities.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

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