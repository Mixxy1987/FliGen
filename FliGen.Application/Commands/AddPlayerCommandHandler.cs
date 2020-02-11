using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Domain.Entities;
using FliGen.Domain.Repositories;
using MediatR;

namespace FliGen.Application.Commands
{
    public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand>
    {
        private readonly IPlayerRepository _repository;

        public AddPlayerCommandHandler(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddPlayerCommand request, CancellationToken cancellationToken)
        {
            var player = new Player()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Rates = new List<PlayerRate>()
                {
                    new PlayerRate()
                    {
                        Date = DateTime.Now,
                        Rate = request.Rate
                    }
                }
            };
            
            await _repository.AddAsync(player);
            
            return Unit.Value;
        }
    }
}
