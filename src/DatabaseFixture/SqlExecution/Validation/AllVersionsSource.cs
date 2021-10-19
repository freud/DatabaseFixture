using System;
using System.Linq;
using Ardalis.GuardClauses;
using Dapper;
using DatabaseFixture.SqlExtensions;
using DatabaseFixture.Versioning.Strategies;

namespace DatabaseFixture.SqlExecution.Validation
{
    public class AllVersionsSource
    {
        private readonly IVersionFactory _versionFactory;
        private readonly string _connectionString;

        public AllVersionsSource(
            string connectionString,
            IVersionFactory versionFactory)
        {
            _connectionString = Guard.Against.NullOrWhiteSpace(connectionString, nameof(connectionString));
            _versionFactory = Guard.Against.Null(versionFactory, nameof(versionFactory));
        }

        public IOrderedEnumerable<DatabaseVersion> GetAll()
        {
            return _connectionString.Execute(connection =>
            {
                var allVersions = connection.Query<DatabaseVersionDto>("SELECT * FROM DatabaseVersion ORDER BY Id");
                
                return allVersions
                    .Select(v =>
                    {
                        var version = _versionFactory.Create(v.Version);
                        return new DatabaseVersion(v.Id, version, v.AppliedAt, v.AppliedSqlContent);
                    })
                    .OrderBy(v => v.Version);
            });
        }

        private class DatabaseVersionDto
        {
            public int Id { get; set; }
            public string Version { get; set; }
            public DateTime AppliedAt { get; set; }
            public string AppliedSqlContent { get; set; }
        }
    }
}