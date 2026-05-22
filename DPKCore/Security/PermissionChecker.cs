using DPKCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DPKCore.Security
{
    public static class PermissionChecker
    {
        // -- PERMISOS --
        public const string FILESYSTEM_USER_READ = "filesystem.user.read";
        public const string FILESYSTEM_USER_WRITE = "filesystem.user.write";

        public const string FILESYSTEM_SOFTWARE_READ = "filesystem.software.read";
        public const string FILESYSTEM_SOFTWARE_WRITE = "filesystem.software.write";

        public const string FILESYSTEM_SYSTEM_READ = "filesystem.system.read";

        public const string NETWORK_INTERNET = "network.internet";
        public const string SYSTEM_UI = "system.ui";
        public const string SYSTEM_CORE = "system.core.access";
        public const string SYSTEM_SHELL = "system.shell.execute";

        public static bool HasPermission(Manifest manifest, string permission)
        {
            if (manifest == null)
                throw new ArgumentNullException(nameof(manifest));

            if (manifest.Permissions == null)
                return false;

            return manifest.Permissions.Contains(permission);
        }

        public static void Require(Manifest manifest, string permission)
        {
            if (!HasPermission(manifest, permission))
                throw new UnauthorizedAccessException(
                    $"Permission denied: {permission} (app: {manifest.Name})"
                );
        }

        public static void RequireUserFS(Manifest manifest, bool write = false)
        {
            Require(manifest,
                write
                    ? FILESYSTEM_USER_WRITE
                    : FILESYSTEM_USER_READ
            );
        }

        public static void RequireSoftwareFS(Manifest manifest, bool write = false)
        {
            Require(manifest,
                write
                    ? FILESYSTEM_SOFTWARE_WRITE
                    : FILESYSTEM_SOFTWARE_READ
            );
        }

        public static void RequireInternet(Manifest manifest)
        {
            Require(manifest, NETWORK_INTERNET);
        }

        public static void RequireUI(Manifest manifest)
        {
            Require(manifest, SYSTEM_UI);
        }

        public static void RequireSystemCore(Manifest manifest)
        {
            Require(manifest, SYSTEM_CORE);
        }

        public static bool IsMinimalApp(Manifest manifest)
        {
            return manifest.Permissions == null || manifest.Permissions.Length == 0;
        }
    }
}