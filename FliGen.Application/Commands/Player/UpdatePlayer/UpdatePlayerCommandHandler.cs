using FliGen.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Common.SeedWork.Repository;

namespace FliGen.Application.Commands.Player.UpdatePlayer
{
    public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand>
    {
        private readonly IUnitOfWork _uow;

        public UpdatePlayerCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var playerRepo = _uow.GetRepositoryAsync<Domain.Entities.Player>();

            /*Domain.Entities.Player player = (await playerRepo.GetListAsync(
                predicate: x => x.Id == request.Id,
                include: source => source.Include(a => a.Rates),
                size : 1,
                cancellationToken : cancellationToken)).Items[0];*/

            Domain.Entities.Player player = await playerRepo.SingleAsync(
                predicate: x => x.Id == request.Id,
                include: source => source.Include(a => a.Rates));

            double playerRate = player.Rates.OrderBy(y => y.Date).Last().Value;

            if (playerRate.ToString("F2") != request.Rate)
            {
                PlayerRate newPlayerRate = new PlayerRate(DateTime.Now, request.Rate, player.Id);
                var playerRatesRepo = _uow.GetRepositoryAsync<PlayerRate>();
                await playerRatesRepo.AddAsync(newPlayerRate, cancellationToken);
            }

            playerRepo.UpdateAsync(Domain.Entities.Player.GetUpdated(
                player.Id, request.FirstName, request.LastName, player.ExternalId));

            _uow.SaveChanges();

            return Unit.Value;
        }
    }
}
