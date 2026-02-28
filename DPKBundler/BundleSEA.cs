using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DPKBundler
{
    public partial class BundleSEA : Form
    {
        public BundleSEA()
        {
            InitializeComponent();
        }

        private void BundleSEA_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists(textBox1.Text))
            {
                MessageBox.Show("The executable file does not exist. Choose it again.");
                return;
            }

            textBox1.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            maskedTextBox1.Enabled = true;
            button3.Enabled = true;
        }

        private string name;
        private string version;
        private string author;
        private void button3_Click(object sender, EventArgs e)
        {
            string name = textBox2.Text;
            string version = maskedTextBox1.Text;
            string author = textBox3.Text;
            textBox2.Enabled = false;
            maskedTextBox1.Enabled = false;
            textBox3.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = true;
            this.name = name;
            this.version = version;
            this.author = author;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        class manifestJson
        {
            public string name { get; set; }
            public string version { get; set; }
            public string author { get; set; }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string sourceExeFilePath = textBox1.Text;
            string tempDir = Path.GetTempPath();
            string tempFolder = Path.Combine(tempDir, "dpkbundler_" + Guid.NewGuid(), "src");
            Directory.CreateDirectory(tempFolder);
            FileInfo fi = new FileInfo(sourceExeFilePath);
            File.Copy(sourceExeFilePath, Path.Combine(tempFolder, fi.Name));
            Directory.CreateDirectory(Path.Combine(tempFolder, "meta"));
            string meta = JsonSerializer.Serialize(new manifestJson { name = name, version = version, author = author },
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(Path.Combine(tempFolder, "meta", "manifest.json"), meta);
            string destZipFilePath = saveFileDialog1.FileName;
            ZipFile.CreateFromDirectory(tempFolder, destZipFilePath);
            if (Path.ChangeExtension(destZipFilePath, ".dpk") != destZipFilePath)
            {
                File.Move(destZipFilePath, Path.ChangeExtension(destZipFilePath, ".dpk"));
            }

            DirectoryInfo di = new DirectoryInfo(tempFolder);
            Directory.Delete(di.Parent.FullName, true);
        }
    }
}