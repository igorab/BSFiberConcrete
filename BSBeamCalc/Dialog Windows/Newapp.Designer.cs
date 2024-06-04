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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Newapp));
            Label1 = new Label();
            Label2 = new Label();
            Label3 = new Label();
            GroupBox1 = new GroupBox();
            RadioButton6 = new RadioButton();
            RadioButton6.CheckedChanged += new EventHandler(RadioButton6_CheckedChanged);
            RadioButton5 = new RadioButton();
            RadioButton4 = new RadioButton();
            RadioButton4.CheckedChanged += new EventHandler(RadioButton4_CheckedChanged);
            RadioButton3 = new RadioButton();
            RadioButton2 = new RadioButton();
            RadioButton1 = new RadioButton();
            TextBox1 = new TextBox();
            TextBox1.TextChanged += new EventHandler(TextBox1_TextChanged);
            TextBox2 = new TextBox();
            TextBox2.TextChanged += new EventHandler(TextBox2_TextChanged);
            TextBox3 = new TextBox();
            TextBox3.TextChanged += new EventHandler(TextBox3_TextChanged);
            Label5 = new Label();
            Label6 = new Label();
            TextBox4 = new TextBox();
            TextBox4.TextChanged += new EventHandler(TextBox4_TextChanged);
            TextBox5 = new TextBox();
            TextBox5.TextChanged += new EventHandler(TextBox5_TextChanged);
            Label4 = new Label();
            TextBox6 = new TextBox();
            Button_Ok = new Button();
            Button_Ok.Click += new EventHandler(Button_Ok_Click);
            Button_cancel = new Button();
            Button_cancel.Click += new EventHandler(Button_cancel_Click);
            GroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Location = new Point(38, 15);
            Label1.Name = "Label1";
            Label1.Size = new Size(84, 13);
            Label1.TabIndex = 0;
            Label1.Text = "Name Of Work :" + '\r' + '\n';
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Location = new Point(38, 41);
            Label2.Name = "Label2";
            Label2.Size = new Size(114, 13);
            Label2.TabIndex = 1;
            Label2.Text = "Total Length of beam :";
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.Location = new Point(39, 69);
            Label3.Name = "Label3";
            Label3.Size = new Size(73, 13);
            Label3.TabIndex = 2;
            Label3.Text = "No., of Span :";
            // 
            // GroupBox1
            // 
            GroupBox1.Controls.Add(RadioButton6);
            GroupBox1.Controls.Add(RadioButton5);
            GroupBox1.Controls.Add(RadioButton4);
            GroupBox1.Controls.Add(RadioButton3);
            GroupBox1.Controls.Add(RadioButton2);
            GroupBox1.Controls.Add(RadioButton1);
            GroupBox1.Location = new Point(40, 185);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(245, 123);
            GroupBox1.TabIndex = 3;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "End Condition";
            // 
            // RadioButton6
            // 
            RadioButton6.AutoSize = true;
            RadioButton6.Location = new Point(131, 51);
            RadioButton6.Name = "RadioButton6";
            RadioButton6.Size = new Size(88, 17);
            RadioButton6.TabIndex = 5;
            RadioButton6.TabStop = true;
            RadioButton6.Text = "Pinned - Free";
            RadioButton6.UseVisualStyleBackColor = true;
            // 
            // RadioButton5
            // 
            RadioButton5.AutoSize = true;
            RadioButton5.Location = new Point(21, 85);
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
            RadioButton4.Location = new Point(131, 85);
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
            // TextBox1
            // 
            TextBox1.Location = new Point(178, 12);
            TextBox1.Name = "TextBox1";
            TextBox1.Size = new Size(70, 20);
            TextBox1.TabIndex = 5;
            TextBox1.Text = "Beam1";
            // 
            // TextBox2
            // 
            TextBox2.Location = new Point(179, 39);
            TextBox2.Name = "TextBox2";
            TextBox2.Size = new Size(69, 20);
            TextBox2.TabIndex = 6;
            TextBox2.Text = "10";
            // 
            // TextBox3
            // 
            TextBox3.Location = new Point(178, 67);
            TextBox3.Name = "TextBox3";
            TextBox3.Size = new Size(70, 20);
            TextBox3.TabIndex = 7;
            TextBox3.Text = "1";
            // 
            // Label5
            // 
            Label5.AutoSize = true;
            Label5.Location = new Point(38, 97);
            Label5.Margin = new Padding(2, 0, 2, 0);
            Label5.Name = "Label5";
            Label5.Size = new Size(119, 13);
            Label5.TabIndex = 9;
            Label5.Text = "Modulus of Elasticity E :";
            // 
            // Label6
            // 
            Label6.AutoSize = true;
            Label6.Location = new Point(38, 122);
            Label6.Margin = new Padding(2, 0, 2, 0);
            Label6.Name = "Label6";
            Label6.Size = new Size(101, 13);
            Label6.TabIndex = 10;
            Label6.Text = "Moment of Inertia I :";
            // 
            // TextBox4
            // 
            TextBox4.Location = new Point(178, 94);
            TextBox4.Margin = new Padding(2, 2, 2, 2);
            TextBox4.Name = "TextBox4";
            TextBox4.Size = new Size(70, 20);
            TextBox4.TabIndex = 11;
            TextBox4.Text = "20000";
            // 
            // TextBox5
            // 
            TextBox5.Location = new Point(178, 119);
            TextBox5.Margin = new Padding(2, 2, 2, 2);
            TextBox5.Name = "TextBox5";
            TextBox5.Size = new Size(71, 20);
            TextBox5.TabIndex = 12;
            TextBox5.Text = "1";
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.Location = new Point(9, 150);
            Label4.Margin = new Padding(2, 0, 2, 0);
            Label4.Name = "Label4";
            Label4.Size = new Size(161, 13);
            Label4.TabIndex = 13;
            Label4.Text = "Shear deformation g = (EI/ksAG)";
            // 
            // TextBox6
            // 
            TextBox6.Location = new Point(179, 148);
            TextBox6.Margin = new Padding(2, 2, 2, 2);
            TextBox6.Name = "TextBox6";
            TextBox6.Size = new Size(69, 20);
            TextBox6.TabIndex = 14;
            TextBox6.Text = "0";
            // 
            // Button_Ok
            // 
            Button_Ok.Location = new Point(37, 328);
            Button_Ok.Name = "Button_Ok";
            Button_Ok.Size = new Size(75, 23);
            Button_Ok.TabIndex = 15;
            Button_Ok.Text = "Ok";
            Button_Ok.UseVisualStyleBackColor = true;
            // 
            // Button_cancel
            // 
            Button_cancel.Location = new Point(184, 328);
            Button_cancel.Name = "Button_cancel";
            Button_cancel.Size = new Size(75, 23);
            Button_cancel.TabIndex = 16;
            Button_cancel.Text = "Cancel";
            Button_cancel.UseVisualStyleBackColor = true;
            // 
            // Newapp
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(316, 363);
            Controls.Add(Button_cancel);
            Controls.Add(Button_Ok);
            Controls.Add(TextBox6);
            Controls.Add(Label4);
            Controls.Add(TextBox5);
            Controls.Add(TextBox4);
            Controls.Add(Label6);
            Controls.Add(Label5);
            Controls.Add(TextBox3);
            Controls.Add(TextBox2);
            Controls.Add(TextBox1);
            Controls.Add(GroupBox1);
            Controls.Add(Label3);
            Controls.Add(Label2);
            Controls.Add(Label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Newapp";
            Opacity = 0.7d;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "New";
            GroupBox1.ResumeLayout(false);
            GroupBox1.PerformLayout();
            Load += new EventHandler(Newapp_Load);
            ResumeLayout(false);
            PerformLayout();

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