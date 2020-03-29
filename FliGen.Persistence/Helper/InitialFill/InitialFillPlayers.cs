using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FliGen.Persistence.Helper.InitialFill
{
    public static class InitialFillPlayers
    {
        public static void NamesAndRatesFill(MigrationBuilder migrationBuilder)
        {
            const string migrateExistingOperationEntriesQuery = @"
	INSERT INTO [Player](LastName, FirstName)
	VALUES
    (@@lastName, @@firstName)

    INSERT INTO [PlayerRate](Date, Value, PlayerId, LeagueId)
	SELECT '01.01.2020', @@rate, player.Id, league.Id
	FROM [Player] as player, [League] as league
	WHERE [Player].[FirstName] = @@firstName AND [Player].[LastName] = @@lastName AND [League].Name=@@leagueName
";
            InsertPlayersRateForLeague(migrationBuilder, InitialFillData.Leagues[0], migrateExistingOperationEntriesQuery);
            InsertPlayersRateForLeague(migrationBuilder, InitialFillData.Leagues[1], migrateExistingOperationEntriesQuery);
        }

        private static void InsertPlayersRateForLeague(
            MigrationBuilder migrationBuilder, 
            string leagueName,
            string query)
        {
            foreach (var kv in InitialFillData.Players)
            {
                var kvSplitted = kv.Split(' ');

                string q = MigrationHelpers.ReplaceVariablesWithValues(
                    query,
                    new List<KeyValuePair<string, object>>()
                    {
                        new KeyValuePair<string, object>("@@lastName", kvSplitted[0]),
                        new KeyValuePair<string, object>("@@firstName", kvSplitted[1]),
                        new KeyValuePair<string, object>("@@rate", kvSplitted[2]),
                        new KeyValuePair<string, object>("@@leagueName", leagueName),
                    }
                );
                migrationBuilder.Sql(MigrationHelpers.ConvertScriptToDynamicSql(q));
            }
        }

        public static void LeaguePlayerLinksFill(MigrationBuilder migrationBuilder)
        {
            const string lpQuery = @"
    INSERT INTO [LeaguePlayerLinks](LeagueId, PlayerId, CreationTime, JoinTime, LeaguePlayerRoleId)
	SELECT league.Id, player.Id, @@creationTime, @@joinTime, 2
	FROM [Player] as player, [League] as league
	WHERE [Player].[FirstName] = @@firstName AND [Player].[LastName] = @@lastName AND [League].Name=@@leagueName
";
            InsertLeague(migrationBuilder, InitialFillData.Players, InitialFillData.Leagues[0], lpQuery);
            InsertLeague(migrationBuilder, InitialFillData.Players.Take(InitialFillData.HockeyLeaguePlayers), InitialFillData.Leagues[1], lpQuery);
        }

        private static void InsertLeague(MigrationBuilder migrationBuilder, IEnumerable<string> players,
            string leagueName, string query)
        {
            foreach (var kv in players)
            {
                InsertLeagueFromList(migrationBuilder, leagueName, kv, query);
            }
        }


        private static void InsertLeagueFromList(MigrationBuilder migrationBuilder, string leagueName, string kvPlayer,
            string query)
        {
            var firstNameLastName = kvPlayer.Split(' ');

            string q = MigrationHelpers.ReplaceVariablesWithValues(
                query,
                new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@@lastName", firstNameLastName[0]),
                    new KeyValuePair<string, object>("@@firstName", firstNameLastName[1]),
                    new KeyValuePair<string, object>("@@leagueName", leagueName),
                    new KeyValuePair<string, object>("@@creationTime", "2020-01-01"),
                    new KeyValuePair<string, object>("@@joinTime", "2020-01-01")
                }
            );
            migrationBuilder.Sql(MigrationHelpers.ConvertScriptToDynamicSql(q));
        }
    }
}