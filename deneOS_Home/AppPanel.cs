using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneOS_Home
{
    public partial class AppPanel : UserControl
    {
        public string AppID { get; set; } // Podrías usar esto para identificar la instancia

        public AppPanel(Image icon, string title, string appId)
        {
            InitializeComponent();
            pictureBox1.Image = icon;
            label1.Text = title;
            AppID = appId;
        }
    }

}
