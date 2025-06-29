using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneOS_Launcher
{
    public partial class SelENorES : Form
    {
        public enum Language
        {
            English,
            Spanish
        }
        public static Language SelectedLanguage { get; private set; }
        public SelENorES()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectedLanguage = Language.English;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedLanguage = Language.Spanish;
            Close();
        }
    }
}
