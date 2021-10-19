using Ardalis.GuardClauses;
using DatabaseFixture.SqlExecution;

namespace DatabaseFixture.SqlContentTypes
{
    public abstract class SqlContent
    {
        public string RawSql { get; }

        protected SqlContent(string rawSql)
        {
            RawSql = Guard.Against.NullOrWhiteSpace(rawSql, nameof(rawSql));
        }

        public override string ToString() => RawSql;

        public static implicit operator string(SqlContent content)
        {
            return content.RawSql;
        }

        public abstract void Apply(SqlContentApplier applier);
    }
}