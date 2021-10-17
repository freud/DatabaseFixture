using System.IO;
using Ardalis.GuardClauses;
using DatabaseFixture.SqlExecution;

namespace DatabaseFixture.DatabaseSource
{
    public class SqlFile
    {
        public FileInfo File { get; }
        private readonly FileIsAppliedInDatabaseChecker _checker;

        public SqlFile(FileInfo file, FileIsAppliedInDatabaseChecker checker)
        {
            File = Guard.Against.Null(file, nameof(file));
            _checker = Guard.Against.Null(checker, nameof(checker));
        }

        public SqlFileContent GetContent()
        {
            using var streamReader = File.OpenText();
            var content = streamReader.ReadToEnd();
            return new SqlFileContent(content);
        }

        public bool CheckIfAppliedInDatabase()
        {
            return _checker.CheckIfFileIsAlreadyApplied(this);
        }
    }
}