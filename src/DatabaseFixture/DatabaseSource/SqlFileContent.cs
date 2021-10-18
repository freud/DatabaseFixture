using System;
using System.IO;
using DatabaseFixture.SqlContentTypes;
using DatabaseFixture.SqlExecution;
using DatabaseFixture.Versioning;
using DatabaseFixture.Versioning.Strategies;

namespace DatabaseFixture.DatabaseSource
{
    public class SqlFileContent : VersionedSqlContent
    {
        private SqlFileContent(string rawSql, IVersion version) : base(rawSql, version)
        {
        }

        public static SqlFileContent FromFile(FileInfo file, Func<FileInfo, IVersion> versionFactory)
        {
            using var reader = file.OpenText();
            var content = reader.ReadToEnd();
            return new SqlFileContent(content, versionFactory(file));
        }
    }
}