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
        private bool isDirty = false;
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
            if (!CheckSaveBeforeContinue())
                return;

            textBox1.Clear();
            currentFilePath = "";
            isFileOpen = false;
            isDirty = false;
            UpdateTitle();

        }
        bool CheckSaveBeforeContinue()
        {
            if (!isDirty)
                return true;

            var result = MessageBox.Show(
                @"Unsaved changes. Keep them?",
                @"deneNotes",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Cancel)
                return false;

            if (result == DialogResult.Yes)
                Guardar();

            return true;
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
            File.ReadAllLines(openFileDialog1.FileName).ToList().ForEach(line =>
            {
                // Si no es la última línea, agregar un salto de línea
                if (line == File.ReadAllLines(openFileDialog1.FileName).Last())
                    textBox1.AppendText(line);
                else
                    textBox1.AppendText(line + Environment.NewLine);
            });
            isFileOpen = true;
            currentFilePath = openFileDialog1.FileName;
            UpdateTitle();
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e) => Guardar();

        void Guardar()
        {/*
            if (isFileOpen && !string.IsNullOrEmpty(currentFilePath))
            {
                File.WriteAllText(currentFilePath, textBox1.Text);
            }
            else
            {
                saveFileDialog1.ShowDialog();
            }*/
            if (!isFileOpen)
            {
                GuardarComo();
                return;
            }

            File.WriteAllText(currentFilePath, textBox1.Text);
            isDirty = false;
            UpdateTitle();

        }
        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuardarComo();
        }
        SaveFileDialog saveFileDialog = new SaveFileDialog()
        {
            Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
            DefaultExt = "txt",
            AddExtension = true,
            Title = "Save As"
        };
        void GuardarComo()
        {
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            currentFilePath = saveFileDialog.FileName;
            File.WriteAllText(currentFilePath, textBox1.Text);

            isFileOpen = true;
            isDirty = false;
            UpdateTitle();
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
            //new about().ShowDialog();
            Process.Start("C:\\DENEOS\\core\\aboutDialogs.exe", "deneNotes");
        }

        private void verLaAyudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://repoficialx.xyz/deneos/help/deneNotes");
        }

        private void acercarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Font = new Font(textBox1.Font.Name, textBox1.Font.Size + 2, textBox1.Font.Style, textBox1.Font.Unit, textBox1.Font.GdiCharSet, textBox1.Font.GdiVerticalFont);
        }

        private void alejarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Font = new Font(textBox1.Font.Name, textBox1.Font.Size >= 2 ? textBox1.Font.Size - 2 : textBox1.Font.Size, textBox1.Font.Style, textBox1.Font.Unit, textBox1.Font.GdiCharSet, textBox1.Font.GdiVerticalFont);
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
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                Font old = textBox1.Font;
                Font newFont = new Font(fontDialog1.Font.FontFamily, old.Size, fontDialog1.Font.Style);
                textBox1.Font = newFont;
            }

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
            toolStripStatusLabel1.Text = $"Line {textBox1.GetLineFromCharIndex(textBox1.SelectionStart) + 1}, column {textBox1.SelectionStart - textBox1.GetFirstCharIndexFromLine(textBox1.GetLineFromCharIndex(textBox1.SelectionStart)) + 1}";
            toolStripStatusLabel2.Text = $"{(int)Math.Round((textBox1.Font.Size / 12) * 100)}%";
            toolStripStatusLabel3.Text = getNLineMethod(textBox1);
            toolStripStatusLabel4.Text = Encoding.UTF8.EncodingName;
        }

        string getNLineMethod(TextBox textBox)
        {
            string text = textBox.Text;

            if (text.Contains("\r\n"))
            {
                return "CRLF";
            }
            else if (text.Contains("\n"))
            {
                return "LF";
            }
            else if (text.Contains("\r"))
            {
                return "CR";
            }
            else
            {
                return "Not enough lines";
            }

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

        private void ediciónToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            isDirty = true;
            UpdateTitle();
        }

        void UpdateTitle()
        {
            string name = isFileOpen ? Path.GetFileName(currentFilePath) : "Untitled";

            Text = $@"deneNotes - {name}" + (isDirty ? "*" : "");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CheckSaveBeforeContinue())
                e.Cancel = true;

        }

        private void toolStripSeparator4_Click(object sender, EventArgs e)
        {
            
        }
    }
}