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
            var directory = "ExampleSqlFiles";
            var connectionString = "Server=192.168.100.100; Database=DatabaseFixtureExample5; User Id=sa; Password=sa@102.2021;";
            DatabaseFixture.Create(directory, connectionString).Execute();
        }
    }
}