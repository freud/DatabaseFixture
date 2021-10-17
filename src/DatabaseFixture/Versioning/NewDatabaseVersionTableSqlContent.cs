using DatabaseFixture.SqlExecution;

namespace DatabaseFixture.Versioning
{
    public class NewDatabaseVersionTableSqlContent : SqlContent
    {
        private const string CreateDatabaseVersionTableSql = @"
            IF OBJECT_ID('[dbo].[DatabaseVersion]', 'U') IS NULL
                CREATE TABLE [dbo].[DatabaseVersion] (
                    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
                    [Version] VARCHAR(100) NOT NULL,
                    [AppliedAt] DATETIME NOT NULL,
                    [AppliedSqlContent] NVARCHAR(MAX) NOT NULL
                )
        ";

        public NewDatabaseVersionTableSqlContent() : base(CreateDatabaseVersionTableSql)
        {
        }

        public static SqlContent Create()
        {
            return new NewDatabaseVersionTableSqlContent();
        }
    }
}