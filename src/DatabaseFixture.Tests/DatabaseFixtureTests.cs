using System.Linq;
using Dapper;
using DatabaseFixture.SqlExtensions;
using DatabaseFixture.Versioning.Strategies.IncrementalVersion;
using DatabaseFixture.Versioning.Strategies.SemVer;
using Shouldly;
using Xunit;
using Xunit.Repeat;

namespace DatabaseFixture.Tests
{
    public class DatabaseFixtureTests
    {
        [Theory]
        [Repeat(100)]
        public void execute__applies_all_sql_files(int iterationNumber)
        {
            var directory = "ExampleSqlFilesWithSemVer";
            var connectionString = "Server=192.168.100.100; Database=DatabaseFixture_ExampleSqlFilesWithSemVer; User Id=sa; Password=sa@102.2021;";
            var versionStrategy = new SemVerFactory();
            DatabaseFixture.Create(directory, connectionString, versionStrategy).Execute();

            var savedVersions = connectionString
                .Execute(connection => connection.Query<string>("SELECT [Version] FROM [DatabaseVersion]"))
                .ToList();
            savedVersions.Count.ShouldBe(3);
            savedVersions.ElementAt(0).ShouldBe("1.0.0");
            savedVersions.ElementAt(1).ShouldBe("1.0.1");
            savedVersions.ElementAt(2).ShouldBe("1.1.0");
        }

        [Theory]
        [Repeat(100)]
        public void execute__incremental_version_strategy__applies_all_sql_files_with_correct_version(int iterationNumber)
        {
            var directory = "ExampleSqlFilesWithIncrementalVersion";
            var connectionString = "Server=192.168.100.100; Database=DatabaseFixture_ExampleSqlFilesWithIncrementalVersion; User Id=sa; Password=sa@102.2021;";
            var versionStrategy = new IncrementalVersionFactory();
            DatabaseFixture.Create(directory, connectionString, versionStrategy).Execute();

            var savedVersions = connectionString
                .Execute(connection => connection.Query<string>("SELECT [Version] FROM [DatabaseVersion]"))
                .ToList();
            savedVersions.Count.ShouldBe(3);
            savedVersions.ElementAt(0).ShouldBe("1");
            savedVersions.ElementAt(1).ShouldBe("2");
            savedVersions.ElementAt(2).ShouldBe("3");
        }
    }
}