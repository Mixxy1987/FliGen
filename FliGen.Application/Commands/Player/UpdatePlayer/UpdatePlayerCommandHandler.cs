﻿using FliGen.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Application.Commands.Player.UpdatePlayer
{
    public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand>
    {
        private readonly IPlayerRepository _repository;

        public UpdatePlayerCommandHandler(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(Domain.Entities.Player.GetUpdated(request.Id, request.FirstName, request.LastName, double.Parse(request.Rate)));
            
            return Unit.Value;
        }
    }
}