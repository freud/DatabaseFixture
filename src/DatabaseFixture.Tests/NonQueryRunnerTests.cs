using DatabaseFixture.SqlContentTypes;
using DatabaseFixture.SqlExecution;
using Shouldly;
using Xunit;

namespace DatabaseFixture.Tests
{
    public class NonQueryRunnerTests
    {
        [Fact]
        public void does_not_throw()
        {
            var sut = new NonQueryRunner("Server=192.168.100.100; Database=master; User Id=sa; Password=sa@102.2021;");
            Should.NotThrow(() => sut.Execute(new TestSqlContent()));
        }

        private class TestSqlContent : PredefinedSqlContent
        {
            protected internal TestSqlContent() : base(@"
                DROP DATABASE IF EXISTS [DatabaseFixture]
                GO
                CREATE DATABASE [DatabaseFixture]
                GO
                USE [DatabaseFixture]
                GO
                CREATE TABLE Test (TestColumn VARCHAR(MAX) NOT NULL)
                GO"
            )
            {
            }
        }
    }
}