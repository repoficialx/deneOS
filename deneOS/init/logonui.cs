using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Traductor;

namespace deneOS.init
{
    public partial class logonui : Form
    {
        public Form formToShow;
        /// <summary>
        /// Inicialización de Compontentes vía InitializeComponent();
        /// </summary>
        public logonui()
        {
            if (flagMgmt.DisableLockScreen)
            {
                // Cerrar formulario y abrir el escritorio directamente lanzando una advertencia de seguridad
                MessageBox.Show($"{T("lockscrdisabled")} {T("thismayposeasecrisk")}", (string)T("warn"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                new desktop().Show();
                new tbar().Show();
                return;
            }
            InitializeComponent();
            dosu.UI.Scaling.ScaleForm(this);

            this.Show();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            // ✅ Limpiar caché al cerrar
            dosu.UI.Scaling.ClearCache();
        }
        /// <summary>
        /// Obtener nombre del mes a partir del número del mes usando el sistema de traducción vía json
        /// </summary>
        /// <param name="month">numero del mes pasado a string</param>
        /// <returns></returns>
        string MonthNum2Month(string month)
        {
            switch (month)
            {
                case "01":
                    return (string)T("mo1");
                case "02":
                    return (string)T("mo2");
                case "03":
                    return (string)T("mo3");
                case "04":
                    return (string)T("mo4");
                case "05":
                    return (string)T("mo5");
                case "06":
                    return (string)T("mo6");
                case "07":
                    return (string)T("mo7");
                case "08":
                    return (string)T("mo8");
                case "09":
                    return (string)T("mo9");
                case "10":
                    return (string)T("mo10");
                case "11":
                    return (string)T("mo11");
                case "12":
                    return (string)T("mo12");
                default:
                    return "";
            }
        }
        /// <summary>
        /// Acceder al sistema de traducciones y ponerle .Text a todos los Control la traducción
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logonui_Load(object sender, EventArgs e)
        {
            if (Debugger.IsAttached)
            {
                button1.Show();
            }
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
                    MessageBox.Show($"{T("cfgflcorr")} {T("plsfxit")}", "deneOS Home Edition", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
                else
                {
                    //formToShow = new newusr();
                }
            }
            else
            {
                formToShow = new login();
            }
            Panel panelOculto = new Panel();
            this.Controls.Add(panelOculto);
            this.ActiveControl = panelOculto; // Evita que button2 tome el foco
            this.KeyPreview = true;
            //MessageBox.Show($"Control con foco: {this.ActiveControl?.Name}");
            this.Activate();
            this.Focus();

        }
        /// <summary>
        /// Trabajos de fecha como asignar las variables de días, meses, años, etc.
        /// </summary>
        void Time_Stuff()
        {
            try
            { //-------------------------Time
                string hh;
                string mm;
                if (DateTime.Now.Hour.ToString().Length < 2) /*Si la hora tiene 1 dígito*/
                {
                    hh = $"0{DateTime.Now.Hour}"; /* Anteponer un 0 */
                }
                else
                {
                    hh = DateTime.Now.Hour.ToString(); /*Si la hora tiene 2 dígitos, dejar iguak*/
                }
                if (DateTime.Now.Minute.ToString().Length < 2) /*Si el minuto tiene 1 dígito*/
                {
                    mm = $"0{DateTime.Now.Minute}"; /* Anteponer un 0 */
                }
                else
                {
                    mm = DateTime.Now.Minute.ToString(); /*  Si el minuto tiene 2 dígitos, dejar igual */
                }
                string hhmm = hh + ":" + mm; /*HHMM = HH:MM*/
                txt1.Text = hhmm; /*TXT1=HHMM*/
            }
            catch
            {
                txt1.Text = (string)T("txt1");
            }
        }
        /// <summary>
        /// Trabajos de hora como asignar las variables de hora, minuto, segundo...
        /// </summary>
        void Date_Stuff()
        {
            //---------------------------Date
            string dddd; // DÍA DE LA SEMANA
            string dd; // DÍA
            string ddd; // DÍA CON TERMINACIÓN (ENG.)
            string mm2; // MES
            string yyyy; // AÑO (4 CIFRAS)
            if (DateTime.Now.Day.ToString().Length < 2)
            {
                dd = $"0{DateTime.Now.Day}"; // DÍA= 0(DÍA DE 1 CIFRA)
            }
            else
            {
                dd = DateTime.Now.Day.ToString(); // DIA= DÍA DE 2 CIFRAS
            }
            if (DateTime.Now.Month.ToString().Length < 2)
            {
                mm2 = $"0{DateTime.Now.Month}"; // MES= 0(MES DE 1 CIFRA)
            }
            else
            {
                mm2 = DateTime.Now.Month.ToString(); // MES= MES DE 2 CIFRAS
            }


            if (T("sisfct") is bool sisfctValue && !sisfctValue) // Si es booleano y es false]
            {
                ddd = dd; // DÍA= DÍA DE 2 CIFRAS
            }
            else // SI (SI) TERMINACIÓN
            {
                if (dd.EndsWith("1") && int.Parse(dd) != 11)
                {
                    ddd = dd + "st";
                }
                else if (dd.EndsWith("2") && int.Parse(dd) != 12)
                {
                    ddd = dd + "nd";
                }
                else if (dd.EndsWith("3") && int.Parse(dd) != 13)
                {
                    ddd = dd + "rd";
                }
                else
                {
                    ddd = dd + "th";
                }
            }

            string ddddnl;
            ddddnl = DateTime.Now.DayOfWeek.ToString().Substring(0, 3);
            dddd = (string)T($"dow{ddddnl.ToLower()}");
            yyyy = DateTime.Now.Year.ToString();
            txt2.Text = $"{dddd}., {ddd} {MonthNum2Month(mm2)} {yyyy}";
        }
        /// <summary>
        /// Actualizar en la pantalla la hora (00:00) y la fecha (31-12-9999)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinuteUpdate_Tick(object sender, EventArgs e)
        {
            Time_Stuff();
            Date_Stuff();
        }
        /// <summary>
        /// Botón de apagar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
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
        /// <summary>
        /// Inicio del formulario de inicio de sesión tras presionar INTRO.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logonui_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Evita que Enter llegue al botón
                if (formToShow.IsDisposed)
                {
                    if (formToShow is login)
                    {
                        formToShow = new login();
                    }/*
                    else if (formToShow is newusr)
                    {
                        //formToShow = new newusr();
                    }*/
                }
                formToShow.Show();
                formToShow.BringToFront();
                if (formToShow is login loginForm)
                {
                    Close();
                }
                /*else if (formToShow is newusr newUserForm)
                {
                    formToShow = new login();
                    formToShow.Show();
                    logonui_KeyDown(sender, e); // Llama a la función de inicio de sesión
                }*/
            }
        }
        /// <summary>
        /// Prevención de cierre de formulario usando técnicas como ALT+F4 o relacionadas (para evitar dejar al usuario sin
        /// escritorio, que deneOS no se quede a medio cerrar, que pueda reiniciar el ordenador sin problema alguno, etc.) usando
        /// e.Cancel = true;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logonui_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe");
            Application.Exit();
            
        }
    }
}