using System.Threading;
using System.Threading.Tasks;
using FliGen.Common.SeedWork.Repository;
using FliGen.Common.SeedWork.Repository.Paging;
using FliGen.Services.Leagues.Application.Dto;
using FliGen.Services.Leagues.Domain.Entities;
using MediatR;

namespace FliGen.Services.Leagues.Application.Queries.LeagueInformation
{
    public class LeagueInformationQueryHandler : IRequestHandler<LeagueInformationQuery, LeagueInformationDto>
    {
        private readonly IUnitOfWork _uow;

        public LeagueInformationQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<LeagueInformationDto> Handle(LeagueInformationQuery request, CancellationToken cancellationToken)
        {
            var repo = _uow.GetReadOnlyRepository<League>();
            //todo::
            IPaginate<League> leagues = repo.GetList();

            var leaguesInfoDto = new LeagueInformationDto();

            return Task.FromResult(leaguesInfoDto);
        }
    }
}
