using System;
using BCrypt.Net;

namespace deneOS.Security
{
    /// <summary>
    /// Gestión segura de contraseñas usando BCrypt
    /// </summary>
    public static class PasswordHasher
    {
        // Nivel de trabajo de BCrypt (12 es un buen balance seguridad/rendimiento)
        private const int WorkFactor = 12;

        /// <summary>
        /// Genera un hash seguro de la contraseña
        /// </summary>
        /// <param name="password">Contraseña en texto plano</param>
        /// <returns>Hash BCrypt de la contraseña</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("La contraseña no puede estar vacía", nameof(password));
            }

            // BCrypt automáticamente genera un salt único por cada hash
            return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
        }

        /// <summary>
        /// Verifica si una contraseña coincide con su hash
        /// </summary>
        /// <param name="password">Contraseña en texto plano a verificar</param>
        /// <param name="hashedPassword">Hash almacenado</param>
        /// <returns>True si coincide, False si no</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(hashedPassword))
            {
                return false;
            }

            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
            catch (BCrypt.Net.SaltParseException)
            {
                // El hash no es válido (puede ser texto plano antiguo)
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Error verificando contraseña: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Verifica si un string es un hash BCrypt válido
        /// </summary>
        public static bool IsValidHash(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                return false;

            // Los hashes BCrypt empiezan con $2a$, $2b$ o $2y$
            return hash.StartsWith("$2a$") ||
                   hash.StartsWith("$2b$") ||
                   hash.StartsWith("$2y$");
        }
    }
}