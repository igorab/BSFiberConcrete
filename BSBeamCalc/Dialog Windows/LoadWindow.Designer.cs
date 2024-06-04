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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadWindow));
            TabControl1 = new TabControl();
            TabPage1 = new TabPage();
            Label12 = new Label();
            Button7 = new Button();
            Button7.Click += new EventHandler(Button7_Click);
            Button8 = new Button();
            Button8.Click += new EventHandler(Button8_Click);
            TextBox1 = new TextBox();
            TextBox1.TextChanged += new EventHandler(TextBox1_TextChanged);
            TrackBar1 = new TrackBar();
            TrackBar1.Scroll += new EventHandler(TrackBar1_Scroll);
            Label5 = new Label();
            Label4 = new Label();
            Label1 = new Label();
            TabPage2 = new TabPage();
            Label14 = new Label();
            Label13 = new Label();
            Button4 = new Button();
            Button4.Click += new EventHandler(Button4_Click);
            Button2 = new Button();
            Button2.Click += new EventHandler(Button2_Click);
            TextBox4 = new TextBox();
            TextBox4.TextChanged += new EventHandler(TextBox4_TextChanged);
            TrackBar4 = new TrackBar();
            TrackBar4.Scroll += new EventHandler(TrackBar4_Scroll);
            Label10 = new Label();
            Label11 = new Label();
            TextBox3 = new TextBox();
            TextBox3.TextChanged += new EventHandler(TextBox3_TextChanged);
            TrackBar3 = new TrackBar();
            TrackBar3.Scroll += new EventHandler(TrackBar3_Scroll);
            Label8 = new Label();
            Label9 = new Label();
            Label2 = new Label();
            TabPage3 = new TabPage();
            Label15 = new Label();
            Button3 = new Button();
            Button3.Click += new EventHandler(Button3_Click);
            Button9 = new Button();
            Button9.Click += new EventHandler(Button9_Click);
            RadioButton2 = new RadioButton();
            RadioButton2.CheckedChanged += new EventHandler(RadioButton2_CheckedChanged);
            RadioButton1 = new RadioButton();
            RadioButton1.CheckedChanged += new EventHandler(RadioButton1_CheckedChanged);
            TextBox2 = new TextBox();
            TrackBar2 = new TrackBar();
            TrackBar2.Scroll += new EventHandler(TrackBar2_Scroll);
            Label6 = new Label();
            Label7 = new Label();
            Label3 = new Label();
            Panel1 = new Panel();
            Panel1.Paint += new PaintEventHandler(Panel1_Paint);
            PictureBox1 = new PictureBox();
            MenuStrip1 = new MenuStrip();
            EditToolStripMenuItem = new ToolStripMenuItem();
            UndoToolStripMenuItem = new ToolStripMenuItem();
            UndoToolStripMenuItem.Click += new EventHandler(UndoToolStripMenuItem_Click);
            TabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TrackBar1).BeginInit();
            TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TrackBar4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TrackBar3).BeginInit();
            TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TrackBar2).BeginInit();
            Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            MenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // TabControl1
            // 
            TabControl1.Controls.Add(TabPage1);
            TabControl1.Controls.Add(TabPage2);
            TabControl1.Controls.Add(TabPage3);
            TabControl1.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            TabControl1.Location = new Point(3, 204);
            TabControl1.Margin = new Padding(4);
            TabControl1.Multiline = true;
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(463, 225);
            TabControl1.TabIndex = 0;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = SystemColors.ButtonFace;
            TabPage1.Controls.Add(Label12);
            TabPage1.Controls.Add(Button7);
            TabPage1.Controls.Add(Button8);
            TabPage1.Controls.Add(TextBox1);
            TabPage1.Controls.Add(TrackBar1);
            TabPage1.Controls.Add(Label5);
            TabPage1.Controls.Add(Label4);
            TabPage1.Controls.Add(Label1);
            TabPage1.ForeColor = Color.Green;
            TabPage1.Location = new Point(4, 26);
            TabPage1.Margin = new Padding(4);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(4);
            TabPage1.Size = new Size(455, 195);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "Point Load";
            // 
            // Label12
            // 
            Label12.AutoSize = true;
            Label12.Location = new Point(350, 52);
            Label12.Name = "Label12";
            Label12.Size = new Size(17, 17);
            Label12.TabIndex = 21;
            Label12.Text = "0";
            // 
            // Button7
            // 
            Button7.Location = new Point(373, 160);
            Button7.Name = "Button7";
            Button7.Size = new Size(75, 23);
            Button7.TabIndex = 19;
            Button7.Text = "Modify";
            Button7.UseVisualStyleBackColor = true;
            // 
            // Button8
            // 
            Button8.Location = new Point(289, 160);
            Button8.Name = "Button8";
            Button8.Size = new Size(75, 23);
            Button8.TabIndex = 18;
            Button8.Text = "ADD";
            Button8.UseVisualStyleBackColor = true;
            // 
            // TextBox1
            // 
            TextBox1.Location = new Point(86, 101);
            TextBox1.Name = "TextBox1";
            TextBox1.Size = new Size(54, 24);
            TextBox1.TabIndex = 6;
            // 
            // TrackBar1
            // 
            TrackBar1.BackColor = SystemColors.ButtonFace;
            TrackBar1.Location = new Point(84, 42);
            TrackBar1.Name = "TrackBar1";
            TrackBar1.Size = new Size(260, 56);
            TrackBar1.TabIndex = 3;
            TrackBar1.TickStyle = TickStyle.TopLeft;
            // 
            // Label5
            // 
            Label5.AutoSize = true;
            Label5.Location = new Point(7, 104);
            Label5.Name = "Label5";
            Label5.Size = new Size(76, 17);
            Label5.TabIndex = 2;
            Label5.Text = "Intensity:";
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.Location = new Point(10, 52);
            Label4.Name = "Label4";
            Label4.Size = new Size(73, 17);
            Label4.TabIndex = 1;
            Label4.Text = "Location:";
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Font = new Font("Verdana", 11.25f, FontStyle.Underline, GraphicsUnit.Point, 0);
            Label1.Location = new Point(7, 4);
            Label1.Name = "Label1";
            Label1.Size = new Size(111, 23);
            Label1.TabIndex = 0;
            Label1.Text = "Point Load";
            // 
            // TabPage2
            // 
            TabPage2.BackColor = SystemColors.ButtonFace;
            TabPage2.Controls.Add(Label14);
            TabPage2.Controls.Add(Label13);
            TabPage2.Controls.Add(Button4);
            TabPage2.Controls.Add(Button2);
            TabPage2.Controls.Add(TextBox4);
            TabPage2.Controls.Add(TrackBar4);
            TabPage2.Controls.Add(Label10);
            TabPage2.Controls.Add(Label11);
            TabPage2.Controls.Add(TextBox3);
            TabPage2.Controls.Add(TrackBar3);
            TabPage2.Controls.Add(Label8);
            TabPage2.Controls.Add(Label9);
            TabPage2.Controls.Add(Label2);
            TabPage2.ForeColor = Color.DeepPink;
            TabPage2.Location = new Point(4, 26);
            TabPage2.Margin = new Padding(4);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new Padding(4);
            TabPage2.Size = new Size(455, 195);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "UVL";
            // 
            // Label14
            // 
            Label14.AutoSize = true;
            Label14.Location = new Point(350, 125);
            Label14.Name = "Label14";
            Label14.Size = new Size(17, 17);
            Label14.TabIndex = 19;
            Label14.Text = "0";
            // 
            // Label13
            // 
            Label13.AutoSize = true;
            Label13.Location = new Point(350, 51);
            Label13.Name = "Label13";
            Label13.Size = new Size(17, 17);
            Label13.TabIndex = 18;
            Label13.Text = "0";
            // 
            // Button4
            // 
            Button4.Location = new Point(373, 158);
            Button4.Name = "Button4";
            Button4.Size = new Size(75, 23);
            Button4.TabIndex = 16;
            Button4.Text = "Modify";
            Button4.UseVisualStyleBackColor = true;
            // 
            // Button2
            // 
            Button2.Location = new Point(289, 158);
            Button2.Name = "Button2";
            Button2.Size = new Size(75, 23);
            Button2.TabIndex = 15;
            Button2.Text = "ADD";
            Button2.UseVisualStyleBackColor = true;
            // 
            // TextBox4
            // 
            TextBox4.Location = new Point(97, 156);
            TextBox4.Name = "TextBox4";
            TextBox4.Size = new Size(54, 24);
            TextBox4.TabIndex = 14;
            // 
            // TrackBar4
            // 
            TrackBar4.Location = new Point(86, 115);
            TrackBar4.Name = "TrackBar4";
            TrackBar4.Size = new Size(258, 56);
            TrackBar4.TabIndex = 13;
            TrackBar4.TickStyle = TickStyle.TopLeft;
            // 
            // Label10
            // 
            Label10.AutoSize = true;
            Label10.Location = new Point(9, 163);
            Label10.Name = "Label10";
            Label10.Size = new Size(85, 17);
            Label10.TabIndex = 12;
            Label10.Text = "EIntensity:";
            // 
            // Label11
            // 
            Label11.AutoSize = true;
            Label11.Location = new Point(12, 125);
            Label11.Name = "Label11";
            Label11.Size = new Size(82, 17);
            Label11.TabIndex = 11;
            Label11.Text = "ELocation:";
            // 
            // TextBox3
            // 
            TextBox3.Location = new Point(95, 86);
            TextBox3.Name = "TextBox3";
            TextBox3.Size = new Size(54, 24);
            TextBox3.TabIndex = 10;
            // 
            // TrackBar3
            // 
            TrackBar3.Location = new Point(84, 41);
            TrackBar3.Name = "TrackBar3";
            TrackBar3.Size = new Size(260, 56);
            TrackBar3.TabIndex = 9;
            TrackBar3.TickStyle = TickStyle.TopLeft;
            // 
            // Label8
            // 
            Label8.AutoSize = true;
            Label8.Location = new Point(7, 89);
            Label8.Name = "Label8";
            Label8.Size = new Size(86, 17);
            Label8.TabIndex = 8;
            Label8.Text = "SIntensity:";
            // 
            // Label9
            // 
            Label9.AutoSize = true;
            Label9.Location = new Point(10, 51);
            Label9.Name = "Label9";
            Label9.Size = new Size(83, 17);
            Label9.TabIndex = 7;
            Label9.Text = "SLocation:";
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Font = new Font("Verdana", 12.0f, FontStyle.Underline, GraphicsUnit.Point, 0);
            Label2.Location = new Point(7, 4);
            Label2.Name = "Label2";
            Label2.Size = new Size(250, 25);
            Label2.TabIndex = 0;
            Label2.Text = "Uniformly Varying Load";
            // 
            // TabPage3
            // 
            TabPage3.BackColor = SystemColors.ButtonFace;
            TabPage3.Controls.Add(Label15);
            TabPage3.Controls.Add(Button3);
            TabPage3.Controls.Add(Button9);
            TabPage3.Controls.Add(RadioButton2);
            TabPage3.Controls.Add(RadioButton1);
            TabPage3.Controls.Add(TextBox2);
            TabPage3.Controls.Add(TrackBar2);
            TabPage3.Controls.Add(Label6);
            TabPage3.Controls.Add(Label7);
            TabPage3.Controls.Add(Label3);
            TabPage3.ForeColor = Color.Orange;
            TabPage3.Location = new Point(4, 26);
            TabPage3.Margin = new Padding(4);
            TabPage3.Name = "TabPage3";
            TabPage3.Padding = new Padding(4);
            TabPage3.Size = new Size(455, 195);
            TabPage3.TabIndex = 2;
            TabPage3.Text = "Moment";
            // 
            // Label15
            // 
            Label15.AutoSize = true;
            Label15.Location = new Point(352, 55);
            Label15.Name = "Label15";
            Label15.Size = new Size(17, 17);
            Label15.TabIndex = 21;
            Label15.Text = "0";
            // 
            // Button3
            // 
            Button3.Location = new Point(373, 160);
            Button3.Name = "Button3";
            Button3.Size = new Size(75, 23);
            Button3.TabIndex = 19;
            Button3.Text = "Modify";
            Button3.UseVisualStyleBackColor = true;
            // 
            // Button9
            // 
            Button9.Location = new Point(292, 160);
            Button9.Name = "Button9";
            Button9.Size = new Size(75, 23);
            Button9.TabIndex = 18;
            Button9.Text = "ADD";
            Button9.UseVisualStyleBackColor = true;
            // 
            // RadioButton2
            // 
            RadioButton2.AutoSize = true;
            RadioButton2.Location = new Point(338, 112);
            RadioButton2.Name = "RadioButton2";
            RadioButton2.Size = new Size(100, 21);
            RadioButton2.TabIndex = 13;
            RadioButton2.Text = "ClockWise";
            RadioButton2.UseVisualStyleBackColor = true;
            // 
            // RadioButton1
            // 
            RadioButton1.AutoSize = true;
            RadioButton1.Checked = true;
            RadioButton1.Location = new Point(187, 112);
            RadioButton1.Name = "RadioButton1";
            RadioButton1.Size = new Size(140, 21);
            RadioButton1.TabIndex = 12;
            RadioButton1.TabStop = true;
            RadioButton1.Text = "Anti -ClockWise";
            RadioButton1.UseVisualStyleBackColor = true;
            // 
            // TextBox2
            // 
            TextBox2.Location = new Point(88, 108);
            TextBox2.Name = "TextBox2";
            TextBox2.Size = new Size(54, 24);
            TextBox2.TabIndex = 10;
            // 
            // TrackBar2
            // 
            TrackBar2.Location = new Point(86, 45);
            TrackBar2.Name = "TrackBar2";
            TrackBar2.Size = new Size(260, 56);
            TrackBar2.TabIndex = 9;
            TrackBar2.TickStyle = TickStyle.TopLeft;
            // 
            // Label6
            // 
            Label6.AutoSize = true;
            Label6.Location = new Point(9, 111);
            Label6.Name = "Label6";
            Label6.Size = new Size(76, 17);
            Label6.TabIndex = 8;
            Label6.Text = "Intensity:";
            // 
            // Label7
            // 
            Label7.AutoSize = true;
            Label7.Location = new Point(12, 55);
            Label7.Name = "Label7";
            Label7.Size = new Size(73, 17);
            Label7.TabIndex = 7;
            Label7.Text = "Location:";
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.Font = new Font("Verdana", 11.25f, FontStyle.Underline, GraphicsUnit.Point, 0);
            Label3.Location = new Point(7, 4);
            Label3.Name = "Label3";
            Label3.Size = new Size(86, 23);
            Label3.TabIndex = 0;
            Label3.Text = "Moment";
            // 
            // Panel1
            // 
            Panel1.BackColor = Color.WhiteSmoke;
            Panel1.BorderStyle = BorderStyle.Fixed3D;
            Panel1.Controls.Add(PictureBox1);
            Panel1.Controls.Add(MenuStrip1);
            Panel1.Location = new Point(3, 3);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(460, 200);
            Panel1.TabIndex = 1;
            // 
            // PictureBox1
            // 
            PictureBox1.BackColor = Color.Transparent;
            PictureBox1.Dock = DockStyle.Fill;
            PictureBox1.Enabled = false;
            PictureBox1.Location = new Point(0, 0);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(456, 196);
            PictureBox1.TabIndex = 1;
            PictureBox1.TabStop = false;
            // 
            // MenuStrip1
            // 
            MenuStrip1.Dock = DockStyle.Bottom;
            MenuStrip1.Items.AddRange(new ToolStripItem[] { EditToolStripMenuItem });
            MenuStrip1.Location = new Point(0, 172);
            MenuStrip1.Name = "MenuStrip1";
            MenuStrip1.Size = new Size(456, 24);
            MenuStrip1.TabIndex = 0;
            MenuStrip1.Text = "MenuStrip1";
            MenuStrip1.Visible = false;
            // 
            // EditToolStripMenuItem
            // 
            EditToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { UndoToolStripMenuItem });
            EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            EditToolStripMenuItem.Size = new Size(47, 20);
            EditToolStripMenuItem.Text = "Edit";
            // 
            // UndoToolStripMenuItem
            // 
            UndoToolStripMenuItem.Name = "UndoToolStripMenuItem";
            UndoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            UndoToolStripMenuItem.Size = new Size(165, 24);
            UndoToolStripMenuItem.Text = "Undo";
            // 
            // LoadWindow
            // 
            AutoScaleDimensions = new SizeF(10.0f, 18.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(469, 429);
            Controls.Add(Panel1);
            Controls.Add(TabControl1);
            Font = new Font("Verdana", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "LoadWindow";
            Opacity = 0.92d;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoadWindow";
            TabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TrackBar1).EndInit();
            TabPage2.ResumeLayout(false);
            TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TrackBar4).EndInit();
            ((System.ComponentModel.ISupportInitialize)TrackBar3).EndInit();
            TabPage3.ResumeLayout(false);
            TabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TrackBar2).EndInit();
            Panel1.ResumeLayout(false);
            Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            MenuStrip1.ResumeLayout(false);
            MenuStrip1.PerformLayout();
            Load += new EventHandler(LoadWindow_Load);
            ResumeLayout(false);

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