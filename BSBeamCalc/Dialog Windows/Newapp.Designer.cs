using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CBAnsDes
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Newapp : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Newapp));
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.RadioButton6 = new System.Windows.Forms.RadioButton();
            this.RadioButton5 = new System.Windows.Forms.RadioButton();
            this.RadioButton4 = new System.Windows.Forms.RadioButton();
            this.RadioButton3 = new System.Windows.Forms.RadioButton();
            this.RadioButton2 = new System.Windows.Forms.RadioButton();
            this.RadioButton1 = new System.Windows.Forms.RadioButton();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.TextBox3 = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.TextBox4 = new System.Windows.Forms.TextBox();
            this.TextBox5 = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.TextBox6 = new System.Windows.Forms.TextBox();
            this.Button_Ok = new System.Windows.Forms.Button();
            this.Button_cancel = new System.Windows.Forms.Button();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(38, 15);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(89, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Наименование :\r\n";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(38, 41);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(117, 13);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Полная длина балки :";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(39, 69);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(73, 13);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "No., of Span :";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.RadioButton6);
            this.GroupBox1.Controls.Add(this.RadioButton5);
            this.GroupBox1.Controls.Add(this.RadioButton4);
            this.GroupBox1.Controls.Add(this.RadioButton3);
            this.GroupBox1.Controls.Add(this.RadioButton2);
            this.GroupBox1.Controls.Add(this.RadioButton1);
            this.GroupBox1.Location = new System.Drawing.Point(40, 185);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(245, 123);
            this.GroupBox1.TabIndex = 3;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "End Condition";
            // 
            // RadioButton6
            // 
            this.RadioButton6.AutoSize = true;
            this.RadioButton6.Location = new System.Drawing.Point(131, 51);
            this.RadioButton6.Name = "RadioButton6";
            this.RadioButton6.Size = new System.Drawing.Size(88, 17);
            this.RadioButton6.TabIndex = 5;
            this.RadioButton6.TabStop = true;
            this.RadioButton6.Text = "Pinned - Free";
            this.RadioButton6.UseVisualStyleBackColor = true;
            this.RadioButton6.CheckedChanged += new System.EventHandler(this.RadioButton6_CheckedChanged);
            // 
            // RadioButton5
            // 
            this.RadioButton5.AutoSize = true;
            this.RadioButton5.Location = new System.Drawing.Point(21, 85);
            this.RadioButton5.Name = "RadioButton5";
            this.RadioButton5.Size = new System.Drawing.Size(92, 17);
            this.RadioButton5.TabIndex = 4;
            this.RadioButton5.TabStop = true;
            this.RadioButton5.Text = "Fixed - Pinned";
            this.RadioButton5.UseVisualStyleBackColor = true;
            // 
            // RadioButton4
            // 
            this.RadioButton4.AutoSize = true;
            this.RadioButton4.Location = new System.Drawing.Point(131, 85);
            this.RadioButton4.Name = "RadioButton4";
            this.RadioButton4.Size = new System.Drawing.Size(76, 17);
            this.RadioButton4.TabIndex = 3;
            this.RadioButton4.Text = "Free - Free";
            this.RadioButton4.UseVisualStyleBackColor = true;
            this.RadioButton4.CheckedChanged += new System.EventHandler(this.RadioButton4_CheckedChanged);
            // 
            // RadioButton3
            // 
            this.RadioButton3.AutoSize = true;
            this.RadioButton3.Location = new System.Drawing.Point(131, 18);
            this.RadioButton3.Name = "RadioButton3";
            this.RadioButton3.Size = new System.Drawing.Size(100, 17);
            this.RadioButton3.TabIndex = 2;
            this.RadioButton3.Text = "Pinned - Pinned";
            this.RadioButton3.UseVisualStyleBackColor = true;
            // 
            // RadioButton2
            // 
            this.RadioButton2.AutoSize = true;
            this.RadioButton2.Location = new System.Drawing.Point(21, 51);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(80, 17);
            this.RadioButton2.TabIndex = 1;
            this.RadioButton2.Text = "Fixed - Free";
            this.RadioButton2.UseVisualStyleBackColor = true;
            // 
            // RadioButton1
            // 
            this.RadioButton1.AutoSize = true;
            this.RadioButton1.Checked = true;
            this.RadioButton1.Location = new System.Drawing.Point(21, 19);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(84, 17);
            this.RadioButton1.TabIndex = 0;
            this.RadioButton1.TabStop = true;
            this.RadioButton1.Text = "Fixed - Fixed";
            this.RadioButton1.UseVisualStyleBackColor = true;
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(178, 12);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(70, 20);
            this.TextBox1.TabIndex = 5;
            this.TextBox1.Text = "Beam1";
            this.TextBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new System.Drawing.Point(179, 39);
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(69, 20);
            this.TextBox2.TabIndex = 6;
            this.TextBox2.Text = "10";
            this.TextBox2.TextChanged += new System.EventHandler(this.TextBox2_TextChanged);
            // 
            // TextBox3
            // 
            this.TextBox3.Location = new System.Drawing.Point(178, 67);
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.Size = new System.Drawing.Size(70, 20);
            this.TextBox3.TabIndex = 7;
            this.TextBox3.Text = "1";
            this.TextBox3.TextChanged += new System.EventHandler(this.TextBox3_TextChanged);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(38, 97);
            this.Label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(114, 13);
            this.Label5.TabIndex = 9;
            this.Label5.Text = "Модуль упругости E :";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(38, 122);
            this.Label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(104, 13);
            this.Label6.TabIndex = 10;
            this.Label6.Text = "Момент инерции I :";
            // 
            // TextBox4
            // 
            this.TextBox4.Location = new System.Drawing.Point(178, 94);
            this.TextBox4.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.Size = new System.Drawing.Size(70, 20);
            this.TextBox4.TabIndex = 11;
            this.TextBox4.Text = "20000";
            this.TextBox4.TextChanged += new System.EventHandler(this.TextBox4_TextChanged);
            // 
            // TextBox5
            // 
            this.TextBox5.Location = new System.Drawing.Point(178, 119);
            this.TextBox5.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox5.Name = "TextBox5";
            this.TextBox5.Size = new System.Drawing.Size(71, 20);
            this.TextBox5.TabIndex = 12;
            this.TextBox5.Text = "1";
            this.TextBox5.TextChanged += new System.EventHandler(this.TextBox5_TextChanged);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(9, 150);
            this.Label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(161, 13);
            this.Label4.TabIndex = 13;
            this.Label4.Text = "Shear deformation g = (EI/ksAG)";
            // 
            // TextBox6
            // 
            this.TextBox6.Location = new System.Drawing.Point(179, 148);
            this.TextBox6.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox6.Name = "TextBox6";
            this.TextBox6.Size = new System.Drawing.Size(69, 20);
            this.TextBox6.TabIndex = 14;
            this.TextBox6.Text = "0";
            // 
            // Button_Ok
            // 
            this.Button_Ok.Location = new System.Drawing.Point(37, 328);
            this.Button_Ok.Name = "Button_Ok";
            this.Button_Ok.Size = new System.Drawing.Size(75, 23);
            this.Button_Ok.TabIndex = 15;
            this.Button_Ok.Text = "Ok";
            this.Button_Ok.UseVisualStyleBackColor = true;
            this.Button_Ok.Click += new System.EventHandler(this.Button_Ok_Click);
            // 
            // Button_cancel
            // 
            this.Button_cancel.Location = new System.Drawing.Point(184, 328);
            this.Button_cancel.Name = "Button_cancel";
            this.Button_cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_cancel.TabIndex = 16;
            this.Button_cancel.Text = "Cancel";
            this.Button_cancel.UseVisualStyleBackColor = true;
            this.Button_cancel.Click += new System.EventHandler(this.Button_cancel_Click);
            // 
            // Newapp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 363);
            this.Controls.Add(this.Button_cancel);
            this.Controls.Add(this.Button_Ok);
            this.Controls.Add(this.TextBox6);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.TextBox5);
            this.Controls.Add(this.TextBox4);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.TextBox3);
            this.Controls.Add(this.TextBox2);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Newapp";
            this.Opacity = 0.7D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Новый расчет";
            this.Load += new System.EventHandler(this.Newapp_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal Label Label1;
        internal Label Label2;
        internal Label Label3;
        internal GroupBox GroupBox1;

        internal RadioButton RadioButton4;
        internal RadioButton RadioButton3;
        internal RadioButton RadioButton2;
        internal RadioButton RadioButton1;
        internal TextBox TextBox1;
        internal TextBox TextBox2;
        internal TextBox TextBox3;
        internal RadioButton RadioButton5;
        internal RadioButton RadioButton6;
        internal Label Label5;
        internal Label Label6;
        internal TextBox TextBox4;
        internal TextBox TextBox5;
        internal Label Label4;
        internal TextBox TextBox6;
        internal Button Button_Ok;
        internal Button Button_cancel;
    }
}