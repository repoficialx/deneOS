using System;
using System.IO;
using System.Windows.Forms;
using static Traductor;

namespace deneOS.Security
{
    /// <summary>
    /// Gestiona la migración de contraseñas en texto plano a hashes seguros
    /// </summary>
    public static class PasswordMigration
    {
        private const string ConfigFile = @"C:\DENEOS\sysconf\config.ini";
        private const string BackupFile = @"C:\DENEOS\sysconf\config.ini.backup";

        /// <summary>
        /// Verifica si hay contraseñas en texto plano y las migra
        /// </summary>
        public static void MigrateIfNeeded()
        {
            if (!File.Exists(ConfigFile))
                return;

            try
            {
                var lines = File.ReadAllLines(ConfigFile);

                if (lines.Length < 3)
                    return;

                // Verificar si la línea de contraseña existe y no es un hash
                string passwordLine = lines[2];

                if (!passwordLine.ToLower().StartsWith("password = "))
                    return;

                string currentPassword = passwordLine.Substring(11).Trim();

                // Si ya es un hash BCrypt, no hacer nada
                if (PasswordHasher.IsValidHash(currentPassword))
                {
                    Console.WriteLine("[INFO] Password was already hashed. No migration needed.");
                    return;
                }

                // Es texto plano, necesitamos migrar
                Console.WriteLine("[WARN] Password in plain text detected. Starting migration...");

                // Crear backup antes de modificar
                File.Copy(ConfigFile, BackupFile, true);
                Console.WriteLine($"[INFO] Backup saved in: {BackupFile}");

                // Hashear la contraseña
                string hashedPassword = PasswordHasher.HashPassword(currentPassword);

                // Actualizar el archivo
                lines[2] = $"password = {hashedPassword}";
                File.WriteAllLines(ConfigFile, lines);

                Console.WriteLine("[SUCCESS] Password migrated successfully to secure hash");

                MessageBox.Show(
                    (string)T("passwordmigrated") ?? "Your password has been migrated to a secure format.",
                    "deneOS Security",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Error during password migration: {ex.Message}");

                // Restaurar backup si existe
                if (File.Exists(BackupFile))
                {
                    File.Copy(BackupFile, ConfigFile, true);
                    Console.WriteLine("[INFO] Backup restored due to error");
                }

                MessageBox.Show(
                    $"Error migrating password: {ex.Message}",
                    "deneOS Security Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Restaura el backup si es necesario
        /// </summary>
        public static void RestoreBackup()
        {
            if (File.Exists(BackupFile))
            {
                File.Copy(BackupFile, ConfigFile, true);
                Console.WriteLine("[INFO] Configuration restored from backup");
            }
        }
    }
}