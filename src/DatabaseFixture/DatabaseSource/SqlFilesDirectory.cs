using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ardalis.GuardClauses;
using DatabaseFixture.Versioning.Factories;

namespace DatabaseFixture.DatabaseSource
{
    public class SqlFilesDirectory
    {
        private readonly IVersionFromFileFactory _versionFactory;
        private readonly string _path;

        public SqlFilesDirectory(string path, IVersionFromFileFactory versionFactory)
        {
            _path = Guard.Against.DirectoryExists(path, nameof(path));
            _versionFactory = Guard.Against.Null(versionFactory, nameof(versionFactory));
        }

        public IEnumerable<SqlFile> GetAll()
        {
            return Directory
                .GetFiles(_path, "*.sql", SearchOption.TopDirectoryOnly)
                .Select(file => new FileInfo(file))
                .Select(file => new SqlFile(file, _versionFactory));
        }
    }
}