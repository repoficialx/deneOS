using System;
using System.IO.Compression;
using System.Text.Json;
using System.Windows;
using static System.Console;
using static System.Net.Mime.MediaTypeNames;

namespace DPKXT.Console;

internal static class Program
{
    private static Meta metaInfo;
    private static string tempPath;
    private static string rutaDPK;
    public static string appFolder; // ruta donde se descomprimió el .dpk
    private static string dpkPath;

    [STAThread]
    static void Main(string[] args)
    {
        string? rutaDpk = args.Length > 0 ? args[0] : null;
        if (args.Length == 0 || string.IsNullOrEmpty(rutaDpk) || !System.IO.File.Exists(rutaDpk))
        {
            WriteLine("[ERROR] Debe proporcionar la ruta de un archivo .dpk válido.");
            Environment.Exit(-1);
        }

        WriteLine("Modo consola activado (instalación silenciosa)");
        rutaDPK = rutaDpk;
        InstalarDPK(args[0], out _, out _, out _);
        Environment.Exit(0);
    }

    public static void InstalarDPK(string dpk, out string name, out string version, out string author)
    {

        string[][] estructura =
        [
            [dpk, "{0}"],
                [dpk, "app.exe"],
                [dpk, "meta", "manifest.json"],
                [dpk, "meta", "manifest.json", "name"],
                [dpk, "meta", "manifest.json", "version"],
                [dpk, "meta", "manifest.json", "author"]
        ];

        // Primero: Extraer el .dpk a una carpeta temporal y separar la carpeta \meta y el resto de archivos
        string tempPath = Path.Combine(Path.GetTempPath(), "dpkxt_temp");
        if (Directory.Exists(tempPath))
        {
            Directory.Delete(tempPath, true); // limpia si ya existe
        }
        try
        {
            ZipFile.ExtractToDirectory(dpk, tempPath);
            WriteLine("Archivo .dpk descomprimido correctamente");
        }
        catch (Exception ex)
        {
            WriteLine("Error al descomprimir el archivo .dpk: " + ex.Message);
            version = "";
            name = "";
            author = "";
            return;
        }
        // Separar la carpeta meta y el resto de archivos
        string metaPath = Path.Combine(tempPath, "meta");
        if (!Directory.Exists(metaPath))
        {
            WriteLine("No se encontró la carpeta 'meta' en el archivo .dpk.");
            version = "";
            name = "";
            author = "";
            return;
        }
        // Leer manifest.json y usar JsonSerializer para obtener la información de la aplicación con la clase Meta
        string manifest = Path.Combine(metaPath, "manifest.json");
        if (!File.Exists(manifest))
        {
            WriteLine("No se encontró el archivo 'manifest.json' en la carpeta 'meta'.");
            version = "";
            name = "";
            author = "";
            return;
        }
        string jsonContent = File.ReadAllText(manifest);
        try
        {
            metaInfo = JsonSerializer.Deserialize<Meta>(jsonContent) ?? new Meta { name = "", author = "", version = "" };
        }
        catch (JsonException ex)
        {
            WriteLine("Error al deserializar el JSON: " + ex.Message);
            version = "";
            name = "";
            author = "";
            return;
        }
            name = metaInfo.name;
            version = metaInfo.version;
            author = metaInfo.author;
            WriteLine("Name: " + name);
            WriteLine("Version: " + version);
            WriteLine("Author: " + author);
        // Extraer el resto de archivos a una C:\SOFTWARE\(name)
        string destino = Path.Combine(@"C:\SOFTWARE", name);
        if (!Directory.Exists(destino))
        {
            Directory.CreateDirectory(destino);
        }
        else
        {
            Directory.Delete(destino, true); // Limpiar si ya existe
            Directory.CreateDirectory(destino); // Re-crear la carpeta
        }
        try
        {
            // Extraer todo excepto la carpeta meta
            foreach (string file in Directory.GetFiles(tempPath, "*", SearchOption.AllDirectories))
            {
                if (!file.StartsWith(metaPath))
                {
                    string destFile = Path.Combine(destino, Path.GetFileName(file));
                    File.Copy(file, destFile, true);
                }
            }
            WriteLine("Instalación completada correctamente.");
        }
        catch (Exception ex)
        {
            WriteLine("Error al instalar: " + ex.Message);
        }
        // Limpiar carpeta temporal
        try
        {
            Directory.Delete(tempPath, true);
        }
        catch (Exception ex)
        {
            WriteLine("Error al limpiar la carpeta temporal: " + ex.Message);
        }
        // Si se ha llegado hasta aquí, la instalación fue exitosa
                WriteLine("Instalación limpiada correctamente.");
    }

    public class Meta
    {
        public string name { get; set; }
        public string version { get; set; }
        public string author { get; set; }
    }

}
