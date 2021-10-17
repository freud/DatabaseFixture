using System;
using Ardalis.GuardClauses;
using Microsoft.Data.SqlClient;

namespace DatabaseFixture.DatabaseSource
{
    public class FileIsAppliedInDatabaseChecker
    {
        private readonly SqlConnection _connection;

        public FileIsAppliedInDatabaseChecker(SqlConnection connection)
        {
            _connection = Guard.Against.Null(connection, nameof(connection));
        }

        public bool CheckIfFileIsAlreadyApplied(SqlFile file)
        {
            throw new NotImplementedException();
        }
    }
}