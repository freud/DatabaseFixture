using Ardalis.GuardClauses;
using DatabaseFixture.Versioning;

namespace DatabaseFixture.SqlExecution
{
    public abstract class SqlContent
    {
        public string RawSql { get; }
        public IVersion Version { get; }

        protected SqlContent(string rawSql, IVersion version)
        {
            RawSql = Guard.Against.NullOrWhiteSpace(rawSql, nameof(rawSql));
            Version = Guard.Against.Null(version, nameof(version));
        }

        public void Apply(SqlContentApplier applier)
        {
            applier.Apply(this);
        }

        public override string ToString() => RawSql;

        public static implicit operator string(SqlContent content)
        {
            return content.RawSql;
        }
    }
}