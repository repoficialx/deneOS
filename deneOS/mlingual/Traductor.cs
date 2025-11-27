using deneOS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.Json;
using System.Windows.Forms;


public static class Traductor
{
    private static Dictionary<string, object> traducciones = new Dictionary<string, object>();
    public static bool UN_ST = false;
    public static void Cargar(string idioma)
    {
        UN_ST = false;
        string ruta = Path.Combine(@"C:\DENEOS\", "lang", $"{idioma}.json");

        Console.WriteLine($"[INFO] Intentando cargar idioma desde: {ruta}");

        if (File.Exists(ruta))
        {
            try
            {
                string json = File.ReadAllText(ruta);

                // ✅ Deserializar como Dictionary<string, object>
                traducciones = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

                Console.WriteLine($"[SUCCESS] {traducciones.Count} traducciones cargadas");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Error parseando JSON: {ex.Message}");
                traducciones = new Dictionary<string, object>();
            }
        }
        else
        {
            Console.WriteLine($"[WARN] Archivo de idioma no encontrado: {ruta}");
            traducciones = new Dictionary<string, object>();
        }
    }
    static bool NeedsUpdate(string localPath, string remoteUrl)
    {
        try
        {
            Ping ping = new();
            IPAddress google = new([142, 250, 184, 14]);
            var reply = ping.Send(google);
            if (!(reply.Status == IPStatus.Success))
            {
                Console.WriteLine("[ERROR] No Internet Connection.", "deneOS", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: No Internet Connection.", "deneOS", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return false;
        }

        string remoteContent = null;
        using (HttpClient client = new())
        {
            remoteContent = client.GetStringAsync(remoteUrl).GetAwaiter().GetResult();
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
            // ✅ Manejar diferentes tipos de JSON
            if (valor is JsonElement jsonElement)
            {
                switch (jsonElement.ValueKind)
                {
                    case JsonValueKind.True:
                        return true;
                    case JsonValueKind.False:
                        return false;
                    case JsonValueKind.Number:
                        if (jsonElement.TryGetInt32(out int intValue))
                            return intValue;
                        if (jsonElement.TryGetDouble(out double doubleValue))
                            return doubleValue;
                        return valor;
                    case JsonValueKind.String:
                        return jsonElement.GetString();
                    default:
                        return jsonElement.ToString();
                }
            }

            // Si ya es un tipo primitivo (bool, int, string)
            return valor;
        }

        return $"[{clave}]"; // Clave no encontrada
    }

}
