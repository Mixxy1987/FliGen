using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Tours.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Tours.Application.Queries.MyTours
{
    public class MyToursQueryHandler : IRequestHandler<MyToursQuery, IEnumerable<Dto.Tour>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MyToursQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Dto.Tour>> Handle(MyToursQuery request, CancellationToken cancellationToken)
        {
			var teamPlayerLinksRepo = _uow.GetReadOnlyRepository<TeamPlayerLink>();

            var teamPlayerLinks = teamPlayerLinksRepo
                .GetList(tpl => tpl.PlayerId == request.UserId, size : request.Size);

            if (teamPlayerLinks.Count == 0)
            {
                return null;
			}

            var teamRepo = _uow.GetRepositoryAsync<Team>();
            var toursRepo = _uow.GetRepositoryAsync<Tour>();
			var tours = new List<Dto.Tour>();
			foreach (var tpl in teamPlayerLinks.Items)
            {
                int tourId = (await teamRepo.SingleAsync(team => team.Id == tpl.TeamId)).TourId;
                Tour tour = await toursRepo.SingleAsync(t => t.Id == tourId);
                tours.Add(_mapper.Map<Dto.Tour>(tour));
			}

			return tours;
        }
    }
}