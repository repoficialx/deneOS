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
                    return "January";
                default:
                    return "";
                case "02":
                    return "February";
                case "03":
                    return "March";
                case "04":
                    return "April";
                case "05":
                    return "May";
                case "06":
                    return "June";
                case "07":
                    return "July";
                case "08":
                    return "August";
                case "09":
                    return "September";
                case "10":
                    return "October";
                case "11":
                    return "November";
                case "12":
                    return "December";
            }
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
            label1.Text = hhmm;
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
            label2.Text = $"{dddd}., {ddd} {MonthNum2Month(mm2)} {yyyy}";
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

        }
    }
}
