using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CBAnsDes
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class addmember : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(addmember));
            this.Label1 = new System.Windows.Forms.Label();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.TextBox3 = new System.Windows.Forms.TextBox();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.TextBox6 = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Button_ok = new System.Windows.Forms.Button();
            this.Button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(10, 16);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(90, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Длина пролета :";
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(173, 14);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(70, 20);
            this.TextBox1.TabIndex = 1;
            this.TextBox1.Text = "4";
            this.TextBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // TextBox3
            // 
            this.TextBox3.Location = new System.Drawing.Point(173, 63);
            this.TextBox3.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.Size = new System.Drawing.Size(71, 20);
            this.TextBox3.TabIndex = 16;
            this.TextBox3.Text = "1";
            this.TextBox3.TextChanged += new System.EventHandler(this.TextBox3_TextChanged);
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new System.Drawing.Point(173, 37);
            this.TextBox2.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(70, 20);
            this.TextBox2.TabIndex = 15;
            this.TextBox2.Text = "20000";
            this.TextBox2.TextChanged += new System.EventHandler(this.TextBox2_TextChanged);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(9, 65);
            this.Label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(104, 13);
            this.Label6.TabIndex = 14;
            this.Label6.Text = "Момент инерции I :";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(9, 40);
            this.Label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(114, 13);
            this.Label5.TabIndex = 13;
            this.Label5.Text = "Модуль упругости E :";
            // 
            // TextBox6
            // 
            this.TextBox6.Location = new System.Drawing.Point(173, 89);
            this.TextBox6.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox6.Name = "TextBox6";
            this.TextBox6.Size = new System.Drawing.Size(69, 20);
            this.TextBox6.TabIndex = 18;
            this.TextBox6.Text = "0";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(9, 92);
            this.Label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(161, 13);
            this.Label4.TabIndex = 17;
            this.Label4.Text = "Shear deformation g = (EI/ksAG)";
            // 
            // Button_ok
            // 
            this.Button_ok.Location = new System.Drawing.Point(35, 141);
            this.Button_ok.Name = "Button_ok";
            this.Button_ok.Size = new System.Drawing.Size(75, 23);
            this.Button_ok.TabIndex = 19;
            this.Button_ok.Text = "Ok";
            this.Button_ok.UseVisualStyleBackColor = true;
            this.Button_ok.Click += new System.EventHandler(this.Button_ok_Click);
            // 
            // Button_cancel
            // 
            this.Button_cancel.Location = new System.Drawing.Point(167, 141);
            this.Button_cancel.Name = "Button_cancel";
            this.Button_cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_cancel.TabIndex = 20;
            this.Button_cancel.Text = "Cancel";
            this.Button_cancel.UseVisualStyleBackColor = true;
            this.Button_cancel.Click += new System.EventHandler(this.Button_cancel_Click);
            // 
            // addmember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 176);
            this.Controls.Add(this.Button_cancel);
            this.Controls.Add(this.Button_ok);
            this.Controls.Add(this.TextBox6);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.TextBox3);
            this.Controls.Add(this.TextBox2);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "addmember";
            this.Opacity = 0.7D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить пролет";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal Label Label1;
        internal TextBox TextBox1;

        internal TextBox TextBox3;
        internal TextBox TextBox2;
        internal Label Label6;
        internal Label Label5;
        internal TextBox TextBox6;
        internal Label Label4;
        internal Button Button_ok;
        internal Button Button_cancel;
    }
}