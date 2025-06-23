using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Shell;
using MessageBox = System.Windows.Forms.MessageBox;
#pragma warning disable CS8618 // Para evitar advertencias de propiedades no inicializadas
#pragma warning disable IDE0044 // Para evitar advertencias de campos no utilizados
namespace DPKXT
{
    public partial class Form1 : Form
    {
        private static Meta metaInfo;
        private static string tempPath;
        private static string rutaDPK;
        public static string appFolder; // ruta donde se descomprimió el .dpk
        private static string dpkPath;

        public Form1(string? rutaDpk)
        {
            InitializeComponent();
            dpkPath = rutaDpk ?? ""; // o lanza un error si quieres obligar a tener ruta
        }


        public Form1()
        {
            InitializeComponent();

            // Simula apertura de un .dpk
            string rutaDPK = "./denenotes.dpk";
            if (File.Exists(rutaDPK))
            {
                MostrarMetaDesdeDPK(rutaDPK);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(dpkPath) || !File.Exists(dpkPath))
            {
                MessageBox.Show("No se especificó un archivo .dpk válido.");
                Close();
                return;
            }

            // Carpeta temporal donde se extraerá el .dpk
            appFolder = Path.Combine(Path.GetTempPath(), "deneOS_" + Path.GetFileNameWithoutExtension(dpkPath));

            try
            {
                if (Directory.Exists(appFolder))
                    Directory.Delete(appFolder, true); // limpia si ya existe

                ZipFile.ExtractToDirectory(dpkPath, appFolder);
                MessageBox.Show("Archivo .dpk descomprimido correctamente");

                // Ahora puedes leer el manifest.json y mostrar info
                string manifestPath = Path.Combine(appFolder, "meta", "manifest.json");
                if (File.Exists(manifestPath))
                {
                    string json = File.ReadAllText(manifestPath);
                    var metaInfo = JsonSerializer.Deserialize<Meta>(json) ?? new Meta { name = "", author = "", version = "" };
                    txtName.Text = metaInfo.name;
                    txtVersion.Text = metaInfo.version;
                    txtAuthor.Text = metaInfo.author;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar el archivo .dpk: " + ex.Message);
                Close();
            }
        }

        private void MostrarMetaDesdeDPK(string ruta)
        {
            // Suponiendo estructura: .dpk = carpeta zip con /meta/manifest.json
            try
            {
                string tempPath = Path.Combine(Path.GetTempPath(), "./dpkxt_temp");
                if (Directory.Exists(tempPath)) Directory.Delete(tempPath, true);
                System.IO.Compression.ZipFile.ExtractToDirectory(ruta, tempPath);

                string manifestPath = Path.Combine(tempPath, "meta", "manifest.json");
                if (File.Exists(manifestPath))
                {
                    string json = File.ReadAllText(manifestPath);
                    metaInfo = JsonSerializer.Deserialize<Meta>(json) ?? new Meta { name = "", author = "", version = "" };

                    txtName.Text = metaInfo.name;
                    txtVersion.Text = metaInfo.version;
                    txtAuthor.Text = metaInfo.author;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error leyendo DPK: " + ex.Message);
            }
        }
        private void btnInstalar_Click(object sender, EventArgs e)
        {
            InstalarDPK(dpkPath, out string name, out string version, out string author, showGUI: true);
        }

        public static void InstalarDPK(string dpk)
        {
            new Form1().InstalarDPK(dpk, out string name, out string version, out string author, showGUI: false);
        }

        public void InstalarDPK(string dpk, out string name, out string version, out string author, bool showGUI = false)
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
                DialogResult? _ = showGUI ? MessageBox.Show("Archivo .dpk descomprimido correctamente") : null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al descomprimir el archivo .dpk: " + ex.Message);
                version = "";
                name = "";
                author = "";
                return;
            }
            // Separar la carpeta meta y el resto de archivos
            string metaPath = Path.Combine(tempPath, "meta");
            if (!Directory.Exists(metaPath))
            {
                MessageBox.Show("No se encontró la carpeta 'meta' en el archivo .dpk.");
                version = "";
                name = "";
                author = "";
                return;
            }
            // Leer manifest.json y usar JsonSerializer para obtener la información de la aplicación con la clase Meta
            string manifest = Path.Combine(metaPath, "manifest.json");
            if (!File.Exists(manifest))
            {
                MessageBox.Show("No se encontró el archivo 'manifest.json' en la carpeta 'meta'.");
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
                MessageBox.Show("Error al deserializar el JSON: " + ex.Message);
                version = "";
                name = "";
                author = "";
                return;
            }
            // si showGUI es true, asignar a version, name y author los valores del json
            if (showGUI)
            {
                name = metaInfo.name;
                version = metaInfo.version;
                author = metaInfo.author;
                this.txtName.Text = name;
                txtVersion.Text = version;
                txtAuthor.Text = author;
            }
            else
            {
                // Si no se muestra GUI, retornar los valores directamente
                name = metaInfo.name;
                version = metaInfo.version;
                author = metaInfo.author;
            }
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
                DialogResult? _ = showGUI ? MessageBox.Show("Instalación completada correctamente.") : null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al instalar: " + ex.Message);
            }
            // Limpiar carpeta temporal
            try
            {
                Directory.Delete(tempPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar la carpeta temporal: " + ex.Message);
            }
            // Si se ha llegado hasta aquí, la instalación fue exitosa
            if (showGUI)
            {
                MessageBox.Show("Instalación limpiada correctamente.");
            }
        }

        public class Meta
        {
            public string name { get; set; }
            public string version { get; set; }
            public string author { get; set; }
        }
    }
}
