using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FliGen.Domain.Common.Repository;
using MediatR;

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

            return _mapper.Map<Dto.LeagueInformation>(await leagueRepo.SingleAsync(x => x.Id == request.LeagueId));
        }
    }
}
