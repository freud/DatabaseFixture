using System;
using System.Linq;
using Ardalis.GuardClauses;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DatabaseFixture.DatabaseSource
{
    public class FileIsAppliedInDatabaseChecker
    {
        private readonly SqlConnection _connection;

        public FileIsAppliedInDatabaseChecker(SqlConnection connection)
        {
            _connection = Guard.Against.Null(connection, nameof(connection));
        }

        public bool CheckIfFileIsAlreadyApplied(SqlFile sqlFile)
        {
            var results = _connection.Query(
                @"SELECT * FROM [dbo].[DatabaseVersion] 
                  WHERE [AppliedSqlContent] = @AppliedSqlContent",
                  new { AppliedSqlContent = sqlFile.GetContent() }
                ).ToList();

            if (results.Count > 1)
            {
                throw new Exception($"More than one applied sql contents found for {sqlFile.File.FullName}");
            }

            return results.Count == 1;
        }
    }
}