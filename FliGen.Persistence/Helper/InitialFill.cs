using System;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.Linq;

namespace FliGen.Persistence.Helper
{

    public static class InitialFillData
    {
        public static readonly List<string> Players = new List<string>
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
        public static readonly List<string> Leagues = new List<string> { "FLI", "FLIHockey" };
        public static readonly List<(string, string)> SeasonDates = new List<(string, string)>
        {
            ("2015-01-01", "2015-12-31"),
            ("2016-01-01", "2016-12-31"),
            ("2017-01-01", "2017-12-31"),
            ("2018-01-01", "2018-12-31"),
            ("2019-01-01", "2019-12-31"),
            ("2020-01-01", "2020-12-31")
        };
    }
    public static class InitialFillPlayers
    {
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
            foreach (var kv in InitialFillData.Players)
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
            FillLeague(migrationBuilder, InitialFillData.Players, InitialFillData.Leagues[0], lpQuery);
            FillLeague(migrationBuilder, InitialFillData.Players.Take(15), InitialFillData.Leagues[1], lpQuery);
        }

        private static void FillLeague(MigrationBuilder migrationBuilder, IEnumerable<string> players, string leagueName, string query)
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

            InsertSeasons(migrationBuilder, InitialFillData.SeasonDates, InitialFillData.Leagues[0], insertSeasonsQuery);
            InsertSeasons(migrationBuilder, InitialFillData.SeasonDates.TakeLast(3), InitialFillData.Leagues[1], insertSeasonsQuery);
        }

        private static void InsertSeasons(MigrationBuilder migrationBuilder, IEnumerable<(string, string)> dates, string leagueName, string query)
        {
            foreach (var (startDate, finishDate) in dates)
            {
                InsertSeasonsFromList(migrationBuilder, leagueName, startDate, finishDate, query);
            }
        }

        private static void InsertSeasonsFromList(MigrationBuilder migrationBuilder, string leagueName, string startDate, string finishDate, string query)
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

    public static class InitialFillTours
    {
        private const int ToursCount = 50;
        private const int MaxGoals = 16;

        public static void ToursFill(MigrationBuilder migrationBuilder)
        {
            const string insertToursQuery = @"
    INSERT INTO [Tour](Date, HomeCount, GuestCount, SeasonId)
	SELECT @@date, @@homeCount, @@guestCount, Id
	FROM [Season]
	WHERE [Season].[Start] = @@startSeasonDate
    ";

            foreach (var date in InitialFillData.SeasonDates)
            {
                InsertTours(migrationBuilder, date.Item1, insertToursQuery);
            }
        }

        private static void InsertTours(MigrationBuilder migrationBuilder, string seasonStartDate, string query)
        {
            var currentTourDate = DateTime.Parse(seasonStartDate);

            for (int i = 0; i < ToursCount; i++)
            {
                InsertToursFromList(migrationBuilder, seasonStartDate, currentTourDate.ToString("yyyy-MM-dd"), query);
                currentTourDate = currentTourDate.AddDays(7);
            }
        }

        private static void InsertToursFromList(MigrationBuilder migrationBuilder, string seasonStartDate, string tourDate, string query)
        {
            var random = new Random();

            string q = MigrationHelpers.ReplaceVariablesWithValues(
                query,
                new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@@date", tourDate),
                    new KeyValuePair<string, object>("@@homeCount", random.Next(0, MaxGoals)),
                    new KeyValuePair<string, object>("@@guestCount", random.Next(0, MaxGoals)),
                    new KeyValuePair<string, object>("@@startSeasonDate", seasonStartDate),
                }
            );
            migrationBuilder.Sql(MigrationHelpers.ConvertScriptToDynamicSql(q));
        }
    }

    public static class InitialFillTeams
    {
        

        public static void TeamsFill(MigrationBuilder migrationBuilder)
        {
            DateTime startDate = DateTime.Parse(InitialFillData.SeasonDates[0].Item1);

            /*const string insertTeamsQuery = @"
    INSERT INTO [Team](Date, Name, TeamRole)
	SELECT @@date, @@name, @@teamRole, Id
	FROM [League]
	WHERE [League].[Name] = @@leagueName
    ";

            FillSeasons(migrationBuilder, InitialFillData.SeasonDates, InitialFillData.Leagues[0], insertSeasonsQuery);
            FillSeasons(migrationBuilder, InitialFillData.SeasonDates.TakeLast(3), InitialFillData.Leagues[1], insertSeasonsQuery);*/
        }
    }
}