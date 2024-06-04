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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(addmember));
            Label1 = new Label();
            TextBox1 = new TextBox();
            TextBox1.TextChanged += new EventHandler(TextBox1_TextChanged);
            TextBox3 = new TextBox();
            TextBox3.TextChanged += new EventHandler(TextBox3_TextChanged);
            TextBox2 = new TextBox();
            TextBox2.TextChanged += new EventHandler(TextBox2_TextChanged);
            Label6 = new Label();
            Label5 = new Label();
            TextBox6 = new TextBox();
            Label4 = new Label();
            Button_ok = new Button();
            Button_ok.Click += new EventHandler(Button_ok_Click);
            Button_cancel = new Button();
            Button_cancel.Click += new EventHandler(Button_cancel_Click);
            SuspendLayout();
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Location = new Point(10, 16);
            Label1.Name = "Label1";
            Label1.Size = new Size(74, 13);
            Label1.TabIndex = 0;
            Label1.Text = "Span Length :";
            // 
            // TextBox1
            // 
            TextBox1.Location = new Point(173, 14);
            TextBox1.Name = "TextBox1";
            TextBox1.Size = new Size(70, 20);
            TextBox1.TabIndex = 1;
            TextBox1.Text = "4";
            // 
            // TextBox3
            // 
            TextBox3.Location = new Point(173, 63);
            TextBox3.Margin = new Padding(2, 2, 2, 2);
            TextBox3.Name = "TextBox3";
            TextBox3.Size = new Size(71, 20);
            TextBox3.TabIndex = 16;
            TextBox3.Text = "1";
            // 
            // TextBox2
            // 
            TextBox2.Location = new Point(173, 37);
            TextBox2.Margin = new Padding(2, 2, 2, 2);
            TextBox2.Name = "TextBox2";
            TextBox2.Size = new Size(70, 20);
            TextBox2.TabIndex = 15;
            TextBox2.Text = "20000";
            // 
            // Label6
            // 
            Label6.AutoSize = true;
            Label6.Location = new Point(9, 65);
            Label6.Margin = new Padding(2, 0, 2, 0);
            Label6.Name = "Label6";
            Label6.Size = new Size(101, 13);
            Label6.TabIndex = 14;
            Label6.Text = "Moment of Inertia I :";
            // 
            // Label5
            // 
            Label5.AutoSize = true;
            Label5.Location = new Point(9, 40);
            Label5.Margin = new Padding(2, 0, 2, 0);
            Label5.Name = "Label5";
            Label5.Size = new Size(119, 13);
            Label5.TabIndex = 13;
            Label5.Text = "Modulus of Elasticity E :";
            // 
            // TextBox6
            // 
            TextBox6.Location = new Point(173, 89);
            TextBox6.Margin = new Padding(2, 2, 2, 2);
            TextBox6.Name = "TextBox6";
            TextBox6.Size = new Size(69, 20);
            TextBox6.TabIndex = 18;
            TextBox6.Text = "0";
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.Location = new Point(9, 92);
            Label4.Margin = new Padding(2, 0, 2, 0);
            Label4.Name = "Label4";
            Label4.Size = new Size(161, 13);
            Label4.TabIndex = 17;
            Label4.Text = "Shear deformation g = (EI/ksAG)";
            // 
            // Button_ok
            // 
            Button_ok.Location = new Point(35, 141);
            Button_ok.Name = "Button_ok";
            Button_ok.Size = new Size(75, 23);
            Button_ok.TabIndex = 19;
            Button_ok.Text = "Ok";
            Button_ok.UseVisualStyleBackColor = true;
            // 
            // Button_cancel
            // 
            Button_cancel.Location = new Point(167, 141);
            Button_cancel.Name = "Button_cancel";
            Button_cancel.Size = new Size(75, 23);
            Button_cancel.TabIndex = 20;
            Button_cancel.Text = "Cancel";
            Button_cancel.UseVisualStyleBackColor = true;
            // 
            // addmember
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(268, 176);
            Controls.Add(Button_cancel);
            Controls.Add(Button_ok);
            Controls.Add(TextBox6);
            Controls.Add(Label4);
            Controls.Add(TextBox3);
            Controls.Add(TextBox2);
            Controls.Add(Label6);
            Controls.Add(Label5);
            Controls.Add(TextBox1);
            Controls.Add(Label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "addmember";
            Opacity = 0.7d;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add member";
            ResumeLayout(false);
            PerformLayout();

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