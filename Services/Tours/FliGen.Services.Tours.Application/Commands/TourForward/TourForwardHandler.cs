﻿using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Tours.Application.Events;
using FliGen.Services.Tours.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using FliGen.Services.Tours.Domain.Entities.Enum;

namespace FliGen.Services.Tours.Application.Commands.TourForward
{
    public class TourForwardHandler : ICommandHandler<TourForward>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBusPublisher _busPublisher;

        public TourForwardHandler(
            IUnitOfWork uow,
            IBusPublisher busPublisher)
        {
            _uow = uow;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(TourForward command, ICorrelationContext context)
        {
            var tourRepo = _uow.GetRepositoryAsync<Tour>();

            if (command.TourId is null)
            {
                var newTour = Tour.Create(command.Date, command.SeasonId);
                var newTourId = (await tourRepo.AddAsync(newTour)).Entity.Id;
                await _busPublisher.PublishAsync(new TourCreated(newTourId), context);
            }
            else
            {
                Tour tour = await tourRepo.SingleAsync(t => t.Id == command.TourId);
                tour.MoveTourStatusForward();
                tourRepo.UpdateAsync(tour);

                if (tour.TourStatus.Equals(TourStatus.RegistrationOpened))
                {
                    await _busPublisher.PublishAsync(new TourRegistrationOpened(tour.Id, command.LeagueId, tour.Date), context);
                }

                if (tour.TourStatus.Equals(TourStatus.RegistrationClosed))
                {
                    var tourRegRepo = _uow.GetReadOnlyRepository<TourRegistration>();

                    var playersId = tourRegRepo.GetList(tr => tr.TourId == tour.Id); //todo:: get all

                    int[] pid = playersId.Items.Select(tr => tr.PlayerId).ToArray();

                    await _busPublisher.PublishAsync(
                        new TourRegistrationClosed(
                            tour.Id,
                            command.LeagueId, 
                            command.PlayersInTeam, 
                            command.TeamsInTour,
                            pid,
                            command.GenerateTeamsStrategy),
                        context);
                }
            }

            _uow.SaveChanges();
        }
    }
}
