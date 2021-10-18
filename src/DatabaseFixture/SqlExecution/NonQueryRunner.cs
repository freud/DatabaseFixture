using System;
using Ardalis.GuardClauses;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace DatabaseFixture.SqlExecution
{
    public class NonQueryRunner
    {
        private readonly SqlConnection _connection;

        public NonQueryRunner(SqlConnection connection)
        {
            _connection = Guard.Against.Null(connection, nameof(connection));
        }

        public void Execute(SqlContent content)
        {
            var server = new Server(new ServerConnection(_connection));
            server.ConnectionContext.ExecuteNonQuery(content.RawSql);
        }
    }
}