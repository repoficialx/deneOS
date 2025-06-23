using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Traductor;

namespace deneOS_Home.init
{
    public partial class logonui : Form
    {
        public Panel panelToShow;
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
            this.Show();
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
            bool Corrupted = ( userSpecified && !passSpecified ) || ( !userSpecified && passSpecified) ;
            Panel panelToShow;
            if (!isValid)
            {
                if (Corrupted)
                {
                    MessageBox.Show($"{T("cfgflcorr")} {T("plsfxit")}", "deneOS Home Edition", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                    panelToShow = panel1;
                }
                else
                {
                    panelToShow = panel2;
                }
            }
            else
            {
                panelToShow = panel1;
            }
                Panel panelOculto = new Panel();
            this.Controls.Add(panelOculto);
            this.ActiveControl = panelOculto; // Evita que button2 tome el foco
            this.KeyPreview = true;
            //MessageBox.Show($"Control con foco: {this.ActiveControl?.Name}");
            this.Activate();
            this.Focus();
            this.panelToShow = panelToShow;
            txt3.Text = (string)T("txt3");
            txt4.Text = (string)T("txt4");
            txt5.Text = (string)T("txt5");
            txt6.Text = (string)T("txt6");
            txt9.Text = (string)T("txt9");
            txt10.Text = (string)T("txt10");
            txt11.Text = (string)T("txt11");
            txt12.Text = (string)T("txt12");
            txt13.Text = (string)T("txt13");
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
                if (dd.EndsWith("1") && int.Parse(dd)!=11)
                {
                    ddd = dd + "st";
                }
                else if (dd.EndsWith("2") && int.Parse(dd)!=12)
                {
                    ddd = dd + "nd";
                }
                else if (dd.EndsWith("3") && int.Parse(dd)!=13)
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
            //Process proc = new Process(); proc.StartInfo.FileName = "shutdown"; proc.StartInfo.Arguments = "-r -f -t 0"; proc.Start();

            Environment.Exit(0);
        }
        /// <summary>
        /// Botón de inicio de sesión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            string usr;
            string pss;
            var cfgInfo = File.Exists(@"C:\DENEOS\sysconf\config.ini") ? File.ReadAllLines(@"C:\DENEOS\sysconf\config.ini") : new string[] { "", "", "" };
            if (cfgInfo[2].ToLower().Contains("password = "))
            {
                pss = cfgInfo[2].Substring(11);
            }
            else
            {
                MessageBox.Show((string)T("npss"), "dene Safety", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                pss = "";
            }
            if (cfgInfo[1].ToLower().Contains("username = "))
            {
                usr = cfgInfo[1].Substring(11);
            }
            else
            {
                MessageBox.Show((string)T("nuss"), "dene Safety", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usr = "";
            }
            _login(pss, usr);

        }
        /// <summary>
        /// Proceso de bienvenida al usuario
        /// </summary>
        /// <param name="usr"></param>
        /// <param name="pss"></param>
        private void _login(string usr, string pss)
        {
            if (boxusr.Text == pss && boxpass.Text == usr)
            {
                MessageBox.Show((string)T("welctodeneosE"), (string)T("welc"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Hide();
                new desktop().Show();
                new tbar().Show();
            }
            else
            {
                MessageBox.Show((string)T("invusrpss"), (string)T("err"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Proceso de creación de un usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt13_Click(object sender, EventArgs e)
        {
            
            if (boxregpass.Text == boxregpassag.Text)
            {
                string[] file =
                {
                    "[deneOS Home]",
                    string.Format
                    (
                        "username = {0}",
                        boxregusr.Text
                    ),
                    string.Format
                    (
                        "password = {0}",
                        boxregpass.Text
                    )
                };
                File.WriteAllLines(@"C:\DENEOS\sysconf\config.ini", file);
                MessageBox.Show((string)T("usrcrscc"), (string)T("suc"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show((string)T("pssdntmatch"), (string)T("pssdntmatch"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Inicio del formulario de inicio de sesión tras presionar INTRO.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logonui_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("KEYDOWN EVENT TOOGLED ON");
            if (e.KeyCode == Keys.Enter)
            {
                //MessageBox.Show("KEYDOWN EVENT ENTER KEY PRESSED");
                e.SuppressKeyPress = true; // Evita que Enter llegue al botón
                panelToShow.Show();
                panelToShow.BringToFront();
                txt6.Focus();
            }
            /*else
            {
                MessageBox.Show($"KEYDOWN EVENT {e.KeyCode} PRESSED");
            }*/
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void txt3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void txt5_Click(object sender, EventArgs e)
        {

        }

        private void boxpass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt4_Click(object sender, EventArgs e)
        {

        }

        private void boxusr_TextChanged(object sender, EventArgs e)
        {

        }
    }
}