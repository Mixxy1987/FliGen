using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FliGen.Persistence.Helper.InitialFill
{
    public class MigrationHelpers
    {
        private MigrationHelpers()
        {
            throw new Exception("This class shouldn't be instantiated");
        }

        public static string GetDynamicSqlFromFile(string filepath)
        {
            var sqlFile = Path.Combine(Environment.CurrentDirectory, filepath);
            return ConvertScriptToDynamicSql(File.ReadAllText(sqlFile));
        }

        public static string ConvertScriptToDynamicSql(string script)
        {
            return $"EXEC sp_executesql N'{script.Replace("'", "''")}'";
        }

        public static string ConvertScriptToDynamicSql(StringBuilder builder)
        {
            builder.Replace("'", "''");
            builder.Insert(0, "EXEC sp_executesql N'");
            builder.Append("'");
            return builder.ToString();
        }

        public static string ReplaceVariablesWithValues(string sourceQuery, KeyValuePair<string, object>[] values)
        {
            return ReplaceVariablesWithValuesCore(new StringBuilder(sourceQuery), values);
        }

        public static string ReplaceVariablesWithValues(string sourceQuery, IEnumerable<KeyValuePair<string, object>> values)
        {
            return ReplaceVariablesWithValuesCore(new StringBuilder(sourceQuery), values);
        }

        public static string ReplaceVariableWithValue(string query, KeyValuePair<string, object> value)
        {
            var b = new StringBuilder(query);
            ReplaceVariableWithValue(b, value);
            return b.ToString();
        }

        public static void ReplaceVariableWithValue(StringBuilder builder, KeyValuePair<string, object> value)
        {
            object parameterValue = value.Value;
            string replacement;
            if (parameterValue == null)
            {
                replacement = "NULL";
            }
            else
            {
                switch (parameterValue)
                {
                    case string s:
                        replacement = $"N'{s}'";
                        break;
                    case TimeSpan ts:
                        replacement = ts >= TimeSpan.Zero ? $"'+{ts:hh\\:mm}'" : $"'-{ts:hh\\:mm}'";
                        break;
                    default:
                        // TODO: Support different types: DateTime, TimeSpan, Guid, Blob, etc.
                        replacement = parameterValue.ToString();
                        break;
                }
            }

            builder.Replace(value.Key, replacement);
        }

        private static string ReplaceVariablesWithValuesCore(StringBuilder builder, IEnumerable<KeyValuePair<string, object>> values)
        {
            foreach (KeyValuePair<string, object> value in values)
            {
                ReplaceVariableWithValue(builder, value);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Just a stub to create helper methods, which generate sql queries by their self.
        /// </summary>
        /// <param name="text"></param>
        private static void Sql(string text)
        {
            throw new NotImplementedException();
        }
    }
}