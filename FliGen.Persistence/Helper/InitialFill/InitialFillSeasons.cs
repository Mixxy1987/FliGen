using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FliGen.Persistence.Helper.InitialFill
{
    public static class InitialFillSeasons
    {
        public static void SeasonsAndToursFill(MigrationBuilder migrationBuilder)
        {
            const string insertSeasonsQuery = @"
    INSERT INTO [Season](Start, Finish, LeagueId)
	SELECT @@startDate,@@finishDate, Id
	FROM [League]
	WHERE [League].[Name] = @@leagueName
    ";

            InsertSeasons(migrationBuilder, InitialFillData.SeasonDates, InitialFillData.Leagues[0],
                insertSeasonsQuery);
            InsertSeasons(migrationBuilder, InitialFillData.SeasonDates.TakeLast(3), InitialFillData.Leagues[1],
                insertSeasonsQuery);
        }

        private static void InsertSeasons(MigrationBuilder migrationBuilder, IEnumerable<(string, string)> dates,
            string leagueName, string query)
        {
            foreach (var (startDate, finishDate) in dates)
            {
                InsertSeasonsFromList(migrationBuilder, leagueName, startDate, finishDate, query);
            }
        }

        private static void InsertSeasonsFromList(MigrationBuilder migrationBuilder, string leagueName,
            string startDate, string finishDate, string query)
        {
            string q = MigrationHelpers.ReplaceVariablesWithValues(
                query,
                new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@@startDate", startDate),
                    new KeyValuePair<string, object>("@@finishDate", finishDate),
                    new KeyValuePair<string, object>("@@leagueName", leagueName),
                }
            );
            migrationBuilder.Sql(MigrationHelpers.ConvertScriptToDynamicSql(q));
        }
    }
}