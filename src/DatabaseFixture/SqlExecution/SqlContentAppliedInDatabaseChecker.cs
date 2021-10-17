using System;
using System.Linq;
using Ardalis.GuardClauses;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DatabaseFixture.SqlExecution
{
    public class SqlContentAppliedInDatabaseChecker
    {
        private readonly SqlConnection _connection;

        public SqlContentAppliedInDatabaseChecker(SqlConnection connection)
        {
            _connection = Guard.Against.Null(connection, nameof(connection));
        }

        public bool CheckIfAlreadyApplied(SqlContent content)
        {
            var results = _connection.Query(
                @"SELECT * FROM [dbo].[DatabaseVersion] 
                  WHERE [AppliedSqlContent] = @AppliedSqlContent",
                  new { AppliedSqlContent = content.RawSql }
                ).ToList();

            if (results.Count > 1)
            {
                throw new Exception($"More than one applied sql contents found for {content.RawSql}");
            }

            return results.Count == 1;
        }
    }
}
