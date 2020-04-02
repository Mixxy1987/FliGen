using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Leagues.Application.Dto;
using MediatR;

namespace FliGen.Services.Leagues.Application.Queries.LeagueTypes
{
    public class LeagueTypesQueryHandler : IRequestHandler<LeagueTypesQuery, IEnumerable<LeagueType>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public LeagueTypesQueryHandler(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LeagueType>> Handle(LeagueTypesQuery request, CancellationToken cancellationToken)
        {
            var repo = _uow.GetRepositoryAsync<Domain.Entities.Enum.LeagueType>();
            var leagueTypes = await repo.GetListAsync(cancellationToken: cancellationToken);

            return leagueTypes.Items.Select(x => _mapper.Map<LeagueType>(x)).ToArray();
        }
    }
}
