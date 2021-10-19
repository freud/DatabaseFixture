using Ardalis.GuardClauses;
using DatabaseFixture.SqlContentTypes;
using DatabaseFixture.SqlExtensions;
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

        public void Execute(SqlContentWithoutDatabase content)
        {
            _connectionString.ExecuteWithDatabase("master", connection =>
            {
                var serverConnection = new ServerConnection(connection);
                var server = new Server(serverConnection);
                server.ConnectionContext.ExecuteNonQuery(content.RawSql);
            });
        }

        public void Execute(SqlContent content)
        {
            _connectionString.Execute(connection =>
            {
                var serverConnection = new ServerConnection(connection);
                var server = new Server(serverConnection);
                server.ConnectionContext.ExecuteNonQuery(content.RawSql);
            });
        }
    }
}