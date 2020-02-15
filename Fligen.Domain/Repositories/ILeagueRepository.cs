using System.Collections.Generic;
using FliGen.Domain.Entities;
using System.Threading.Tasks;
using FliGen.Domain.Common;

namespace FliGen.Domain.Repositories
{
    public interface ILeagueRepository : IRepository<League>
    {
        Task<IEnumerable<LeagueType>> GetTypes();
        Task<League> GetByIdAsync(int id);
        Task CreateAsync(League league);
        Task DeleteByIdAsync(int id);
    }
}