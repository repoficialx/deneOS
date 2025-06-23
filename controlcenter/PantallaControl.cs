using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace controlcenter
{
    public partial class PantallaControl : UserControl
    {
        public PantallaControl()
        {
            InitializeComponent();
            SetupUI();
        }

        private void PantallaControl_Load(object sender, EventArgs e)
        {

        }
        private void SetupUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            // Ícono
            PictureBox icono = new PictureBox();
            icono.Image = (Properties.Resources.icons8_illumination_brightness_100); // pon un ícono aquí desde Resources
            icono.Size = new Size(32, 32);
            icono.Location = new Point(20, 20);
            icono.SizeMode = PictureBoxSizeMode.Zoom;

            // Etiqueta
            Label lbl = new Label();
            lbl.Text = (string)T("brightness");
            lbl.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lbl.AutoSize = true;
            lbl.Location = new Point(60, 25);

            // Slider
            TrackBar slider = new TrackBar();
            slider.Minimum = 0;
            slider.Maximum = 100;
            slider.Value = 50;
            slider.TickStyle = TickStyle.None;
            slider.Width = 250;
            slider.Location = new Point(20, 70);
            slider.Scroll += (s, e) => {
                // Aquí pondrías el código para ajustar el brillo
                Console.WriteLine($"{T("newbrightness")}: {slider.Value}%");
            };

            // Agrega los controles al panel
            this.Controls.Add(icono);
            this.Controls.Add(lbl);
            this.Controls.Add(slider);
        }
    }
}
