namespace DatabaseFixture.SqlContentTypes
{
    public abstract class SqlContentWithoutDatabase : PredefinedSqlContent
    {
        protected SqlContentWithoutDatabase(string rawSql) : base(rawSql)
        {
        }
    }
}