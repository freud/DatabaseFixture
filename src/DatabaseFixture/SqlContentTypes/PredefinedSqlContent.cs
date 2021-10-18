namespace DatabaseFixture.SqlContentTypes
{
    public abstract class PredefinedSqlContent : SqlContent
    {
        protected PredefinedSqlContent(string rawSql) : base(rawSql)
        {
        }
    }
}