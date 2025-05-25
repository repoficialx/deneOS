using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneNotes
{
    public partial class Form1 : Form
    {
        bool isFileOpen = false;
        string currentFilePath = string.Empty;
        public Form1()
        {
            InitializeComponent();
            if (isFileOpen)
            {
                File.ReadAllLines(currentFilePath).ToList().ForEach(line => textBox1.AppendText(line + Environment.NewLine));
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void ventanaNuevaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath);
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            File.ReadAllLines(openFileDialog1.FileName).ToList().ForEach(line => textBox1.AppendText(line + Environment.NewLine));
            isFileOpen = true;
            currentFilePath = openFileDialog1.FileName;
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isFileOpen && !string.IsNullOrEmpty(currentFilePath))
            {
                File.WriteAllText(currentFilePath, textBox1.Text);
            }
            else
            {
                saveFileDialog1.ShowDialog();
            }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            File.WriteAllLines(saveFileDialog1.FileName, textBox1.Lines);
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
        }

        private void acercaDeDeneNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new about().ShowDialog();
        }

        private void verLaAyudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://repoficialx.xyz/deneos/help/deneNotes.html");
        }

        private void acercarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Font = new Font(textBox1.Font.Name, textBox1.Font.Size + 2, textBox1.Font.Style,textBox1.Font.Unit, textBox1.Font.GdiCharSet, textBox1.Font.GdiVerticalFont);
        }

        private void alejarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Font = new Font(textBox1.Font.Name, textBox1.Font.Size - 2, textBox1.Font.Style, textBox1.Font.Unit, textBox1.Font.GdiCharSet, textBox1.Font.GdiVerticalFont);
        }

        private void restaurarAlPredeterminadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Font = new Font(textBox1.Font.Name, 12, textBox1.Font.Style, textBox1.Font.Unit, textBox1.Font.GdiCharSet, textBox1.Font.GdiVerticalFont);
        }

        private void barraDeEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (barraDeEstadoToolStripMenuItem.Checked)
            {
                statusStrip1.Visible = false;
                barraDeEstadoToolStripMenuItem.Checked = false;
                barraDeEstadoToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
            else
            {
                statusStrip1.Visible = true;
                barraDeEstadoToolStripMenuItem.Checked = true;
                barraDeEstadoToolStripMenuItem.CheckState = CheckState.Checked;
            }
        }

        private void ajusteDeLíneaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ajusteDeLíneaToolStripMenuItem.Checked)
            {
                textBox1.WordWrap = false;
                ajusteDeLíneaToolStripMenuItem.Checked = false;
                ajusteDeLíneaToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
            else
            {
                textBox1.WordWrap = true;
                ajusteDeLíneaToolStripMenuItem.Checked = true;
                ajusteDeLíneaToolStripMenuItem.CheckState = CheckState.Checked;
            }
        }

        private void fuenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
        }

        private void deshacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Copy();
        }

        private void pegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Eliminar el texto seleccionado
            if (textBox1.SelectionLength > 0)
            {
                textBox1.SelectedText = string.Empty;
            }
            else
            {
                // Si no hay texto seleccionado, eliminar el texto en la posición del cursor
                int cursorPosition = textBox1.SelectionStart;
                if (cursorPosition > 0)
                {
                    textBox1.Text = textBox1.Text.Remove(cursorPosition - 1, 1);
                    textBox1.SelectionStart = cursorPosition - 1; // Mover el cursor hacia atrás
                }
            }
        }

        private void búsquedaConBingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.bing.com/search?q=" + Uri.EscapeDataString(textBox1.SelectedText));
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void seleccionarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Seleccionar todo el texto en el TextBox
            textBox1.SelectAll();
        }

        private void horaYFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(DateTime.Now.ToString());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = $"Línea {textBox1.GetLineFromCharIndex(textBox1.SelectionStart) + 1}, Columna {textBox1.SelectionStart - textBox1.GetFirstCharIndexFromLine(textBox1.GetLineFromCharIndex(textBox1.SelectionStart)) + 1}";
            toolStripStatusLabel2.Text = $"{(textBox1.Font.Size / 12) * 100}%";
            toolStripStatusLabel3.Text = "LF";
            toolStripStatusLabel4.Text = Encoding.UTF8.ToString();
        }

        private void verToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {
            textBox1.Font = fontDialog1.Font;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Back)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("+{LEFT}{DEL}");
            }
        }
    }
}