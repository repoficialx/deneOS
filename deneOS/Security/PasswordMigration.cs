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
                    Console.WriteLine("[INFO] Contraseña ya está hasheada correctamente");
                    return;
                }

                // Es texto plano, necesitamos migrar
                Console.WriteLine("[WARN] Contraseña en texto plano detectada. Iniciando migración...");

                // Crear backup antes de modificar
                File.Copy(ConfigFile, BackupFile, true);
                Console.WriteLine($"[INFO] Backup creado en: {BackupFile}");

                // Hashear la contraseña
                string hashedPassword = PasswordHasher.HashPassword(currentPassword);

                // Actualizar el archivo
                lines[2] = $"password = {hashedPassword}";
                File.WriteAllLines(ConfigFile, lines);

                Console.WriteLine("[SUCCESS] Contraseña migrada exitosamente a hash seguro");

                MessageBox.Show(
                    (string)T("passwordmigrated") ?? "Su contraseña ha sido migrada a un formato seguro.",
                    "deneOS Security",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Error durante migración de contraseña: {ex.Message}");

                // Restaurar backup si existe
                if (File.Exists(BackupFile))
                {
                    File.Copy(BackupFile, ConfigFile, true);
                    Console.WriteLine("[INFO] Backup restaurado debido a error");
                }

                MessageBox.Show(
                    $"Error migrando contraseña: {ex.Message}",
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
                Console.WriteLine("[INFO] Configuración restaurada desde backup");
            }
        }
    }
}