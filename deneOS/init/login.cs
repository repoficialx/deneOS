#pragma warning disable
using deneOS.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Traductor;

namespace deneOS.init
{
    public partial class login : Form
    {
        public login()
        {

            InitializeComponent();
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
            //debug
            txt3.Text = (string)T("txt3");
            txt4.Text = (string)T("txt4");
            txt5.Text = (string)T("txt5");
            txt6.Text = (string)T("txt6");
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (BackgroundImage != null)
            {
                e.Graphics.DrawImage(BackgroundImage, ClientRectangle);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void login_Load(object sender, EventArgs e)
        {
            string pss = "";
            string usr = "";
            string configFile = @"C:\DENEOS\sysconf\config.ini";
            bool fileExists = File.Exists(configFile);
            int headerLn = 0;
            int userLn = 1;
            int passLn = 2;
            string[] cfgInfo = new string[3];
            cfgInfo = fileExists ? File.ReadAllLines(configFile).Concat(new string[] { "", "", "" }).Take(3).ToArray() : new string[] { "", "", "" };
            bool userSpecified = cfgInfo[userLn].Contains("username = ");
            bool passSpecified = cfgInfo[passLn].Contains("password = ");
            bool isValid = userSpecified && passSpecified;
            bool Corrupted = (userSpecified && !passSpecified) || (!userSpecified && passSpecified);
            if (!isValid)
            {
                if (Corrupted)
                {
                    MessageBox.Show($"{T("cfgflcorr")} {T("plsfxit")}", "deneOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
                else
                {
                    Close();
                }
            }
            else
            {

            }
            Panel panelOculto = new Panel();
            this.Controls.Add(panelOculto);
            if (boxusr.Enabled)
            {
                if (boxusr.Visible)
                {
                    //this.ActiveControl = boxusr;
                }

            }
            this.AcceptButton = txt6;
            this.KeyPreview = true;
            this.Activate();
            this.Focus();
        }

        private void txt6_Click(object sender, EventArgs e)
        {
            string usr = "";
            string pssHash = "";

            var cfgInfo = File.Exists(@"C:\DENEOS\sysconf\config.ini")
                ? File.ReadAllLines(@"C:\DENEOS\sysconf\config.ini")
                : new string[] { "", "", "" };

            if (cfgInfo.Length < 3)
            {
                MessageBox.Show(
                    (string)T("cfgflcorr") ?? "Archivo de configuración corrupto",
                    "deneOS Safety",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // Extraer password hash
            if (cfgInfo[2].ToLower().Contains("password = "))
            {
                pssHash = cfgInfo[2].Substring(11).Trim();
            }
            else
            {
                MessageBox.Show((string)T("npss"), "dene Safety", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Extraer username
            if (cfgInfo[1].ToLower().Contains("username = "))
            {
                usr = cfgInfo[1].Substring(11).Trim();
            }
            else
            {
                MessageBox.Show((string)T("nuss"), "dene Safety", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Pasar el hash, no texto plano
            _login(usr, pssHash);
        }
        private void _login(string storedUsername, string storedPasswordHash)
        {
            string inputUsername = boxusr.Text;
            string inputPassword = boxpass.Text;

            // Validar que no estén vacíos
            if (string.IsNullOrWhiteSpace(inputUsername) || string.IsNullOrWhiteSpace(inputPassword))
            {
                MessageBox.Show(
                    (string)T("emptyfields") ?? "Por favor complete todos los campos",
                    (string)T("err"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // ✅ Verificar usuario y contraseña de forma SEGURA
            bool usernameMatch = inputUsername.Equals(storedUsername, StringComparison.Ordinal);
            bool passwordMatch = PasswordHasher.VerifyPassword(inputPassword, storedPasswordHash);

            if (usernameMatch && passwordMatch)
            {
                Console.WriteLine($"[SUCCESS] Login exitoso para usuario: {inputUsername}");
                MessageBox.Show(
                    (string)T("welctodeneosE"),
                    (string)T("welc"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Hide();
                new desktop().Show();
                new tbar().Show();
            }
            else
            {
                Console.WriteLine($"[WARN] Login fallido para usuario: {inputUsername}");
                MessageBox.Show(
                    (string)T("invusrpss"),
                    (string)T("err"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                // Limpiar el campo de contraseña por seguridad
                boxpass.Clear();
                boxpass.Focus();
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void login_Click(object sender, EventArgs e)
        {
            
        }
    }
}
