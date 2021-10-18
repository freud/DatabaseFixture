﻿using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using DatabaseFixture.DatabaseSource;
using DatabaseFixture.SqlExecution;
using DatabaseFixture.SqlExecution.PredefinedSql;
using DatabaseFixture.SqlExtensions;
using DatabaseFixture.Versioning;
using DatabaseFixture.Versioning.Factories;
using Microsoft.Data.SqlClient;

namespace DatabaseFixture
{
    public class DatabaseFixture
    {
        private readonly SqlFilesDirectory _directory;
        private readonly SqlContentApplier _applier;
        private readonly SqlConnectionStringBuilder _builder;

        public DatabaseFixture(
            SqlFilesDirectory directory, 
            SqlContentApplier applier,
            string connectionString)
        {
            _directory = Guard.Against.Null(directory, nameof(directory));
            _applier = Guard.Against.Null(applier, nameof(applier));
            _builder = new SqlConnectionStringBuilder(connectionString);
        }

        public void Execute()
        {
            CreateDatabaseIfNotExistsSqlContent.Create(_builder.InitialCatalog).Apply(_applier);
            CreateDatabaseVersionTableSqlContent.Create().Apply(_applier);
            foreach (var content in _directory.GetAll())
            {
                content.Apply(_applier);
            }
        }

        public static DatabaseFixture Create(string sqlFilesDirectory, string connectionString)
        {
            var versionFactory = new SemVersionFromFileFromFileFactory();
            var directory = new SqlFilesDirectory(sqlFilesDirectory, versionFactory);
            var applier = new SqlContentApplier(new NonQueryRunner(connectionString), connectionString, new SqlContentAppliedInDatabaseChecker(connectionString));
            return new(directory, applier, connectionString);
        }
    }
}
