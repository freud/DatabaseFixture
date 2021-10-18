using System.Data.SqlClient;
using Dapper;
using DatabaseFixture.SqlExecution;
using DatabaseFixture.Versioning;
using Shouldly;
using Xunit;

namespace DatabaseFixture.Tests.Versioning
{
    public class CreateDatabaseVersionSqlContentTests
    {
        [Fact]
        public void executed__DatabaseVersion_table_does_not_exist__creates_the_table()
        {
            _runner.Execute(new CreateDatabaseVersionTableSqlContent());

            _connection
                .QuerySingle<int>("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'DatabaseVersion'")
                .ShouldBe(1);
        }

        [Fact]
        public void executed__DatabaseVersion_table_exists__does_nothing()
        {
            // arrange
            _runner.Execute(new CreateDatabaseVersionTableSqlContent());
            _connection.Execute("INSERT INTO [dbo].[DatabaseVersion]([Version], [AppliedAt], [AppliedSqlContent]) VALUES ('1.0.0', '2021-05-01 12:00', 'GO')");

            // act
            _runner.Execute(new CreateDatabaseVersionTableSqlContent());

            // assert
            _connection
                .QuerySingle<int>("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'DatabaseVersion'")
                .ShouldBe(1);
            _connection
                .QuerySingle<int>("SELECT COUNT(*) FROM [dbo].[DatabaseVersion]")
                .ShouldBe(1);
        }

        private readonly NonQueryRunner _runner;
        private readonly SqlConnection _connection;

        public CreateDatabaseVersionSqlContentTests()
        {
            var connectionString = "Server=192.168.100.100; Database=master; User Id=sa; Password=sa@102.2021;";
            _runner = new NonQueryRunner(connectionString);
            _connection = new SqlConnection(connectionString);
        }
    }
}