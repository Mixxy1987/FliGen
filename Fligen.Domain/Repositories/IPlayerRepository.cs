﻿using FliGen.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using FliGen.Domain.Common;

namespace FliGen.Domain.Repositories
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<IEnumerable<Player>> GetAsync();
        Task AddAsync(Player player);
        Task RemoveAsync(Player player);
        Task UpdateAsync(Player player);
    }
}