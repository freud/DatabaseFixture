using System;
using Dapper;
using Ardalis.GuardClauses;
using DatabaseFixture.SqlContentTypes;
using DatabaseFixture.SqlExtensions;

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

        public void Apply(SqlContentWithoutDatabase content)
        {
            _runner.Execute(content);
        }

        public void Apply(PredefinedSqlContent content)
        {
            _runner.Execute(content);
        }

        public void Apply(VersionedSqlContent content)
        {
            if (_checker.CheckIfAlreadyApplied(content))
            {
                return;
            }

            _runner.Execute(content);

            _connectionString.Execute(connection => connection.Execute(
                $"INSERT INTO [dbo].[DatabaseVersion]([Version], [AppliedAt], [AppliedSqlContent]) " +
                $"VALUES(@Version, @UtcNow, @RawSql)",
                new
                {
                    Version = content.Version.ToString(),
                    RawSql = content.ToString(),
                    UtcNow = DateTime.UtcNow
                })
            );
        }
    }
}
