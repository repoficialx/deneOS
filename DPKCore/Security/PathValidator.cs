using System;
using System.IO;

namespace DPKCore.Security
{
    public static class PathValidator
    {
        public static readonly string USER_ROOT = @"C:\DNUSR";
        public static readonly string SOFTWARE_ROOT = @"C:\SOFTWARE";
        public static readonly string SYSTEM_ROOT = @"C:\DENEOS";

        public static bool ContainsTraversal(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return true;

            return path.Contains("..") || path.Contains("../") || path.Contains("..\\");
        }

        public static string Normalize(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Path is null or empty.");

            string full = Path.GetFullPath(path);
            return full.TrimEnd(Path.DirectorySeparatorChar);
        }

        public static bool IsInside(string basePath, string targetPath)
        {
            string baseFull = Normalize(basePath);
            string targetFull = Normalize(targetPath);

            return targetFull.StartsWith(baseFull, StringComparison.OrdinalIgnoreCase);
        }

        public static bool CanWriteUser(string path)
        {
            string full = Normalize(path);
            return IsInside(USER_ROOT, full);
        }

        public static bool CanWriteSoftware(string path)
        {
            string full = Normalize(path);
            return IsInside(SOFTWARE_ROOT, full);
        }

        public static bool CanAccessSystem(string path, bool allowSystemFlag = false)
        {
            string full = Normalize(path);

            if (!IsInside(SYSTEM_ROOT, full))
                return false;

            return allowSystemFlag;
        }

        public static void ValidateWrite(string path, string mode = "software")
        {
            if (ContainsTraversal(path))
                throw new UnauthorizedAccessException("Path traversal detected.");

            string full = Normalize(path);

            bool allowed = mode switch
            {
                "user" => CanWriteUser(full),
                "software" => CanWriteSoftware(full),
                "system" => CanAccessSystem(full, false),
                _ => false
            };

            if (!allowed)
                throw new UnauthorizedAccessException($"Access denied to path: {full}");
        }
    }
}