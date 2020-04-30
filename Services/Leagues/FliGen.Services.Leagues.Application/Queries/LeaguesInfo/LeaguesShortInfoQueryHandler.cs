using FliGen.Common.SeedWork.Repository;
using FliGen.Common.SeedWork.Repository.Paging;
using FliGen.Services.Leagues.Application.Dto;
using FliGen.Services.Leagues.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Leagues.Application.Queries.LeaguesInfo
{
    public class LeaguesShortInfoQueryHandler : IRequestHandler<LeaguesShortInfoQuery, LeaguesShortInfoDto>
    {
        private readonly IUnitOfWork _uow;

        public LeaguesShortInfoQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<LeaguesShortInfoDto> Handle(LeaguesShortInfoQuery request, CancellationToken cancellationToken)
        {
            var repo = _uow.GetReadOnlyRepository<League>();

            IPaginate<League> leagues = repo.GetList();

            var leaguesInfoDto = new LeaguesShortInfoDto
            {
                Count = leagues.Count
            };
           
            return Task.FromResult(leaguesInfoDto);
        }
    }
}
