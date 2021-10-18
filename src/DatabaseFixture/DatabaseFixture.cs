using Ardalis.GuardClauses;
using DatabaseFixture.DatabaseSource;
using DatabaseFixture.SqlExecution;
using DatabaseFixture.SqlExecution.PredefinedSql;
using DatabaseFixture.Versioning;
using Microsoft.Data.SqlClient;

namespace DatabaseFixture
{
    public class DatabaseFixture
    {
        private readonly string _connectionString;
        private readonly SqlFilesDirectory _directory;
        private readonly SqlContentApplier _applier;
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;

        public DatabaseFixture(
            SqlFilesDirectory directory, 
            SqlContentApplier applier,
            string connectionString)
        {
            _directory = Guard.Against.Null(directory, nameof(directory));
            _applier = Guard.Against.Null(applier, nameof(applier));
            _connectionString = Guard.Against.NullOrWhiteSpace(connectionString, nameof(connectionString));
            _connectionStringBuilder = new SqlConnectionStringBuilder(_connectionString);
        }

        public void Execute()
        {
            CreateDatabaseIfNotExistsSqlContent
                .Create(_connectionStringBuilder.InitialCatalog)
                .Apply(_applier);

            NewDatabaseVersionTableSqlContent
                .Create()
                .Apply(_applier);

            foreach (var sqlFile in _directory.GetAll())
            {
                sqlFile.Apply(_applier);
            }
        }
    }
}
