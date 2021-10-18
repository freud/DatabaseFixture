using System;
using Dapper;
using Ardalis.GuardClauses;
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

        public void Apply(SqlContent content)
        {
            if (content is PredefinedSqlContent)
            {
                _runner.Execute(content);
                return;
            }

            if (content is not VersionedSqlContent versionedSqlContent)
            {
                throw new NotSupportedException();
            }

            if (_checker.CheckIfAlreadyApplied(versionedSqlContent))
            {
                return;
            }

            _runner.Execute(content);

            _connectionString.Execute(connection => connection.Execute(
                $"INSERT INTO [dbo].[DatabaseVersion]([Version], [AppliedAt], [AppliedSqlContent]) " +
                $"VALUES(@Version, @UtcNow, @RawSql)",
                new
                {
                    Version = versionedSqlContent.Version.ToString(),
                    RawSql = content.ToString(),
                    UtcNow = DateTime.UtcNow
                })
            );
        }
    }
}
