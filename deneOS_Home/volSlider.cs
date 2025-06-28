using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneOS_Home
{
    public partial class volSlider : Form
    {
        public volSlider()
        {
            InitializeComponent();
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - 200, 40);
            this.BackColor = ColorTranslator.FromHtml(globaldata.wallpaperPredominantColorHex);
            this.Opacity = 0.95;
        }

        private void volSlider_Load(object sender, EventArgs e)
        {
            TrackBar slider = new TrackBar
            {
                Minimum = 0,
                Maximum = 100,
                Value = (int)dosu.Audio.VolumeManager.GetSystemVolume(), // tu método GetVolume
                TickStyle = TickStyle.None,
                Width = 150
            };

            slider.Scroll += (s, ev) =>
            {
                //vls.SetSystemVolume(slider.Value / 100f); // tu método SetVolume
                float value = (float)slider.Value / 100f;
                dosu.Audio.VolumeManager.SetSystemVolume(value);
            };

            this.Controls.Add(slider);

            this.Deactivate += (s, ev) => this.Close(); // se cierra al perder foco
        }
    }
}
