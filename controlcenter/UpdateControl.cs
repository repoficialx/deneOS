using controlcenter;
using System.Diagnostics;
using System.Text.Json;
#pragma warning disable
public partial class UpdateControl : UserControl
{
    private string currentVersion = "0.0";
    private string latestVersion = "0.0";
    private string changelog = "";
    private string downloadUrl = "";
    private Label lblCurrentVersion;
    private Label lblLatestVersion;
    private RichTextBox txtChangelog;
    private Button btnCheckUpdates;
    private Button btnInstallUpdate;
    private Label lblUpdateStatus;
    private PictureBox pbUpdateStatus;

    public UpdateControl()
    {
        InitializeComponent();
        GetCurrentVersion();
    }
    

    private void InitializeComponent()
    {
        this.lblCurrentVersion = new Label();
        this.lblLatestVersion = new Label();
        this.txtChangelog = new RichTextBox();
        this.btnCheckUpdates = new Button();
        this.btnInstallUpdate = new Button();
        this.lblUpdateStatus = new Label();
        this.pbUpdateStatus = new PictureBox();

        // UpdateControl
        this.Size = new Size(99950, 405540);
        this.BackColor = Color.White;

        // lblCurrentVersion
        this.lblCurrentVersion.Text = $"{T("actualVersion")}: ?";
        this.lblCurrentVersion.Location = new Point(20, 20);
        this.lblCurrentVersion.AutoSize = true;

        // lblLatestVersion
        this.lblLatestVersion.Text = $"{T("latestVersion")}: ?";
        this.lblLatestVersion.Location = new Point(20, 50);
        this.lblLatestVersion.AutoSize = true;

        // pbUpdateStatus
        this.pbUpdateStatus.Size = new Size(24, 24);
        this.pbUpdateStatus.Location = new Point(20, 80);
        this.pbUpdateStatus.SizeMode = PictureBoxSizeMode.Zoom;
        //this.pbUpdateStatus.Image = controlcenter.Properties.Resources.icons8_info; 

        // lblUpdateStatus
        //this.lblUpdateStatus.Text = "Estado de la actualización";
        this.lblUpdateStatus.Location = new Point(55, 84);
        this.lblUpdateStatus.AutoSize = true;

        // txtChangelog
        this.txtChangelog.Location = new Point(20, 120);
        this.txtChangelog.Size = new Size(440, 180);
        this.txtChangelog.ReadOnly = true;
        this.txtChangelog.BackColor = Color.White;
        this.txtChangelog.BorderStyle = BorderStyle.FixedSingle;

        // btnCheckUpdates
        this.btnCheckUpdates.Text = (string)T("searchForUpdates");
        this.btnCheckUpdates.Location = new Point(20, 320);
        this.btnCheckUpdates.Size = new Size(200, 30);
        this.btnCheckUpdates.Click += new EventHandler(this.btnCheckUpdates_Click);

        // btnInstallUpdate
        this.btnInstallUpdate.Text = (string)T("installUpdate");
        this.btnInstallUpdate.Location = new Point(260, 320);
        this.btnInstallUpdate.Size = new Size(200, 30);
        this.btnInstallUpdate.Enabled = false;
        this.btnInstallUpdate.Click += new EventHandler(this.btnInstallUpdate_Click);

        // Add controls
        this.Controls.Add(this.lblCurrentVersion);
        this.Controls.Add(this.lblLatestVersion);
        this.Controls.Add(this.pbUpdateStatus);
        this.Controls.Add(this.lblUpdateStatus);
        this.Controls.Add(this.txtChangelog);
        this.Controls.Add(this.btnCheckUpdates);
        this.Controls.Add(this.btnInstallUpdate);
    }

    private void GetCurrentVersion()
    {
        var x = FileVersionInfo.GetVersionInfo(@"C:\DENEOS\core\deneOS.exe");
        int[] preVersion = { x.FileMajorPart, x.FileMinorPart, x.FileBuildPart };
        string tag = preVersion[2] == 0 ? "" : (preVersion[2] == 1 ? "b" : "a");
        currentVersion = $"{preVersion[0]}.{preVersion[1]}{tag}";
        lblCurrentVersion.Text = (string)T("actualVersion") + ": " + currentVersion;
    }

    private async void btnCheckUpdates_Click(object sender, EventArgs e)
    {
        using HttpClient client = new HttpClient();
        try
        {
            string json = await client.GetStringAsync("https://repoficialx.xyz/deneOS/api/versions.json");
            var info = JsonSerializer.Deserialize<UpdateInfo>(json);

            if (info != null)
            {
                latestVersion = info.latestVersion;
                changelog = info.changelog;
                downloadUrl = info.download;
                
                if (latestVersion != currentVersion)
                {
                    SetUpdateStatus(string.Format("⚠️ {0}", T("availableUpdate")), controlcenter.Properties.Resources.icons8_medium_risk_100);
                }
                else
                {
                    if (latestVersion.EndsWith('b')||latestVersion.EndsWith('a'))
                    {
                        SetUpdateStatus(string.Format("🧪 {0}", T("previewInstalled")), controlcenter.Properties.Resources.icons8_info);
                    }
                    else
                    {
                        SetUpdateStatus(string.Format("✅ {0}", T("systemUpdated")), controlcenter.Properties.Resources.icons8_checkmark);
                    }
                }
                    lblLatestVersion.Text = (string)T("latestVersion") + ": " + latestVersion;
                txtChangelog.Text = changelog;

                btnInstallUpdate.Enabled = latestVersion != currentVersion;
            }
        }
        catch
        {
            MessageBox.Show((string)T("errCouldntgetUpdateInformation"), (string)T("err"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    void SetUpdateStatus(string texto, Image icono)
    {
        lblUpdateStatus.Text = texto;
        pbUpdateStatus.Image = icono;

    }

    private void btnInstallUpdate_Click(object sender, EventArgs e)
    {
        //Process.Start("deneOS.exe", "/installUpdate:" + downloadUrl);
        new UpdateScreen().Show();
        Application.Exit();
    }

    private class UpdateInfo
    {
        public string latestVersion { get; set; }
        public string download { get; set; }
        public string changelog { get; set; }
    }
}
