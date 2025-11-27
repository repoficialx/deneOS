using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.UI.Input;

namespace deneOS.OOBE
{
    public partial class Region : Form
    {
        public Region()
        {
            InitializeComponent();
        }
        //9/3: ES; 10/4: AD; 11/5: FR; 12/6: DE; 13/7: US; 14/8: UK; 15/9: N/D
        private void label9_Click(object sender, EventArgs e)
        {
            SetLanguage("es");
        }
        List<Label> labels
        {
            get
            {
                labels.Clear();
                foreach (Control control in this.Controls)
                {
                    if (control is Label label)
                    {
                        labels.Add(label);
                    }
                }
                return labels;
            }
        }
        void SetLanguage(string language)
        {
            switch (language)
            {
                //9/3: ES; 10/4: AD; 11/5: FR; 12/6: DE; 13/7: US; 14/8: UK; 15/9: N/D
                case "es":
                    label9.BackColor = Color.Turquoise;
                    break;

                case "ad":
                    label10.BackColor = Color.Turquoise;
                    break;

                case "fr":
                    label11.BackColor = Color.Turquoise;
                    break;

                case "de":
                    label12.BackColor = Color.Turquoise;
                    break;

                case "en":
                    label13.BackColor = Color.Turquoise;
                    break;

                case "gb":
                    label14.BackColor = Color.Turquoise;
                    break;
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            SetLanguage("ad");
        }

        private void label11_Click(object sender, EventArgs e)
        {
            SetLanguage("fr");
        }

        private void label12_Click(object sender, EventArgs e)
        {
            SetLanguage("de");
        }

        private void label13_Click(object sender, EventArgs e)
        {
            SetLanguage("en");
        }

        private void label14_Click(object sender, EventArgs e)
        {
            SetLanguage("gb");
        }

        private void label15_Click(object sender, EventArgs e)
        {
            // otro
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SetLanguage("es");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            SetLanguage("ad");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            SetLanguage("fr");
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            SetLanguage("de");
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            SetLanguage("en");
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            SetLanguage("gb");
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            //otro
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Label selectedLabel = null;
            string[] labelnames =
            {
                "label9",
                "label10",
                "label11",
                "label12",
                "label13",
                "label14",
                "label15"
            };
            foreach (Label label in labels)// Labels
            {
                if (labelnames.Contains(label.Name)) {//Labels de idiomas
                    if (label.BackColor == Color.Turquoise) //Label de idioma ELEGIDO
                    {
                        selectedLabel = label;
                    }
                }
            }

        }

        void ApplyLanguage(string language)
        {

        }
    }
}
