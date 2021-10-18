using System.IO;
using System.Linq;
using Ardalis.GuardClauses;
using DatabaseFixture.Versioning.Strategies;

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

        public IOrderedEnumerable<SqlFileContent> GetAll()
        {
            return Directory
                .GetFiles(_path, "*.sql", SearchOption.TopDirectoryOnly)
                .Select(file => new FileInfo(file))
                .Select(file => SqlFileContent.FromFile(file, fileInfo => _versionFactory.Create(fileInfo)))
                .OrderByDescending(file => file.Version);
        }
    }
}