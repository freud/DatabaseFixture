using System.IO;
using Ardalis.GuardClauses;

namespace DatabaseFixture.SqlExecution
{
    public class SqlFileContent : SqlContent
    {
        public SqlFileContent(string rawSql) : base(rawSql)
        {
        }

        public static SqlContent FromFile(string filepath)
        {
            Guard.Against.NullOrWhiteSpace(filepath, nameof(filepath));
            return new SqlFileContent(File.ReadAllText(filepath));
        }
    }
}