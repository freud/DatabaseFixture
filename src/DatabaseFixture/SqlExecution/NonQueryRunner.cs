using Ardalis.GuardClauses;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace DatabaseFixture.SqlExecution
{
    public class NonQueryRunner
    {
        private readonly string _sqlConnectionString;

        public NonQueryRunner(string sqlConnectionString)
        {
            _sqlConnectionString = Guard.Against.NullOrWhiteSpace(sqlConnectionString, nameof(sqlConnectionString));
        }

        public void Execute(SqlContent content)
        {
            using (var connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();
                var server = new Server(new ServerConnection(connection));
                server.ConnectionContext.ExecuteNonQuery(content.RawSql);   
            }
        }
    }
}