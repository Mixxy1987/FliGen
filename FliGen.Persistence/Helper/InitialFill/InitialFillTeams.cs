using System;
using System.Collections.Generic;
using System.Linq;
using FliGen.Domain.Common;
using FliGen.Domain.Entities.Enum;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FliGen.Persistence.Helper.InitialFill
{
    public static class InitialFillTeams
    {
        public static void TeamsFill(MigrationBuilder migrationBuilder, int toursCount)
        {
            const string insertTeamsQuery = @"
    INSERT INTO [Team](TeamRoleId, TourId, Name, PlayersCount)
	SELECT @@teamRoleId, @@tourId, @@name, @@playersCount
    ";

            InsertTeams(migrationBuilder, insertTeamsQuery, toursCount);
        }

        private static void InsertTeams(MigrationBuilder migrationBuilder, string query, int toursCount)
        {
            List<TeamRole> roles = Enumeration.GetAll<TeamRole>().ToList();
            var random = new Random();
            for (int tourId = 1; tourId <= toursCount; tourId++)
            {
                int rnd = random.Next(0, InitialFillData.TeamCombinations.Count);
                InsertTeam(migrationBuilder, roles[0].Id, tourId, InitialFillData.TeamCombinations[rnd].Item1,
                    query); // home team
                InsertTeam(migrationBuilder, roles[1].Id, tourId, InitialFillData.TeamCombinations[rnd].Item2,
                    query); // guest team
            }
        }

        private static void InsertTeam(MigrationBuilder migrationBuilder, int roleId, int tourId, string teamName,
            string query)
        {
            string q = MigrationHelpers.ReplaceVariablesWithValues(
                query,
                new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("@@teamRoleId", roleId),
                    new KeyValuePair<string, object>("@@tourId", tourId),
                    new KeyValuePair<string, object>("@@name", teamName),
                    new KeyValuePair<string, object>("@@playersCount", InitialFillData.TeamPlayersCount),
                }
            );
            migrationBuilder.Sql(MigrationHelpers.ConvertScriptToDynamicSql(q));
        }
    }
}