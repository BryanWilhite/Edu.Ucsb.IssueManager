using System;
using System.IO;
using System.Reflection;

namespace Edu.Ucsb.Core
{
    /// <summary>
    /// Static members related to <see cref="System.Reflection"/>.
    /// </summary>
    public static class FrameworkAssemblyUtility
    {
        /// <summary>
        /// Gets the directory name from assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">assembly;The expected assembly is not here.</exception>
        public static string GetPathFromAssembly(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly), "The expected assembly is not here.");

            var hasCodeBaseOnWindows =
                !string.IsNullOrWhiteSpace(assembly.CodeBase)
                &&
                !FrameworkFileUtility.IsForwardSlashSystem()
                ;

            var location = hasCodeBaseOnWindows ?
                assembly.CodeBase.Replace("file:///", string.Empty) :
                assembly.Location;

            var root = Path.GetDirectoryName(location);
            return root;
        }

        /// <summary>
        /// Gets the path from assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="fileSegment">The file segment.</param>
        public static string GetPathFromAssembly(Assembly assembly, string fileSegment)
        {
            if (string.IsNullOrWhiteSpace(fileSegment)) throw new ArgumentNullException("fileSegment", "The expected file segment is not here.");

            fileSegment = FrameworkFileUtility.TrimLeadingDirectorySeparatorChars(fileSegment);
            if (Path.IsPathRooted(fileSegment)) throw new FormatException("The expected relative path is not here.");

            fileSegment = FrameworkFileUtility.NormalizePath(fileSegment);

            var root = GetPathFromAssembly(assembly);
            var levels = FrameworkFileUtility.CountParentDirectoryChars(fileSegment);
            if (levels > 0) root = FrameworkFileUtility.GetParentDirectory(root, levels);

            var path = FrameworkFileUtility.GetCombinedPath(root, fileSegment);
            return path;
        }
    }
}
