namespace deneFiles
{/*
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.backToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deneossysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parentDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 32);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(18, 16);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(711, 328);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("file:///C:/DNUSR", System.UriKind.Absolute);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backToolStripMenuItem,
            this.forwardToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.deneossysToolStripMenuItem,
            this.userToolStripMenuItem,
            this.parentDirectoryToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(711, 32);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // backToolStripMenuItem
            // 
            this.backToolStripMenuItem.Image = global::deneFiles.Properties.Resources.icons8_back_100;
            this.backToolStripMenuItem.Name = "backToolStripMenuItem";
            this.backToolStripMenuItem.Size = new System.Drawing.Size(78, 28);
            this.backToolStripMenuItem.Text = "back";
            this.backToolStripMenuItem.Click += new System.EventHandler(this.backToolStripMenuItem_Click);
            // 
            // forwardToolStripMenuItem
            // 
            this.forwardToolStripMenuItem.Image = global::deneFiles.Properties.Resources.icons8_forward_100;
            this.forwardToolStripMenuItem.Name = "forwardToolStripMenuItem";
            this.forwardToolStripMenuItem.Size = new System.Drawing.Size(99, 28);
            this.forwardToolStripMenuItem.Text = "forward";
            this.forwardToolStripMenuItem.Click += new System.EventHandler(this.forwardToolStripMenuItem_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Image = global::deneFiles.Properties.Resources.icons8_rotate_100;
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(90, 28);
            this.reloadToolStripMenuItem.Text = "reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // deneossysToolStripMenuItem
            // 
            this.deneossysToolStripMenuItem.Image = global::deneFiles.Properties.Resources.icons8_software_100;
            this.deneossysToolStripMenuItem.Name = "deneossysToolStripMenuItem";
            this.deneossysToolStripMenuItem.Size = new System.Drawing.Size(106, 28);
            this.deneossysToolStripMenuItem.Text = "Software";
            this.deneossysToolStripMenuItem.Click += new System.EventHandler(this.deneossysToolStripMenuItem_Click);
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.Image = global::deneFiles.Properties.Resources.icons8_group_100__1_;
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(76, 28);
            this.userToolStripMenuItem.Text = "User";
            this.userToolStripMenuItem.Click += new System.EventHandler(this.userToolStripMenuItem_Click);
            // 
            // parentDirectoryToolStripMenuItem
            // 
            this.parentDirectoryToolStripMenuItem.Image = global::deneFiles.Properties.Resources.icons8_prev_100;
            this.parentDirectoryToolStripMenuItem.Name = "parentDirectoryToolStripMenuItem";
            this.parentDirectoryToolStripMenuItem.Size = new System.Drawing.Size(153, 28);
            this.parentDirectoryToolStripMenuItem.Text = "parent directory";
            this.parentDirectoryToolStripMenuItem.Click += new System.EventHandler(this.parentDirectoryToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(711, 360);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "deneFiles";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem backToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forwardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deneossysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parentDirectoryToolStripMenuItem;
    }*/

    partial class Form1
    {
        private SplitContainer splitContainer1;
        private TreeView treeView1;
        private ListView listView1;
        private ImageList imageList1;
        private ToolStrip toolStrip1;
        private ToolStripButton backButton;
        private ToolStripButton forwardButton;
        private ToolStripLabel pathLabel;

        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            treeView1 = new TreeView();
            listView1 = new ListView();
            imageList1 = new ImageList();
            toolStrip1 = new ToolStrip();
            backButton = new ToolStripButton("⬅");
            forwardButton = new ToolStripButton("➡");
            pathLabel = new ToolStripLabel();

            toolStrip1.Items.Add(backButton);
            toolStrip1.Items.Add(forwardButton);
            toolStrip1.Items.Add(new ToolStripSeparator());
            toolStrip1.Items.Add(pathLabel);

            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Panel1.Controls.Add(treeView1);
            splitContainer1.Panel2.Controls.Add(listView1);

            treeView1.Dock = DockStyle.Fill;
            listView1.Dock = DockStyle.Fill;

            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.SmallImageList = imageList1;
            listView1.LargeImageList = imageList1;

            listView1.Columns.Add("Nombre", 250);
            listView1.Columns.Add("Tipo", 120);
            listView1.Columns.Add("Tamaño", 100);
            listView1.Columns.Add("Modificado", 150);

            Controls.Add(splitContainer1);
            Controls.Add(toolStrip1);

            toolStrip1.Dock = DockStyle.Top;

            Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            Text = "deneFiles";
            Width = 900;
            Height = 600;
        }
    }
}

