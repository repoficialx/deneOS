using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneOS
{
    internal class exMgmt
    {
        public class exception : Form
        {
            public static string msg = "";
            #region initialization
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
            private void InitializeComponent()
            {
                //this.components = new System.ComponentModel.Container();
                this.message = new System.Windows.Forms.Label();
                this.title = new System.Windows.Forms.Label();
                this.ok = new System.Windows.Forms.Button();
                //((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
                this.SuspendLayout();
                // 
                // message
                // 
                this.message.AutoSize = true;
                this.message.ForeColor = System.Drawing.SystemColors.Control;
                this.message.Location = new System.Drawing.Point(0, 35);
                this.message.Name = "message";
                this.message.Size = new System.Drawing.Size(66, 32);
                this.message.TabIndex = 11;
                this.message.Text = msg;
                // 
                // ok
                // 
                this.ok.BackColor = System.Drawing.SystemColors.ControlText;
                this.ok.ForeColor = System.Drawing.SystemColors.Window;
                this.ok.Location = new System.Drawing.Point(5, 262);
                this.ok.Name = "boxregusr";
                this.ok.Size = new System.Drawing.Size(790, 39);
                this.ok.TabIndex = 10;
                // 
                // title
                // 
                this.title.AutoSize = true;
                this.title.ForeColor = System.Drawing.SystemColors.Control;
                this.title.Location = new System.Drawing.Point(0, 0);
                this.title.Name = "message";
                this.title.Size = new System.Drawing.Size(66, 32);
                this.title.TabIndex = 11;
                this.title.Text = msg;
                // 
                // exception
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
                this.BackColor = System.Drawing.SystemColors.ControlText;
                this.ClientSize = new System.Drawing.Size(800, 305);
                this.Controls.Add(this.message);
                this.Controls.Add(this.title);
                this.Controls.Add(this.ok);
                this.Font = new System.Drawing.Font("Segoe UI", 12F);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.ForeColor = System.Drawing.SystemColors.Control;
                this.Name = "exception";
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                this.Text = "Exception Management | deneOS";
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
                this.ResumeLayout(false);
                this.PerformLayout();

            }
            private System.Windows.Forms.Label message;
            private System.Windows.Forms.Label title;
            private System.Windows.Forms.Button ok;

            #endregion
            public exception(string message, string title, string buttonText, Action buttonAction)
            {
                InitializeComponent();
                this.message.Text = message;
                this.title.Text = title;
                this.ok.Text = buttonText;
                this.ok.Click += (sender, e) =>
                {
                    buttonAction?.Invoke();
                    this.Close();
                };
            }
            public exception(bool critical, string errorcode)
            {
                if (critical)
                {
                    EmergencyScreen _ = new EmergencyScreen(errorcode);
                    _.Show();
                }
            }
        }
    }
}
