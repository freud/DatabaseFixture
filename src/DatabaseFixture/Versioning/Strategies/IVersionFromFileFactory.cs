using System.IO;

namespace DatabaseFixture.Versioning.Strategies
{
    public interface IVersionFromFileFactory
    {
        public IVersion Create(FileInfo fileInfo);
    }
}