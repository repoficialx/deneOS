using System;
using System.Diagnostics;
using System.IO;

namespace deneOS
{
    internal class deneTerm
    {
        static string currentPath = @"C:\DNUSR";
        static readonly string userPath = @"C:\DNUSR";
        static readonly string softwarePath = @"C:\SOFTWARE";

        static void Main(string[] args)
        {
            Console.Title = "deneTerm - Terminal Segura";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.OutputEncoding = System.Text.Encoding.UTF8;


            Console.WriteLine("deneTerm - Terminal segura de deneOS");
            Console.WriteLine("Escribe 'help' para ver los comandos disponibles.\n");

            while (true)
            {
                Console.Write($"{GetAlias(currentPath)}\\> ");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input)) continue;

                string[] parts = input.Split(' ', 2);
                string cmd = parts[0].ToLower();
                string arg = parts.Length > 1 ? parts[1] : "";

                var parts1 = input.Trim().Split(' ', 2);
                var command = parts1[0].ToLower();
                var argument = parts1.Length > 1 ? parts[1] : "";

                switch (cmd)
                {
                    case "help":
                        Console.WriteLine("Comandos disponibles:");
                        Console.WriteLine("- help: Muestra esta ayuda");
                        Console.WriteLine("- ls: Lista archivos del directorio actual");
                        Console.WriteLine("- cd <carpeta>: Cambia de carpeta dentro del directorio permitido");
                        Console.WriteLine("- chamadi <user/software>: Cambia entre ~\\ (usuario) y ~S\\ (software)");
                        Console.WriteLine("- repair: Ejecuta herramientas de reparación");
                        Console.WriteLine("- exit: Cierra la terminal");
                        break;

                    case "ls":
                        ListDirectory();
                        break;

                    case "cd":
                        ChangeDirectory(arg);
                        break;

                    case "chamadi":
                        ChangeMainDirectory(arg);
                        break;

                    case "repair":
                        StartRepair();
                        break;

                    case "exit":
                        Console.WriteLine("Saliendo de deneTerm...");
                        return;

                    default:
                        Console.WriteLine("❓ Comando no reconocido. Escribe 'help' para ver los comandos.");
                        break;

                    case "mkdir": CreateDirectory(argument); break;
                    case "rmdir": RemoveDirectory(argument); break;
                    case "mkfile": CreateFile(argument); break;
                    case "rmfile": DeleteFile(argument); break;
                    case "openfile": OpenFile(argument); break;
                    case "openapp": OpenApp(argument); break;
                    case "pwd": Console.WriteLine(GetAlias(currentPath)); break;
                }
            }
        }

        static void ListDirectory()
        {
            try
            {
                string[] dirs = Directory.GetDirectories(currentPath);
                string[] files = Directory.GetFiles(currentPath);

                Console.WriteLine("📁 Directorios:");
                foreach (var dir in dirs)
                    Console.WriteLine("  [D] " + Path.GetFileName(dir));

                Console.WriteLine("📄 Archivos:");
                foreach (var file in files)
                    Console.WriteLine("  [F] " + Path.GetFileName(file));
            }
            catch
            {
                Console.WriteLine("❌ Error al listar el contenido.");
            }
        }

        static void ChangeDirectory(string folder)
        {
            if (string.IsNullOrWhiteSpace(folder))
            {
                Console.WriteLine("❌ Ruta vacía no permitida.");
                return;
            }

            string newPath;

            if (folder == "..")
            {
                string parentPath = Directory.GetParent(currentPath)?.FullName ?? "";

                // No permitir salir de la main-directory
                if (currentPath.StartsWith(userPath) && !parentPath.StartsWith(userPath) ||
                    currentPath.StartsWith(softwarePath) && !parentPath.StartsWith(softwarePath))
                {
                    Console.WriteLine("❌ No puedes salir de la main-directory actual.");
                    return;
                }

                newPath = parentPath;
            }
            else
            {
                newPath = Path.Combine(currentPath, folder);
            }

            if (Directory.Exists(newPath) && IsAllowedPath(newPath))
            {
                currentPath = newPath;
            }
            else
            {
                Console.WriteLine("❌ Directorio no válido o fuera de límites.");
            }
        }


        static void ChangeMainDirectory(string target)
        {
            if (target.ToLower() == "user")
            {
                currentPath = userPath;
                Console.WriteLine("✅ Cambiado a ~\\ (Usuario)");
            }
            else if (target.ToLower() == "software")
            {
                currentPath = softwarePath;
                Console.WriteLine("✅ Cambiado a ~S\\ (Software)");
            }
            else
            {
                Console.WriteLine("❌ Opción no válida. Usa 'chamadi user' o 'chamadi software'.");
            }
        }

        static bool IsAllowedPath(string path)
        {
            return path.StartsWith(userPath) || path.StartsWith(softwarePath);
        }

        static string GetAlias(string path)
        {
            if (path.StartsWith(userPath))
            {
                string sub = path.Substring(userPath.Length).TrimStart('\\', '/');
                return "~" + (sub != "" ? "\\" + sub : "");
            }
            if (path.StartsWith(softwarePath))
            {
                string sub = path.Substring(softwarePath.Length).TrimStart('\\', '/');
                return "~S" + (sub != "" ? "\\" + sub : "");
            }
            return "?";
        }


        static void StartRepair()
        {
            Console.WriteLine("🔧 Iniciando reparación con SFC y DISM...");
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c sfc /scannow",
                    UseShellExecute = true
                });

                Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c DISM /Online /Cleanup-Image /RestoreHealth",
                    UseShellExecute = true
                });

                Console.WriteLine("🛠 Reparaciones iniciadas. Puede tardar unos minutos.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error al ejecutar reparación: " + ex.Message);
            }
        }
        static void CreateDirectory(string name)
        {
            string path = Path.Combine(currentPath, name);
            if (!IsAllowedPath(path)) { Console.WriteLine("❌ Acceso denegado."); return; }

            try
            {
                Directory.CreateDirectory(path);
                Console.WriteLine("✅ Carpeta creada.");
            }
            catch { Console.WriteLine("❌ Error al crear carpeta."); }
        }

        static void RemoveDirectory(string name)
        {
            string path = Path.Combine(currentPath, name);
            if (!IsAllowedPath(path)) { Console.WriteLine("❌ Acceso denegado."); return; }

            try
            {
                Directory.Delete(path, true);
                Console.WriteLine("🗑️ Carpeta eliminada.");
            }
            catch { Console.WriteLine("❌ Error al eliminar carpeta."); }
        }
        static void CreateFile(string name)
        {
            string path = Path.Combine(currentPath, name);
            if (!IsAllowedPath(path)) { Console.WriteLine("❌ Acceso denegado."); return; }

            try
            {
                File.WriteAllText(path, ""); // Vacío
                Console.WriteLine("✅ Archivo creado.");
            }
            catch { Console.WriteLine("❌ Error al crear archivo."); }
        }

        static void DeleteFile(string name)
        {
            string path = Path.Combine(currentPath, name);
            if (!IsAllowedPath(path)) { Console.WriteLine("❌ Acceso denegado."); return; }

            try
            {
                File.Delete(path);
                Console.WriteLine("🗑️ Archivo eliminado.");
            }
            catch { Console.WriteLine("❌ Error al eliminar archivo."); }
        }
        static void OpenFile(string name)
        {
            string path = Path.Combine(currentPath, name);
            if (!IsAllowedPath(path)) { Console.WriteLine("❌ Acceso denegado."); return; }

            try
            {
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                Console.WriteLine("📄 Archivo abierto.");
            }
            catch { Console.WriteLine("❌ No se pudo abrir el archivo."); }
        }

        static void OpenApp(string name)
        {

            try
            {
                Process.Start(new ProcessStartInfo(name) { UseShellExecute = true });
                Console.WriteLine("🚀 Aplicación lanzada.");
            }
            catch { Console.WriteLine("❌ No se pudo abrir la app."); }
        }


    }
}
