using Microsoft.Win32;
using System.Diagnostics;
using System.Text.Json;
using dosu;

namespace deneOS_Launcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("C:\\DENEOS\\core\\deneOS.exe")) return;
            button1.Text = @"INSTALL►";
            button1.Click -= button1_Click!;
            button1.Click += Install_Click!;
        }

        async void Install_Click(object sender, EventArgs e)
        {
            SetupScreen setup = new SetupScreen();
            setup.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Process dnh = new Process();
            dnh.StartInfo.FileName = "c:\\DENEOS\\core\\deneOS.exe";
            dnh.StartInfo.Verb = "runas";
            dnh.StartInfo.UseShellExecute = true;
            dnh.Start();
        }
        private bool f10Pressed = false;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.F10 || f10Pressed) return;
            f10Pressed = true;
            System.Media.SystemSounds.Beep.Play();
            //System.Media.SoundPlayer sp = new();
            //sp.SoundLocation = "deneos_sound.wav";
            //sp.Play();

            if (button1.Text != @"INSTALL►")
            {
                button1.Text = @"INSTALL►";
                button1.Click -= button1_Click!;
                button1.Click += Install_Click!;
            }
            else
            {
                button1.Text = @"RUN►";
                button1.Click -= Install_Click!;
                button1.Click += button1_Click!;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10) f10Pressed = false;
        }
    }
}
