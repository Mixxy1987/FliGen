using AutoFixture;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Leagues.Application.Commands.JoinLeague;
using FliGen.Services.Leagues.Application.Dto;
using FliGen.Services.Leagues.Application.Services;
using FliGen.Services.Leagues.Domain.Entities;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using LeagueSettings = FliGen.Services.Leagues.Domain.Entities.LeagueSettings;

namespace FliGen.Services.Leagues.IntegrationTests
{
    public class JoinLeagueTests
    {
        [Fact]
        public async Task JoinLeagueShouldChangePlayerStatus()
        {
            var fix = new Fixture();
            int leagueId = fix.Create<int>();
            int playerId = fix.Create<int>();
            var command = new JoinLeague()
            {
                LeagueId = leagueId,
                PlayerExternalId = Guid.NewGuid().ToString()
            };

            var retDto = new PlayerInternalIdDto()
            {
                InternalId = playerId
            };

            var uow = Substitute.For<IUnitOfWork>();
            var playersService = Substitute.For<IPlayersService>();

            playersService.GetInternalIdAsync(command.PlayerExternalId).ReturnsForAnyArgs(retDto);

            var leagueSettingsRepo = Substitute.For<IRepositoryAsync<LeagueSettings>>();
            var leaguePlayerLinksRepo = Substitute.For<IRepositoryAsync<LeaguePlayerLink>>();

            var lsWithoutConfirm = LeagueSettings.Create(true, false, leagueId);
            var lsWithConfirm = LeagueSettings.Create(true, true, leagueId);

            var leftLpl = LeaguePlayerLink.CreateJoinedLink(leagueId, playerId);
            leftLpl.UpdateToLeft();

            var hs = new HashSet<(LeagueSettings, LeaguePlayerLink, Action)>
            {
                (
                    lsWithoutConfirm,
                    null,
                    () => leaguePlayerLinksRepo.Received().AddAsync(Arg.Is<LeaguePlayerLink>(x => x.InJoinedStatus()))
                ),
                (
                    lsWithoutConfirm,
                    leftLpl,
                    () => leaguePlayerLinksRepo.Received().AddAsync(Arg.Is<LeaguePlayerLink>(x => x.InJoinedStatus()))
                ),
                (
                    lsWithoutConfirm,
                    LeaguePlayerLink.CreateWaitingLink(leagueId, playerId),
                    () => leaguePlayerLinksRepo.Received().UpdateAsync(Arg.Is<LeaguePlayerLink>(x => x.InJoinedStatus()))
                ),
                (
                    lsWithoutConfirm,
                    LeaguePlayerLink.CreateJoinedLink(leagueId, playerId),
                    () => leaguePlayerLinksRepo.Received().UpdateAsync(Arg.Is<LeaguePlayerLink>(x => x.InLeftStatus()))
                ),
                (
                    lsWithConfirm,
                    null,
                    () => leaguePlayerLinksRepo.Received().AddAsync(Arg.Is<LeaguePlayerLink>(x => x.InWaitingStatus()))
                ),
                (
                    lsWithConfirm,
                    leftLpl,
                    () => leaguePlayerLinksRepo.Received().AddAsync(Arg.Is<LeaguePlayerLink>(x => x.InJoinedStatus()))
                ),
                (
                    lsWithConfirm,
                    LeaguePlayerLink.CreateWaitingLink(leagueId, playerId),
                    () => leaguePlayerLinksRepo.Received().RemoveAsync(Arg.Is<LeaguePlayerLink>(x => x.InWaitingStatus()))
                ),
                (
                    lsWithConfirm,
                    LeaguePlayerLink.CreateJoinedLink(leagueId, playerId),
                    () => leaguePlayerLinksRepo.Received().UpdateAsync(Arg.Is<LeaguePlayerLink>(x => x.InLeftStatus()))
                ),
            };

            foreach (var (lsItem, lplItem, checkAction) in hs)
            {
                leagueSettingsRepo.SingleAsync().ReturnsForAnyArgs(lsItem);
                leaguePlayerLinksRepo.SingleAsync().ReturnsForAnyArgs(lplItem);

                uow.GetRepositoryAsync<LeagueSettings>().ReturnsForAnyArgs(leagueSettingsRepo);
                uow.GetRepositoryAsync<LeaguePlayerLink>().ReturnsForAnyArgs(leaguePlayerLinksRepo);

                var commandHandler = new JoinLeagueHandler(uow, playersService);
                await commandHandler.HandleAsync(command, new CorrelationContext());

                checkAction();
            }
        }
    }
}