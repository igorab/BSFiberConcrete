namespace BSFiberConcrete.Beam
{
    partial class BSBeamCalcView
    {
                                private System.ComponentModel.IContainer components = null;
                                        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
                                                private void InitializeComponent()
        {
            this.panel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
                                                this.panel.BackColor = System.Drawing.Color.White;
            this.panel.Location = new System.Drawing.Point(176, 42);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(674, 330);
            this.panel.TabIndex = 0;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
                                                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 450);
            this.Controls.Add(this.panel);
            this.Name = "BSBeamCalcView";
            this.Text = "BSBeam";
            this.Load += new System.EventHandler(this.BSBeamCalcView_Load);
            this.ResumeLayout(false);
        }
                private System.Windows.Forms.Panel panel;
    }
}