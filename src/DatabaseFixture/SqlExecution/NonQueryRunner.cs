using Ardalis.GuardClauses;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace DatabaseFixture.SqlExecution
{
    public class NonQueryRunner
    {
        private readonly string _connectionString;

        public NonQueryRunner(string connectionString)
        {
            _connectionString = Guard.Against.Null(connectionString, nameof(connectionString));
        }

        public void Execute(SqlContent content)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var server = new Server(new ServerConnection(connection));
            server.ConnectionContext.ExecuteNonQuery(content.RawSql);
        }
    }
}