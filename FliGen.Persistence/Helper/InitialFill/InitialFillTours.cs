using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FliGen.Persistence.Helper.InitialFill
{
    public static class InitialFillTours
    {
        public static int ToursFill(MigrationBuilder migrationBuilder)
        {
            const string insertToursQuery = @"
    INSERT INTO [Tour](Date, HomeCount, GuestCount, SeasonId)
	SELECT @@date, @@homeCount, @@guestCount, Id
	FROM [Season]
	WHERE [Season].[Start] = @@startSeasonDate
    ";
            int toursCount = 0;
            foreach (var date in InitialFillData.SeasonDates)
            {
                toursCount += InsertTours(migrationBuilder, date.Item1, insertToursQuery);
            }

            return toursCount;
        }

        private static int InsertTours(MigrationBuilder migrationBuilder, string seasonStartDate, string query)
        {
            var currentTourDate = DateTime.Parse(seasonStartDate);
            int i = 0;
            for (i = 0; i < InitialFillData.ToursInSeasonCount; i++)
            {
                InsertToursFromList(migrationBuilder, seasonStartDate, currentTourDate.ToString("yyyy-MM-dd"), query);
                currentTourDate = currentTourDate.AddDays(7);
                if (currentTourDate >= DateTime.Now)
                {
                    break;
                }
            }

            return i;
        }

        private static void InsertToursFromList(MigrationBuilder migrationBuilder, string seasonStartDate,
            string tourDate, string query)
        {
            var random = new Random();

            string q = MigrationHelpers.ReplaceVariablesWithValues(
                query,
                new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@@date", tourDate),
                    new KeyValuePair<string, object>("@@homeCount", random.Next(0, InitialFillData.MaxGoals)),
                    new KeyValuePair<string, object>("@@guestCount", random.Next(0, InitialFillData.MaxGoals)),
                    new KeyValuePair<string, object>("@@startSeasonDate", seasonStartDate),
                }
            );
            migrationBuilder.Sql(MigrationHelpers.ConvertScriptToDynamicSql(q));
        }
    }
}