namespace Internet
{
    partial class ExtendedWLANInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            button1 = new Button();
            rssi = new Label();
            strength = new Label();
            uspeed = new Label();
            dspeed = new Label();
            band = new Label();
            ssid = new Label();
            button2 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(40, 20);
            label1.TabIndex = 0;
            label1.Text = "SSID";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 29);
            label2.Name = "label2";
            label2.Size = new Size(43, 20);
            label2.TabIndex = 1;
            label2.Text = "Band";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 49);
            label3.Name = "label3";
            label3.Size = new Size(122, 20);
            label3.TabIndex = 2;
            label3.Text = "Download speed";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 69);
            label4.Name = "label4";
            label4.Size = new Size(102, 20);
            label4.TabIndex = 3;
            label4.Text = "Upload speed";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 89);
            label5.Name = "label5";
            label5.Size = new Size(108, 20);
            label5.TabIndex = 4;
            label5.Text = "Signal strength";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 109);
            label6.Name = "label6";
            label6.Size = new Size(34, 20);
            label6.TabIndex = 5;
            label6.Text = "Rssi";
            // 
            // button1
            // 
            button1.Location = new Point(12, 154);
            button1.Name = "button1";
            button1.Size = new Size(258, 28);
            button1.TabIndex = 6;
            button1.Text = "Ok";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // rssi
            // 
            rssi.AutoSize = true;
            rssi.Location = new Point(142, 109);
            rssi.Name = "rssi";
            rssi.Size = new Size(17, 20);
            rssi.TabIndex = 12;
            rssi.Text = "0";
            // 
            // strength
            // 
            strength.AutoSize = true;
            strength.Location = new Point(142, 89);
            strength.Name = "strength";
            strength.Size = new Size(45, 20);
            strength.TabIndex = 11;
            strength.Text = "100%";
            // 
            // uspeed
            // 
            uspeed.AutoSize = true;
            uspeed.Location = new Point(142, 69);
            uspeed.Name = "uspeed";
            uspeed.Size = new Size(66, 20);
            uspeed.TabIndex = 10;
            uspeed.Text = "70 Mbps";
            // 
            // dspeed
            // 
            dspeed.AutoSize = true;
            dspeed.Location = new Point(142, 49);
            dspeed.Name = "dspeed";
            dspeed.Size = new Size(66, 20);
            dspeed.TabIndex = 9;
            dspeed.Text = "70 Mbps";
            // 
            // band
            // 
            band.AutoSize = true;
            band.Location = new Point(142, 29);
            band.Name = "band";
            band.Size = new Size(60, 20);
            band.TabIndex = 8;
            band.Text = "2.4 GHz";
            // 
            // ssid
            // 
            ssid.AutoSize = true;
            ssid.Location = new Point(142, 9);
            ssid.Name = "ssid";
            ssid.Size = new Size(34, 20);
            ssid.TabIndex = 7;
            ssid.Text = "ssid";
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.Popup;
            button2.Font = new Font("Material Icons", 9F);
            button2.Location = new Point(250, 5);
            button2.Name = "button2";
            button2.Size = new Size(26, 24);
            button2.TabIndex = 13;
            button2.Text = "refresh";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 2000;
            timer1.Tick += timer1_Tick;
            // 
            // ExtendedWLANInfo
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button1;
            ClientSize = new Size(282, 194);
            ControlBox = false;
            Controls.Add(button2);
            Controls.Add(rssi);
            Controls.Add(strength);
            Controls.Add(uspeed);
            Controls.Add(dspeed);
            Controls.Add(band);
            Controls.Add(ssid);
            Controls.Add(button1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ExtendedWLANInfo";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Extended information";
            Load += ExtendedWLANInfo_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button button1;
        private Label rssi;
        private Label strength;
        private Label uspeed;
        private Label dspeed;
        private Label band;
        private Label ssid;
        private Button button2;
        private System.Windows.Forms.Timer timer1;
    }
}