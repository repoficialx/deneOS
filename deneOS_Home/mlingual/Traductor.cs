using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class Traductor
{
    private static Dictionary<string, string> traducciones = new Dictionary<string, string>();

    public static void Cargar(string idioma)
    {
        string ruta = Path.Combine(System.Windows.Forms.Application.StartupPath, "lang", $"{idioma}.json");

        if (File.Exists(ruta))
        {
            string json = File.ReadAllText(ruta);
            traducciones = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
        else
        {
            traducciones = new Dictionary<string, string>(); // vacío si no existe  
        }
    }

    public static string T(string clave)
    {
        return traducciones.TryGetValue(clave, out var valor) ? valor : $"[{clave}]";
    }
}
