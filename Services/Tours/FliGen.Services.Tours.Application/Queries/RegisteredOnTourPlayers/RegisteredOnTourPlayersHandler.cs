using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Tours.Application.Dto;
using FliGen.Services.Tours.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Tours.Application.Queries.RegisteredOnTourPlayers
{
    public class RegisteredOnTourPlayersHandler : IRequestHandler<RegisteredOnTourPlayers, IEnumerable<PlayerInternalIdDto>>
    {
        private readonly IUnitOfWork _uow;

        public RegisteredOnTourPlayersHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<IEnumerable<PlayerInternalIdDto>> Handle(RegisteredOnTourPlayers request, CancellationToken cancellationToken)
        {
            //todo:: paged return or all?

            var toursRegRepo = _uow.GetReadOnlyRepository<TourRegistration>();
            var tourReg = toursRegRepo.GetList(t => t.TourId == request.TourId);
            if (tourReg is null)
            {
                return null;
            }

            return Task.FromResult(tourReg.Items.Select(t => new PlayerInternalIdDto {InternalId = t.PlayerId}));
        }
    }
}