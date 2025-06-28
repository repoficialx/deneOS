using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Traductor;

namespace deneOS
{
    public partial class sm : Form
    {
        public sm()
        {
            InitializeComponent();
        }

        private void sm_Load(object sender, EventArgs e)
        {
            int startMenuWidth = 378;
            int startMenuHeight = 513;

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            int taskbarHeight = 60;

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.Size = new Size(startMenuWidth, startMenuHeight);
            this.Location = new Point(0, screenHeight - taskbarHeight - startMenuHeight);
            this.BackColor = globaldata.isImageLoaded ? ColorTranslator.FromHtml(globaldata.wallpaperPredominantColorHex) : Color.Black;

            panel2.BringToFront();

            rec.Text = (string)T("dApp");
            this.rec.Location = new System.Drawing.Point(180, 21);
            if (System.IO.File.Exists(@"C:\SOFTWARE\Recip\recip.wpi"))
            {
                rec.Text = (string)T("App");
                rec.Location = new System.Drawing.Point(312, 21);
            }
            var Day = DateTime.Now.Day;
            switch (Day)
            {
                //for each case (day) set to calendarPic the resource with format [c]+[day]
                case 1:
                    calendarPic.Image = Properties.Resources.c1;
                    break;
                case 2:
                    calendarPic.Image = Properties.Resources.c2;
                    break;
                case 3:
                    calendarPic.Image = Properties.Resources.c3;
                    break;
                case 4:
                    calendarPic.Image = Properties.Resources.c4;
                    break;
                case 5:
                    calendarPic.Image = Properties.Resources.c5;
                    break;
                case 6:
                    calendarPic.Image = Properties.Resources.c6;
                    break;
                case 7:
                    calendarPic.Image = Properties.Resources.c7;
                    break;
                case 8:
                    calendarPic.Image = Properties.Resources.c8;
                    break;
                case 9:
                    calendarPic.Image = Properties.Resources.c9;
                    break;
                case 10:
                    calendarPic.Image = Properties.Resources.c10;
                    break;
                case 11:
                    calendarPic.Image = Properties.Resources.c11;
                    break;
                case 12:
                    calendarPic.Image = Properties.Resources.c12;
                    break;
                case 13:
                    calendarPic.Image = Properties.Resources.c13;
                    break;
                case 14:
                    calendarPic.Image = Properties.Resources.c14;
                    break;
                case 15:
                    calendarPic.Image = Properties.Resources.c15;
                    break;
                case 16:
                    calendarPic.Image = Properties.Resources.c16;
                    break;
                case 17:
                    calendarPic.Image = Properties.Resources.c17;
                    break;
                case 18:
                    calendarPic.Image = Properties.Resources.c18;
                    break;
                case 19:
                    calendarPic.Image = Properties.Resources.c19;
                    break;
                case 20:
                    calendarPic.Image = Properties.Resources.c20;
                    break;
                case 21:
                    calendarPic.Image = Properties.Resources.c21;
                    break;
                case 22:
                    calendarPic.Image = Properties.Resources.c22;
                    break;
                case 23:
                    calendarPic.Image = Properties.Resources.c23;
                    break;
                case 24:
                    calendarPic.Image = Properties.Resources.c24;
                    break;
                case 25:
                    calendarPic.Image = Properties.Resources.c25;
                    break;
                case 26:
                    calendarPic.Image = Properties.Resources.c26;
                    break;
                case 27:
                    calendarPic.Image = Properties.Resources.c27;
                    break;
                case 28:
                    calendarPic.Image = Properties.Resources.c28;
                    break;
                case 29:
                    calendarPic.Image = Properties.Resources.c29;
                    break;
                case 30:
                    calendarPic.Image = Properties.Resources.c30;
                    break;
                case 31:
                    calendarPic.Image = Properties.Resources.c31;
                    break;
                default:
                    calendarPic.Image = Properties.Resources.ScheduleTime_80;
                    break;
            }

            this.TopMost = true;

            this.term.Text = (string)T("iApp");
            this.ie.Text = (string)T("App");
            cio.Text = (string)T("App");
            def.Text = (string)T("sApp");
            dnn.Text = (string)T("sApp");
            dnno.Text = (string)T("sApp");
            dnu.Text = (string)T("sApp");
            
        }

        private void panel12_Click(object sender, EventArgs e)
        {
            //INICIAR DENESTORE
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\DENEOS\systemApps\deneNavi\Internet Explorer 11.exe");
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            Process.Start("C:\\DENEOS\\systemApps\\deneNotes\\deneNotes.exe");
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            //INICIAR DENEUPDATE
        }

        private void panel7_Click(object sender, EventArgs e)
        {
            panel4_Click(sender, e); 
        }

        private void panel8_Click(object sender, EventArgs e)
        {
            //INICIAR CALENDAR.IO
            Process.Start("C:\\Users\\rayel\\source\\repos\\!New\\repos\\deneOS\\CalendarIO\\bin\\Debug\\net9.0-windows10.0.26100.0\\CalendarIO.exe");
        }

        private void panel9_Click(object sender, EventArgs e)
        {
            //INICIAR DENETERMINAL
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            //INICIAR DENEFILES
            string deneFiles = @"C:\DENEOS\systemApps\deneFiles\deneFiles.exe";
            if (!File.Exists(deneFiles))
            {
                MessageBox.Show((string)T("dfni"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Si el archivo existe, iniciar el proceso
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = deneFiles,
                Arguments = flagMgmt.EnableRoot ? "/dangerZone:enableRoot" : "",
                UseShellExecute = true
            };
            Process.Start(startInfo);
        }

        private void panel10_Click(object sender, EventArgs e)
        {
            string installPath;
            //COMPROBAR SI RECIP ESTÁ INSTALADO. SINO: INSTALAR, SI SÍ: INICIAR.
            if (System.IO.File.Exists(@"C:\SOFTWARE\Recip\recip.wpi"))
            {
                installPath = @"C:\SOFTWARE\Recip\recip.wpi";
                System.Diagnostics.Process.Start(installPath);
            }
            else
            {
                //descargar el archivo Recip.dnpkg
                using (var client = new System.Net.WebClient())
                {
                    Random random = new Random();
                    string dtdNMR = random.Next(-2147483648, 2147483647).ToString();
                    string url = "http://inscorp.x10.mx/recip/versiones/downloads/v1.0/Recip_DNF481_deneOS.meta";
                    string path = $@"C:\DENEOS\appDnpkgTmpDownload\{dtdNMR}\Recip_DNF481_deneOS.meta";
                    Directory.CreateDirectory($"C:\\DENEOS\\appDnpkgTmpDownload\\{dtdNMR}");
                    client.DownloadFile(url, path);
                    url = "http://inscorp.x10.mx/recip/versiones/downloads/v1.0/Recip_DNF481_deneOS.zip";
                    path = $@"C:\DENEOS\appDnpkgTmpDownload\{dtdNMR}\Recip_DNF481_deneOS.zip";
                    client.DownloadFile(url, path);
                    url = "http://inscorp.x10.mx/recip/versiones/downloads/v1.0/dnpai.exe";
                    path = $@"C:\DENEOS\appDnpkgTmpDownload\{dtdNMR}\dnpai.wpi";
                    client.DownloadFile(url, path);
                    url = "http://inscorp.x10.mx/recip/versiones/downloads/v1.0/dnpai.exe.config";
                    path = $@"C:\DENEOS\appDnpkgTmpDownload\{dtdNMR}\dnpai.wpi.config";
                    client.DownloadFile(url, path);
                    url = "http://inscorp.x10.mx/recip/versiones/downloads/v1.0/dnpai.pdb";
                    path = $@"C:\DENEOS\appDnpkgTmpDownload\{dtdNMR}\dnpai.pdb";
                    client.DownloadFile(url, path);
                    Process dnpai = new Process();
                    dnpai.StartInfo = new ProcessStartInfo
                    {
                        FileName = $@"C:\DENEOS\appDnpkgTmpDownload\{dtdNMR}\dnpai.wpi",
                        Arguments = $"\"C:\\DENEOS\\appDnpkgTmpDownload\\{dtdNMR}\\Recip_DNF481_deneOS.meta\" \"C:\\DENEOS\\appDnpkgTmpDownload\\{dtdNMR}\\Recip_DNF481_deneOS.zip\" /s",
                        UseShellExecute = true,
                        Verb = "runas"
                    };
                    try
                    {
                        dnpai.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR 0x5"+ex.Message+ex.StackTrace+ex.Source);
                    }
                    dnpai.WaitForExit();
                    Directory.Delete($"C:\\DENEOS\\appDnpkgTmpDownload\\{dtdNMR}", true);
                    rec.Text = "App";
                    rec.Location = new System.Drawing.Point(312, 21);
                }
            }
        }   
        private void button4_Click(object sender, EventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "explorer.exe",
                UseShellExecute = true,
                WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Windows)
            };

            Process.Start(psi);

            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string a1; string b1; object c1; object d1;
            a1 = (string)T("sdopt");
            b1 = "deneOS Home Edition";
            c1 = MessageBoxButtons.YesNoCancel;
            d1 = MessageBoxIcon.Question;
            var a = MessageBox.Show(a1, b1, (MessageBoxButtons)c1, (MessageBoxIcon)d1);

            switch (a)
            {
                case DialogResult.Yes:
                    buttonApagarElegante_Click();
                    break;
                case DialogResult.No:
                    var psi = new ProcessStartInfo
                    {
                        FileName = "explorer.exe",
                        UseShellExecute = true,
                        WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Windows)
                    };

                    Process.Start(psi);

                    Environment.Exit(0);
                    break;
                case DialogResult.Cancel:
                    return;
            }
        }
        private async void buttonApagarElegante_Click()
        {
            for (int i = 5; i >= 1; i--)
            {
                MessageBox.Show($"{T("syspwoffin")} {i} {T("ss")}...", "deneOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await Task.Delay(1000); // espera 1 segundo
            }

            Process.Start("shutdown", "-s -f -t 0");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
