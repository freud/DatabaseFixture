using System.IO;

namespace DatabaseFixture.Versioning.Factories
{
    public interface IVersionFromFileFactory
    {
        public IVersion Create(FileInfo fileInfo);
    }
}