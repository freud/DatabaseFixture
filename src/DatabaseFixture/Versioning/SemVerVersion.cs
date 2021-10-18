using Ardalis.GuardClauses;

namespace DatabaseFixture.Versioning
{
    public class SemVerVersion : VersionStrategy<SemVerVersion>, IVersion
    {
        public int Major { get; }
        public int Minor { get; }
        public int Patch { get; }
        public override string DisplayName => $"{Major}.{Minor}.{Patch}";

        public SemVerVersion(int major, int minor, int patch)
        {
            Major = Guard.Against.Negative(major, nameof(major));
            Minor = Guard.Against.Negative(minor, nameof(minor));
            Patch = Guard.Against.Negative(patch, nameof(patch));
        }

        public override int CompareTo(SemVerVersion obj)
        {
            Guard.Against.Null(obj, nameof(obj));

            if (obj.Major < Major) return -1;
            if (obj.Major > Major) return 1;
            if (obj.Minor < Minor) return -1;
            if (obj.Minor > Minor) return 1;
            if (obj.Patch < Patch) return -1;
            if (obj.Patch > Patch) return 1;

            return 0;
        }
    }
}