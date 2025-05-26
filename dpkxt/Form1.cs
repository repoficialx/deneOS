using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Shell;

namespace DPKXT
{
    public partial class Form1 : Form
    {
        private Meta metaInfo;
        private string tempPath;
        private string rutaDPK;
        string appFolder; // ruta donde se descomprimió el .dpk
        private string dpkPath;

        public Form1(string rutaDpk)
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
                    var metaInfo = JsonSerializer.Deserialize<Meta>(json);
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
                    metaInfo = JsonSerializer.Deserialize<Meta>(json);

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
            try
            {
                MessageBox.Show("Paso 1: Validando carpeta destino");

                string nombreApp = txtName.Text;
                string destino = Path.Combine(@"C:\SOFTWARE", nombreApp);

                MessageBox.Show($"Paso 2: Ruta de destino: {destino}");

                if (!Directory.Exists(destino))
                {
                    Directory.CreateDirectory(destino);
                    MessageBox.Show("Paso 3: Carpeta creada");
                }

                MessageBox.Show("Paso 4: Extrayendo ZIP");

                string zipPath = Path.Combine(appFolder, "denenotes.zip");
                ZipFile.ExtractToDirectory(zipPath, destino);

                MessageBox.Show("Instalación completada correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al instalar: " + ex.Message + "\n" + ex.StackTrace);
            }

        }
        void debug(string msg)
        {
            Console.WriteLine(msg);
        }
    }
    
    public class Meta
    {
        public string name { get; set; }
        public string version { get; set; }
        public string author { get; set; }
    }
}
