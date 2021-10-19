using System;
using DatabaseFixture.Versioning.Strategies;

namespace DatabaseFixture.DatabaseSource.Validation
{
    public class LowerAndNotAppliedVersionAppeared : Exception
    {
        public LowerAndNotAppliedVersionAppeared(IVersion version) 
            : base($"There were new version {version} appeared with lower number than the latest one. " +
                   $"Check if it's not added accidentally.")
        {
            
        }
    }
}