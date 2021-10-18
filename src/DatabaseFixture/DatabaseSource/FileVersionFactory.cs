using System.IO;
using Ardalis.GuardClauses;
using DatabaseFixture.Versioning;

namespace DatabaseFixture.DatabaseSource
{
    public class SemVersionFromFileFactory
    {
        private readonly FileInfo _file;

        public SemVersionFromFileFactory(FileInfo file)
        {
            _file = Guard.Against.FileExists(file, nameof(file));
        }

        public IVersion Create()
        {
            // ToDo: parse filename, to create semver format
            // ToDo: make the versioning type configurable
            return new SemVerVersion(1, 0, 0);
        }
    }
}