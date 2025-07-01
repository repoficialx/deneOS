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
                Console.WriteLine($"{T("newbrightness")}: {slider.Value}%");
            };

            // Label con el brillo
            Label lbl2 = new Label();
            lbl2.Text = dosu.BrightnessLaptopMGMT.GetBrightness().ToString()+'%';
            lbl2.Font = new Font("Segoe UI", 11);
            lbl2.AutoSize = true;
            lbl2.Location = new Point(285, 70);

            // Botón establecer el brillo
            Button button = new Button();
            button.Text = $"{T("setbgns")}";
            button.AutoSize = true;
            button.Location = new Point(20, 130);
            button.Click += async (s, e) => {
                //await dosu.BrightnessLaptopMGMT.SetBrightnessAsync((byte)slider.Value);
                dosu.BrightnessLaptopMGMT.SetBrightness((byte)slider.Value);
            };
            button.Font = new Font("Segoe UI", 11);


            // Agrega los controles al panel
            this.Controls.Add(icono);
            this.Controls.Add(lbl);
            this.Controls.Add(slider);
            this.Controls.Add(lbl2);
            this.Controls.Add(button);
            
            switch (dosu.BrightnessMGMT.GetBrightness())
            {
                case -2:
                    // error obteniendo brillo
                    slider.Enabled = false;
                    break;
                case -1:
                    // error obteniendo monitor
                    slider.Enabled = false;
                    break;
                case null:
                    slider.Enabled = false;
                    break;
                default:
                    slider.Value = (int)dosu.BrightnessMGMT.GetBrightness()!;
                    break;
            }
        }
    }
}
