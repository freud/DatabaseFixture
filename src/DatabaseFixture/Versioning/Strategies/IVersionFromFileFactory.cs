using System.IO;

namespace DatabaseFixture.Versioning.Strategies
{
    public interface IVersionFactory
    {
        public IVersion Create(FileInfo fileInfo);
        public IVersion Create(string versionDisplayName);
    }
}