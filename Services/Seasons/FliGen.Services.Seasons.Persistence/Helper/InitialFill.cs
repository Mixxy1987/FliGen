using FliGen.Common.Sql;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.Linq;

namespace FliGen.Services.Seasons.Persistence.Helper
{
    public static class InitialFill
    {
        public static void SeasonsFill(MigrationBuilder migrationBuilder)
        {
            const string insertSeasonsQuery = @"
    INSERT INTO [Season](Start, Finish, LeagueId)
	SELECT @@startDate, @@finishDate, @@leagueId
    ";

            InsertSeasons(
                migrationBuilder, 
                InitialFillData.SeasonDates.Take(6),
                InitialFillData.Leagues[0].Item1,
                InitialFillData.Leagues[0].Item2,
                insertSeasonsQuery);

            InsertSeasons(
                migrationBuilder, 
                InitialFillData.SeasonDates.TakeLast(3),
                InitialFillData.Leagues[1].Item1,
                InitialFillData.Leagues[1].Item2,
                insertSeasonsQuery);
        }

        private static void InsertSeasons(
            MigrationBuilder migrationBuilder,
            IEnumerable<(int, string, string)> dates,
            int leagueId,
            string leagueName,
            string query)
        {
            foreach (var (_, startDate, finishDate) in dates)
            {
                InsertSeasonsFromList(migrationBuilder, leagueId, leagueName, startDate, finishDate, query);
            }
        }

        private static void InsertSeasonsFromList(
            MigrationBuilder migrationBuilder,
            int leagueId,
            string leagueName,
            string startDate,
            string finishDate,
            string query)
        {
            string q = MigrationHelpers.ReplaceVariablesWithValues(
                query,
                new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@@startDate", startDate),
                    new KeyValuePair<string, object>("@@finishDate", finishDate),
                    new KeyValuePair<string, object>("@@leagueId", leagueId),
                    new KeyValuePair<string, object>("@@leagueName", leagueName)
                }
            );
            migrationBuilder.Sql(MigrationHelpers.ConvertScriptToDynamicSql(q));
        }
    }
}