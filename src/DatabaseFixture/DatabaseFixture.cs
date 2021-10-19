using Ardalis.GuardClauses;
using DatabaseFixture.DatabaseSource;
using DatabaseFixture.DatabaseSource.Validation;
using DatabaseFixture.SqlExecution;
using DatabaseFixture.Versioning.Initialization;
using DatabaseFixture.Versioning.Strategies;
using DatabaseFixture.Versioning.Strategies.SemVer;
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

        public static DatabaseFixture Create(string sqlFilesDirectory, string connectionString, IVersionFactory? versionStrategy = null)
        {
            var versionFactory = versionStrategy ?? new SemVerFactory();
            var validator = new SourceConsistencyValidator(new AllVersionsSource(connectionString, versionStrategy));
            var directory = new SqlFilesDirectory(sqlFilesDirectory, versionFactory, validator);
            var applier = new SqlContentApplier(new NonQueryRunner(connectionString), connectionString, new SqlContentAppliedInDatabaseChecker(connectionString));
            return new DatabaseFixture(directory, applier, connectionString);
        }
    }
}
