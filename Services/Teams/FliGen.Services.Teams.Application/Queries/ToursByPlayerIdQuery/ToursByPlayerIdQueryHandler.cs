using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Common.SeedWork.Repository.Paging;
using FliGen.Services.Teams.Application.Dto;
using FliGen.Services.Teams.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Teams.Application.Queries.ToursByPlayerIdQuery
{
    public class ToursByPlayerIdQueryHandler : IRequestHandler<ToursByPlayerIdQuery, ToursByPlayerIdDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ToursByPlayerIdQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public Task<ToursByPlayerIdDto> Handle(ToursByPlayerIdQuery request, CancellationToken cancellationToken)
        {
            var teamPlayerLinksRepo = _uow.GetReadOnlyRepository<TeamPlayerLink>();

            IPaginate<TeamPlayerLink> teamPlayerLinks = teamPlayerLinksRepo
                .GetList(
                    predicate: tpl => tpl.PlayerId == request.PlayerId,
                    size: request.Size,
                    index: request.Page);

            if (teamPlayerLinks.Count == 0)
            {
                return null;
            }

            var teamIds = teamPlayerLinks.Items.Select(x => x.TeamId).ToList();

            var teamRepo = _uow.GetReadOnlyRepository<Team>();

            var toursByPlayerIdDto = new ToursByPlayerIdDto()
            {
                ToursId = teamRepo.GetList(
                        predicate: x => teamIds.Contains(x.Id),
                        size: teamIds.Count)
                    .Items
                    .Select(x => x.TourId)
                    .ToArray()
            };

            return Task.FromResult(toursByPlayerIdDto);
        }
    }
}