namespace BSFiberConcrete.Section.DrawBeamSection
{
    partial class DrawBeamSection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlForPlot = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlForPlot
            // 
            this.pnlForPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForPlot.Location = new System.Drawing.Point(0, 0);
            this.pnlForPlot.Name = "pnlForPlot";
            this.pnlForPlot.Size = new System.Drawing.Size(427, 420);
            this.pnlForPlot.TabIndex = 0;
            // 
            // DrawBeamSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 420);
            this.Controls.Add(this.pnlForPlot);
            this.Name = "DrawBeamSection";
            this.Text = "Мозаика сечения";
            this.Load += new System.EventHandler(this.DrawBeamSection_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlForPlot;
    }
}