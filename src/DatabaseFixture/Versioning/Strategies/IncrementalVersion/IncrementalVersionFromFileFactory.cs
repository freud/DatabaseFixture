using System;
using System.IO;
using System.Text.RegularExpressions;
using Ardalis.GuardClauses;

namespace DatabaseFixture.Versioning.Strategies.IncrementalVersion
{
    public class IncrementalVersionFactory : IVersionFactory
    {
        public IVersion Create(FileInfo file)
        {
            Guard.Against.FileExists(file, nameof(file));

            var versionParts = Parse(file.Name);

            return new IncrementalVersion(versionParts.VersionNumber);
        }

        public IVersion Create(string versionDisplayName)
        {
            var versionParts = Parse(versionDisplayName);
            return new IncrementalVersion(versionParts.VersionNumber);
        }

        private static dynamic Parse(string versionText)
        {
            var matches = Regex.Match(versionText, 
                @"^(?<VersionNumber>[0-9]+).*", 
                RegexOptions.IgnoreCase | RegexOptions.Singleline);

            if (matches.Success == false)
            {
                throw new NotSupportedException(@$"Filename ""{versionText}"" is not supported with {nameof(IncrementalVersion)} versioning strategy");
            }

            var versionNumber = int.Parse(matches.Groups["VersionNumber"].Value);

            return new { VersionNumber = versionNumber };
        }
    }
}