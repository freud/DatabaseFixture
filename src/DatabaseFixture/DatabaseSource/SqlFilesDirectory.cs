using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ardalis.GuardClauses;

namespace DatabaseFixture.DatabaseSource
{
    public class SqlFilesDirectory
    {
        private readonly FileIsAppliedInDatabaseChecker _checker;
        private readonly string _path;

        public SqlFilesDirectory(string path, FileIsAppliedInDatabaseChecker checker)
        {
            _path = Guard.Against.NullOrWhiteSpace(path, nameof(path));
            _checker = Guard.Against.Null(checker, nameof(checker));
        }

        public IEnumerable<SqlFile> GetAll()
        {
            return Directory
                .GetFiles(_path, "*.sql", SearchOption.TopDirectoryOnly)
                .Select(file => new FileInfo(file))
                .Select(file => new SqlFile(file, _checker));
        }
    }
}