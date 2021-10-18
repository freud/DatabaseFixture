using System.IO;
using Ardalis.GuardClauses;
using DatabaseFixture.SqlExecution;
using DatabaseFixture.Versioning.Factories;

namespace DatabaseFixture.DatabaseSource
{
    public class SqlFile
    {
        private readonly FileInfo _file;
        private readonly IVersionFromFileFactory _versionFromFileFactory;

        public SqlFile(FileInfo file, IVersionFromFileFactory versionFromFileFactory)
        {
            _file = Guard.Against.FileExists(file, nameof(file));
            _versionFromFileFactory = Guard.Against.Null(versionFromFileFactory, nameof(versionFromFileFactory));
        }

        public void Apply(SqlContentApplier applier)
        {
            SqlFileContent
                .FromFile(_file, fileInfo => _versionFromFileFactory.Create(fileInfo))
                .Apply(applier);
        }
    }
}