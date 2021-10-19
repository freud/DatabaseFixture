using Ardalis.GuardClauses;

namespace DatabaseFixture.Versioning.Strategies.IncrementalVersion
{
    public class IncrementalVersion : VersionStrategy<IncrementalVersion>, IVersion
    {
        public int VersionNumber { get; }
        public override string DisplayName => VersionNumber.ToString();

        public IncrementalVersion(int versionNumber)
        {
            VersionNumber = Guard.Against.Negative(versionNumber, nameof(versionNumber));
        }

        public override int CompareTo(IncrementalVersion obj)
        {
            Guard.Against.Null(obj, nameof(obj));

            if (obj.VersionNumber < VersionNumber) return -1;
            if (obj.VersionNumber > VersionNumber) return 1;

            return 0;
        }
    }
}