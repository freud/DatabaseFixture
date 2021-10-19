using System;
using System.IO;
using Ardalis.GuardClauses;
using DatabaseFixture.DatabaseSource.Validation;
using DatabaseFixture.SqlContentTypes;
using DatabaseFixture.Versioning.Strategies;

namespace DatabaseFixture.DatabaseSource
{
    public class SqlFileContent : VersionedSqlContent
    {
        private SqlFileContent(string rawSql, IVersion version) : base(rawSql, version)
        {
        }

        public static SqlFileContent FromFile(FileInfo file, 
            Func<FileInfo, IVersion> versionFactory,
            SourceConsistencyValidator validator)
        {
            Guard.Against.Null(file, nameof(file));
            Guard.Against.Null(versionFactory, nameof(versionFactory));
            Guard.Against.Null(validator, nameof(validator));
            
            using var reader = file.OpenText();
            var content = reader.ReadToEnd();
            var version = versionFactory(file);
            validator.ThrowIfNotValid(version);
            return new SqlFileContent(content, version);
        }
    }
}