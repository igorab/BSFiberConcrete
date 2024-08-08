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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelMax = new System.Windows.Forms.Label();
            this.labelMin = new System.Windows.Forms.Label();
            this.numMaxValue = new System.Windows.Forms.NumericUpDown();
            this.numMinValue = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinValue)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlForPlot
            // 
            this.pnlForPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlForPlot.Location = new System.Drawing.Point(0, 53);
            this.pnlForPlot.Name = "pnlForPlot";
            this.pnlForPlot.Size = new System.Drawing.Size(686, 455);
            this.pnlForPlot.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.32035F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.67965F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 468F));
            this.tableLayoutPanel1.Controls.Add(this.labelMax, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelMin, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.numMaxValue, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.numMinValue, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(698, 47);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // labelMax
            // 
            this.labelMax.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelMax.AutoSize = true;
            this.labelMax.Location = new System.Drawing.Point(42, 5);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(61, 13);
            this.labelMax.TabIndex = 0;
            this.labelMax.Text = "Максимум";
            // 
            // labelMin
            // 
            this.labelMin.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelMin.AutoSize = true;
            this.labelMin.Location = new System.Drawing.Point(48, 28);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(55, 13);
            this.labelMin.TabIndex = 1;
            this.labelMin.Text = "Минимум";
            // 
            // numMaxValue
            // 
            this.numMaxValue.DecimalPlaces = 4;
            this.numMaxValue.ForeColor = System.Drawing.Color.Red;
            this.numMaxValue.Location = new System.Drawing.Point(109, 3);
            this.numMaxValue.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numMaxValue.Minimum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            -2147483648});
            this.numMaxValue.Name = "numMaxValue";
            this.numMaxValue.Size = new System.Drawing.Size(117, 20);
            this.numMaxValue.TabIndex = 2;
            // 
            // numMinValue
            // 
            this.numMinValue.DecimalPlaces = 4;
            this.numMinValue.ForeColor = System.Drawing.Color.Purple;
            this.numMinValue.Location = new System.Drawing.Point(109, 26);
            this.numMinValue.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numMinValue.Minimum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            -2147483648});
            this.numMinValue.Name = "numMinValue";
            this.numMinValue.Size = new System.Drawing.Size(117, 20);
            this.numMinValue.TabIndex = 3;
            // 
            // DrawBeamSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 508);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.pnlForPlot);
            this.Name = "DrawBeamSection";
            this.Text = "Мозаика сечения";
            this.Load += new System.EventHandler(this.DrawBeamSection_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlForPlot;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelMax;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.NumericUpDown numMaxValue;
        private System.Windows.Forms.NumericUpDown numMinValue;
    }
}