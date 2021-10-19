using System;
using System.IO;
using System.Linq;
using Ardalis.GuardClauses;
using DatabaseFixture.DatabaseSource.Validation;

namespace DatabaseFixture.DatabaseSource
{
    public class SqlFilesDirectory
    {
        private readonly Func<FileInfo, SqlFileContent> _sqlFileContentFactory;
        private readonly string _path;

        public SqlFilesDirectory(string path, Func<FileInfo, SqlFileContent> sqlFileContentFactory, SourceConsistencyValidator validator)
        {
            _path = Guard.Against.DirectoryExists(path, nameof(path));
            _sqlFileContentFactory = Guard.Against.Null(sqlFileContentFactory, nameof(sqlFileContentFactory));
        }

        public IOrderedEnumerable<SqlFileContent> GetAll()
        {
            return Directory
                .GetFiles(_path, "*.sql", SearchOption.TopDirectoryOnly)
                .Select(file => new FileInfo(file))
                .Select(file => _sqlFileContentFactory(file))
                .OrderByDescending(file => file.Version);
        }
    }
}