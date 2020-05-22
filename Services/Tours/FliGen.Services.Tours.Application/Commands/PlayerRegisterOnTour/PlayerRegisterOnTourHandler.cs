using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using FliGen.Common.Types;
using FliGen.Services.Tours.Application.Dto;
using FliGen.Services.Tours.Application.Dto.Enum;
using FliGen.Services.Tours.Application.Queries.LeaguesQuery;
using FliGen.Services.Tours.Application.Services;
using FliGen.Services.Tours.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using FliGen.Services.Tours.Domain.Common;
using Tour = FliGen.Services.Tours.Domain.Entities.Tour;

namespace FliGen.Services.Tours.Application.Commands.PlayerRegisterOnTour
{
    public class PlayerRegisterOnTourHandler : ICommandHandler<PlayerRegisterOnTour>
    {
        private readonly IUnitOfWork _uow;
        private readonly IPlayersService _playersService;
        private readonly ILeaguesService _leaguesService;

        public PlayerRegisterOnTourHandler(
            IUnitOfWork uow,
            IPlayersService playersService,
            ILeaguesService leaguesService)
        {
            _uow = uow;
            _playersService = playersService;
            _leaguesService = leaguesService;
        }

        public async Task HandleAsync(PlayerRegisterOnTour command, ICorrelationContext context)
        {
            await ValidateTourOrThrowAsync(command);

            DateTime.TryParse(command.RegistrationDate, out var registrationDate);

            if (string.IsNullOrWhiteSpace(command.PlayerExternalId))
            {
                if (command.PlayerInternalIds is null || command.PlayerInternalIds.Length == 0)
                {
                    throw new FliGenException(ErrorCodes.EmptyPlayersList, "Players id list is empty.");
                }
                var tourRegistrationRepo = _uow.GetRepositoryAsync<TourRegistration>();
                foreach (var playerInternalId in command.PlayerInternalIds)
                {
                    await RegisterPlayer(tourRegistrationRepo, command.TourId, playerInternalId, registrationDate, true);
                }
            }
            else
            {
                PlayerInternalIdDto playerInternalIdDto = await _playersService.GetInternalIdAsync(command.PlayerExternalId);
                await ValidatePlayerStatusOrThrowAsync(command, playerInternalIdDto);

                var tourRegistrationRepo = _uow.GetRepositoryAsync<TourRegistration>();
                await RegisterPlayer(tourRegistrationRepo, command.TourId, playerInternalIdDto.InternalId, registrationDate);
            }

            _uow.SaveChanges();
        }

        private async Task RegisterPlayer(
            IRepositoryAsync<TourRegistration> tourRegistrationRepo,
            int tourId,
            int playerInternalId,
            DateTime registrationDate,
            bool ignoreAlreadyRegistered = false)
        {
            TourRegistration tourRegistration = await tourRegistrationRepo.SingleAsync(
                tr => tr.PlayerId == playerInternalId &&
                      tr.TourId == tourId);
            if (!(tourRegistration is null))
            {
                if (ignoreAlreadyRegistered)
                {
                    return;
                }

                tourRegistrationRepo.RemoveAsync(tourRegistration);

                /*throw new FliGenException(
                    ErrorCodes.PlayerAlreadyRegistered,
                    $"Player with id {playerInternalId} is already registered on tour: {tourId}");*/
                return;
            }

            await tourRegistrationRepo.AddAsync(
                TourRegistration.Create(tourId, playerInternalId, registrationDate));
        }

        private async Task ValidateTourOrThrowAsync(PlayerRegisterOnTour command)
        {
            var tourRepo = _uow.GetRepositoryAsync<Tour>();
            Tour tour = await tourRepo.SingleAsync(x => x.Id == command.TourId);
            if (tour is null)
            {
                throw new FliGenException(
                    ErrorCodes.InvalidTourId,
                    $"There is no tour with id: {command.TourId}");
            }

            if (tour.IsRegistrationIsNotYetOpened())
            {
                throw new FliGenException(
                    ErrorCodes.TourRegistrationIsNotYetOpened,
                    $"Registration is not yet opened for tour with id: {command.TourId}");
            }

            if (tour.IsRegistrationClosed())
            {
                throw new FliGenException(
                    ErrorCodes.TourRegistrationIsClosed,
                    $"Registration is already closed for tour with id: {command.TourId}");
            }
        }

        private async Task ValidatePlayerStatusOrThrowAsync(
            PlayerRegisterOnTour command,
            PlayerInternalIdDto playerInternalIdDto)
        {
            if (playerInternalIdDto is null)
            {
                throw new FliGenException(
                    ErrorCodes.NoPlayerWithSuchId,
                    $"There is no player with external id: {command.PlayerExternalId}");
            }

            var leagues = await _leaguesService.GetLeaguesAsync(
                new LeaguesQuery()
                {
                    PlayerExternalId = command.PlayerExternalId,
                    LeagueId = new[] { command.LeagueId }
                });

            if (leagues is null)
            {
                throw new FliGenException(
                    ErrorCodes.NoLeagueWithSuchId,
                    $"There is no league with id: {command.LeagueId}");
            }

            var leaguesArr = leagues.ToList();

            if (leaguesArr.Count == 0)
            {
                throw new FliGenException(
                    ErrorCodes.PlayerIsNotAMemberOfLeague,
                    $"Player with id {command.PlayerExternalId} is not a member of league: {command.LeagueId}");
            }

            if (leaguesArr.Count != 0)
            {
                var statuses = leaguesArr[0].PlayersLeagueStatuses.ToArray();
                if (statuses.Length == 0  || statuses[0].PlayerLeagueJoinStatus != PlayerLeagueJoinStatus.Joined)
                {
                    throw new FliGenException(
                        ErrorCodes.PlayerIsNotAMemberOfLeague,
                        $"Player with id {command.PlayerExternalId} is not a member of league: {command.LeagueId}");
                }
            }

        }
    }
}
