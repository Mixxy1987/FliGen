using FliGen.Common.SeedWork.Repository;
using FliGen.Common.SeedWork.Repository.Paging;
using FliGen.Services.Leagues.Application.Dto;
using FliGen.Services.Leagues.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Leagues.Application.Queries.LeaguesInfo
{
    public class LeaguesInfoQueryHandler : IRequestHandler<LeaguesInfoQuery, LeaguesInfoDto>
    {
        private readonly IUnitOfWork _uow;

        public LeaguesInfoQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<LeaguesInfoDto> Handle(LeaguesInfoQuery request, CancellationToken cancellationToken)
        {
            var repo = _uow.GetReadOnlyRepository<League>();

            IPaginate<League> leagues = repo.GetList();

            var leaguesInfoDto = new LeaguesInfoDto
            {
                Count = leagues.Count
            };
           
            return Task.FromResult(leaguesInfoDto);
        }
    }
}
