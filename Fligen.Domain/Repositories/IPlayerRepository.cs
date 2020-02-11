using FliGen.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FliGen.Domain.Repositories
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetPlayersWithRatesAsync();
        Task AddPlayer(Player player);
        Task RemovePlayer(Player player);
        Task UpdatePlayer(Player player);
    }
}