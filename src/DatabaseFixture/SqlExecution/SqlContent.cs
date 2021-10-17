using Ardalis.GuardClauses;

namespace DatabaseFixture.SqlExecution
{
    public class SqlContent
    {
        public string RawSql { get; protected set; }

        protected SqlContent(string rawSql)
        {
            RawSql = Guard.Against.NullOrWhiteSpace(rawSql, nameof(rawSql));
        }
    }
}