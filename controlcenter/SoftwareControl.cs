using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
#pragma warning disable
namespace controlcenter;

public partial class SoftwareControl : UserControl
{
    ListBox? userAppsList;
    ListBox? systemAppsList;

    public SoftwareControl()
    {
        InitializeComponent();
        SetupUI();
        LoadAppLists();
    }

    private void SetupUI()
    {
        this.Dock = DockStyle.Fill;
        this.BackColor = Color.White;

        // Etiqueta apps de usuario
        Label lblUserApps = new Label();
        lblUserApps.Text = (string)T("userappsinstalled")+":";
        lblUserApps.Location = new Point(20, 20);
        lblUserApps.Font = new Font("Segoe UI", 10);
        lblUserApps.AutoSize = true;

        // ListBox de apps de usuario
        userAppsList = new ListBox();
        userAppsList.Location = new Point(20, 50);
        userAppsList.Size = new Size(250, 200);

        // Botón de desinstalar
        Button btnUninstall = new Button();
        btnUninstall.Text = (string)T("uninstallapp");
        btnUninstall.Location = new Point(20, 260);
        btnUninstall.Click += BtnUninstall_Click;
        btnUninstall.Size = new Size(100, 30);

        // Etiqueta apps del sistema
        Label lblSysApps = new Label();
        lblSysApps.Text = (string)T("sysappsinstalled") + ":";
        lblSysApps.Location = new Point(300, 20);
        lblSysApps.Font = new Font("Segoe UI", 10);
        lblSysApps.AutoSize = true;

        // ListBox de apps del sistema
        systemAppsList = new ListBox();
        systemAppsList.Location = new Point(300, 50);
        systemAppsList.Size = new Size(250, 200);
        systemAppsList.Enabled = false; // solo lectura

        // Añadir a control
        this.Controls.Add(lblUserApps);
        this.Controls.Add(userAppsList);
        this.Controls.Add(btnUninstall);
        this.Controls.Add(lblSysApps);
        this.Controls.Add(systemAppsList);
    }

    private void LoadAppLists()
    {
        string softwarePath = @"C:\SOFTWARE\";
        string sysAppsPath = @"C:\DENEOS\systemApps\";

        if (Directory.Exists(softwarePath))
        {
            string[] userApps = Directory.GetDirectories(softwarePath);
            foreach (var app in userApps)
                userAppsList.Items.Add(Path.GetFileName(app));
        }

        if (Directory.Exists(sysAppsPath))
        {
            string[] systemApps = Directory.GetDirectories(sysAppsPath);
            foreach (var app in systemApps)
                systemAppsList.Items.Add(Path.GetFileName(app));
        }
    }

    private void BtnUninstall_Click(object sender, EventArgs e)
    {
        if (userAppsList.SelectedItem != null)
        {
            string appName = userAppsList.SelectedItem.ToString();
            string fullPath = Path.Combine(@"C:\SOFTWARE\", appName);

            var result = MessageBox.Show(string.Format((string)T("uninstallappdesc"), appName), (string)T("uninstallappconfirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Directory.Delete(fullPath, true);
                    userAppsList.Items.Remove(appName);
                    MessageBox.Show((string)T("appuninstalled"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{T("erruninstallingapp")}: {ex.Message}");
                }
            }
        }
    }
}
