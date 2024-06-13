using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CBAnsDes
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class BeamCreate : Form
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
            this.components = new System.ComponentModel.Container();
            this.coverpic = new System.Windows.Forms.Panel();
            this.respic = new System.Windows.Forms.Panel();
            this.res_coverpic = new System.Windows.Forms.PictureBox();
            this.mainpic = new System.Windows.Forms.Panel();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.ContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddMemberToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.EditEndsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.EditMemebrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveMemberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddLoadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveLoadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddMemberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditEndsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.EditLoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveLoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.BendingMomentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShearForceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeflectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SlopeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coverpic.SuspendLayout();
            this.respic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.res_coverpic)).BeginInit();
            this.mainpic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.ContextMenuStrip1.SuspendLayout();
            this.ContextMenuStrip2.SuspendLayout();
            this.ContextMenuStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // coverpic
            // 
            this.coverpic.BackColor = System.Drawing.Color.White;
            this.coverpic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.coverpic.Controls.Add(this.respic);
            this.coverpic.Controls.Add(this.mainpic);
            this.coverpic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coverpic.Location = new System.Drawing.Point(0, 0);
            this.coverpic.Name = "coverpic";
            this.coverpic.Size = new System.Drawing.Size(741, 435);
            this.coverpic.TabIndex = 0;
            // 
            // respic
            // 
            this.respic.BackColor = System.Drawing.Color.White;
            this.respic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.respic.Controls.Add(this.res_coverpic);
            this.respic.Location = new System.Drawing.Point(78, 303);
            this.respic.Name = "respic";
            this.respic.Size = new System.Drawing.Size(198, 91);
            this.respic.TabIndex = 1;
            this.respic.Paint += new System.Windows.Forms.PaintEventHandler(this.respic_Paint);
            this.respic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.respic_MouseClick);
            this.respic.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.respic_MouseDoubleClick);
            this.respic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.respic_MouseDown);
            this.respic.MouseEnter += new System.EventHandler(this.respic_MouseEnter);
            this.respic.MouseLeave += new System.EventHandler(this.respic_MouseLeave);
            this.respic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.respic_MouseMove);
            this.respic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.respic_MouseUp);
            this.respic.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.respic_MouseWheel);
            // 
            // res_coverpic
            // 
            this.res_coverpic.BackColor = System.Drawing.Color.Transparent;
            this.res_coverpic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.res_coverpic.Enabled = false;
            this.res_coverpic.Location = new System.Drawing.Point(0, 0);
            this.res_coverpic.Margin = new System.Windows.Forms.Padding(2);
            this.res_coverpic.Name = "res_coverpic";
            this.res_coverpic.Size = new System.Drawing.Size(196, 89);
            this.res_coverpic.TabIndex = 0;
            this.res_coverpic.TabStop = false;
            // 
            // mainpic
            // 
            this.mainpic.BackColor = System.Drawing.Color.White;
            this.mainpic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainpic.Controls.Add(this.PictureBox1);
            this.mainpic.Location = new System.Drawing.Point(9, 11);
            this.mainpic.Name = "mainpic";
            this.mainpic.Size = new System.Drawing.Size(624, 258);
            this.mainpic.TabIndex = 0;
            this.mainpic.Paint += new System.Windows.Forms.PaintEventHandler(this.mainpic_Paint);
            this.mainpic.DoubleClick += new System.EventHandler(this.mainpic_DoubleClick);
            this.mainpic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mainpic_MouseClick);
            this.mainpic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainpic_MouseDown);
            this.mainpic.MouseEnter += new System.EventHandler(this.mainpic_MouseEnter);
            this.mainpic.MouseLeave += new System.EventHandler(this.mainpic_MouseLeave);
            this.mainpic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainpic_MouseMove);
            this.mainpic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainpic_MouseUp);
            this.mainpic.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.mainpic_MouseWheel);
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox1.Enabled = false;
            this.PictureBox1.Location = new System.Drawing.Point(0, 0);
            this.PictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(622, 256);
            this.PictureBox1.TabIndex = 0;
            this.PictureBox1.TabStop = false;
            // 
            // ContextMenuStrip1
            // 
            this.ContextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddMemberToolStripMenuItem1,
            this.EditEndsToolStripMenuItem,
            this.ToolStripSeparator9,
            this.EditMemebrToolStripMenuItem,
            this.RemoveMemberToolStripMenuItem,
            this.AddLoadToolStripMenuItem1,
            this.RemoveLoadsToolStripMenuItem});
            this.ContextMenuStrip1.Name = "ContextMenuStrip1";
            this.ContextMenuStrip1.Size = new System.Drawing.Size(170, 166);
            // 
            // AddMemberToolStripMenuItem1
            // 
            this.AddMemberToolStripMenuItem1.Image = global::CBAnsDes.My.Resources.Resources.addmember;
            this.AddMemberToolStripMenuItem1.Name = "AddMemberToolStripMenuItem1";
            this.AddMemberToolStripMenuItem1.Size = new System.Drawing.Size(169, 26);
            this.AddMemberToolStripMenuItem1.Text = "Add Member";
            this.AddMemberToolStripMenuItem1.Click += new System.EventHandler(this.AddMemberToolStripMenuItem1_Click);
            // 
            // EditEndsToolStripMenuItem
            // 
            this.EditEndsToolStripMenuItem.Image = global::CBAnsDes.My.Resources.Resources.editends;
            this.EditEndsToolStripMenuItem.Name = "EditEndsToolStripMenuItem";
            this.EditEndsToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.EditEndsToolStripMenuItem.Text = "Edit Ends";
            this.EditEndsToolStripMenuItem.Click += new System.EventHandler(this.EditEndsToolStripMenuItem_Click);
            // 
            // ToolStripSeparator9
            // 
            this.ToolStripSeparator9.Name = "ToolStripSeparator9";
            this.ToolStripSeparator9.Size = new System.Drawing.Size(166, 6);
            // 
            // EditMemebrToolStripMenuItem
            // 
            this.EditMemebrToolStripMenuItem.Image = global::CBAnsDes.My.Resources.Resources.editmember;
            this.EditMemebrToolStripMenuItem.Name = "EditMemebrToolStripMenuItem";
            this.EditMemebrToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.EditMemebrToolStripMenuItem.Text = "Edit Memebr";
            this.EditMemebrToolStripMenuItem.Click += new System.EventHandler(this.EditMemebrToolStripMenuItem_Click);
            // 
            // RemoveMemberToolStripMenuItem
            // 
            this.RemoveMemberToolStripMenuItem.Image = global::CBAnsDes.My.Resources.Resources.deletemember;
            this.RemoveMemberToolStripMenuItem.Name = "RemoveMemberToolStripMenuItem";
            this.RemoveMemberToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.RemoveMemberToolStripMenuItem.Text = "Remove Member";
            this.RemoveMemberToolStripMenuItem.Click += new System.EventHandler(this.RemoveMemberToolStripMenuItem_Click);
            // 
            // AddLoadToolStripMenuItem1
            // 
            this.AddLoadToolStripMenuItem1.Image = global::CBAnsDes.My.Resources.Resources.addload;
            this.AddLoadToolStripMenuItem1.Name = "AddLoadToolStripMenuItem1";
            this.AddLoadToolStripMenuItem1.Size = new System.Drawing.Size(169, 26);
            this.AddLoadToolStripMenuItem1.Text = "Add Load";
            this.AddLoadToolStripMenuItem1.Click += new System.EventHandler(this.AddLoadToolStripMenuItem1_Click);
            // 
            // RemoveLoadsToolStripMenuItem
            // 
            this.RemoveLoadsToolStripMenuItem.Image = global::CBAnsDes.My.Resources.Resources.removeloads;
            this.RemoveLoadsToolStripMenuItem.Name = "RemoveLoadsToolStripMenuItem";
            this.RemoveLoadsToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.RemoveLoadsToolStripMenuItem.Text = "Remove Loads";
            this.RemoveLoadsToolStripMenuItem.Click += new System.EventHandler(this.RemoveLoadsToolStripMenuItem_Click);
            // 
            // ContextMenuStrip2
            // 
            this.ContextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ContextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddMemberToolStripMenuItem,
            this.EditEndsToolStripMenuItem1,
            this.ToolStripSeparator1,
            this.EditLoadToolStripMenuItem,
            this.RemoveLoadToolStripMenuItem});
            this.ContextMenuStrip2.Name = "ContextMenuStrip2";
            this.ContextMenuStrip2.Size = new System.Drawing.Size(151, 114);
            // 
            // AddMemberToolStripMenuItem
            // 
            this.AddMemberToolStripMenuItem.Image = global::CBAnsDes.My.Resources.Resources.addmember;
            this.AddMemberToolStripMenuItem.Name = "AddMemberToolStripMenuItem";
            this.AddMemberToolStripMenuItem.Size = new System.Drawing.Size(150, 26);
            this.AddMemberToolStripMenuItem.Text = "Add Member";
            this.AddMemberToolStripMenuItem.Click += new System.EventHandler(this.AddMemberToolStripMenuItem_Click);
            // 
            // EditEndsToolStripMenuItem1
            // 
            this.EditEndsToolStripMenuItem1.Image = global::CBAnsDes.My.Resources.Resources.editends;
            this.EditEndsToolStripMenuItem1.Name = "EditEndsToolStripMenuItem1";
            this.EditEndsToolStripMenuItem1.Size = new System.Drawing.Size(150, 26);
            this.EditEndsToolStripMenuItem1.Text = "Edit Ends";
            this.EditEndsToolStripMenuItem1.Click += new System.EventHandler(this.EditEndsToolStripMenuItem1_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(147, 6);
            // 
            // EditLoadToolStripMenuItem
            // 
            this.EditLoadToolStripMenuItem.Image = global::CBAnsDes.My.Resources.Resources.modifyload;
            this.EditLoadToolStripMenuItem.Name = "EditLoadToolStripMenuItem";
            this.EditLoadToolStripMenuItem.Size = new System.Drawing.Size(150, 26);
            this.EditLoadToolStripMenuItem.Text = "Edit Load";
            this.EditLoadToolStripMenuItem.Click += new System.EventHandler(this.EditLoadToolStripMenuItem_Click);
            // 
            // RemoveLoadToolStripMenuItem
            // 
            this.RemoveLoadToolStripMenuItem.Image = global::CBAnsDes.My.Resources.Resources.removeload;
            this.RemoveLoadToolStripMenuItem.Name = "RemoveLoadToolStripMenuItem";
            this.RemoveLoadToolStripMenuItem.Size = new System.Drawing.Size(150, 26);
            this.RemoveLoadToolStripMenuItem.Text = "Remove Load";
            this.RemoveLoadToolStripMenuItem.Click += new System.EventHandler(this.RemoveLoadToolStripMenuItem_Click);
            // 
            // ContextMenuStrip3
            // 
            this.ContextMenuStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ContextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BendingMomentToolStripMenuItem,
            this.ShearForceToolStripMenuItem,
            this.DeflectionToolStripMenuItem,
            this.SlopeToolStripMenuItem});
            this.ContextMenuStrip3.Name = "ContextMenuStrip3";
            this.ContextMenuStrip3.Size = new System.Drawing.Size(172, 108);
            // 
            // BendingMomentToolStripMenuItem
            // 
            this.BendingMomentToolStripMenuItem.Image = global::CBAnsDes.My.Resources.Resources.bm;
            this.BendingMomentToolStripMenuItem.Name = "BendingMomentToolStripMenuItem";
            this.BendingMomentToolStripMenuItem.Size = new System.Drawing.Size(171, 26);
            this.BendingMomentToolStripMenuItem.Text = "Bending Moment";
            this.BendingMomentToolStripMenuItem.Click += new System.EventHandler(this.BendingMomentToolStripMenuItem_Click);
            // 
            // ShearForceToolStripMenuItem
            // 
            this.ShearForceToolStripMenuItem.Image = global::CBAnsDes.My.Resources.Resources.shear;
            this.ShearForceToolStripMenuItem.Name = "ShearForceToolStripMenuItem";
            this.ShearForceToolStripMenuItem.Size = new System.Drawing.Size(171, 26);
            this.ShearForceToolStripMenuItem.Text = "Shear Force";
            this.ShearForceToolStripMenuItem.Click += new System.EventHandler(this.ShearForceToolStripMenuItem_Click);
            // 
            // DeflectionToolStripMenuItem
            // 
            this.DeflectionToolStripMenuItem.Image = global::CBAnsDes.My.Resources.Resources.bend;
            this.DeflectionToolStripMenuItem.Name = "DeflectionToolStripMenuItem";
            this.DeflectionToolStripMenuItem.Size = new System.Drawing.Size(171, 26);
            this.DeflectionToolStripMenuItem.Text = "Deflection";
            this.DeflectionToolStripMenuItem.Click += new System.EventHandler(this.DeflectionToolStripMenuItem_Click);
            // 
            // SlopeToolStripMenuItem
            // 
            this.SlopeToolStripMenuItem.Image = global::CBAnsDes.My.Resources.Resources.Slope;
            this.SlopeToolStripMenuItem.Name = "SlopeToolStripMenuItem";
            this.SlopeToolStripMenuItem.Size = new System.Drawing.Size(171, 26);
            this.SlopeToolStripMenuItem.Text = "Slope";
            this.SlopeToolStripMenuItem.Click += new System.EventHandler(this.SlopeToolStripMenuItem_Click);
            // 
            // BeamCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(741, 435);
            this.Controls.Add(this.coverpic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "BeamCreate";
            this.Opacity = 0.9D;
            this.Text = "beamcreate";
            this.Load += new System.EventHandler(this.beamcreate_Load);
            this.coverpic.ResumeLayout(false);
            this.respic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.res_coverpic)).EndInit();
            this.mainpic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ContextMenuStrip1.ResumeLayout(false);
            this.ContextMenuStrip2.ResumeLayout(false);
            this.ContextMenuStrip3.ResumeLayout(false);
            this.ResumeLayout(false);

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