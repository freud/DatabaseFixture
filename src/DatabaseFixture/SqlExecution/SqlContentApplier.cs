using System;
using System.Data.Common;
using Ardalis.GuardClauses;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DatabaseFixture.SqlExecution
{
    public class SqlContentApplier
    {
        private readonly NonQueryRunner _runner;
        private readonly DbConnection _connection;
        private readonly SqlContentAppliedInDatabaseChecker _checker;

        public SqlContentApplier(
            NonQueryRunner runner,
            SqlConnection connection,
            SqlContentAppliedInDatabaseChecker checker)
        {
            _runner = Guard.Against.Null(runner, nameof(runner));
            _connection = Guard.Against.Null(connection, nameof(connection));
            _checker = Guard.Against.Null(checker, nameof(checker));
        }

        public void Apply(SqlContent content)
        {
            if (_checker.CheckIfAlreadyApplied(content))
            {
                return;
            }

            _runner.Execute(content);

            _connection.Execute(
                $"INSERT INTO [dbo].[DatabaseVersion]([Version], [AppliedAt], [AppliedSqlContent]) " +
                $"VALUES('anything-for-now', '{DateTime.UtcNow}', '{content.RawSql}')"
            );
        }
    }
}