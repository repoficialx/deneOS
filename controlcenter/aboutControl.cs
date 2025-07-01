using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using System.ComponentModel;

namespace deneOS_Home
{
    public partial class AboutControl : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static string version { get; set; }
        public AboutControl()
        {
            var x = FileVersionInfo.GetVersionInfo(@"C:\DENEOS\core\deneOS.exe");
            int[] preVersion =
            {
                x.FileMajorPart,
                x.FileMinorPart,
                x.FileBuildPart
            };
            string preVersion_ = preVersion[0].ToString()+'.'+preVersion[1].ToString()+ (preVersion[2] == 0 ? "" : (preVersion[2] == 1 ? "b" : "a"));
            version = "v" + preVersion_;
            InitializeComponent();
            LoadInfo();
        }

        private void LoadInfo()
        {
            lblTitle.Text = "deneOS";
            lblVersion.Text = $"{(string)T("ver")}: {version} ({(string)T("publicbeta")})";
            lblCopyright.Text = $"© 2025 repoficialx. {(string)T("arr")}.";
        }

        private void btnCopyInfo_Click(object sender, EventArgs e)
        {
            string userAppsPath = @"C:\SOFTWARE\";
            string systemAppsPath = @"C:\DENEOS\systemApps\";

            int userAppCount = Directory.Exists(userAppsPath) ? Directory.GetDirectories(userAppsPath).Length : 0;
            int systemAppCount = Directory.Exists(systemAppsPath) ? Directory.GetDirectories(systemAppsPath).Length : 0;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"deneOS {version}");
            sb.AppendLine($"{T("corepath")}: C:\\DENEOS\\");
            sb.AppendLine($"{T("flags")}: /dangerZone:enableRoot /safeMode");
            sb.AppendLine(string.Format("{2}: {0} {3}, {1} {4}", userAppCount, systemAppCount, T("installedfemplural"), T("usrapps"), T("usrapps") ));
            Clipboard.SetText(sb.ToString());
            MessageBox.Show((string)T("infocopiedtoclipboard"), "Copiado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnChangelog_Click(object sender, EventArgs e)
        {
            string changelogPath = @"C:\DENEOS\sysconf\changelog.md";
            if (File.Exists(changelogPath))
                Process.Start("notepad.exe", changelogPath);
            else
                MessageBox.Show((string)T("changelognotfound"), (string)T("err"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        async void btnFeedback_Click(object sender, EventArgs e)
        {
            string review = InputMessageBox.Show((string)T("comment"), (string)T("feedback"));
            await EnviarFeedbackDiscord(review);
        }

        async Task EnviarFeedbackDiscord(string mensaje)
        {
            // EmergencyUI
            var _psi = new ProcessStartInfo
            {
                FileName = "taskkill",
                Arguments = "/f /im deneOS.exe",
                UseShellExecute = true,
                Verb = "runas",
                CreateNoWindow = true
            };
            var _ = new Process();
            _.StartInfo = _psi;
            _.Start();
            _.WaitForExit(); // Espera a que el proceso se cierre
            var psi = new ProcessStartInfo
            {
                FileName = @"C:\DENEOS\core\deneOS.exe",
                Arguments = "/emergencyUI",
                UseShellExecute = true,
                Verb = "runas"
            };

            Process.Start(psi);
            var payload = new
            {
                username = "deneOS Feedback Hub",
                avatar_url = "https://repoficialx.xyz/denelogo.png",
                content = "**📬 Nuevo feedback desde deneOS**",
                embeds = new[]
                {
                    new {
                        title = "📝 Comentario",
                        description = mensaje, // input del usuario
                        color = 3447003, // azul
                        fields = new[] {
                            new { name = "Usuario", value = Environment.UserName, inline = true },
                            new { name = "Categoría", value = /*categoriaSeleccionada*/"No Implementado", inline = true },
                            new { name = "Versión", value = "v0.1b", inline = true },
                            new { name = "Modo root", value = /*flagMgmt.EnableRoot.ToString()*/ "No Implementado", inline = true }
                        },
                        footer = new {
                            text = DateTime.Now.ToString("g") // ej. 16/06/2025 12:34
                        }
                    }
                }
            };


            var json = JsonSerializer.Serialize(payload); 
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            await client.PostAsync("https://discord.com/api/webhooks/1385960884513411102/BJ-rIZIpzijQafXUx484K5ZepWxY4FbiHZqugcwKg_NzP6aUEWR_9Q6uQsX78mzATGom", httpContent);
        }

        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblVersion;
        private Label lblCopyright;
        private PictureBox logo;
        private Button btnCopyInfo;
        private Button btnChangelog;
        private Button btnFeedback;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblVersion = new Label();
            this.lblCopyright = new Label();
            this.logo = new PictureBox();
            this.btnCopyInfo = new Button();
            this.btnChangelog = new Button();
            this.btnFeedback = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();

            // Logo
            this.logo.Location = new Point(20, 20);
            this.logo.Size = new Size(64, 64);
            this.logo.SizeMode = PictureBoxSizeMode.Zoom;
            this.logo.Image = controlcenter.Properties.Resources.icons8_about_me_100; // Asegúrate de tener el recurso

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(100, 25);
            this.lblTitle.Text = "deneOS";

            // lblVersion
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new Font("Segoe UI", 10F);
            this.lblVersion.Location = new Point(100, 60);

            // Copyright
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new Point(20, 100);
            this.lblCopyright.Size = new Size(300, 15);

            // btnCopyInfo
            this.btnCopyInfo.Text = (string)T("copysysinfo");
            this.btnCopyInfo.Location = new Point(20, 130);
            this.btnCopyInfo.Click += new EventHandler(this.btnCopyInfo_Click);
            this.btnCopyInfo.AutoSize = true;

            // btnChangelog
            this.btnChangelog.Text = (string)T("viewchangelog");
            this.btnChangelog.Location = new Point(20, 170);
            this.btnChangelog.Click += new EventHandler(this.btnChangelog_Click);
            this.btnChangelog.AutoSize = true;

            // btnFeedback
            this.btnFeedback.Text = (string)T("sendfeedback");
            this.btnFeedback.Location = new Point(20, 210);
            this.btnFeedback.Click += new EventHandler(this.btnFeedback_Click);
            this.btnFeedback.AutoSize = true;

            // AboutControl
            this.Controls.Add(this.logo);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.btnCopyInfo);
            this.Controls.Add(this.btnChangelog);
            this.Controls.Add(this.btnFeedback);
            this.Size = new Size(400, 260);

            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
