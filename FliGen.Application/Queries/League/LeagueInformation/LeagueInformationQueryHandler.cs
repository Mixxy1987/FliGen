using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Application.Queries.League.LeagueInformation
{
    public class LeagueInformationQueryHandler : IRequestHandler<LeagueInformationQuery, Dto.LeagueInformation>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public LeagueInformationQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Dto.LeagueInformation> Handle(LeagueInformationQuery request, CancellationToken cancellationToken)
        {
            var leagueRepo = _uow.GetRepositoryAsync<Domain.Entities.League>();

            Domain.Entities.League league = await leagueRepo.SingleAsync(
                predicate: l => l.Id == request.LeagueId,
                include: q => q
                    .Include(l => l.Seasons)
                    .ThenInclude(s => s.Tours)
                    .ThenInclude(t => t.Teams));


            return _mapper.Map<Dto.LeagueInformation>(league);
        }
    }
}