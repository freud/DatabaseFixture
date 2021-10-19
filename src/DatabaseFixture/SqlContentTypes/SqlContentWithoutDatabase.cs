using DatabaseFixture.SqlExecution;

namespace DatabaseFixture.SqlContentTypes
{
    public abstract class SqlContentWithoutDatabase : PredefinedSqlContent
    {
        protected SqlContentWithoutDatabase(string rawSql) : base(rawSql)
        {
        }

        public override void Apply(SqlContentApplier applier)
        {
            applier.Apply(this);
        }
    }
}