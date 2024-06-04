using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CBAnsDes
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Ends_Editor : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Ends_Editor));
            GroupBox1 = new GroupBox();
            RadioButton6 = new RadioButton();
            RadioButton5 = new RadioButton();
            RadioButton4 = new RadioButton();
            RadioButton3 = new RadioButton();
            RadioButton2 = new RadioButton();
            RadioButton1 = new RadioButton();
            Button_ok = new Button();
            Button_ok.Click += new EventHandler(Button_ok_Click);
            Button_cancel = new Button();
            Button_cancel.Click += new EventHandler(Button_cancel_Click);
            GroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // GroupBox1
            // 
            GroupBox1.Controls.Add(RadioButton6);
            GroupBox1.Controls.Add(RadioButton5);
            GroupBox1.Controls.Add(RadioButton4);
            GroupBox1.Controls.Add(RadioButton3);
            GroupBox1.Controls.Add(RadioButton2);
            GroupBox1.Controls.Add(RadioButton1);
            GroupBox1.Location = new Point(12, 12);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(245, 123);
            GroupBox1.TabIndex = 4;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "End Condition";
            // 
            // RadioButton6
            // 
            RadioButton6.AutoSize = true;
            RadioButton6.Location = new Point(131, 51);
            RadioButton6.Name = "RadioButton6";
            RadioButton6.Size = new Size(82, 17);
            RadioButton6.TabIndex = 5;
            RadioButton6.TabStop = true;
            RadioButton6.Text = "Pinned-Free";
            RadioButton6.UseVisualStyleBackColor = true;
            // 
            // RadioButton5
            // 
            RadioButton5.AutoSize = true;
            RadioButton5.Location = new Point(21, 83);
            RadioButton5.Name = "RadioButton5";
            RadioButton5.Size = new Size(92, 17);
            RadioButton5.TabIndex = 4;
            RadioButton5.TabStop = true;
            RadioButton5.Text = "Fixed - Pinned";
            RadioButton5.UseVisualStyleBackColor = true;
            // 
            // RadioButton4
            // 
            RadioButton4.AutoSize = true;
            RadioButton4.Location = new Point(131, 83);
            RadioButton4.Name = "RadioButton4";
            RadioButton4.Size = new Size(76, 17);
            RadioButton4.TabIndex = 3;
            RadioButton4.Text = "Free - Free";
            RadioButton4.UseVisualStyleBackColor = true;
            // 
            // RadioButton3
            // 
            RadioButton3.AutoSize = true;
            RadioButton3.Location = new Point(131, 18);
            RadioButton3.Name = "RadioButton3";
            RadioButton3.Size = new Size(100, 17);
            RadioButton3.TabIndex = 2;
            RadioButton3.Text = "Pinned - Pinned";
            RadioButton3.UseVisualStyleBackColor = true;
            // 
            // RadioButton2
            // 
            RadioButton2.AutoSize = true;
            RadioButton2.Location = new Point(21, 51);
            RadioButton2.Name = "RadioButton2";
            RadioButton2.Size = new Size(80, 17);
            RadioButton2.TabIndex = 1;
            RadioButton2.Text = "Fixed - Free";
            RadioButton2.UseVisualStyleBackColor = true;
            // 
            // RadioButton1
            // 
            RadioButton1.AutoSize = true;
            RadioButton1.Checked = true;
            RadioButton1.Location = new Point(21, 19);
            RadioButton1.Name = "RadioButton1";
            RadioButton1.Size = new Size(84, 17);
            RadioButton1.TabIndex = 0;
            RadioButton1.TabStop = true;
            RadioButton1.Text = "Fixed - Fixed";
            RadioButton1.UseVisualStyleBackColor = true;
            // 
            // Button_ok
            // 
            Button_ok.Location = new Point(33, 169);
            Button_ok.Name = "Button_ok";
            Button_ok.Size = new Size(75, 23);
            Button_ok.TabIndex = 5;
            Button_ok.Text = "Ok";
            Button_ok.UseVisualStyleBackColor = true;
            // 
            // Button_cancel
            // 
            Button_cancel.Location = new Point(144, 169);
            Button_cancel.Name = "Button_cancel";
            Button_cancel.Size = new Size(75, 23);
            Button_cancel.TabIndex = 6;
            Button_cancel.Text = "Cancel";
            Button_cancel.UseVisualStyleBackColor = true;
            // 
            // Ends_Editor
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(266, 204);
            Controls.Add(Button_cancel);
            Controls.Add(Button_ok);
            Controls.Add(GroupBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Ends_Editor";
            Opacity = 0.7d;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ends Editor";
            GroupBox1.ResumeLayout(false);
            GroupBox1.PerformLayout();
            Load += new EventHandler(Ends_Editor_Load);
            ResumeLayout(false);

        }
        internal GroupBox GroupBox1;
        internal RadioButton RadioButton5;
        internal RadioButton RadioButton4;
        internal RadioButton RadioButton3;
        internal RadioButton RadioButton2;
        internal RadioButton RadioButton1;

        internal RadioButton RadioButton6;
        internal Button Button_ok;
        internal Button Button_cancel;
    }
}