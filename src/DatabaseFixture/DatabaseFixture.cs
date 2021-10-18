using Ardalis.GuardClauses;
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
        private readonly string _defaultDatabaseName;

        public DatabaseFixture(
            SqlFilesDirectory directory, 
            SqlContentApplier applier,
            SqlConnection connection)
        {
            _directory = Guard.Against.Null(directory, nameof(directory));
            _applier = Guard.Against.Null(applier, nameof(applier));
            _connection = Guard.Against.Null(connection, nameof(connection));
            _defaultDatabaseName = "master";
        }

        public void Execute()
        {
            var destinationDatabase = _connection.Database;
            var builder = new SqlConnectionStringBuilder(_connection.ConnectionString)
            {
                InitialCatalog = _defaultDatabaseName
            };
            _connection.ConnectionString = builder.ToString();
            _connection.Open();

            CreateDatabaseIfNotExistsSqlContent
                .Create(destinationDatabase)
                .Apply(_applier);

            _connection.ChangeDatabase(destinationDatabase);

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
