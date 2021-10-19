using DatabaseFixture.SqlExecution;

namespace DatabaseFixture.SqlContentTypes
{
    public abstract class PredefinedSqlContent : SqlContent
    {
        protected PredefinedSqlContent(string rawSql) : base(rawSql)
        {
        }

        public override void Apply(SqlContentApplier applier)
        {
            applier.Apply(this);
        }
    }
}