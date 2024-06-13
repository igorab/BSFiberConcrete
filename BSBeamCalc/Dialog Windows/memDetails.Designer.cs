using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CBAnsDes
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class memDetails : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is not null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(memDetails));
            TextBox1 = new TextBox();
            SuspendLayout();
            // 
            // TextBox1
            // 
            TextBox1.Dock = DockStyle.Fill;
            TextBox1.Font = new Font("Verdana", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            TextBox1.ForeColor = Color.DarkGreen;
            TextBox1.Location = new Point(0, 0);
            TextBox1.Margin = new Padding(4);
            TextBox1.Multiline = true;
            TextBox1.Name = "TextBox1";
            TextBox1.ScrollBars = ScrollBars.Both;
            TextBox1.Size = new Size(920, 537);
            TextBox1.TabIndex = 0;
            TextBox1.Text = "-------------------------------------------------" + '\r' + '\n';
            TextBox1.WordWrap = false;
            // 
            // memDetails
            // 
            AutoScaleDimensions = new SizeF(8.0f, 16.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(920, 537);
            Controls.Add(TextBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "memDetails";
            ShowInTaskbar = false;
            Text = "Matrix Details";
            Load += new EventHandler(memDetails_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal TextBox TextBox1;
    }
}