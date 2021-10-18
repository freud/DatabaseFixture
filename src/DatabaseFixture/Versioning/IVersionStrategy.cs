using System;

namespace DatabaseFixture.Versioning
{
    public interface IVersion : IComparable
    {
        string DisplayName { get; }
    }
}