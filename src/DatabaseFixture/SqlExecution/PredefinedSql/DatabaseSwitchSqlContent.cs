namespace DatabaseFixture.SqlExecution.PredefinedSql
{
    public abstract class DatabaseSwitchSqlContent : PredefinedSqlContent
    {
        protected DatabaseSwitchSqlContent(string databaseToApplyOn, string databaseToSwitchAfter, string rawSql) : base(rawSql)
        {
        }
    }
}