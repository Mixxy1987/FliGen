﻿using System.Collections.Generic;

namespace FliGen.Common.Sql
{
    public static class InitialFillData
    {
        public static readonly List<(int, string)> Players = new List<(int, string)>
        {
            (1, "Матюнин Валентин 7.2"),
            (2, "Волчков Вячеслав 7.1"),
            (3, "Никитин Евгений 7.1"),
            (4, "Алтухов Антон 7.2"),
            (5, "Виноходов Игорь 7.3"),
            (6, "Галицкий Вячеслав 7.2"),
            (7, "Косенков Олег 7.5"),
            (8, "Мухин Иван 7.3"),
            (9, "Попов Александр 7.4"),
            (10, "Попов Артем 7.4"),
            (11, "Растаев Дмитрий 7.4"),
            (12, "Филинов Павел 7.3"),
            (13, "Масюк Родион 7.3"),
            (14, "Дубцов Максим 7.2"),
            (15, "Ахтямов Руслан 6.9"),
            (16, "Яшин Анатолий 7.1"),
            (17, "Зырянов Егор 7.1"),
            (18, "Сгибнев Андрей 7.2"),
            (19, "Ляшук Алексей 6.5"),
            (20, "Ларичкин Алексей 7.0"),
            (21, "Абушахмин Роман 7.4"),
            (22, "Абрамов Дмитрий 7.4"),
            (23, "Алтухов Илья 7.4"),
            (24, "Аль-Махлай Виталий 7.4"),
            (25, "Антипов Илья 7.4"),
            (26, "Арабей Евгений 7.4"),
            (27, "Аскаров Ришат 7.4"),
            (28, "Богачев Николай 7.4"),
            (29, "Биляуэр Константин 7.4"),
            (30, "Гриднев Виталий 7.4"),
            (31, "Гугняев Антон 7.4"),
            (32, "Гусев Павел 7.4"),
            (33, "Демидов Дмитрий 7.4"),
            (34, "Дорожко Никита 7.4"),
            (35, "Дубенский Андрей 7.4"),
            (36, "Захаревич Дмитрий 7.4"),
            (37, "Егоров Роман 7.4"),
            (38, "Добряков Аскар 7.4"),
            (39, "Дутлов Леонид 7.4"),
            (40, "Казанцев Павел 7.4"),
            (41, "Зайцев Сергей 7.4"),
            (42, "Захаревич Олег 7.4"),
            (43, "Иванов Александр 7.4"),
            (44, "Иванов Павел 7.2"),
            (45, "Камзалаков Иван 7.1"),
            (46, "Ларионов Олег 7.9"),
            (47, "Кувшинов Максим 7.1"),
            (48, "Кувшинов Роман 7.2"),
            (49, "Кухахметов Марат 7.2"),
            (50, "Лаврега Игорь 7.1")
        };

        public static readonly List<(int, string)> Leagues = new List<(int, string)>
        {
            (1, "FLI"),
            (2, "FLIHockey")
        };

        public static readonly List<(string, string)> TeamCombinations = new List<(string, string)>
        {
            ("Лазурные", "Красные"),
            ("Красные", "Лазурные")
        };

        public static readonly List<(int, string, string)> SeasonDates = new List<(int, string, string)>
        {
            (1, "2015-01-01", "2015-12-31"),
            (2, "2016-01-01", "2016-12-31"),
            (3, "2017-01-01", "2017-12-31"),
            (4, "2018-01-01", "2018-12-31"),
            (5, "2019-01-01", "2019-12-31"),
            (6, "2020-01-01", "2020-12-31"),
            (7, "2018-01-01", "2018-12-31"),
            (8, "2019-01-01", "2019-12-31"),
            (9, "2020-01-01", "2020-12-31")
        };

        public const int HockeyLeaguePlayers = 50;
        public const int FootballLeaguePlayers = 50;
        public const int ToursInSeasonCount = 50;
        public const int MaxGoals = 16;
        public const int TeamPlayersCount = 8;
    }
}