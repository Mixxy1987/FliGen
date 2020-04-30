using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Seasons.Application.Dto;
using FliGen.Services.Seasons.Application.Services;
using FliGen.Services.Seasons.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Seasons.Application.Queries.Seasons
{
    public class SeasonsQueryHandler : IRequestHandler<SeasonsQuery, IEnumerable<SeasonDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IToursService _toursServe;

        public SeasonsQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            IToursService toursService)
        {
	        _uow = uow;
	        _mapper = mapper;
            _toursServe = toursService;
        }

        public async Task<IEnumerable<SeasonDto>> Handle(SeasonsQuery request, CancellationToken cancellationToken)
        {
            var seasonsRepo = _uow.GetRepositoryAsync<Season>();

            //todo::
            return null;
        }
    }
}
