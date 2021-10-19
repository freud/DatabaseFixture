using System.Linq;
using Ardalis.GuardClauses;
using DatabaseFixture.Versioning.Strategies;

namespace DatabaseFixture.SqlExecution.Validation
{
    public class SourceConsistencyValidator
    {
        private readonly AllVersionsSource _source;

        public SourceConsistencyValidator(AllVersionsSource source)
        {
            _source = Guard.Against.Null(source, nameof(source));
        }

        public void ThrowIfNotValid(IVersion version)
        {
            Guard.Against.Null(version, nameof(version));

            var allVersions = _source.GetAll();

            var versionToApplyIsLowerThanTheLatest = CheckLowerAndNotAppliedVersionsExist(version, allVersions);
            if (versionToApplyIsLowerThanTheLatest)
            {
                throw new LowerAndNotAppliedVersionAppeared(version);
            }
        }

        private static bool CheckLowerAndNotAppliedVersionsExist(
            IVersion version, IOrderedEnumerable<DatabaseVersion>? allVersions)
        {
            var versionIsAlreadyApplied = allVersions.Any(v => v.Version.Equals(version));
            var latestAppliedVersion = allVersions.First().Version;
            var latestAppliedVersionIsHigher = version.CompareTo(latestAppliedVersion) <= 0;
            var versionToApplyIsLowerThanTheLatest = versionIsAlreadyApplied == false && latestAppliedVersionIsHigher;
            return versionToApplyIsLowerThanTheLatest;
        }
    }
}