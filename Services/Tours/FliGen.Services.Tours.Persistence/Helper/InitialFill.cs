using FliGen.Common.Sql;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FliGen.Services.Tours.Persistence.Helper
{
    public static class InitialFill
    {
        public static int ToursFill(MigrationBuilder migrationBuilder)
        {
            const string insertToursQuery = @"
    INSERT INTO [Tour](Date, HomeCount, GuestCount, SeasonId, TourStatusId)
	SELECT @@date, @@homeCount, @@guestCount, @@seasonId, 5";

            int toursCount = 0;
            foreach (var date in InitialFillData.SeasonDates)
            {
                toursCount += InsertTours(migrationBuilder, date.Item1, date.Item2, insertToursQuery);
            }

            return toursCount;
        }

        private static int InsertTours(
            MigrationBuilder migrationBuilder,
            int seasonId,
            string seasonStartDate,
            string query)
        {
            var currentTourDate = DateTime.Parse(seasonStartDate);
            int i;
            for (i = 0; i < InitialFillData.ToursInSeasonCount; i++)
            {
                InsertToursFromList(migrationBuilder, seasonId, seasonStartDate, currentTourDate.ToString("yyyy-MM-dd"), query);
                currentTourDate = currentTourDate.AddDays(7);
                if (currentTourDate >= DateTime.Now)
                {
                    break;
                }
            }

            return i;
        }

        private static void InsertToursFromList(
            MigrationBuilder migrationBuilder,
            int seasonId,
            string seasonStartDate,
            string tourDate,
            string query)
        {
            var random = new Random();

            string q = MigrationHelpers.ReplaceVariablesWithValues(
                query,
                new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@@date", tourDate),
                    new KeyValuePair<string, object>("@@homeCount", random.Next(0, InitialFillData.MaxGoals)),
                    new KeyValuePair<string, object>("@@guestCount", random.Next(0, InitialFillData.MaxGoals)),
                    new KeyValuePair<string, object>("@@seasonId", seasonId),
                    new KeyValuePair<string, object>("@@startSeasonDate", seasonStartDate),
                }
            );
            migrationBuilder.Sql(MigrationHelpers.ConvertScriptToDynamicSql(q));
        }
    }
}