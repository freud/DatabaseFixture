using System;
using Microsoft.Data.SqlClient;

namespace DatabaseFixture.SqlExtensions
{
    public static class ConnectionExtensions
    {
        public static void Execute(this string connectionString, Action<SqlConnection> action)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                action(connection);
            }
        }

        public static void ExecuteWithDatabase(this string connectionString, string databaseName, Action<SqlConnection> action)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = databaseName
            };

            using (var connection = new SqlConnection(connectionStringBuilder.ToString()))
            {
                connection.Open();
                action(connection);
            }
        }

        public static T Execute<T>(this string connectionString, Func<SqlConnection, T> action)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return action(connection);
            }
        }
    }
}