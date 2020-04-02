using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Leagues.Application.Dto;
using MediatR;

namespace FliGen.Services.Leagues.Application.Queries.GetLeagueTypes
{
    public class GetLeagueTypesQueryHandler : IRequestHandler<GetLeagueTypesQuery, IEnumerable<Dto.LeagueType>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetLeagueTypesQueryHandler(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Dto.LeagueType>> Handle(GetLeagueTypesQuery request, CancellationToken cancellationToken)
        {
            var repo = _uow.GetRepositoryAsync<Domain.Entities.Enum.LeagueType>();
            var leagueTypes = await repo.GetListAsync(cancellationToken: cancellationToken);

            return leagueTypes.Items.Select(x => _mapper.Map<LeagueType>(x)).ToArray();
        }
    }
}
