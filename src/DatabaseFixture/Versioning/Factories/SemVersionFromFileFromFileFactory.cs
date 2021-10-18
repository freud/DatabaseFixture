﻿using System;
using System.IO;
using System.Text.RegularExpressions;
using Ardalis.GuardClauses;

namespace DatabaseFixture.Versioning.Factories
{
    public class SemVersionFromFileFromFileFactory : IVersionFromFileFactory
    {
        public IVersion Create(FileInfo file)
        {
            Guard.Against.FileExists(file, nameof(file));

            var versionParts = Parse(file.Name);

            return new SemVerVersion(
                versionParts.Major,
                versionParts.Minor, 
                versionParts.Patch
            );
        }

        private static dynamic Parse(string filename)
        {
            var matches = Regex.Match(filename, 
                @"^(?<Major>[0-9]+)\.(?<Minor>[0-9]+)\.(?<Patch>[0-9]+).*", 
                RegexOptions.IgnoreCase | RegexOptions.Singleline);

            if (matches.Success == false)
            {
                throw new NotSupportedException(@$"Filename ""{filename}"" is not supported with SemVer versioning strategy");
            }

            var major = int.Parse(matches.Groups["Major"].Value);
            var minor = int.Parse(matches.Groups["Minor"].Value);
            var patch = int.Parse(matches.Groups["Patch"].Value);

            return new { Major = major, Minor = minor, Patch = patch };
        }
    }
}