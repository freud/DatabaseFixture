namespace DatabaseFixture.Versioning
{
    internal class InternalVersion : VersionStrategy<SemVerVersion>, IVersion
    {
        public override string DisplayName => "internal-version";

        public override int CompareTo(SemVerVersion obj)
        {
            return 0;
        }
    }
}