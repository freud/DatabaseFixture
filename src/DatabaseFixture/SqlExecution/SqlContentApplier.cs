using System;
using Dapper;
using Ardalis.GuardClauses;
using Microsoft.Data.SqlClient;

namespace DatabaseFixture.SqlExecution
{
    public class SqlContentApplier
    {
        private readonly NonQueryRunner _runner;
        private readonly string _connectionString;
        private readonly SqlContentAppliedInDatabaseChecker _checker;

        public SqlContentApplier(
            NonQueryRunner runner,
            string connectionString,
            SqlContentAppliedInDatabaseChecker checker)
        {
            _runner = Guard.Against.Null(runner, nameof(runner));
            _connectionString = Guard.Against.NullOrWhiteSpace(connectionString, nameof(connectionString));
            _checker = Guard.Against.Null(checker, nameof(checker));
        }

        public void Apply(SqlContent content)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                ApplyContent(connection, content);
            }
        }
        
        private void ApplyContent(SqlConnection connection, SqlContent content)
        {
            if (content is PredefinedSqlContent)
            {
                _runner.Execute(content);
                return;
            }

            if (_checker.CheckIfAlreadyApplied(content))
            {
                return;
            }

            _runner.Execute(content);

            if (content is VersionedSqlContent versionedSqlContent)
            {
                connection.Execute(
                    $"INSERT INTO [dbo].[DatabaseVersion]([Version], [AppliedAt], [AppliedSqlContent]) " +
                    $"VALUES(@Version, @UtcNow, @RawSql)",
                    new
                    {
                        Version = versionedSqlContent.Version.ToString(),
                        RawSql = content.ToString(),
                        UtcNow = DateTime.UtcNow
                    }
                );
            }
        }
    }
}
