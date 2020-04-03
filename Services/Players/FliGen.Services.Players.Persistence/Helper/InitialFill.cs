using FliGen.Common.Sql;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;

namespace FliGen.Services.Players.Persistence.Helper
{
    public static class InitialFill
    {
        public static void NamesAndRatesFill(MigrationBuilder migrationBuilder)
        {
            const string playersQuery = @"
	INSERT INTO [Player](LastName, FirstName)
	VALUES
    (@@lastName, @@firstName)
";
            InsertPlayers(migrationBuilder, playersQuery);

            const string playerRatesQuery = @"
    INSERT INTO [PlayerRate](Date, Value, PlayerId, LeagueId)
	SELECT '01.01.2020', @@rate, @@playerId, @@leagueId
";
            InsertPlayersRateForLeague(migrationBuilder, InitialFillData.Leagues[0].Item1, playerRatesQuery);
            InsertPlayersRateForLeague(migrationBuilder, InitialFillData.Leagues[1].Item1, playerRatesQuery);
        }

        private static void InsertPlayers(MigrationBuilder migrationBuilder, string query)
        {
            foreach (var kv in InitialFillData.Players)
            {
                var kvSplitted = kv.Item2.Split(' ');

                string q = MigrationHelpers.ReplaceVariablesWithValues(
                    query,
                    new List<KeyValuePair<string, object>>
                    {
                        new KeyValuePair<string, object>("@@lastName", kvSplitted[0]),
                        new KeyValuePair<string, object>("@@firstName", kvSplitted[1]),
                    }
                );
                migrationBuilder.Sql(MigrationHelpers.ConvertScriptToDynamicSql(q));
            }
        }

        private static void InsertPlayersRateForLeague(
            MigrationBuilder migrationBuilder, 
            int leagueId,
            string query)
        {
            foreach (var kv in InitialFillData.Players)
            {
                var kvSplitted = kv.Item2.Split(' ');

                string q = MigrationHelpers.ReplaceVariablesWithValues(
                    query,
                    new List<KeyValuePair<string, object>>
                    {
                        new KeyValuePair<string, object>("@@rate", kvSplitted[2]),
                        new KeyValuePair<string, object>("@@playerId", kv.Item1),
                        new KeyValuePair<string, object>("@@leagueId", leagueId),
                    }
                );
                migrationBuilder.Sql(MigrationHelpers.ConvertScriptToDynamicSql(q));
            }
        }
    }
}