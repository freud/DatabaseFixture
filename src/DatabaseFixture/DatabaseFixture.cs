using Ardalis.GuardClauses;
using DatabaseFixture.DatabaseSource;
using DatabaseFixture.SqlExecution;
using DatabaseFixture.Versioning;

namespace DatabaseFixture
{
    public class DatabaseFixture
    {
        private readonly SqlFilesDirectory _directory;
        private readonly SqlContentApplier _applier;

        public DatabaseFixture(SqlFilesDirectory directory, SqlContentApplier applier)
        {
            _directory = Guard.Against.Null(directory, nameof(directory));
            _applier = Guard.Against.Null(applier, nameof(applier));
        }

        public void Execute()
        {
            NewDatabaseVersionTableSqlContent.Create().Apply(_applier);

            foreach (var sqlFile in _directory.GetAll())
            {
                sqlFile.Apply(_applier);
            }
        }
    }
}
