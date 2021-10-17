using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ardalis.GuardClauses;

namespace DatabaseFixture.DatabaseSource
{
    public class SqlFilesDirectory
    {
        private readonly string _path;

        public SqlFilesDirectory(string path)
        {
            _path = Guard.Against.DirectoryExists(path, nameof(path));
        }

        public IEnumerable<SqlFile> GetAll()
        {
            return Directory
                .GetFiles(_path, "*.sql", SearchOption.TopDirectoryOnly)
                .Select(file => new FileInfo(file))
                .Select(file => new SqlFile(file));
        }
    }
}