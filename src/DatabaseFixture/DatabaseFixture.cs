using System.Collections.Generic;
using Ardalis.GuardClauses;
using DatabaseFixture.DatabaseSource;
using DatabaseFixture.SqlExecution;
using DatabaseFixture.SqlExecution.PredefinedSql;
using DatabaseFixture.SqlExtensions;
using DatabaseFixture.Versioning;
using DatabaseFixture.Versioning.Factories;

namespace DatabaseFixture
{
    public class DatabaseFixture
    {
        private readonly SqlFilesDirectory _directory;
        private readonly SqlContentApplier _applier;
        private readonly string _connectionString;

        public DatabaseFixture(
            SqlFilesDirectory directory, 
            SqlContentApplier applier,
            string connectionString)
        {
            _directory = Guard.Against.Null(directory, nameof(directory));
            _applier = Guard.Against.Null(applier, nameof(applier));
            _connectionString = Guard.Against.NullOrWhiteSpace(connectionString, nameof(connectionString));
        }

        public void Execute()
        {
            _connectionString.Execute(connection =>
            {
                var sqlToApply = new List<SqlContent>();
                sqlToApply.Add(CreateDatabaseIfNotExistsSqlContent.Create(connection.Database));
                sqlToApply.Add(CreateDatabaseVersionTableSqlContent.Create());
                sqlToApply.AddRange(_directory.GetAll());
                sqlToApply.ForEach(sql => sql.Apply(_applier));
            });
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
