namespace DatabaseFixture.SqlExecution.PredefinedSql
{
    public class CreateDatabaseIfNotExistsSqlContent : PredefinedSqlContent
    {
        public CreateDatabaseIfNotExistsSqlContent(string databaseName) : base(
            @$"
                IF NOT EXISTS(
	                SELECT * FROM [sys].[databases]
	                WHERE name = '{databaseName}'
                )
                BEGIN
	                CREATE DATABASE [{databaseName}]
                END
                GO
            ")
        {
        }

        public static CreateDatabaseIfNotExistsSqlContent Create(string databaseName)
        {
            return new CreateDatabaseIfNotExistsSqlContent(databaseName);
        }
    }
}