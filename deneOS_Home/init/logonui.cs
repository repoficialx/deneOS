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
                    return T("mo1");
                default:
                    return "";
                case "02":
                    return T("mo2");
                case "03":
                    return T("mo3");
                case "04":
                    return T("mo4");
                case "05":
                    return T("mo5");
                case "06":
                    return T("mo6");
                case "07":
                    return T("mo7");
                case "08":
                    return T("mo8");
                case "09":
                    return T("mo9");
                case "10":
                    return T("mo10");
                case "11":
                    return T("mo11");
                case "12":
                    return T("mo12");
                default:
                    return "";
            }
        }
        private void logonui_Load(object sender, EventArgs e)
        {
            //Load language
            string lang = File.Exists(@"C:\Program Files\iNS\deneOS\HomeEdition\cfg\lang.ini") ? File.ReadAllText(@"C:\Program Files\iNS\deneOS\HomeEdition\cfg\lang.ini") : "en";
            Cargar(lang);
            txt1.Text = T("txt1");
            txt2.Text = T("txt2");
            txt3.Text = T("txt3");
            txt4.Text = T("txt4");
            txt5.Text = T("txt5");
            txt6.Text = T("txt6");
            txt7.Text = T("txt7");
            txt8.Text = T("txt8");
            txt9.Text = T("txt9");
            txt10.Text = T("txt10");
            txt11.Text = T("txt11");
            txt12.Text = T("txt12");
            txt13.Text = T("txt13");
        }
        void DateTime_Stuff()
        {

        }
        private void MinuteUpdate_Tick(object sender, EventArgs e)
        {
            //-------------------------Time
            string hh;
            string mm;
            if (DateTime.Now.Hour.ToString().Length < 2)
            {
                hh = $"0{DateTime.Now.Hour}";
            }
            else
            {
                hh = DateTime.Now.Hour.ToString();
            }
            if (DateTime.Now.Minute.ToString().Length < 2)
            {
                mm = $"0{DateTime.Now.Minute}";
            }
            else
            {
                mm = DateTime.Now.Minute.ToString();
            }
            string hhmm = hh + ":" + mm;
            txt1.Text = hhmm;
            //---------------------------Date
            string dddd;
            string dd;
            string ddd;
            string mm2;
            string yyyy;
            if (DateTime.Now.Day.ToString().Length < 2)
            {
                dd = $"0{DateTime.Now.Day}";
            }
            else
            {
                dd = DateTime.Now.Day.ToString();
            }
            if (DateTime.Now.Month.ToString().Length < 2)
            {
                mm2 = $"0{DateTime.Now.Month}";
            }
            else
            {
                mm2 = DateTime.Now.Month.ToString();
            }
            if (dd.EndsWith("1"))
            {
                ddd = dd + "st";
            }
            else if (dd.EndsWith("2"))
            {
                ddd = dd + "nd";
            }
            else if (dd.EndsWith("3"))
            {
                ddd = dd + "rd";
            }
            else
            {
                ddd = dd + "th";
            }
            dddd = DateTime.Now.DayOfWeek.ToString().Substring(0, 3);
            yyyy = DateTime.Now.Year.ToString();
            txt2.Text = $"{dddd}., {ddd} {MonthNum2Month(mm2)} {yyyy}";
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
                MessageBox.Show("No password set! Please enable password on settings.", "Non-protected!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                pss = "";
            }
            if (cfgInfo[1].ToLower().Contains("username = "))
            {
                usr = cfgInfo[1].Substring(11);
            }
            else
            {
                //button1.Show();
                MessageBox.Show("No user set! Please create user on settings.", "Non-protected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
