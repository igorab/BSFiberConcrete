using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CBAnsDes
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class LoadWindow : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadWindow));
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Label12 = new System.Windows.Forms.Label();
            this.Button7 = new System.Windows.Forms.Button();
            this.Button8 = new System.Windows.Forms.Button();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.TrackBar1 = new System.Windows.Forms.TrackBar();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Button4 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.TextBox4 = new System.Windows.Forms.TextBox();
            this.TrackBar4 = new System.Windows.Forms.TrackBar();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.TextBox3 = new System.Windows.Forms.TextBox();
            this.TrackBar3 = new System.Windows.Forms.TrackBar();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Label15 = new System.Windows.Forms.Label();
            this.Button3 = new System.Windows.Forms.Button();
            this.Button9 = new System.Windows.Forms.Button();
            this.RadioButton2 = new System.Windows.Forms.RadioButton();
            this.RadioButton1 = new System.Windows.Forms.RadioButton();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.TrackBar2 = new System.Windows.Forms.TrackBar();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UndoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar3)).BeginInit();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar2)).BeginInit();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.MenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Controls.Add(this.TabPage3);
            this.TabControl1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControl1.Location = new System.Drawing.Point(3, 204);
            this.TabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.TabControl1.Multiline = true;
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(463, 225);
            this.TabControl1.TabIndex = 0;
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.TabPage1.Controls.Add(this.Label12);
            this.TabPage1.Controls.Add(this.Button7);
            this.TabPage1.Controls.Add(this.Button8);
            this.TabPage1.Controls.Add(this.TextBox1);
            this.TabPage1.Controls.Add(this.TrackBar1);
            this.TabPage1.Controls.Add(this.Label5);
            this.TabPage1.Controls.Add(this.Label4);
            this.TabPage1.Controls.Add(this.Label1);
            this.TabPage1.ForeColor = System.Drawing.Color.Green;
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.TabPage1.Size = new System.Drawing.Size(455, 199);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Сосредоточенная сила";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(350, 52);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(14, 13);
            this.Label12.TabIndex = 21;
            this.Label12.Text = "0";
            // 
            // Button7
            // 
            this.Button7.Location = new System.Drawing.Point(373, 160);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(75, 23);
            this.Button7.TabIndex = 19;
            this.Button7.Text = "Изменить";
            this.Button7.UseVisualStyleBackColor = true;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // Button8
            // 
            this.Button8.Location = new System.Drawing.Point(289, 160);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(75, 23);
            this.Button8.TabIndex = 18;
            this.Button8.Text = "Добавить";
            this.Button8.UseVisualStyleBackColor = true;
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(86, 101);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(54, 21);
            this.TextBox1.TabIndex = 6;
            this.TextBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // TrackBar1
            // 
            this.TrackBar1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.TrackBar1.Location = new System.Drawing.Point(84, 42);
            this.TrackBar1.Name = "TrackBar1";
            this.TrackBar1.Size = new System.Drawing.Size(260, 45);
            this.TrackBar1.TabIndex = 3;
            this.TrackBar1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.TrackBar1.Scroll += new System.EventHandler(this.TrackBar1_Scroll);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(7, 104);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(62, 13);
            this.Label5.TabIndex = 2;
            this.Label5.Text = "Intensity:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(10, 52);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(59, 13);
            this.Label4.TabIndex = 1;
            this.Label4.Text = "Location:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(7, 4);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(86, 18);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Point Load";
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.TabPage2.Controls.Add(this.Label14);
            this.TabPage2.Controls.Add(this.Label13);
            this.TabPage2.Controls.Add(this.Button4);
            this.TabPage2.Controls.Add(this.Button2);
            this.TabPage2.Controls.Add(this.TextBox4);
            this.TabPage2.Controls.Add(this.TrackBar4);
            this.TabPage2.Controls.Add(this.Label10);
            this.TabPage2.Controls.Add(this.Label11);
            this.TabPage2.Controls.Add(this.TextBox3);
            this.TabPage2.Controls.Add(this.TrackBar3);
            this.TabPage2.Controls.Add(this.Label8);
            this.TabPage2.Controls.Add(this.Label9);
            this.TabPage2.Controls.Add(this.Label2);
            this.TabPage2.ForeColor = System.Drawing.Color.DeepPink;
            this.TabPage2.Location = new System.Drawing.Point(4, 22);
            this.TabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.TabPage2.Size = new System.Drawing.Size(455, 199);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "UVL";
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(350, 125);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(14, 13);
            this.Label14.TabIndex = 19;
            this.Label14.Text = "0";
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(350, 51);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(14, 13);
            this.Label13.TabIndex = 18;
            this.Label13.Text = "0";
            // 
            // Button4
            // 
            this.Button4.Location = new System.Drawing.Point(373, 158);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(75, 23);
            this.Button4.TabIndex = 16;
            this.Button4.Text = "Modify";
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(289, 158);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(75, 23);
            this.Button2.TabIndex = 15;
            this.Button2.Text = "ADD";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // TextBox4
            // 
            this.TextBox4.Location = new System.Drawing.Point(97, 156);
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.Size = new System.Drawing.Size(54, 21);
            this.TextBox4.TabIndex = 14;
            this.TextBox4.TextChanged += new System.EventHandler(this.TextBox4_TextChanged);
            // 
            // TrackBar4
            // 
            this.TrackBar4.Location = new System.Drawing.Point(86, 115);
            this.TrackBar4.Name = "TrackBar4";
            this.TrackBar4.Size = new System.Drawing.Size(258, 45);
            this.TrackBar4.TabIndex = 13;
            this.TrackBar4.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.TrackBar4.Scroll += new System.EventHandler(this.TrackBar4_Scroll);
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(9, 163);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(69, 13);
            this.Label10.TabIndex = 12;
            this.Label10.Text = "EIntensity:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(12, 125);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(66, 13);
            this.Label11.TabIndex = 11;
            this.Label11.Text = "ELocation:";
            // 
            // TextBox3
            // 
            this.TextBox3.Location = new System.Drawing.Point(95, 86);
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.Size = new System.Drawing.Size(54, 21);
            this.TextBox3.TabIndex = 10;
            this.TextBox3.TextChanged += new System.EventHandler(this.TextBox3_TextChanged);
            // 
            // TrackBar3
            // 
            this.TrackBar3.Location = new System.Drawing.Point(84, 41);
            this.TrackBar3.Name = "TrackBar3";
            this.TrackBar3.Size = new System.Drawing.Size(260, 45);
            this.TrackBar3.TabIndex = 9;
            this.TrackBar3.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.TrackBar3.Scroll += new System.EventHandler(this.TrackBar3_Scroll);
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(7, 89);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(70, 13);
            this.Label8.TabIndex = 8;
            this.Label8.Text = "SIntensity:";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(10, 51);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(67, 13);
            this.Label9.TabIndex = 7;
            this.Label9.Text = "SLocation:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(7, 4);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(197, 18);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "Uniformly Varying Load";
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.TabPage3.Controls.Add(this.Label15);
            this.TabPage3.Controls.Add(this.Button3);
            this.TabPage3.Controls.Add(this.Button9);
            this.TabPage3.Controls.Add(this.RadioButton2);
            this.TabPage3.Controls.Add(this.RadioButton1);
            this.TabPage3.Controls.Add(this.TextBox2);
            this.TabPage3.Controls.Add(this.TrackBar2);
            this.TabPage3.Controls.Add(this.Label6);
            this.TabPage3.Controls.Add(this.Label7);
            this.TabPage3.Controls.Add(this.Label3);
            this.TabPage3.ForeColor = System.Drawing.Color.Orange;
            this.TabPage3.Location = new System.Drawing.Point(4, 22);
            this.TabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.TabPage3.Size = new System.Drawing.Size(455, 199);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Изгибающий момент";
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(352, 55);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(14, 13);
            this.Label15.TabIndex = 21;
            this.Label15.Text = "0";
            // 
            // Button3
            // 
            this.Button3.Location = new System.Drawing.Point(373, 160);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(75, 23);
            this.Button3.TabIndex = 19;
            this.Button3.Text = "Modify";
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Button9
            // 
            this.Button9.Location = new System.Drawing.Point(292, 160);
            this.Button9.Name = "Button9";
            this.Button9.Size = new System.Drawing.Size(75, 23);
            this.Button9.TabIndex = 18;
            this.Button9.Text = "ADD";
            this.Button9.UseVisualStyleBackColor = true;
            this.Button9.Click += new System.EventHandler(this.Button9_Click);
            // 
            // RadioButton2
            // 
            this.RadioButton2.AutoSize = true;
            this.RadioButton2.Location = new System.Drawing.Point(338, 112);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(84, 17);
            this.RadioButton2.TabIndex = 13;
            this.RadioButton2.Text = "ClockWise";
            this.RadioButton2.UseVisualStyleBackColor = true;
            this.RadioButton2.CheckedChanged += new System.EventHandler(this.RadioButton2_CheckedChanged);
            // 
            // RadioButton1
            // 
            this.RadioButton1.AutoSize = true;
            this.RadioButton1.Checked = true;
            this.RadioButton1.Location = new System.Drawing.Point(187, 112);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(115, 17);
            this.RadioButton1.TabIndex = 12;
            this.RadioButton1.TabStop = true;
            this.RadioButton1.Text = "Anti -ClockWise";
            this.RadioButton1.UseVisualStyleBackColor = true;
            this.RadioButton1.CheckedChanged += new System.EventHandler(this.RadioButton1_CheckedChanged);
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new System.Drawing.Point(88, 108);
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(54, 21);
            this.TextBox2.TabIndex = 10;
            // 
            // TrackBar2
            // 
            this.TrackBar2.Location = new System.Drawing.Point(86, 45);
            this.TrackBar2.Name = "TrackBar2";
            this.TrackBar2.Size = new System.Drawing.Size(260, 45);
            this.TrackBar2.TabIndex = 9;
            this.TrackBar2.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.TrackBar2.Scroll += new System.EventHandler(this.TrackBar2_Scroll);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(9, 111);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(62, 13);
            this.Label6.TabIndex = 8;
            this.Label6.Text = "Intensity:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(12, 55);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(59, 13);
            this.Label7.TabIndex = 7;
            this.Label7.Text = "Location:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(7, 4);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(70, 18);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "Moment";
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Panel1.Controls.Add(this.PictureBox1);
            this.Panel1.Controls.Add(this.MenuStrip1);
            this.Panel1.Location = new System.Drawing.Point(3, 3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(460, 200);
            this.Panel1.TabIndex = 1;
            this.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1_Paint);
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox1.Enabled = false;
            this.PictureBox1.Location = new System.Drawing.Point(0, 0);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(456, 196);
            this.PictureBox1.TabIndex = 1;
            this.PictureBox1.TabStop = false;
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditToolStripMenuItem});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 172);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(456, 24);
            this.MenuStrip1.TabIndex = 0;
            this.MenuStrip1.Text = "MenuStrip1";
            this.MenuStrip1.Visible = false;
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UndoToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.EditToolStripMenuItem.Text = "Edit";
            // 
            // UndoToolStripMenuItem
            // 
            this.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem";
            this.UndoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.UndoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.UndoToolStripMenuItem.Text = "Undo";
            this.UndoToolStripMenuItem.Click += new System.EventHandler(this.UndoToolStripMenuItem_Click);
            // 
            // LoadWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 429);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.TabControl1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "LoadWindow";
            this.Opacity = 0.92D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Загружение";
            this.Load += new System.EventHandler(this.LoadWindow_Load);
            this.TabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar3)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar2)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }
        internal TabControl TabControl1;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal Label Label1;
        internal Label Label2;
        internal TrackBar TrackBar1;
        internal Label Label5;
        internal Label Label4;
        internal Label Label3;
        internal Panel Panel1;
        internal TextBox TextBox1;
        internal TextBox TextBox2;
        internal TrackBar TrackBar2;
        internal Label Label6;
        internal Label Label7;
        internal TextBox TextBox4;
        internal TrackBar TrackBar4;
        internal Label Label10;
        internal Label Label11;
        internal TextBox TextBox3;
        internal TrackBar TrackBar3;
        internal Label Label8;
        internal Label Label9;
        internal Button Button2;
        internal RadioButton RadioButton2;
        internal RadioButton RadioButton1;
        internal Button Button7;
        internal Button Button8;
        internal Button Button4;
        internal Button Button3;
        internal Button Button9;
        internal Label Label12;
        internal Label Label14;
        internal Label Label13;
        internal Label Label15;
        internal MenuStrip MenuStrip1;
        internal ToolStripMenuItem EditToolStripMenuItem;
        internal ToolStripMenuItem UndoToolStripMenuItem;
        internal PictureBox PictureBox1;
    }
}