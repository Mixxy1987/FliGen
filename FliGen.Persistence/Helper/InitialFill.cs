using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FliGen.Persistence.Helper
{
    public static class InitialFill
    {
        private static readonly List<string> Players = new List<string>
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
            {"Ларичкин Алексей 7.0" },
            {"Абушахмин Роман 7.4" },
            {"Абрамов Дмитрий 7.4" },
            {"Алтухов Илья 7.4" },
            {"Аль-Махлай Виталий 7.4" },
            {"Антипов Илья 7.4" },
            {"Арабей Евгений 7.4" },
            {"Аскаров Ришат 7.4" },
            {"Богачев Николай 7.4" },
            {"Биляуэр Константин 7.4" },
            {"Гриднев Виталий 7.4" },
            {"Гугняев Антон 7.4" },
            {"Гусев Павел 7.4" },
            {"Демидов Дмитрий 7.4" },
            {"Дорожко Никита 7.4" },
            {"Дубенский Андрей 7.4" },
            {"Захаревич Дмитрий 7.4" },
            {"Егоров Роман 7.4" },
            {"Добряков Аскар 7.4" },
            {"Дутлов Леонид 7.4" },
            {"Казанцев Павел 7.4" },
            {"Зайцев Сергей 7.4" },
            {"Захаревич Олег 7.4" },
            {"Иванов Александр 7.4" },
            {"Иванов Павел 7.2" },
            {"Камзалаков Иван 7.1" },
            {"Ларионов Олег 7.9" },
            {"Кувшинов Максим 7.1" },
            {"Кувшинов Роман 7.2" },
            {"Кухахметов Марат 7.2" },
            {"Лаврега Игорь 7.1" }
        };
        private static readonly List<string> Leagues = new List<string> { "FLI", "FLIHockey" };
        private static readonly List<(string, string)> SeasonDates = new List<(string, string)>
        {
            ("2015-01-01", "2015-12-31"),
            ("2016-01-01", "2016-12-31"),
            ("2017-01-01", "2017-12-31"),
            ("2018-01-01", "2018-12-31"),
            ("2019-01-01", "2019-12-31"),
            ("2020-01-01", "2020-12-31")
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
            foreach (var kv in Players)
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
            const string lpQuery = @"
    INSERT INTO [LeaguePlayerLinks](LeagueId, PlayerId, CreationTime, JoinTime, LeaguePlayerRoleId)
	SELECT league.Id, player.Id, @@creationTime, @@joinTime, 2
	FROM [Player] as player, [League] as league
	WHERE [Player].[FirstName] = @@firstName AND [Player].[LastName] = @@lastName AND [League].Name=@@leagueName
";
            FillLeague(migrationBuilder, Players, Leagues[0], lpQuery);
            FillLeague(migrationBuilder, Players.Take(15).ToList(), Leagues[1], lpQuery);
        }

        public static void SeasonsAndToursFill(MigrationBuilder migrationBuilder)
        {
            const string insertSeasonsQuery = @"
    INSERT INTO [Season](Start, Finish, LeagueId)
	SELECT @@startDate,@@finishDate, Id
	FROM [League]
	WHERE [League].[Name] = @@leagueName
    ";

            FillSeasons(migrationBuilder, SeasonDates, Leagues[0], insertSeasonsQuery);
            FillSeasons(migrationBuilder, SeasonDates.TakeLast(3).ToList(), Leagues[1], insertSeasonsQuery);
        }

        private static void FillSeasons(MigrationBuilder migrationBuilder, List<(string, string)> dates, string leagueName, string query)
        {
            foreach (var kv in dates)
            {
                FillSeasonsFromList(migrationBuilder, leagueName, kv, query);
            }
        }

        private static void FillSeasonsFromList(MigrationBuilder migrationBuilder, string leagueName, (string, string) startAndFinishDate, string query)
        {
            string q = MigrationHelpers.ReplaceVariablesWithValues(
                query,
                new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@@startDate", startAndFinishDate.Item1),
                    new KeyValuePair<string, object>("@@finishDate", startAndFinishDate.Item2),
                    new KeyValuePair<string, object>("@@leagueName", leagueName),
                }
            );
            migrationBuilder.Sql(MigrationHelpers.ConvertScriptToDynamicSql(q));
        }

        private static void FillLeague(MigrationBuilder migrationBuilder, List<string> players, string leagueName, string query)
        {
            foreach (var kv in players)
            {
                FillLeagueFromList(migrationBuilder, leagueName, kv, query);
            }
        }


        private static void FillLeagueFromList(MigrationBuilder migrationBuilder, string leagueName, string kvPlayer, string query)
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