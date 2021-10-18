namespace DatabaseFixture.SqlExecution.PredefinedSql
{
    public class CreateDatabaseIfNotExistsSqlContent : SqlContent
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

        public static SqlContent Create(string databaseName)
        {
            return new CreateDatabaseIfNotExistsSqlContent(databaseName);
        }
    }
}