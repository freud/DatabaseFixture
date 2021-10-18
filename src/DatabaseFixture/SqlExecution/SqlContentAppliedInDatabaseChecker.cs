using System;
using System.Linq;
using Ardalis.GuardClauses;
using Dapper;
using DatabaseFixture.SqlExtensions;

namespace DatabaseFixture.SqlExecution
{
    public class SqlContentAppliedInDatabaseChecker
    {
        private readonly string _connectionString;

        public SqlContentAppliedInDatabaseChecker(string connectionString)
        {
            _connectionString = Guard.Against.NullOrWhiteSpace(connectionString, nameof(connectionString));
        }

        public bool CheckIfAlreadyApplied(SqlContent content)
        {
            return _connectionString.Execute(connection =>
            {
                var results = connection.Query(
                    @"SELECT * FROM [dbo].[DatabaseVersion] WHERE [AppliedSqlContent] = @AppliedSqlContent",
                    new { AppliedSqlContent = content.RawSql }
                ).ToList();

                if (results.Count > 1)
                {
                    throw new Exception($"More than one applied sql contents found for {content.RawSql}");
                }

                return results.Count == 1;
            });
        }
    }
}
