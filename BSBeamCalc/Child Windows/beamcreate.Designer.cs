using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CBAnsDes
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class beamcreate : Form
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
            components = new System.ComponentModel.Container();
            coverpic = new Panel();
            respic = new Panel();
            respic.Paint += new PaintEventHandler(respic_Paint);
            respic.MouseEnter += new EventHandler(respic_MouseEnter);
            respic.MouseLeave += new EventHandler(respic_MouseLeave);
            respic.MouseWheel += new MouseEventHandler(respic_MouseWheel);
            respic.MouseMove += new MouseEventHandler(respic_MouseMove);
            respic.MouseClick += new MouseEventHandler(respic_MouseClick);
            respic.MouseDown += new MouseEventHandler(respic_MouseDown);
            respic.MouseUp += new MouseEventHandler(respic_MouseUp);
            respic.MouseDoubleClick += new MouseEventHandler(respic_MouseDoubleClick);
            mainpic = new Panel();
            mainpic.Paint += new PaintEventHandler(mainpic_Paint);
            mainpic.MouseClick += new MouseEventHandler(mainpic_MouseClick);
            mainpic.MouseDown += new MouseEventHandler(mainpic_MouseDown);
            mainpic.MouseUp += new MouseEventHandler(mainpic_MouseUp);
            mainpic.DoubleClick += new EventHandler(mainpic_DoubleClick);
            mainpic.MouseEnter += new EventHandler(mainpic_MouseEnter);
            mainpic.MouseLeave += new EventHandler(mainpic_MouseLeave);
            mainpic.MouseMove += new MouseEventHandler(mainpic_MouseMove);
            mainpic.MouseWheel += new MouseEventHandler(mainpic_MouseWheel);
            PictureBox1 = new PictureBox();
            ContextMenuStrip1 = new ContextMenuStrip(components);
            AddMemberToolStripMenuItem1 = new ToolStripMenuItem();
            AddMemberToolStripMenuItem1.Click += new EventHandler(AddMemberToolStripMenuItem1_Click);
            EditEndsToolStripMenuItem = new ToolStripMenuItem();
            EditEndsToolStripMenuItem.Click += new EventHandler(EditEndsToolStripMenuItem_Click);
            ToolStripSeparator9 = new ToolStripSeparator();
            EditMemebrToolStripMenuItem = new ToolStripMenuItem();
            EditMemebrToolStripMenuItem.Click += new EventHandler(EditMemebrToolStripMenuItem_Click);
            RemoveMemberToolStripMenuItem = new ToolStripMenuItem();
            RemoveMemberToolStripMenuItem.Click += new EventHandler(RemoveMemberToolStripMenuItem_Click);
            AddLoadToolStripMenuItem1 = new ToolStripMenuItem();
            AddLoadToolStripMenuItem1.Click += new EventHandler(AddLoadToolStripMenuItem1_Click);
            RemoveLoadsToolStripMenuItem = new ToolStripMenuItem();
            RemoveLoadsToolStripMenuItem.Click += new EventHandler(RemoveLoadsToolStripMenuItem_Click);
            ContextMenuStrip2 = new ContextMenuStrip(components);
            AddMemberToolStripMenuItem = new ToolStripMenuItem();
            AddMemberToolStripMenuItem.Click += new EventHandler(AddMemberToolStripMenuItem_Click);
            EditEndsToolStripMenuItem1 = new ToolStripMenuItem();
            EditEndsToolStripMenuItem1.Click += new EventHandler(EditEndsToolStripMenuItem1_Click);
            ToolStripSeparator1 = new ToolStripSeparator();
            EditLoadToolStripMenuItem = new ToolStripMenuItem();
            EditLoadToolStripMenuItem.Click += new EventHandler(EditLoadToolStripMenuItem_Click);
            RemoveLoadToolStripMenuItem = new ToolStripMenuItem();
            RemoveLoadToolStripMenuItem.Click += new EventHandler(RemoveLoadToolStripMenuItem_Click);
            ContextMenuStrip3 = new ContextMenuStrip(components);
            BendingMomentToolStripMenuItem = new ToolStripMenuItem();
            BendingMomentToolStripMenuItem.Click += new EventHandler(BendingMomentToolStripMenuItem_Click);
            ShearForceToolStripMenuItem = new ToolStripMenuItem();
            ShearForceToolStripMenuItem.Click += new EventHandler(ShearForceToolStripMenuItem_Click);
            DeflectionToolStripMenuItem = new ToolStripMenuItem();
            DeflectionToolStripMenuItem.Click += new EventHandler(DeflectionToolStripMenuItem_Click);
            SlopeToolStripMenuItem = new ToolStripMenuItem();
            SlopeToolStripMenuItem.Click += new EventHandler(SlopeToolStripMenuItem_Click);
            res_coverpic = new PictureBox();
            coverpic.SuspendLayout();
            respic.SuspendLayout();
            mainpic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            ContextMenuStrip1.SuspendLayout();
            ContextMenuStrip2.SuspendLayout();
            ContextMenuStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)res_coverpic).BeginInit();
            SuspendLayout();
            // 
            // coverpic
            // 
            coverpic.BackColor = Color.White;
            coverpic.BorderStyle = BorderStyle.Fixed3D;
            coverpic.Controls.Add(respic);
            coverpic.Controls.Add(mainpic);
            coverpic.Dock = DockStyle.Fill;
            coverpic.Location = new Point(0, 0);
            coverpic.Margin = new Padding(4);
            coverpic.Name = "coverpic";
            coverpic.Size = new Size(988, 535);
            coverpic.TabIndex = 0;
            // 
            // respic
            // 
            respic.BackColor = Color.White;
            respic.BorderStyle = BorderStyle.FixedSingle;
            respic.Controls.Add(res_coverpic);
            respic.Location = new Point(104, 373);
            respic.Margin = new Padding(4);
            respic.Name = "respic";
            respic.Size = new Size(263, 112);
            respic.TabIndex = 1;
            // 
            // mainpic
            // 
            mainpic.BackColor = Color.White;
            mainpic.BorderStyle = BorderStyle.FixedSingle;
            mainpic.Controls.Add(PictureBox1);
            mainpic.Location = new Point(12, 14);
            mainpic.Margin = new Padding(4);
            mainpic.Name = "mainpic";
            mainpic.Size = new Size(831, 317);
            mainpic.TabIndex = 0;
            // 
            // PictureBox1
            // 
            PictureBox1.BackColor = Color.Transparent;
            PictureBox1.Dock = DockStyle.Fill;
            PictureBox1.Enabled = false;
            PictureBox1.Location = new Point(0, 0);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(829, 315);
            PictureBox1.TabIndex = 0;
            PictureBox1.TabStop = false;
            // 
            // ContextMenuStrip1
            // 
            ContextMenuStrip1.ImageScalingSize = new Size(20, 20);
            ContextMenuStrip1.Items.AddRange(new ToolStripItem[] { AddMemberToolStripMenuItem1, EditEndsToolStripMenuItem, ToolStripSeparator9, EditMemebrToolStripMenuItem, RemoveMemberToolStripMenuItem, AddLoadToolStripMenuItem1, RemoveLoadsToolStripMenuItem });
            ContextMenuStrip1.Name = "ContextMenuStrip1";
            ContextMenuStrip1.Size = new Size(197, 166);
            // 
            // AddMemberToolStripMenuItem1
            // 
            AddMemberToolStripMenuItem1.Image = My.Resources.Resources.addmember;
            AddMemberToolStripMenuItem1.Name = "AddMemberToolStripMenuItem1";
            AddMemberToolStripMenuItem1.Size = new Size(196, 26);
            AddMemberToolStripMenuItem1.Text = "Add Member";
            // 
            // EditEndsToolStripMenuItem
            // 
            EditEndsToolStripMenuItem.Image = My.Resources.Resources.editends;
            EditEndsToolStripMenuItem.Name = "EditEndsToolStripMenuItem";
            EditEndsToolStripMenuItem.Size = new Size(196, 26);
            EditEndsToolStripMenuItem.Text = "Edit Ends";
            // 
            // ToolStripSeparator9
            // 
            ToolStripSeparator9.Name = "ToolStripSeparator9";
            ToolStripSeparator9.Size = new Size(193, 6);
            // 
            // EditMemebrToolStripMenuItem
            // 
            EditMemebrToolStripMenuItem.Image = My.Resources.Resources.editmember;
            EditMemebrToolStripMenuItem.Name = "EditMemebrToolStripMenuItem";
            EditMemebrToolStripMenuItem.Size = new Size(196, 26);
            EditMemebrToolStripMenuItem.Text = "Edit Memebr";
            // 
            // RemoveMemberToolStripMenuItem
            // 
            RemoveMemberToolStripMenuItem.Image = My.Resources.Resources.deletemember;
            RemoveMemberToolStripMenuItem.Name = "RemoveMemberToolStripMenuItem";
            RemoveMemberToolStripMenuItem.Size = new Size(196, 26);
            RemoveMemberToolStripMenuItem.Text = "Remove Member";
            // 
            // AddLoadToolStripMenuItem1
            // 
            AddLoadToolStripMenuItem1.Image = My.Resources.Resources.addload;
            AddLoadToolStripMenuItem1.Name = "AddLoadToolStripMenuItem1";
            AddLoadToolStripMenuItem1.Size = new Size(196, 26);
            AddLoadToolStripMenuItem1.Text = "Add Load";
            // 
            // RemoveLoadsToolStripMenuItem
            // 
            RemoveLoadsToolStripMenuItem.Image = My.Resources.Resources.removeloads;
            RemoveLoadsToolStripMenuItem.Name = "RemoveLoadsToolStripMenuItem";
            RemoveLoadsToolStripMenuItem.Size = new Size(196, 26);
            RemoveLoadsToolStripMenuItem.Text = "Remove Loads";
            // 
            // ContextMenuStrip2
            // 
            ContextMenuStrip2.ImageScalingSize = new Size(20, 20);
            ContextMenuStrip2.Items.AddRange(new ToolStripItem[] { AddMemberToolStripMenuItem, EditEndsToolStripMenuItem1, ToolStripSeparator1, EditLoadToolStripMenuItem, RemoveLoadToolStripMenuItem });
            ContextMenuStrip2.Name = "ContextMenuStrip2";
            ContextMenuStrip2.Size = new Size(174, 114);
            // 
            // AddMemberToolStripMenuItem
            // 
            AddMemberToolStripMenuItem.Image = My.Resources.Resources.addmember;
            AddMemberToolStripMenuItem.Name = "AddMemberToolStripMenuItem";
            AddMemberToolStripMenuItem.Size = new Size(173, 26);
            AddMemberToolStripMenuItem.Text = "Add Member";
            // 
            // EditEndsToolStripMenuItem1
            // 
            EditEndsToolStripMenuItem1.Image = My.Resources.Resources.editends;
            EditEndsToolStripMenuItem1.Name = "EditEndsToolStripMenuItem1";
            EditEndsToolStripMenuItem1.Size = new Size(173, 26);
            EditEndsToolStripMenuItem1.Text = "Edit Ends";
            // 
            // ToolStripSeparator1
            // 
            ToolStripSeparator1.Name = "ToolStripSeparator1";
            ToolStripSeparator1.Size = new Size(170, 6);
            // 
            // EditLoadToolStripMenuItem
            // 
            EditLoadToolStripMenuItem.Image = My.Resources.Resources.modifyload;
            EditLoadToolStripMenuItem.Name = "EditLoadToolStripMenuItem";
            EditLoadToolStripMenuItem.Size = new Size(173, 26);
            EditLoadToolStripMenuItem.Text = "Edit Load";
            // 
            // RemoveLoadToolStripMenuItem
            // 
            RemoveLoadToolStripMenuItem.Image = My.Resources.Resources.removeload;
            RemoveLoadToolStripMenuItem.Name = "RemoveLoadToolStripMenuItem";
            RemoveLoadToolStripMenuItem.Size = new Size(173, 26);
            RemoveLoadToolStripMenuItem.Text = "Remove Load";
            // 
            // ContextMenuStrip3
            // 
            ContextMenuStrip3.ImageScalingSize = new Size(20, 20);
            ContextMenuStrip3.Items.AddRange(new ToolStripItem[] { BendingMomentToolStripMenuItem, ShearForceToolStripMenuItem, DeflectionToolStripMenuItem, SlopeToolStripMenuItem });
            ContextMenuStrip3.Name = "ContextMenuStrip3";
            ContextMenuStrip3.Size = new Size(198, 108);
            // 
            // BendingMomentToolStripMenuItem
            // 
            BendingMomentToolStripMenuItem.Image = My.Resources.Resources.bm;
            BendingMomentToolStripMenuItem.Name = "BendingMomentToolStripMenuItem";
            BendingMomentToolStripMenuItem.Size = new Size(197, 26);
            BendingMomentToolStripMenuItem.Text = "Bending Moment";
            // 
            // ShearForceToolStripMenuItem
            // 
            ShearForceToolStripMenuItem.Image = My.Resources.Resources.shear;
            ShearForceToolStripMenuItem.Name = "ShearForceToolStripMenuItem";
            ShearForceToolStripMenuItem.Size = new Size(197, 26);
            ShearForceToolStripMenuItem.Text = "Shear Force";
            // 
            // DeflectionToolStripMenuItem
            // 
            DeflectionToolStripMenuItem.Image = My.Resources.Resources.bend;
            DeflectionToolStripMenuItem.Name = "DeflectionToolStripMenuItem";
            DeflectionToolStripMenuItem.Size = new Size(197, 26);
            DeflectionToolStripMenuItem.Text = "Deflection";
            // 
            // SlopeToolStripMenuItem
            // 
            SlopeToolStripMenuItem.Image = My.Resources.Resources.Slope;
            SlopeToolStripMenuItem.Name = "SlopeToolStripMenuItem";
            SlopeToolStripMenuItem.Size = new Size(197, 26);
            SlopeToolStripMenuItem.Text = "Slope";
            // 
            // res_coverpic
            // 
            res_coverpic.BackColor = Color.Transparent;
            res_coverpic.Dock = DockStyle.Fill;
            res_coverpic.Enabled = false;
            res_coverpic.Location = new Point(0, 0);
            res_coverpic.Name = "res_coverpic";
            res_coverpic.Size = new Size(261, 110);
            res_coverpic.TabIndex = 0;
            res_coverpic.TabStop = false;
            // 
            // beamcreate
            // 
            AutoScaleDimensions = new SizeF(8.0f, 16.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(988, 535);
            Controls.Add(coverpic);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            MinimumSize = new Size(533, 369);
            Name = "beamcreate";
            Opacity = 0.9d;
            Text = "beamcreate";
            coverpic.ResumeLayout(false);
            respic.ResumeLayout(false);
            mainpic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            ContextMenuStrip1.ResumeLayout(false);
            ContextMenuStrip2.ResumeLayout(false);
            ContextMenuStrip3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)res_coverpic).EndInit();
            Load += new EventHandler(beamcreate_Load);
            ResumeLayout(false);

        }
        internal Panel coverpic;
        internal Panel mainpic;
        internal ContextMenuStrip ContextMenuStrip1;
        internal ToolStripMenuItem AddMemberToolStripMenuItem1;
        internal ToolStripMenuItem EditEndsToolStripMenuItem;
        internal ToolStripSeparator ToolStripSeparator9;
        internal ToolStripMenuItem EditMemebrToolStripMenuItem;
        internal ToolStripMenuItem RemoveMemberToolStripMenuItem;
        internal ToolStripMenuItem AddLoadToolStripMenuItem1;
        internal ToolStripMenuItem RemoveLoadsToolStripMenuItem;
        internal ContextMenuStrip ContextMenuStrip2;
        internal ToolStripMenuItem EditLoadToolStripMenuItem;
        internal ToolStripMenuItem RemoveLoadToolStripMenuItem;
        internal Panel respic;
        internal ToolStripMenuItem AddMemberToolStripMenuItem;
        internal ToolStripMenuItem EditEndsToolStripMenuItem1;
        internal ToolStripSeparator ToolStripSeparator1;
        internal ContextMenuStrip ContextMenuStrip3;
        internal ToolStripMenuItem BendingMomentToolStripMenuItem;
        internal ToolStripMenuItem ShearForceToolStripMenuItem;
        internal ToolStripMenuItem DeflectionToolStripMenuItem;
        internal ToolStripMenuItem SlopeToolStripMenuItem;
        internal PictureBox PictureBox1;
        internal PictureBox res_coverpic;
    }
}