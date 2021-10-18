using Ardalis.GuardClauses;

namespace DatabaseFixture.SqlExecution
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

        public void Apply(SqlContentApplier applier)
        {
            applier.Apply(this);
        }
    }
}