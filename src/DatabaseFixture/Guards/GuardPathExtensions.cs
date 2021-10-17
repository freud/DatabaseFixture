using System;
using System.IO;

// ReSharper disable once CheckNamespace
namespace Ardalis.GuardClauses
{
    public static class GuardPathExtensions
    {
        public static string DirectoryExists(this IGuardClause guardClause, string directory, string parameterName)
        {
            Guard.Against.NullOrWhiteSpace(directory, parameterName);
            if (Directory.Exists(directory) == false)
            {
                throw new ArgumentException(
                    $"Directory {directory} cannot be found", parameterName, new DirectoryNotFoundException());
            }
            return directory;
        }

        public static FileInfo FileExists(this IGuardClause guardClause, FileInfo fileInfo, string parameterName)
        {
            Guard.Against.Null(fileInfo, parameterName);
            if (fileInfo.Exists == false)
            {
                throw new ArgumentException(
                    $"File {fileInfo.FullName} cannot be found", parameterName,
                    new FileNotFoundException($"File cannot be found", fileInfo.FullName)
                    );
            }
            return fileInfo;
        }
    }
}