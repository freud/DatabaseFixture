using System.IO;
using Ardalis.GuardClauses;

namespace DatabaseFixture.Versioning.Factories
{
    public class SemVersionFromFileFromFileFactory : IVersionFromFileFactory
    {
        public IVersion Create(FileInfo file)
        {
            Guard.Against.FileExists(file, nameof(file));
            // ToDo: parse filename, to create semver format
            return new SemVerVersion(1, 0, 0);
        }
    }
}