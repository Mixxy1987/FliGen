using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FliGen.Persistence.Helper
{
    public static class InitialFill
    {
        public static void NamesAndRatesFill(MigrationBuilder migrationBuilder)
        {
            var dict = new List<string>
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

            const string migrateExistingOperationEntriesQuery = @"
	INSERT INTO [Player](LastName, FirstName)
	VALUES
    (@@lastName, @@firstName)

    INSERT INTO [PlayerRate](Date, Value, PlayerId)
	SELECT '01.01.2020', @@rate, Id
	FROM [Player]
	WHERE [Player].[FirstName] = @@firstName AND [Player].[LastName] = @@lastName
";
            foreach (var kv in dict)
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
    }
}