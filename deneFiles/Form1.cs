using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneFiles
{
    public partial class Form1 : Form
    {
        public enum FolderType
        {
            SOFTWARE,
            DNUSR
        }

        string currentPath;
        Stack<string> backHistory = new();
        Stack<string> forwardHistory = new();
        bool enableRoot;

        public Form1(bool enableRoot = false, FolderType startupFolder = FolderType.DNUSR)
        {
            InitializeComponent();
            this.enableRoot = enableRoot;

            LoadIcons();
            InitTreeView();

            string startPath = startupFolder == FolderType.SOFTWARE
                ? @"C:\SOFTWARE"
                : @"C:\DNUSR";

            if (enableRoot)
            {
                BackColor = Color.FromArgb(255, 240, 230);
                MessageBox.Show(
                    (string)dosu.MUI.T("rootmodewarning"),
                    (string)dosu.MUI.T("dzne"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                startPath = @"C:\";
            }

            NavigateTo(startPath);

            backButton.Click += (_, _) => GoBack();
            forwardButton.Click += (_, _) => GoForward();
            listView1.ItemActivate += ListView_ItemActivate;
        }

        void LoadIcons()
        {
            imageList1.ImageSize = new Size(32, 32);
            imageList1.Images.Add("folder", Properties.Resources.folder);
            imageList1.Images.Add("file", Properties.Resources.file);
            imageList1.Images.Add("exe", Properties.Resources.exe);
            imageList1.Images.Add("doc", Properties.Resources.doc);
        }

        void InitTreeView()
        {
            treeView1.Nodes.Clear();

            TreeNode root = new TreeNode("Este PC");
            treeView1.Nodes.Add(root);

            AddNode(root, @"C:\DNUSR");
            AddNode(root, @"C:\SOFTWARE");

            if (enableRoot)
                AddNode(root, @"C:\");

            treeView1.AfterSelect += (s, e) =>
            {
                if (e.Node?.Tag is string path && Directory.Exists(path))
                    NavigateTo(path);
            };
        }

        void AddNode(TreeNode parent, string path)
        {
            if (!Directory.Exists(path)) return;
            TreeNode node = new TreeNode(Path.GetFileName(path));
            node.Tag = path;
            parent.Nodes.Add(node);
        }

        void NavigateTo(string path)
        {
            if (!Directory.Exists(path)) return;

            if (currentPath != null)
                backHistory.Push(currentPath);

            forwardHistory.Clear();
            LoadDirectory(path);
        }

        void LoadDirectory(string path)
        {
            listView1.Items.Clear();
            currentPath = path;
            pathLabel.Text = denePathParser.denePathParser.Parse(path);

            try
            {
                foreach (var dir in Directory.GetDirectories(path))
                {
                    DirectoryInfo d = new(dir);
                    ListViewItem item = new(d.Name, "folder");
                    item.Tag = d;
                    item.SubItems.Add("Carpeta");
                    item.SubItems.Add("");
                    item.SubItems.Add(d.LastWriteTime.ToString());
                    listView1.Items.Add(item);
                }

                foreach (var file in Directory.GetFiles(path))
                {
                    FileInfo f = new(file);
                    string icon = f.Extension switch
                    {
                        ".exe" => "exe",
                        ".txt" => "doc",
                        _ => "file"
                    };

                    ListViewItem item = new(f.Name, icon);
                    item.Tag = f;
                    item.SubItems.Add(f.Extension);
                    item.SubItems.Add($"{f.Length / 1024} KB");
                    item.SubItems.Add(f.LastWriteTime.ToString());
                    listView1.Items.Add(item);
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show((string)dosu.MUI.T("accessdenied"));
            }
        }

        void ListView_ItemActivate(object sender, EventArgs e)
        {
            var item = listView1.SelectedItems[0];

            if (item.Tag is DirectoryInfo d)
                NavigateTo(d.FullName);
            else if (item.Tag is FileInfo f)
                System.Diagnostics.Process.Start(
                    new System.Diagnostics.ProcessStartInfo(f.FullName) { UseShellExecute = true }
                );
        }

        void GoBack()
        {
            if (backHistory.Count == 0) return;
            forwardHistory.Push(currentPath);
            LoadDirectory(backHistory.Pop());
        }

        void GoForward()
        {
            if (forwardHistory.Count == 0) return;
            backHistory.Push(currentPath);
            LoadDirectory(forwardHistory.Pop());
        }


        /*
        public Form1(bool enableRoot = false, FolderType startupFolder = FolderType.DNUSR)
        {
            InitializeComponent();
            // Si enableRoot es true, se inicia en file:///C:/
            if (enableRoot)
            {
                webBrowser1.Navigate("file:///C:/");
                //añadir un mensaje de advertencia al usuario
                MessageBox.Show("¡Atención! Estás navegando en modo raíz. Ten cuidado con los archivos del sistema.", "Modo Raíz", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //añadir al menuStrip un item que permita ir a la carpeta raíz (C:\)
                ToolStripMenuItem rootMenuItem = new ToolStripMenuItem("Ir a la raíz (C:\\)");
                rootMenuItem.Click += (s, e) => webBrowser1.Navigate("file:///C:/");
                rootMenuItem.Image = Properties.Resources.icons8_program_100;
                this.Size = new Size(this.Size.Width + 100, this.Size.Height);
                menuStrip1.Items.Add(rootMenuItem);
                this.BackColor = Color.FromArgb(255, 240, 230);
            }
            else
            {
                // Si se especifica una carpeta de inicio, se navega a ella
                switch (startupFolder)
                {
                    case FolderType.SOFTWARE:
                        webBrowser1.Navigate("file:///C:/SOFTWARE/");
                        break;
                    case FolderType.DNUSR:
                        webBrowser1.Navigate("file:///C:/DNUSR/");
                        break;
                    default:
                        webBrowser1.Navigate("file:///C:/DNUSR");
                        break;
                }
            }
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void forwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void deneossysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("c:\\SOFTWARE\\");
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("c:\\DNUSR\\");
        }

        private void parentDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string currentUrl = webBrowser1.Url.ToString();
            int lastSlash = currentUrl.LastIndexOf('/');

            if (lastSlash > "file:///C:/".Length) // No te salgas del root
            {
                string parentUrl = currentUrl.Substring(0, lastSlash);
                webBrowser1.Navigate(parentUrl);
            }


        }*/
    }
}
