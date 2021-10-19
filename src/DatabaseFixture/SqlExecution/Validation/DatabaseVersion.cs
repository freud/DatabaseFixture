using System;
using Ardalis.GuardClauses;
using DatabaseFixture.Versioning.Strategies;

namespace DatabaseFixture.SqlExecution.Validation
{
    public class DatabaseVersion
    {
        public int Id { get; }
        public IVersion Version { get; }
        public DateTime AppliedAt { get; }
        public string AppliedSqlContent { get; }

        public DatabaseVersion(int id, IVersion version, DateTime appliedAt, string appliedSqlContent)
        {
            Id = Guard.Against.Negative(id, nameof(id));
            Version = Guard.Against.Null(version, nameof(version));
            AppliedAt = Guard.Against.Default(appliedAt, nameof(appliedAt));
            AppliedSqlContent = Guard.Against.NullOrWhiteSpace(appliedSqlContent, nameof(appliedSqlContent));
        }
    }
}