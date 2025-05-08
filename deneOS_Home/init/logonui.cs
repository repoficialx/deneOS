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
        public logonui()
        {
            InitializeComponent();
        }
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
        private void logonui_Load(object sender, EventArgs e)
        {
            
            txt3.Text = (string)T("txt3");
            txt4.Text = (string)T("txt4");
            txt5.Text = (string)T("txt5");
            txt6.Text = (string)T("txt6");
            txt7.Text = (string)T("txt7");
            txt8.Text = (string)T("txt8");
            txt9.Text = (string)T("txt9");
            txt10.Text = (string)T("txt10");
            txt11.Text = (string)T("txt11");
            txt12.Text = (string)T("txt12");
            txt13.Text = (string)T("txt13");
        }
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
            
            dddd = DateTime.Now.DayOfWeek.ToString().Substring(0, 3);
            yyyy = DateTime.Now.Year.ToString();
            txt2.Text = $"{dddd}., {ddd} {MonthNum2Month(mm2)} {yyyy}";
        }
        private void MinuteUpdate_Tick(object sender, EventArgs e)
        {
            Time_Stuff();
            Date_Stuff();
        }
        string password = null;
        string username = null;
        private void logonui_KeyUp(object sender, KeyEventArgs e)
        {/*
            pictureBox1.Show();
            label3.Show();
            var cfgInfo = File.Exists(@"C:\Program Files\iNS\deneOS\HomeEdition\cfg\config.ini") ? File.ReadAllLines(@"C:\Program Files\iNS\deneOS\HomeEdition\cfg\config.ini") : new string[]{ "", "", "" };
            if (cfgInfo[2].Contains("password = "))
            {
                password = cfgInfo[2].Substring(11);
            }
            if (cfgInfo[1].Contains("username = "))
            {
                username = cfgInfo[1].Substring(11);
                label3.Text = username;
            }
            if (username != null && password != null)
            {
                textBox1.Show();
                button3.Show();
                pictureBox1.Show();
                label3.Show();
            }
            else {
            button1.Show();
            MessageBox.Show("No password set! Please enable password on settings.", "Insecure!", MessageBoxButtons.OK, MessageBoxIcon.Hand);}*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Process proc = new Process(); proc.StartInfo.FileName = "shutdown"; proc.StartInfo.Arguments = "-r -f -t 0"; proc.Start();
            Environment.Exit(0);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            new desktop().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string usr;
            string pss;
            var cfgInfo = File.Exists(@"C:\Program Files\iNS\deneOS\HomeEdition\cfg\config.ini") ? File.ReadAllLines(@"C:\Program Files\iNS\deneOS\HomeEdition\cfg\config.ini") : new string[] { "", "", "" };
            if (cfgInfo[2].ToLower().Contains("password = "))
            {
                pss = cfgInfo[2].Substring(11);
            }
            else
            {
                //button5.Show();
                MessageBox.Show("No password set! Please enable password on settings.", "Dene Safety", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                pss = "";
            }
            if (cfgInfo[1].ToLower().Contains("username = "))
            {
                usr = cfgInfo[1].Substring(11);
            }
            else
            {
                //button1.Show();
                MessageBox.Show("No user set! Please create user on settings.", "Dene Safety", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usr = "";
            }
            _login(pss, usr);

        }
        private void _login(string usr, string pss)
        {
            if (boxusr.Text == pss && boxpass.Text == usr)
            {
                MessageBox.Show("Welcome to deneOS Home Edition!", "Welcome!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                new deneOS_Home.desktop().Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
                File.WriteAllLines(@"C:\Program Files\iNS\deneOS\HomeEdition\cfg\config.ini", file);
                MessageBox.Show("User created successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}