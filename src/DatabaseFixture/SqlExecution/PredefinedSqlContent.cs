using DatabaseFixture.Versioning;

namespace DatabaseFixture.SqlExecution
{
    public abstract class PredefinedSqlContent : SqlContent
    {
        protected PredefinedSqlContent(string rawSql) : base(rawSql, new InternalVersion())
        {
        }
    }
}