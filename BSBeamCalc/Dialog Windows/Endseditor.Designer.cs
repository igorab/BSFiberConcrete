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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ends_Editor));
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.RadioButton6 = new System.Windows.Forms.RadioButton();
            this.RadioButton5 = new System.Windows.Forms.RadioButton();
            this.RadioButton4 = new System.Windows.Forms.RadioButton();
            this.RadioButton3 = new System.Windows.Forms.RadioButton();
            this.RadioButton2 = new System.Windows.Forms.RadioButton();
            this.RadioButton1 = new System.Windows.Forms.RadioButton();
            this.Button_ok = new System.Windows.Forms.Button();
            this.Button_cancel = new System.Windows.Forms.Button();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.RadioButton6);
            this.GroupBox1.Controls.Add(this.RadioButton5);
            this.GroupBox1.Controls.Add(this.RadioButton4);
            this.GroupBox1.Controls.Add(this.RadioButton3);
            this.GroupBox1.Controls.Add(this.RadioButton2);
            this.GroupBox1.Controls.Add(this.RadioButton1);
            this.GroupBox1.Location = new System.Drawing.Point(12, 12);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(283, 123);
            this.GroupBox1.TabIndex = 4;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Закрепления";
            // 
            // RadioButton6
            // 
            this.RadioButton6.AutoSize = true;
            this.RadioButton6.Location = new System.Drawing.Point(141, 52);
            this.RadioButton6.Name = "RadioButton6";
            this.RadioButton6.Size = new System.Drawing.Size(116, 17);
            this.RadioButton6.TabIndex = 5;
            this.RadioButton6.TabStop = true;
            this.RadioButton6.Text = "Шарнир-Свободно";
            this.RadioButton6.UseVisualStyleBackColor = true;
            // 
            // RadioButton5
            // 
            this.RadioButton5.AutoSize = true;
            this.RadioButton5.Location = new System.Drawing.Point(21, 83);
            this.RadioButton5.Name = "RadioButton5";
            this.RadioButton5.Size = new System.Drawing.Size(113, 17);
            this.RadioButton5.TabIndex = 4;
            this.RadioButton5.TabStop = true;
            this.RadioButton5.Text = "Жестко - Шарнир";
            this.RadioButton5.UseVisualStyleBackColor = true;
            // 
            // RadioButton4
            // 
            this.RadioButton4.AutoSize = true;
            this.RadioButton4.Location = new System.Drawing.Point(141, 84);
            this.RadioButton4.Name = "RadioButton4";
            this.RadioButton4.Size = new System.Drawing.Size(132, 17);
            this.RadioButton4.TabIndex = 3;
            this.RadioButton4.Text = "Свободно - Свободно";
            this.RadioButton4.UseVisualStyleBackColor = true;
            // 
            // RadioButton3
            // 
            this.RadioButton3.AutoSize = true;
            this.RadioButton3.Location = new System.Drawing.Point(141, 19);
            this.RadioButton3.Name = "RadioButton3";
            this.RadioButton3.Size = new System.Drawing.Size(112, 17);
            this.RadioButton3.TabIndex = 2;
            this.RadioButton3.Text = "Шарнир - Шарнир";
            this.RadioButton3.UseVisualStyleBackColor = true;
            // 
            // RadioButton2
            // 
            this.RadioButton2.AutoSize = true;
            this.RadioButton2.Location = new System.Drawing.Point(21, 51);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(123, 17);
            this.RadioButton2.TabIndex = 1;
            this.RadioButton2.Text = "Жестко - Свободно";
            this.RadioButton2.UseVisualStyleBackColor = true;
            // 
            // RadioButton1
            // 
            this.RadioButton1.AutoSize = true;
            this.RadioButton1.Checked = true;
            this.RadioButton1.Location = new System.Drawing.Point(21, 19);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(114, 17);
            this.RadioButton1.TabIndex = 0;
            this.RadioButton1.TabStop = true;
            this.RadioButton1.Text = "Жестко - Жестко";
            this.RadioButton1.UseVisualStyleBackColor = true;
            // 
            // Button_ok
            // 
            this.Button_ok.Location = new System.Drawing.Point(33, 169);
            this.Button_ok.Name = "Button_ok";
            this.Button_ok.Size = new System.Drawing.Size(75, 23);
            this.Button_ok.TabIndex = 5;
            this.Button_ok.Text = "Ok";
            this.Button_ok.UseVisualStyleBackColor = true;
            this.Button_ok.Click += new System.EventHandler(this.Button_ok_Click);
            // 
            // Button_cancel
            // 
            this.Button_cancel.Location = new System.Drawing.Point(144, 169);
            this.Button_cancel.Name = "Button_cancel";
            this.Button_cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_cancel.TabIndex = 6;
            this.Button_cancel.Text = "Cancel";
            this.Button_cancel.UseVisualStyleBackColor = true;
            this.Button_cancel.Click += new System.EventHandler(this.Button_cancel_Click);
            // 
            // Ends_Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 206);
            this.Controls.Add(this.Button_cancel);
            this.Controls.Add(this.Button_ok);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Ends_Editor";
            this.Opacity = 0.7D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Опоры - редактровать";
            this.Load += new System.EventHandler(this.Ends_Editor_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);

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