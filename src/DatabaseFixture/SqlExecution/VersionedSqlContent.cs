using Ardalis.GuardClauses;
using DatabaseFixture.Versioning;

namespace DatabaseFixture.SqlExecution
{
    public abstract class VersionedSqlContent : SqlContent
    {
        public IVersion Version { get; }

        protected VersionedSqlContent(string rawSql, IVersion version) : base(rawSql)
        {
            Version = Guard.Against.Null(version, nameof(version));
        }
        
        public void Apply(SqlContentApplier applier)
        {
            applier.Apply(this);
        }
    }
}