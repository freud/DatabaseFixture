using System;

namespace DatabaseFixture.Versioning.Strategies
{
    public interface IVersion : IComparable
    {
        string DisplayName { get; }
    }
}