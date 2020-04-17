using FliGen.Common.SeedWork;
using FliGen.Common.Sql;
using FliGen.Services.Notifications.Domain.Entities.Enum;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;

namespace FliGen.Services.Notifications.Persistence.Helper
{
    public static class InitialFill
    {
        public static void PlayerNotificationLinksFill(MigrationBuilder migrationBuilder)
        {
            const string lpQuery = @"
    INSERT INTO [PlayerNotificationLinks](PlayerId, NotificationTypeId)
	SELECT @@playerId, @@notificationTypeId
";
            InsertNotificationLinks(migrationBuilder, lpQuery);
        }

        private static void InsertNotificationLinks(
            MigrationBuilder migrationBuilder,
            string query)
        {
            foreach (var kv in InitialFillData.Players)
            {
                foreach (var notificationType in Enumeration.GetAll<NotificationType>())
                {
                    InsertLeagueFromList(migrationBuilder, kv.Item1, notificationType.Id, query);
                }
            }
        }


        private static void InsertLeagueFromList(
            MigrationBuilder migrationBuilder,
            int playerId,
            int notificationTypeId,
            string query)
        {
            string q = MigrationHelpers.ReplaceVariablesWithValues(
                query,
                new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("@@playerId", playerId),
                    new KeyValuePair<string, object>("@@notificationTypeId", notificationTypeId),
                }
            );
            migrationBuilder.Sql(MigrationHelpers.ConvertScriptToDynamicSql(q));
        }
    }
}