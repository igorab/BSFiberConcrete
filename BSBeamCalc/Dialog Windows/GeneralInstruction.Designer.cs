using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CBAnsDes
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class GeneralInstruction : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralInstruction));
            RichTextBox1 = new RichTextBox();
            RichTextBox1.TextChanged += new EventHandler(RichTextBox1_TextChanged);
            SuspendLayout();
            // 
            // RichTextBox1
            // 
            RichTextBox1.Dock = DockStyle.Fill;
            RichTextBox1.Location = new Point(0, 0);
            RichTextBox1.Margin = new Padding(3, 4, 3, 4);
            RichTextBox1.Name = "RichTextBox1";
            RichTextBox1.Size = new Size(731, 363);
            RichTextBox1.TabIndex = 0;
            RichTextBox1.Text = resources.GetString("RichTextBox1.Text");
            // 
            // GeneralInstruction
            // 
            AutoScaleDimensions = new SizeF(11.0f, 20.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(731, 363);
            Controls.Add(RichTextBox1);
            Font = new Font("Verdana", 10.2f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "GeneralInstruction";
            ShowInTaskbar = false;
            Text = "General Instruction";
            Load += new EventHandler(GeneralInstruction_Load);
            ResumeLayout(false);

        }
        internal RichTextBox RichTextBox1;
    }
}