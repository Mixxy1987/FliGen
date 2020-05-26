using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Common.SeedWork.Repository.Paging;
using FliGen.Common.Types;
using FliGen.Services.Teams.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Teams.Application.Queries.ToursByPlayerIdQuery
{
    public class ToursByPlayerIdQueryHandler : IRequestHandler<ToursByPlayerIdQuery, PagedResult<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ToursByPlayerIdQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public Task<PagedResult<int>> Handle(ToursByPlayerIdQuery request, CancellationToken cancellationToken)
        {
            var teamPlayerLinksRepo = _uow.GetReadOnlyRepository<TeamPlayerLink>();

            IPaginate<TeamPlayerLink> teamPlayerLinks = teamPlayerLinksRepo
                .GetList(
                    tpl => tpl.PlayerId == request.PlayerId,
                    size: request.Size,
                    index: request.Page);

            if (teamPlayerLinks.Count == 0)
            {
                return null;
            }

            var teamIds = teamPlayerLinks.Items.Select(x => x.TeamId).ToList();

            var teamRepo = _uow.GetReadOnlyRepository<Team>();

            var result = PagedResult<int>.Create(
                teamRepo.GetList(x => teamIds.Contains(x.Id), size: teamIds.Count).Items.Select(x => x.TourId),
                teamPlayerLinks.Index,
                teamPlayerLinks.Size,
                teamPlayerLinks.Pages,
                teamPlayerLinks.Count);

            return Task.FromResult(result);
        }
    }
}