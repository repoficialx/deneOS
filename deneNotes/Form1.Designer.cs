namespace deneNotes
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            textBox1 = new TextBox();
            menuStrip1 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            nuevoToolStripMenuItem = new ToolStripMenuItem();
            ventanaNuevaToolStripMenuItem = new ToolStripMenuItem();
            abrirToolStripMenuItem = new ToolStripMenuItem();
            guardarToolStripMenuItem = new ToolStripMenuItem();
            guardarComoToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            configurarPáginaToolStripMenuItem = new ToolStripMenuItem();
            imprimirToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            salirToolStripMenuItem = new ToolStripMenuItem();
            ediciónToolStripMenuItem = new ToolStripMenuItem();
            deshacerToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            cortarToolStripMenuItem = new ToolStripMenuItem();
            copiarToolStripMenuItem = new ToolStripMenuItem();
            pegarToolStripMenuItem = new ToolStripMenuItem();
            eliminarToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            búsquedaConBingToolStripMenuItem = new ToolStripMenuItem();
            buscarToolStripMenuItem = new ToolStripMenuItem();
            buscarSiguienteToolStripMenuItem = new ToolStripMenuItem();
            buscarAnteriorToolStripMenuItem = new ToolStripMenuItem();
            reemplazarToolStripMenuItem = new ToolStripMenuItem();
            irAToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            seleccionarTodoToolStripMenuItem = new ToolStripMenuItem();
            horaYFechaToolStripMenuItem = new ToolStripMenuItem();
            formatoToolStripMenuItem = new ToolStripMenuItem();
            ajusteDeLíneaToolStripMenuItem = new ToolStripMenuItem();
            fuenteToolStripMenuItem = new ToolStripMenuItem();
            verToolStripMenuItem = new ToolStripMenuItem();
            zoomToolStripMenuItem = new ToolStripMenuItem();
            acercarToolStripMenuItem = new ToolStripMenuItem();
            alejarToolStripMenuItem = new ToolStripMenuItem();
            restaurarAlPredeterminadoToolStripMenuItem = new ToolStripMenuItem();
            barraDeEstadoToolStripMenuItem = new ToolStripMenuItem();
            ayudaToolStripMenuItem = new ToolStripMenuItem();
            verLaAyudaToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            acercaDeDeneNoteToolStripMenuItem = new ToolStripMenuItem();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            toolStripStatusLabel4 = new ToolStripStatusLabel();
            fontDialog1 = new FontDialog();
            timer1 = new System.Windows.Forms.Timer(components);
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Fill;
            textBox1.Location = new Point(0, 24);
            textBox1.Margin = new Padding(2);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(569, 264);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.KeyDown += textBox1_KeyDown;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem, ediciónToolStripMenuItem, formatoToolStripMenuItem, verToolStripMenuItem, ayudaToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(4, 2, 0, 2);
            menuStrip1.Size = new Size(569, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { nuevoToolStripMenuItem, ventanaNuevaToolStripMenuItem, abrirToolStripMenuItem, guardarToolStripMenuItem, guardarComoToolStripMenuItem, toolStripSeparator1, configurarPáginaToolStripMenuItem, imprimirToolStripMenuItem, toolStripSeparator2, salirToolStripMenuItem });
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(37, 20);
            archivoToolStripMenuItem.Text = "&File";
            // 
            // nuevoToolStripMenuItem
            // 
            nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            nuevoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            nuevoToolStripMenuItem.Size = new Size(229, 22);
            nuevoToolStripMenuItem.Text = "&New";
            nuevoToolStripMenuItem.Click += nuevoToolStripMenuItem_Click;
            // 
            // ventanaNuevaToolStripMenuItem
            // 
            ventanaNuevaToolStripMenuItem.Name = "ventanaNuevaToolStripMenuItem";
            ventanaNuevaToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Mayús+N";
            ventanaNuevaToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.N;
            ventanaNuevaToolStripMenuItem.Size = new Size(229, 22);
            ventanaNuevaToolStripMenuItem.Text = "New &window";
            ventanaNuevaToolStripMenuItem.Click += ventanaNuevaToolStripMenuItem_Click;
            // 
            // abrirToolStripMenuItem
            // 
            abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            abrirToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            abrirToolStripMenuItem.Size = new Size(229, 22);
            abrirToolStripMenuItem.Text = "&Open...";
            abrirToolStripMenuItem.Click += abrirToolStripMenuItem_Click;
            // 
            // guardarToolStripMenuItem
            // 
            guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            guardarToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            guardarToolStripMenuItem.Size = new Size(229, 22);
            guardarToolStripMenuItem.Text = "&Save";
            guardarToolStripMenuItem.Click += guardarToolStripMenuItem_Click;
            // 
            // guardarComoToolStripMenuItem
            // 
            guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            guardarComoToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Mayús+S";
            guardarComoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            guardarComoToolStripMenuItem.Size = new Size(229, 22);
            guardarComoToolStripMenuItem.Text = "S&ave as...";
            guardarComoToolStripMenuItem.Click += guardarComoToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(226, 6);
            // 
            // configurarPáginaToolStripMenuItem
            // 
            configurarPáginaToolStripMenuItem.Enabled = false;
            configurarPáginaToolStripMenuItem.Name = "configurarPáginaToolStripMenuItem";
            configurarPáginaToolStripMenuItem.Size = new Size(229, 22);
            configurarPáginaToolStripMenuItem.Text = "Set up page..";
            // 
            // imprimirToolStripMenuItem
            // 
            imprimirToolStripMenuItem.Enabled = false;
            imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            imprimirToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.P;
            imprimirToolStripMenuItem.Size = new Size(229, 22);
            imprimirToolStripMenuItem.Text = "&Print";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(226, 6);
            // 
            // salirToolStripMenuItem
            // 
            salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            salirToolStripMenuItem.Size = new Size(229, 22);
            salirToolStripMenuItem.Text = "&Exit";
            salirToolStripMenuItem.Click += salirToolStripMenuItem_Click;
            // 
            // ediciónToolStripMenuItem
            // 
            ediciónToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { deshacerToolStripMenuItem, toolStripSeparator3, cortarToolStripMenuItem, copiarToolStripMenuItem, pegarToolStripMenuItem, eliminarToolStripMenuItem, toolStripSeparator4, búsquedaConBingToolStripMenuItem, buscarToolStripMenuItem, buscarSiguienteToolStripMenuItem, buscarAnteriorToolStripMenuItem, reemplazarToolStripMenuItem, irAToolStripMenuItem, toolStripSeparator5, seleccionarTodoToolStripMenuItem, horaYFechaToolStripMenuItem });
            ediciónToolStripMenuItem.Name = "ediciónToolStripMenuItem";
            ediciónToolStripMenuItem.Size = new Size(39, 20);
            ediciónToolStripMenuItem.Text = "&Edit";
            ediciónToolStripMenuItem.Click += ediciónToolStripMenuItem_Click;
            // 
            // deshacerToolStripMenuItem
            // 
            deshacerToolStripMenuItem.Name = "deshacerToolStripMenuItem";
            deshacerToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            deshacerToolStripMenuItem.Size = new Size(246, 22);
            deshacerToolStripMenuItem.Text = "Undo";
            deshacerToolStripMenuItem.Click += deshacerToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(243, 6);
            // 
            // cortarToolStripMenuItem
            // 
            cortarToolStripMenuItem.Name = "cortarToolStripMenuItem";
            cortarToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.X;
            cortarToolStripMenuItem.Size = new Size(246, 22);
            cortarToolStripMenuItem.Text = "Cut";
            cortarToolStripMenuItem.Click += cortarToolStripMenuItem_Click;
            // 
            // copiarToolStripMenuItem
            // 
            copiarToolStripMenuItem.Name = "copiarToolStripMenuItem";
            copiarToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            copiarToolStripMenuItem.Size = new Size(246, 22);
            copiarToolStripMenuItem.Text = "Copy";
            copiarToolStripMenuItem.Click += copiarToolStripMenuItem_Click;
            // 
            // pegarToolStripMenuItem
            // 
            pegarToolStripMenuItem.Name = "pegarToolStripMenuItem";
            pegarToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.V;
            pegarToolStripMenuItem.Size = new Size(246, 22);
            pegarToolStripMenuItem.Text = "Paste";
            pegarToolStripMenuItem.Click += pegarToolStripMenuItem_Click;
            // 
            // eliminarToolStripMenuItem
            // 
            eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            eliminarToolStripMenuItem.ShortcutKeys = Keys.Delete;
            eliminarToolStripMenuItem.Size = new Size(246, 22);
            eliminarToolStripMenuItem.Text = "Delete";
            eliminarToolStripMenuItem.Click += eliminarToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(243, 6);
            toolStripSeparator4.Click += toolStripSeparator4_Click;
            // 
            // búsquedaConBingToolStripMenuItem
            // 
            búsquedaConBingToolStripMenuItem.Name = "búsquedaConBingToolStripMenuItem";
            búsquedaConBingToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.F;
            búsquedaConBingToolStripMenuItem.Size = new Size(246, 22);
            búsquedaConBingToolStripMenuItem.Text = "Search on Bing...";
            búsquedaConBingToolStripMenuItem.Click += búsquedaConBingToolStripMenuItem_Click;
            // 
            // buscarToolStripMenuItem
            // 
            buscarToolStripMenuItem.Enabled = false;
            buscarToolStripMenuItem.Name = "buscarToolStripMenuItem";
            buscarToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.B;
            buscarToolStripMenuItem.Size = new Size(246, 22);
            buscarToolStripMenuItem.Text = "Search";
            buscarToolStripMenuItem.Click += buscarToolStripMenuItem_Click;
            // 
            // buscarSiguienteToolStripMenuItem
            // 
            buscarSiguienteToolStripMenuItem.Enabled = false;
            buscarSiguienteToolStripMenuItem.Name = "buscarSiguienteToolStripMenuItem";
            buscarSiguienteToolStripMenuItem.ShortcutKeys = Keys.F3;
            buscarSiguienteToolStripMenuItem.Size = new Size(246, 22);
            buscarSiguienteToolStripMenuItem.Text = "Search next";
            // 
            // buscarAnteriorToolStripMenuItem
            // 
            buscarAnteriorToolStripMenuItem.Enabled = false;
            buscarAnteriorToolStripMenuItem.Name = "buscarAnteriorToolStripMenuItem";
            buscarAnteriorToolStripMenuItem.ShortcutKeys = Keys.Shift | Keys.F3;
            buscarAnteriorToolStripMenuItem.Size = new Size(246, 22);
            buscarAnteriorToolStripMenuItem.Text = "Search previous";
            // 
            // reemplazarToolStripMenuItem
            // 
            reemplazarToolStripMenuItem.Enabled = false;
            reemplazarToolStripMenuItem.Name = "reemplazarToolStripMenuItem";
            reemplazarToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.R;
            reemplazarToolStripMenuItem.Size = new Size(246, 22);
            reemplazarToolStripMenuItem.Text = "Replace";
            // 
            // irAToolStripMenuItem
            // 
            irAToolStripMenuItem.Enabled = false;
            irAToolStripMenuItem.Name = "irAToolStripMenuItem";
            irAToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.T;
            irAToolStripMenuItem.Size = new Size(246, 22);
            irAToolStripMenuItem.Text = "Go to...";
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(243, 6);
            // 
            // seleccionarTodoToolStripMenuItem
            // 
            seleccionarTodoToolStripMenuItem.Name = "seleccionarTodoToolStripMenuItem";
            seleccionarTodoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.A;
            seleccionarTodoToolStripMenuItem.Size = new Size(246, 22);
            seleccionarTodoToolStripMenuItem.Text = "Select all";
            seleccionarTodoToolStripMenuItem.Click += seleccionarTodoToolStripMenuItem_Click;
            // 
            // horaYFechaToolStripMenuItem
            // 
            horaYFechaToolStripMenuItem.Name = "horaYFechaToolStripMenuItem";
            horaYFechaToolStripMenuItem.ShortcutKeys = Keys.F5;
            horaYFechaToolStripMenuItem.Size = new Size(246, 22);
            horaYFechaToolStripMenuItem.Text = "Date and time";
            horaYFechaToolStripMenuItem.Click += horaYFechaToolStripMenuItem_Click;
            // 
            // formatoToolStripMenuItem
            // 
            formatoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ajusteDeLíneaToolStripMenuItem, fuenteToolStripMenuItem });
            formatoToolStripMenuItem.Name = "formatoToolStripMenuItem";
            formatoToolStripMenuItem.Size = new Size(57, 20);
            formatoToolStripMenuItem.Text = "F&ormat";
            // 
            // ajusteDeLíneaToolStripMenuItem
            // 
            ajusteDeLíneaToolStripMenuItem.Checked = true;
            ajusteDeLíneaToolStripMenuItem.CheckState = CheckState.Checked;
            ajusteDeLíneaToolStripMenuItem.Name = "ajusteDeLíneaToolStripMenuItem";
            ajusteDeLíneaToolStripMenuItem.Size = new Size(180, 22);
            ajusteDeLíneaToolStripMenuItem.Text = "Adjust text";
            ajusteDeLíneaToolStripMenuItem.Click += ajusteDeLíneaToolStripMenuItem_Click;
            // 
            // fuenteToolStripMenuItem
            // 
            fuenteToolStripMenuItem.Name = "fuenteToolStripMenuItem";
            fuenteToolStripMenuItem.Size = new Size(180, 22);
            fuenteToolStripMenuItem.Text = "Font...";
            fuenteToolStripMenuItem.Click += fuenteToolStripMenuItem_Click;
            // 
            // verToolStripMenuItem
            // 
            verToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { zoomToolStripMenuItem, barraDeEstadoToolStripMenuItem });
            verToolStripMenuItem.Name = "verToolStripMenuItem";
            verToolStripMenuItem.Size = new Size(44, 20);
            verToolStripMenuItem.Text = "&View";
            verToolStripMenuItem.Click += verToolStripMenuItem_Click;
            // 
            // zoomToolStripMenuItem
            // 
            zoomToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { acercarToolStripMenuItem, alejarToolStripMenuItem, restaurarAlPredeterminadoToolStripMenuItem });
            zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            zoomToolStripMenuItem.Size = new Size(180, 22);
            zoomToolStripMenuItem.Text = "Zoom";
            // 
            // acercarToolStripMenuItem
            // 
            acercarToolStripMenuItem.Name = "acercarToolStripMenuItem";
            acercarToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Oemplus;
            acercarToolStripMenuItem.Size = new Size(263, 22);
            acercarToolStripMenuItem.Text = "Acercar";
            acercarToolStripMenuItem.Click += acercarToolStripMenuItem_Click;
            // 
            // alejarToolStripMenuItem
            // 
            alejarToolStripMenuItem.Name = "alejarToolStripMenuItem";
            alejarToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.OemMinus;
            alejarToolStripMenuItem.Size = new Size(263, 22);
            alejarToolStripMenuItem.Text = "Alejar";
            alejarToolStripMenuItem.Click += alejarToolStripMenuItem_Click;
            // 
            // restaurarAlPredeterminadoToolStripMenuItem
            // 
            restaurarAlPredeterminadoToolStripMenuItem.Name = "restaurarAlPredeterminadoToolStripMenuItem";
            restaurarAlPredeterminadoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D0;
            restaurarAlPredeterminadoToolStripMenuItem.Size = new Size(263, 22);
            restaurarAlPredeterminadoToolStripMenuItem.Text = "Restaurar al predeterminado";
            restaurarAlPredeterminadoToolStripMenuItem.Click += restaurarAlPredeterminadoToolStripMenuItem_Click;
            // 
            // barraDeEstadoToolStripMenuItem
            // 
            barraDeEstadoToolStripMenuItem.Checked = true;
            barraDeEstadoToolStripMenuItem.CheckState = CheckState.Checked;
            barraDeEstadoToolStripMenuItem.Name = "barraDeEstadoToolStripMenuItem";
            barraDeEstadoToolStripMenuItem.Size = new Size(180, 22);
            barraDeEstadoToolStripMenuItem.Text = "Status bar";
            barraDeEstadoToolStripMenuItem.Click += barraDeEstadoToolStripMenuItem_Click;
            // 
            // ayudaToolStripMenuItem
            // 
            ayudaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { verLaAyudaToolStripMenuItem, toolStripSeparator6, acercaDeDeneNoteToolStripMenuItem });
            ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            ayudaToolStripMenuItem.Size = new Size(44, 20);
            ayudaToolStripMenuItem.Text = "&Help";
            // 
            // verLaAyudaToolStripMenuItem
            // 
            verLaAyudaToolStripMenuItem.Enabled = false;
            verLaAyudaToolStripMenuItem.Name = "verLaAyudaToolStripMenuItem";
            verLaAyudaToolStripMenuItem.ShortcutKeys = Keys.F1;
            verLaAyudaToolStripMenuItem.Size = new Size(180, 22);
            verLaAyudaToolStripMenuItem.Text = "Help";
            verLaAyudaToolStripMenuItem.Click += verLaAyudaToolStripMenuItem_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(177, 6);
            // 
            // acercaDeDeneNoteToolStripMenuItem
            // 
            acercaDeDeneNoteToolStripMenuItem.Name = "acercaDeDeneNoteToolStripMenuItem";
            acercaDeDeneNoteToolStripMenuItem.Size = new Size(180, 22);
            acercaDeDeneNoteToolStripMenuItem.Text = "About deneNotes";
            acercaDeDeneNoteToolStripMenuItem.Click += acercaDeDeneNoteToolStripMenuItem_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "Archivos de texto|*.txt|Todos los archivos|*.*";
            openFileDialog1.FileOk += openFileDialog1_FileOk;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.FileName = "Sin título.txt";
            saveFileDialog1.Filter = "Archivos de texto|*.txt|Todos los archivos|*.*";
            saveFileDialog1.FileOk += saveFileDialog1_FileOk;
            // 
            // statusStrip1
            // 
            statusStrip1.GripStyle = ToolStripGripStyle.Visible;
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3, toolStripStatusLabel4 });
            statusStrip1.Location = new Point(0, 266);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 11, 0);
            statusStrip1.Size = new Size(569, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(96, 17);
            toolStripStatusLabel1.Text = "Line X, column Y";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(35, 17);
            toolStripStatusLabel2.Text = "100%";
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Size = new Size(94, 17);
            toolStripStatusLabel3.Text = "Windows (CRLF)";
            // 
            // toolStripStatusLabel4
            // 
            toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            toolStripStatusLabel4.Size = new Size(39, 17);
            toolStripStatusLabel4.Text = "UTF-8";
            // 
            // fontDialog1
            // 
            fontDialog1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            fontDialog1.MaxSize = 12;
            fontDialog1.MinSize = 12;
            fontDialog1.Apply += fontDialog1_Apply;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(569, 288);
            Controls.Add(statusStrip1);
            Controls.Add(textBox1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2);
            Name = "Form1";
            Text = "deneNotes";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ventanaNuevaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarComoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ediciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurarPáginaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem deshacerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cortarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copiarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pegarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem búsquedaConBingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buscarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buscarSiguienteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buscarAnteriorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reemplazarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem irAToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem seleccionarTodoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horaYFechaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajusteDeLíneaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fuenteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barraDeEstadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verLaAyudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem acercaDeDeneNoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alejarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restaurarAlPredeterminadoToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Timer timer1;
    }
}

