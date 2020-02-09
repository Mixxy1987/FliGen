using FliGen.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FliGen.Domain.Repositories
{
    public interface IFLiGenRepository
    {
        Task<IEnumerable<Player>> GetPlayersAsync();
        Task AddPlayer(Player player);
    }
}