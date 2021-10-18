using System.IO;
using Ardalis.GuardClauses;
using DatabaseFixture.SqlExecution;

namespace DatabaseFixture.DatabaseSource
{
    public class SqlFile
    {
        private readonly FileInfo _file;

        public SqlFile(FileInfo file)
        {
            _file = Guard.Against.FileExists(file, nameof(file));
        }

        public void Apply(SqlContentApplier applier)
        {
            SqlFileContent
                .FromFile(_file, fileInfo => new SemVersionFromFileFactory(fileInfo).Create())
                .Apply(applier);
        }
    }
}