namespace DatabaseFixture.SqlExecution.PredefinedSql
{
    public abstract class SqlContentWithoutDatabase : PredefinedSqlContent
    {
        protected SqlContentWithoutDatabase(string rawSql) : base(rawSql)
        {
        }
    }
}