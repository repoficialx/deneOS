using System.Windows.Forms;

namespace deneStore
{
    public class ProgressForm : Form
    {
        public ProgressBar ProgressBar { get; private set; }

        public ProgressForm()
        {
            this.Text = "Instalando...";
            this.Width = 400;
            this.Height = 100;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowIcon = false;

            ProgressBar = new ProgressBar
            {
                Dock = DockStyle.Fill,
                Minimum = 0,
                Maximum = 100,
                Style = ProgressBarStyle.Continuous
            };

            this.Controls.Add(ProgressBar);
        }
    }
}