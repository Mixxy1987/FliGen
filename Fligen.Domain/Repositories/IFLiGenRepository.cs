using Fligen.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fligen.Domain.Repositories
{
    public interface IFLiGenRepository
    {
        Task<IEnumerable<Player>> GetPlayersAsync();
    }
}