namespace BSFiberConcrete.Section
{
    partial class BSSectionDraw
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnPoint = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSectionGrid = new System.Windows.Forms.Button();
            this.pic = new System.Windows.Forms.PictureBox();
            this.btnCalcGo = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
                                                this.panelTop.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelTop.Controls.Add(this.btnPoint);
            this.panelTop.Controls.Add(this.btnLine);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1002, 75);
            this.panelTop.TabIndex = 0;
                                                this.btnPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPoint.Location = new System.Drawing.Point(454, 12);
            this.btnPoint.Name = "btnPoint";
            this.btnPoint.Size = new System.Drawing.Size(43, 37);
            this.btnPoint.TabIndex = 1;
            this.btnPoint.Text = "0";
            this.btnPoint.UseVisualStyleBackColor = true;
            this.btnPoint.Click += new System.EventHandler(this.btnPoint_Click);
                                                this.btnLine.Location = new System.Drawing.Point(507, 12);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(43, 37);
            this.btnLine.TabIndex = 0;
            this.btnLine.Text = "-------";
            this.btnLine.UseVisualStyleBackColor = true;            
                                                this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel1.Controls.Add(this.btnCalcGo);
            this.panel1.Controls.Add(this.btnSectionGrid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 571);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1002, 52);
            this.panel1.TabIndex = 1;
                                                this.btnSectionGrid.Location = new System.Drawing.Point(827, 8);
            this.btnSectionGrid.Name = "btnSectionGrid";
            this.btnSectionGrid.Size = new System.Drawing.Size(54, 32);
            this.btnSectionGrid.TabIndex = 0;
            this.btnSectionGrid.Text = "x-0-y";
            this.btnSectionGrid.UseVisualStyleBackColor = true;
            this.btnSectionGrid.Click += new System.EventHandler(this.btnSectionGrid_Click);
                                                this.pic.BackColor = System.Drawing.Color.White;
            this.pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic.Location = new System.Drawing.Point(0, 75);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(1002, 496);
            this.pic.TabIndex = 2;
            this.pic.TabStop = false;            
            this.pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic_MouseDown);                        
                                                this.btnCalcGo.Location = new System.Drawing.Point(32, 8);
            this.btnCalcGo.Name = "btnCalcGo";
            this.btnCalcGo.Size = new System.Drawing.Size(75, 32);
            this.btnCalcGo.TabIndex = 1;
            this.btnCalcGo.Text = "Go";
            this.btnCalcGo.UseVisualStyleBackColor = true;
            this.btnCalcGo.Click += new System.EventHandler(this.btnCalcGo_Click);
                                                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 623);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BSSectionDraw";
            this.Text = "Нарисовать сечение";
            this.Load += new System.EventHandler(this.BSSectionDraw_Load);
            this.panelTop.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);
        }
                private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Button btnPoint;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnSectionGrid;
        private System.Windows.Forms.Button btnCalcGo;
    }
}