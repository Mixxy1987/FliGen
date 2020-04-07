using FliGen.Common.Sql;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.Linq;

namespace FliGen.Services.Leagues.Persistence.Helper
{
    public static class InitialFill
    {
        public static void LeaguePlayerLinksFill(MigrationBuilder migrationBuilder)
        {
            const string lpQuery = @"
    INSERT INTO [LeaguePlayerLinks](LeagueId, PlayerId, CreationTime, JoinTime, LeaguePlayerRoleId, LeaguePlayerPriority, Actual)
	SELECT @@leagueId, @@playerId, @@creationTime, @@joinTime, 2, 3, 1
";
            InsertLeague(
                migrationBuilder,
                InitialFillData.Players,
                InitialFillData.Leagues[0].Item1,
                InitialFillData.Leagues[0].Item2,
                lpQuery);
            InsertLeague(
                migrationBuilder,
                InitialFillData.Players.Take(InitialFillData.HockeyLeaguePlayers),
                InitialFillData.Leagues[1].Item1,
                InitialFillData.Leagues[1].Item2,
                lpQuery);
        }

        private static void InsertLeague(
            MigrationBuilder migrationBuilder,
            IEnumerable<(int, string)> players,
            int leagueId,
            string leagueName,
            string query)
        {
            foreach (var kv in players)
            {
                InsertLeagueFromList(migrationBuilder, leagueId, leagueName, kv.Item1, kv.Item2, query);
            }
        }


        private static void InsertLeagueFromList(
            MigrationBuilder migrationBuilder,
            int leagueId,
            string leagueName,
            int playerId,
            string kvPlayer,
            string query)
        {
            var firstNameLastName = kvPlayer.Split(' ');

            string q = MigrationHelpers.ReplaceVariablesWithValues(
                query,
                new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@@leagueId", leagueId),
                    new KeyValuePair<string, object>("@@playerId", playerId),
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