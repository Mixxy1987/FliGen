using FliGen.Common.Sql;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FliGen.Services.Teams.Persistence.Helper
{
    public static class InitialFillTeamPlayers
    {
        public static void TeamPlayersFill(MigrationBuilder migrationBuilder, int teamsCount)
        {
            const string insertTeamsQuery = @"
    INSERT INTO [TeamPlayerLinks](TeamId, PlayerId)
	SELECT @@teamId, @@playerId
    ";

            InsertTeamPlayers(migrationBuilder, insertTeamsQuery, teamsCount);
        }

        private static void InsertTeamPlayers(MigrationBuilder migrationBuilder, string query, int teamsCount)
        {
            Random rnd = new Random();
            for (int teamId = 1; teamId <= teamsCount; teamId += 2)
            {
                List<int> playerIds =
                    Enumerable.Range(1, InitialFillData.FootballLeaguePlayers)
                        .OrderBy(x => rnd.Next())
                        .Take(InitialFillData.TeamPlayersCount * 2)
                        .ToList();

                foreach (var playerId in playerIds.Take(InitialFillData.TeamPlayersCount))
                {
                    InsertTeamPlayer(migrationBuilder, playerId, teamId, query);
                }
                foreach (var playerId in playerIds.TakeLast(InitialFillData.TeamPlayersCount))
                {
                    InsertTeamPlayer(migrationBuilder, playerId, teamId + 1, query);
                }
            }


        }
        private static void InsertTeamPlayer(MigrationBuilder migrationBuilder, int playerId, int teamId, string query)
        {
            string q = MigrationHelpers.ReplaceVariablesWithValues(
                query,
                new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@@playerId", playerId),
                    new KeyValuePair<string, object>("@@teamId", teamId)
                }
            );
            migrationBuilder.Sql(MigrationHelpers.ConvertScriptToDynamicSql(q));
        }
    }
}