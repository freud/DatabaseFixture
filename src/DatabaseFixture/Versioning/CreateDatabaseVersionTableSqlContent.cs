using DatabaseFixture.SqlContentTypes;
using DatabaseFixture.SqlExecution;

namespace DatabaseFixture.Versioning.Initialization
{
    public class CreateDatabaseVersionTableSqlContent : PredefinedSqlContent
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

        public CreateDatabaseVersionTableSqlContent() : base(CreateDatabaseVersionTableSql)
        {
        }

        public static CreateDatabaseVersionTableSqlContent Create()
        {
            return new CreateDatabaseVersionTableSqlContent();
        }

        public override void Apply(SqlContentApplier applier)
        {
            applier.Apply(this);
        }
    }
}