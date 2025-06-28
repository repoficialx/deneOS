using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Windows.Forms;

public static class Traductor
{
    private static Dictionary<string, string> traducciones = new Dictionary<string, string>();
    public static bool UN_ST = false;
    public static void Cargar(string idioma)
    {
        UN_ST = false;
        string ruta = Path.Combine(@"C:\DENEOS\", "lang", $"{idioma}.json");
        string lang = idioma;
        string localPath = $@"C:\DENEOS\lang\{lang}.json";



        string remoteUrl = $"https://repoficialx.xyz/deneOS/api/{lang}.json";

        // Descarga desde internet si hay diferencias
        if (NeedsUpdate(localPath, remoteUrl))
        {
            File.WriteAllText(localPath, new WebClient().DownloadString(remoteUrl));
            MessageBox.Show("🔄 Traducciones actualizadas correctamente.", "Actualización de idioma");
        }

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
    static bool NeedsUpdate(string localPath, string remoteUrl)
    {
        string remoteContent = null;
        using (WebClient wc = new WebClient())
        {
            remoteContent = wc.DownloadString(remoteUrl);
        }
        var localContent = File.ReadAllText(localPath);
        return remoteContent != localContent;
    }

    /*public static string T(string clave)
    {
        return traducciones.TryGetValue(clave, out var valor) ? valor : $"[{clave}]";
    }*/
    public static object T(string clave)
    {
        if (UN_ST)
        {
            return clave;
        }
        if (traducciones.TryGetValue(clave, out var valor))
        {
            // Intentar interpretar el valor como booleano
            if (bool.TryParse(valor, out bool boolResult))
            {
                return boolResult;
            }

            // Intentar interpretar el valor como número (opcional)
            if (int.TryParse(valor, out int intResult))
            {
                return intResult;
            }

            // Si no es booleano ni número, devolver como cadena
            return valor;
        }
        //MessageBox.Show($"nº trads: {traducciones.Count}, estado sisfct: {traducciones.ContainsKey("sisfct")}");

        return $"[{clave}]"; // Clave no encontrada
    }

}
