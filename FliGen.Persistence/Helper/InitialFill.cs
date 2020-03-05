using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FliGen.Persistence.Helper
{
    public static class InitialFill
    {
        static readonly List<string> Dict = new List<string>
        {
            {"Матюнин Валентин 7.2"},
            {"Волчков Вячеслав 7.1"},
            {"Никитин Евгений 7.1"},
            {"Алтухов Антон 7.2" },
            {"Виноходов Игорь 7.3" },
            {"Галицкий Вячеслав 7.2" },
            {"Косенков Олег 7.5" },
            {"Мухин Иван 7.3" },
            {"Попов Александр 7.4" },
            {"Попов Артем 7.4" },
            {"Растаев Дмитрий 7.4" },
            {"Филинов Павел 7.3" },
            {"Масюк Родион 7.3" },
            {"Дубцов Максим 7.2" },
            {"Ахтямов Руслан 6.9" },
            {"Яшин Анатолий 7.1" },
            {"Зырянов Егор 7.1" },
            {"Сгибнев Андрей 7.2" },
            {"Ляшук Алексей 6.5" },
            {"Ларичкин Алексей 7.4" }
        };

        public static void NamesAndRatesFill(MigrationBuilder migrationBuilder)
        {
            const string migrateExistingOperationEntriesQuery = @"
	INSERT INTO [Player](LastName, FirstName)
	VALUES
    (@@lastName, @@firstName)

    INSERT INTO [PlayerRate](Date, Value, PlayerId)
	SELECT '01.01.2020', @@rate, Id
	FROM [Player]
	WHERE [Player].[FirstName] = @@firstName AND [Player].[LastName] = @@lastName
";
            foreach (var kv in Dict)
            {
                var kvSplitted = kv.Split(' ');

                string query = MigrationHelpers.ReplaceVariablesWithValues(
                    migrateExistingOperationEntriesQuery,
                    new List<KeyValuePair<string, object>>()
                    {
                        new KeyValuePair<string, object>("@@lastName", kvSplitted[0]),
                        new KeyValuePair<string, object>("@@firstName", kvSplitted[1]),
                        new KeyValuePair<string, object>("@@rate", kvSplitted[2]),
                    }
                );
                migrationBuilder.Sql(MigrationHelpers.ConvertScriptToDynamicSql(query));
            }
        }

        public static void LeaguePlayerLinksFill(MigrationBuilder migrationBuilder)
        {
            var hockeyDict = new List<string>
            {
                {"Матюнин Валентин"},
                {"Волчков Вячеслав"},
                {"Никитин Евгений"},
                {"Алтухов Антон" },
                {"Виноходов Игорь" },
                {"Галицкий Вячеслав" },
                {"Косенков Олег" },
                {"Мухин Иван" },
                {"Попов Александр" },
            };


            const string lpQuery = @"
    INSERT INTO [LeaguePlayerLinks](LeagueId, PlayerId, JoinTime, LeaguePlayerRoleId)
	SELECT league.Id, player.Id, @@joinTime, 2
	FROM [Player] as player, [League] as league
	WHERE [Player].[FirstName] = @@firstName AND [Player].[LastName] = @@lastName AND [League].Name=@@leagueName
";
            var list = new List<string> {"FLI", "FLIHockey" };

            foreach (var kv in hockeyDict)
            {
                FillLeagueFromList(migrationBuilder, "FLIHockey", kv, lpQuery);
            }

            foreach (var kv in Dict)
            {
                FillLeagueFromList(migrationBuilder, "FLI", kv, lpQuery);
            }
        }

        private static void FillLeagueFromList(MigrationBuilder migrationBuilder, string leagueName, string kv, string valuesQuery)
        {
            var kvSplitted = kv.Split(' ');

            string query = MigrationHelpers.ReplaceVariablesWithValues(
                valuesQuery,
                new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@@lastName", kvSplitted[0]),
                    new KeyValuePair<string, object>("@@firstName", kvSplitted[1]),
                    new KeyValuePair<string, object>("@@leagueName", leagueName),
                    new KeyValuePair<string, object>("@@joinTime", "2020-01-01")
                }
            );
            migrationBuilder.Sql(MigrationHelpers.ConvertScriptToDynamicSql(query));
        }
    }
}