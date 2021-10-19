using System;
using System.IO;
using System.Text.RegularExpressions;
using Ardalis.GuardClauses;

namespace DatabaseFixture.Versioning.Strategies.IncrementalVersion
{
    public class IncrementalVersionFromFileFactory : IVersionFromFileFactory
    {
        public IVersion Create(FileInfo file)
        {
            Guard.Against.FileExists(file, nameof(file));

            var versionParts = Parse(file.Name);

            return new IncrementalVersion(versionParts.VersionNumber);
        }

        private static dynamic Parse(string filename)
        {
            var matches = Regex.Match(filename, 
                @"^(?<VersionNumber>[0-9]+)-.*", 
                RegexOptions.IgnoreCase | RegexOptions.Singleline);

            if (matches.Success == false)
            {
                throw new NotSupportedException(@$"Filename ""{filename}"" is not supported with {nameof(IncrementalVersion)} versioning strategy");
            }

            var versionNumber = int.Parse(matches.Groups["VersionNumber"].Value);

            return new { VersionNumber = versionNumber };
        }
    }
}