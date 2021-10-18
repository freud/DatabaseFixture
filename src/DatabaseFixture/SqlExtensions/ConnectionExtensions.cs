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