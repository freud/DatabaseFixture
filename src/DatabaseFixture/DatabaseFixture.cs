﻿using Ardalis.GuardClauses;
using DatabaseFixture.DatabaseSource;
using DatabaseFixture.SqlExecution;
using DatabaseFixture.SqlExecution.PredefinedSql;
using DatabaseFixture.Versioning;
using DatabaseFixture.Versioning.Factories;
using Microsoft.Data.SqlClient;

namespace DatabaseFixture
{
    public class DatabaseFixture
    {
        private readonly SqlFilesDirectory _directory;
        private readonly SqlContentApplier _applier;
        private readonly SqlConnection _connection;

        public DatabaseFixture(
            SqlFilesDirectory directory, 
            SqlContentApplier applier,
            SqlConnection connection)
        {
            _directory = Guard.Against.Null(directory, nameof(directory));
            _applier = Guard.Against.Null(applier, nameof(applier));
            _connection = Guard.Against.Null(connection, nameof(connection));
        }

        public void Execute()
        {
            CreateDatabaseIfNotExistsSqlContent
                .Create(_connection.Database)
                .Apply(_applier);

            CreateDatabaseVersionTableSqlContent
                .Create()
                .Apply(_applier);

            foreach (var sqlFile in _directory.GetAll())
            {
                sqlFile.Apply(_applier);
            }
        }

        public static DatabaseFixture Create(string sqlFilesDirectory, string connectionString)
        {
            var versionFactory = new SemVersionFromFileFromFileFactory();
            var connection = new SqlConnection(connectionString);
            var directory = new SqlFilesDirectory(sqlFilesDirectory, versionFactory);
            var applier = new SqlContentApplier(new NonQueryRunner(connection), connection, new SqlContentAppliedInDatabaseChecker(connection));
            return new(directory, applier, connection);
        }
    }
}
