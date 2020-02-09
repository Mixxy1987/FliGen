using FliGen.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FliGen.Domain.Repositories
{
    public interface IFLiGenRepository
    {
        Task<IEnumerable<Player>> GetPlayersWithRatesAsync();
        Task AddPlayer(Player player);
    }
}