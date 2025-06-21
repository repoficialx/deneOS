using System;
using System.Windows.Forms;
using Microsoft.Win32;

public partial class InicioControl : UserControl
{
#pragma warning disable CS8618
#pragma warning disable CS8602
#pragma warning disable CS8622
    public InicioControl()
    {
        InitializeComponent();
        Dock = DockStyle.Fill;
        UpdateTime();
    }
    private Label labelHora;
    private Label labelFecha;
    private Button btnEstablecerShell;
    private System.Windows.Forms.Timer updateTime;
    private void InitializeComponent()
    {
        updateTime = new();
        updateTime.Interval = 1000; // Actualizar cada segundo
        updateTime.Tick += (s, e) => UpdateTime();
        updateTime.Start();

        labelHora = new Label();
        labelFecha = new Label();
        btnEstablecerShell = new Button();

        // labelHora
        labelHora.Font = new Font("Segoe UI", 32F);
        labelHora.Location = new Point(20, 20);
        labelHora.Size = new Size(500, 60);

        // labelFecha
        labelFecha.Font = new Font("Segoe UI", 14F);
        labelFecha.Location = new Point(20, 90);
        labelFecha.Size = new Size(500, 30);

        // btnEstablecerShell
        btnEstablecerShell.Text = "Establecer deneOS como Shell";
        btnEstablecerShell.Location = new Point(20, 150);
        btnEstablecerShell.Size = new Size(300, 40);

        btnEstablecerShell.Click += btnEstablecerShell_Click;

        // Agregar controles
        Controls.Add(labelHora);
        Controls.Add(labelFecha);
        Controls.Add(btnEstablecerShell);
    }
    private void UpdateTime()
    {
        labelHora.Text = DateTime.Now.ToString("HH:mm:ss");
        labelFecha.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
    }

    private void btnEstablecerShell_Click(object sender, EventArgs e)
    {
        try
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(
                @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);

 
            key.SetValue("Shell", Application.ExecutablePath);
            key.Close();

            MessageBox.Show("deneOS ha sido establecido como shell del sistema. Reinicia para aplicar los cambios.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message, "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
