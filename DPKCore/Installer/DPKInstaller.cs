using DPKCore.Models;
using DPKCore.Security;
using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;

namespace DPKCore.Installer
{
    public class DPKInstaller
    {
        public Manifest Install(string dpkPath, bool silent = false)
        {
            if (string.IsNullOrWhiteSpace(dpkPath))
                throw new ArgumentException("DPK path is empty.");

            if (!File.Exists(dpkPath))
                throw new FileNotFoundException("DPK not found.", dpkPath);

            string tempPath = Path.Combine(
                Path.GetTempPath(),
                "dpkcore_" + Guid.NewGuid().ToString("N")
            );

            try
            {
                Directory.CreateDirectory(tempPath);

                // Extract
                ZipFile.ExtractToDirectory(dpkPath, tempPath);

                // Load manifest
                string manifestPath = Path.Combine(tempPath, "meta", "manifest.json");

                if (!File.Exists(manifestPath))
                    throw new Exception("Manifest missing in DPK.");

                var manifest = JsonSerializer.Deserialize<Manifest>(
                    File.ReadAllText(manifestPath)
                );

                if (manifest == null)
                    throw new Exception("Invalid manifest.");

                // VALIDACIÓN DE PERMISOS
                PermissionChecker.Require(manifest, PermissionChecker.SYSTEM_UI);

                // Install path
                string installPath = Path.Combine(PathValidator.SOFTWARE_ROOT, manifest.Name);

                // Validación de ruta (software)
                PathValidator.ValidateWrite(installPath, "software");

                if (Directory.Exists(installPath))
                    Directory.Delete(installPath, true);

                Directory.CreateDirectory(installPath);

                // Copy files
                foreach (string file in Directory.GetFiles(tempPath, "*", SearchOption.AllDirectories))
                {
                    if (file.Contains(Path.Combine("meta")))
                        continue;

                    string relative = Path.GetRelativePath(tempPath, file);
                    string dest = Path.Combine(installPath, relative);

                    string destDir = Path.GetDirectoryName(dest)!;

                    // seguridad extra
                    PathValidator.ValidateWrite(destDir, "software");

                    Directory.CreateDirectory(destDir);
                    File.Copy(file, dest, true);
                }

                if (!silent)
                {
                    Console.WriteLine($"Installed: {manifest.Name} v{manifest.Version}");
                }

                return manifest;
            }
            finally
            {
                // cleanup
                try
                {
                    if (Directory.Exists(tempPath))
                        Directory.Delete(tempPath, true);
                }
                catch
                {
                    // ignore
                }
            }
        }
    }
}