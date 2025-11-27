using deneOS_Home;

namespace controlcenter
{
    public partial class formAjustes : Form
    {
        public enum Pages
        {
            Inicio, Pantalla, General, Software, Avanzado, AcercaDe, Personalizacion
        }
        public static Pages? CurrentPage = null;
        public static UserControl page2control(Pages? page)
        {
            switch (page)
            {
                case null:
                    return new InicioControl();
                case Pages.Inicio:
                    return new InicioControl();
                case Pages.Pantalla:
                    return new PantallaControl();
                case Pages.General:
                    return new GeneralControl();
                case Pages.Software:
                    return new SoftwareControl();
                case Pages.Avanzado:
                    return new AvanzadoControl();
                case Pages.AcercaDe:
                    return new AboutControl();
                case Pages.Personalizacion:
                //
                /*case Pages.AcercaDe:
                    return new AcercaDeControl();*/
                default:
                    throw new ArgumentOutOfRangeException(nameof(page), page, null);
            }
        }
        public formAjustes()
        {
            InitializeComponent();
            panel2.Controls.Clear();
            UserControl inicio = page2control(CurrentPage);
            panel2.Controls.Add(inicio);
        }

        private Image RedimensionarImagen(Image imgOriginal, int ancho, int alto)
        {
            Bitmap imgEscalada = new Bitmap(ancho, alto);
            using (Graphics g = Graphics.FromImage(imgEscalada))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgOriginal, 0, 0, ancho, alto);
            }
            return imgEscalada;
        }

        private void formAjustes_Load(object sender, EventArgs e)
        {
            // Cargar icono de la ventana
            //this.Icon = RedimensionarImagen(Properties.Resources.icono_ajustes, 16, 16) as Icon;

            btnAcerca.Image = RedimensionarImagen(Properties.Resources.icons8_about_me_100, 30, 30) as Image;
            btnAvanzado.Image = RedimensionarImagen(Properties.Resources.icons8_ethernet_settings_100, 30, 30) as Image;
            btnGeneral.Image = RedimensionarImagen(Properties.Resources.icons8_laptop_settings_100, 30, 30) as Image;
            btnInicio.Image = RedimensionarImagen(Properties.Resources.icons8_home_page_100, 30, 30) as Image;
            btnPantalla.Image = RedimensionarImagen(Properties.Resources.icons8_monitor_100, 30, 30) as Image;
            btnSoftware.Image = RedimensionarImagen(Properties.Resources.icons8_software_100, 30, 30) as Image;
            btnUpd.Image = RedimensionarImagen(Properties.Resources.icons8_windows_update_100, 30, 30) as Image;
            btnCustom.Image = RedimensionarImagen(Properties.Resources.icons8_windows_10_personalization_100, 30, 30) as Image;

            // Textos traducidos

            Text = (string)T("controlcenter");
            btnAcerca.Text = (string)T("about");
            btnAvanzado.Text = (string)T("advanced");
            btnGeneral.Text = (string)T("general");
            btnInicio.Text = (string)T("home");
            btnPantalla.Text = (string)T("screen");
            btnSoftware.Text = (string)T("software");
        }
        private void btnPantalla_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            PantallaControl pantalla = new();
            panel2.Controls.Add(pantalla);
        }

        private void btnGeneral_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            GeneralControl general = new();
            panel2.Controls.Add(general);
        }

        private void btnSoftware_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            SoftwareControl software = new();
            panel2.Controls.Add(software);
        }

        private void btnAvanzado_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            AvanzadoControl avanzado = new();
            panel2.Controls.Add(avanzado);

        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            InicioControl inicio = new();
            panel2.Controls.Add(inicio);

        }

        private void btnAcerca_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            AboutControl acerca = new();
            panel2.Controls.Add(acerca);
        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            UpdateControl upd = new();
            panel2.Controls.Add(upd);
        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            CustomControl custom = new();
            panel2.Controls.Add(custom);
        }
    }
}
