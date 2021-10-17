using System.IO;
using Ardalis.GuardClauses;

namespace DatabaseFixture.DatabaseSource
{
    public class SqlFile
    {
        private readonly FileIsAppliedInDatabaseChecker _checker;
        private readonly FileInfo _file;

        public SqlFile(FileInfo file, FileIsAppliedInDatabaseChecker checker)
        {
            _file = Guard.Against.Null(file, nameof(file));
            _checker = Guard.Against.Null(checker, nameof(checker));
        }
 
        public bool CheckIfAppliedInDatabase()
        {
            return _checker.CheckIfFileIsAlreadyApplied(this);
        }
    }
}