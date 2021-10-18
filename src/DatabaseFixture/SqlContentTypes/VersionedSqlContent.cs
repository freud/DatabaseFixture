using Ardalis.GuardClauses;
using DatabaseFixture.Versioning;
using DatabaseFixture.Versioning.Strategies;

namespace DatabaseFixture.SqlContentTypes
{
    public abstract class VersionedSqlContent : SqlContent
    {
        public IVersion Version { get; }

        protected VersionedSqlContent(string rawSql, IVersion version) : base(rawSql)
        {
            Version = Guard.Against.Null(version, nameof(version));
        }
    }
}