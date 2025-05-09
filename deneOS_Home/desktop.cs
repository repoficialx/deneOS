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

namespace deneOS_Home
{
    public partial class desktop : Form
    {
        public desktop()
        {
            InitializeComponent();
        }

        private void desktop_Load(object sender, EventArgs e)
        {
            label16.Text = "Downloadable App";
            this.label16.Location = new System.Drawing.Point(180, 21);
            if (System.IO.File.Exists(@"C:\Program Files\iNS\deneOS\HomeEdition\apps\Recip\recip.wpi"))
            {
                label16.Text = "App";
                label16.Location = new System.Drawing.Point(312, 21);
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
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (panel2.Visible)
            {
                panel2.Hide();
            }
            else
            {
                panel2.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string a1;string b1;object c1;object d1;
            a1 = "Shutdown options: Do you want to shutdown the computer? (Pressing [YES] will shutdown, pressing [NO] will start Windows, pressing [CANCEL] will return)";
            b1 = "deneOS Home Edition";
            c1=MessageBoxButtons.YesNoCancel;
            d1=MessageBoxIcon.Question;
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
                MessageBox.Show($"El sistema se apagará en {i} segundos...", "deneOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await Task.Delay(1000); // espera 1 segundo
            }

            Process.Start("shutdown", "-s -f -t 0");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            Application.Exit();
        }

        private void panel12_Click(object sender, EventArgs e)
        {
            //INICIAR DENESTORE
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            //INICIAR DENENAVI
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            //INICIAR DENENOTES
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            //INICIAR DENEUPDATE
        }

        private void panel7_Click(object sender, EventArgs e)
        {
            //INICIAR IE11MINIMAL
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
        }

        private void panel10_Click(object sender, EventArgs e)
        {
            string installPath;
            //COMPROBAR SI RECIP ESTÁ INSTALADO. SINO: INSTALAR, SI SÍ: INICIAR.
            if (System.IO.File.Exists(@"C:\Program Files\iNS\deneOS\HomeEdition\apps\Recip\recip.wpi"))
            {
                installPath = @"C:\Program Files\iNS\deneOS\HomeEdition\apps\Recip\recip.wpi";
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
                    string path = $@"C:\Program Files\iNS\deneOS\HomeEdition\appDnpkgTmpDownload\{dtdNMR}\Recip_DNF481_deneOS.meta";
                    Directory.CreateDirectory($"C:\\Program Files\\iNS\\deneOS\\HomeEdition\\appDnpkgTmpDownload\\{dtdNMR}");
                    client.DownloadFile(url, path);
                    url = "http://inscorp.x10.mx/recip/versiones/downloads/v1.0/Recip_DNF481_deneOS.zip";
                    path = $@"C:\Program Files\iNS\deneOS\HomeEdition\appDnpkgTmpDownload\{dtdNMR}\Recip_DNF481_deneOS.zip";
                    client.DownloadFile(url, path);
                    url = "http://inscorp.x10.mx/recip/versiones/downloads/v1.0/dnpai.exe";
                    path = $@"C:\Program Files\iNS\deneOS\HomeEdition\appDnpkgTmpDownload\{dtdNMR}\dnpai.wpi";
                    client.DownloadFile(url, path);
                    url = "http://inscorp.x10.mx/recip/versiones/downloads/v1.0/dnpai.exe.config";
                    path = $@"C:\Program Files\iNS\deneOS\HomeEdition\appDnpkgTmpDownload\{dtdNMR}\dnpai.wpi.config";
                    client.DownloadFile(url, path);
                    url = "http://inscorp.x10.mx/recip/versiones/downloads/v1.0/dnpai.pdb";
                    path = $@"C:\Program Files\iNS\deneOS\HomeEdition\appDnpkgTmpDownload\{dtdNMR}\dnpai.pdb";
                    client.DownloadFile(url, path);
                    Process dnpai = new Process();
                    dnpai.StartInfo = new ProcessStartInfo
                    {
                        FileName = $@"C:\Program Files\iNS\deneOS\HomeEdition\appDnpkgTmpDownload\{dtdNMR}\dnpai.wpi",
                        Arguments = $"\"C:\\Program Files\\iNS\\deneOS\\HomeEdition\\appDnpkgTmpDownload\\{dtdNMR}\\Recip_DNF481_deneOS.meta\" \"C:\\Program Files\\iNS\\deneOS\\HomeEdition\\appDnpkgTmpDownload\\{dtdNMR}\\Recip_DNF481_deneOS.zip\" /s",
                        UseShellExecute = true,
                        Verb = "runas"
                    };
                    try
                    {
                        dnpai.Start();
                    }
                    catch
                    {
                        MessageBox.Show("ERROR 0x000005");
                    }
                    dnpai.WaitForExit();
                    Directory.Delete($"C:\\Program Files\\iNS\\deneOS\\HomeEdition\\appDnpkgTmpDownload\\{dtdNMR}", true);
                    label16.Text = "App";
                    label16.Location = new System.Drawing.Point(312, 21);
                }
            }
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

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

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
