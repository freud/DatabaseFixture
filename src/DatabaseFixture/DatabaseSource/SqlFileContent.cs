using System.IO;
using DatabaseFixture.SqlExecution;

namespace DatabaseFixture.DatabaseSource
{
    public class SqlFileContent : SqlContent
    {
        private SqlFileContent(string rawSql) : base(rawSql)
        {
        }

        public static SqlContent FromFile(FileInfo file)
        {
            using var reader = file.OpenText();
            var content = reader.ReadToEnd();
            return new SqlFileContent(content);
        }
    }
}