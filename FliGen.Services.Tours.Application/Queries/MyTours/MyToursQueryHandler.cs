using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Tours.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
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
                .GetList(
                    predicate: tpl => tpl.PlayerId == request.UserId, 
                    size : request.Size);

            if (teamPlayerLinks.Count == 0)
            {
                return null;
			}

            return GetToursByCondition(teamPlayerLinks.Items, request.QueryType, request.SeasonIds);
        }

        private IEnumerable<Dto.Tour> GetToursByCondition(IEnumerable<TeamPlayerLink> teamPlayerLinks, MyToursQueryType queryType, int[] seasonIds)
        { // todo:: refactor using Specification pattern
            var tours = new List<Dto.Tour>();
            var teamRepo = _uow.GetReadOnlyRepository<Team>();
            var toursRepo = _uow.GetReadOnlyRepository<Tour>();
            foreach (var tpl in teamPlayerLinks)
            {
                int tourId = teamRepo.Single(team => team.Id == tpl.TeamId).TourId;
                Tour tour = toursRepo.Single(t => t.Id == tourId);
                if (queryType == MyToursQueryType.Incoming && tour.IsEnded())
                {
                    continue;
                }
                if (seasonIds.Length != 0 && !seasonIds.Contains(tour.SeasonId))
                {
                    continue;
                }
                tours.Add(_mapper.Map<Dto.Tour>(tour));
            }

            return tours;
        }
    }
}