using System.IO;
using System.Linq;
using Ardalis.GuardClauses;
using DatabaseFixture.SqlExecution.Validation;
using DatabaseFixture.Versioning.Strategies;

namespace DatabaseFixture.DatabaseSource
{
    public class SqlFilesDirectory
    {
        private readonly SourceConsistencyValidator _validator;
        private readonly IVersionFactory _versionFactory;
        private readonly string _path;

        public SqlFilesDirectory(string path, IVersionFactory versionFactory, SourceConsistencyValidator validator)
        {
            _path = Guard.Against.DirectoryExists(path, nameof(path));
            _versionFactory = Guard.Against.Null(versionFactory, nameof(versionFactory));
            _validator = Guard.Against.Null(validator, nameof(validator));
        }

        public IOrderedEnumerable<SqlFileContent> GetAll()
        {
            return Directory
                .GetFiles(_path, "*.sql", SearchOption.TopDirectoryOnly)
                .Select(file => new FileInfo(file))
                .Select(file => SqlFileContent.FromFile(file, fileInfo => _versionFactory.Create(fileInfo), _validator))
                .OrderByDescending(file => file.Version);
        }
    }
}