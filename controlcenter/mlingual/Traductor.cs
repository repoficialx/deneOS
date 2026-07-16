using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Text.Json;
using System.Windows.Forms;
using Windows.Devices.PointOfService;
using Windows.Media.Protection.PlayReady;
using controlcenter;

#pragma warning disable
public static class Traductor
{
    private static Dictionary<string, string> traducciones = new Dictionary<string, string>();
    public static bool UN_ST = false;

    static bool isConnected()
    {
        Ping ping = new Ping();
        try
        {
            PingReply reply = ping.Send("8.8.8.8", 1000);
            if (reply.Status == IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }
    private static readonly HttpClient client = new HttpClient();
    public async static void Cargar(string idioma)
    {
        UN_ST = false;
        string ruta = Path.Combine(@"C:\DENEOS\", "lang", $"{idioma}.json");
        //MessageBox.Show($"Existe el archivo de idioma? {File.Exists(ruta)}.");

        if (File.Exists(ruta))
        {
            string json = File.ReadAllText(ruta);

            if (isConnected())
            {
                try
                {
                    string server = "https://repoficialx.xyz/deneOS/api/";
                    string jsonp = server + idioma + ".json";
                    

                    string ojson = await client.GetStringAsync(jsonp);
                    //MessageBox.Show("Server recibido");

                    if (json != ojson)
                    {
                        //MessageBox.Show("Actualización disponible");
                        File.WriteAllText(ruta, ojson);
                        json = ojson;
                        //MessageBox.Show("Actualizado");
                    }
                    else
                    {
                        //MessageBox.Show("Ya actualizado");
                    }
                }
                catch
                {
                    // mantener traducción local
                }
            }

            //MessageBox.Show(json);
            
            traducciones = JsonSerializer.Deserialize<Dictionary<string, string>>(json);


        }
        else
        {
            //MessageBox.Show("no hay trads");
            traducciones = new Dictionary<string, string>(); // vacío si no existe  
        }
        global.isReady = true;
    }
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

        return $"[{clave}]"; // Clave no encontrada
    }

}
