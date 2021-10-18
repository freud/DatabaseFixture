﻿using Ardalis.GuardClauses;

namespace DatabaseFixture.SqlExecution
{
    public abstract class SqlContent
    {
        public string RawSql { get; protected set; }

        protected SqlContent(string rawSql)
        {
            RawSql = Guard.Against.NullOrWhiteSpace(rawSql, nameof(rawSql));
        }

        public void Apply(SqlContentApplier applier)
        {
            applier.Apply(this);
        }
    }
}