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
            this.labelTension = new System.Windows.Forms.Label();
            this.labelPress = new System.Windows.Forms.Label();
            this.label_e_fbt_max = new System.Windows.Forms.Label();
            this.label_e_b_max = new System.Windows.Forms.Label();
            this.num_e_fbt_max = new System.Windows.Forms.NumericUpDown();
            this.num_e_fb_max = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_e_fbt_max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_e_fb_max)).BeginInit();
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
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 127F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.84615F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.15385F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 124F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tableLayoutPanel1.Controls.Add(this.num_e_fbt_max, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_e_fbt_max, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelMax, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelMin, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.numMaxValue, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.numMinValue, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelTension, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelPress, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_e_b_max, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.num_e_fb_max, 4, 1);
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
            this.labelMax.Location = new System.Drawing.Point(145, 5);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(45, 13);
            this.labelMax.TabIndex = 0;
            this.labelMax.Text = "e, fbt ult";
            // 
            // labelMin
            // 
            this.labelMin.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelMin.AutoSize = true;
            this.labelMin.Location = new System.Drawing.Point(151, 28);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(39, 13);
            this.labelMin.TabIndex = 1;
            this.labelMin.Text = "e b, ult";
            // 
            // numMaxValue
            // 
            this.numMaxValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numMaxValue.DecimalPlaces = 6;
            this.numMaxValue.ForeColor = System.Drawing.Color.Red;
            this.numMaxValue.Location = new System.Drawing.Point(196, 3);
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
            this.numMaxValue.Size = new System.Drawing.Size(123, 20);
            this.numMaxValue.TabIndex = 2;
            // 
            // numMinValue
            // 
            this.numMinValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numMinValue.DecimalPlaces = 6;
            this.numMinValue.ForeColor = System.Drawing.Color.Purple;
            this.numMinValue.Location = new System.Drawing.Point(196, 26);
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
            this.numMinValue.Size = new System.Drawing.Size(123, 20);
            this.numMinValue.TabIndex = 3;
            // 
            // labelTension
            // 
            this.labelTension.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelTension.AutoSize = true;
            this.labelTension.Location = new System.Drawing.Point(52, 5);
            this.labelTension.Name = "labelTension";
            this.labelTension.Size = new System.Drawing.Size(72, 13);
            this.labelTension.TabIndex = 4;
            this.labelTension.Text = "Растяжение:";
            // 
            // labelPress
            // 
            this.labelPress.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelPress.AutoSize = true;
            this.labelPress.Location = new System.Drawing.Point(76, 28);
            this.labelPress.Name = "labelPress";
            this.labelPress.Size = new System.Drawing.Size(48, 13);
            this.labelPress.TabIndex = 5;
            this.labelPress.Text = "Сжатие:";
            // 
            // label_e_fbt_max
            // 
            this.label_e_fbt_max.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_e_fbt_max.AutoSize = true;
            this.label_e_fbt_max.Location = new System.Drawing.Point(390, 5);
            this.label_e_fbt_max.Name = "label_e_fbt_max";
            this.label_e_fbt_max.Size = new System.Drawing.Size(53, 13);
            this.label_e_fbt_max.TabIndex = 6;
            this.label_e_fbt_max.Text = "e, fbt max";
            // 
            // label_e_b_max
            // 
            this.label_e_b_max.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_e_b_max.AutoSize = true;
            this.label_e_b_max.Location = new System.Drawing.Point(396, 28);
            this.label_e_b_max.Name = "label_e_b_max";
            this.label_e_b_max.Size = new System.Drawing.Size(47, 13);
            this.label_e_b_max.TabIndex = 7;
            this.label_e_b_max.Text = "e b, max";
            // 
            // num_e_fbt_max
            // 
            this.num_e_fbt_max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.num_e_fbt_max.DecimalPlaces = 6;
            this.num_e_fbt_max.ForeColor = System.Drawing.Color.Red;
            this.num_e_fbt_max.Location = new System.Drawing.Point(449, 3);
            this.num_e_fbt_max.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.num_e_fbt_max.Minimum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            -2147483648});
            this.num_e_fbt_max.Name = "num_e_fbt_max";
            this.num_e_fbt_max.Size = new System.Drawing.Size(132, 20);
            this.num_e_fbt_max.TabIndex = 8;
            // 
            // num_e_fb_max
            // 
            this.num_e_fb_max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.num_e_fb_max.DecimalPlaces = 6;
            this.num_e_fb_max.ForeColor = System.Drawing.Color.Purple;
            this.num_e_fb_max.Location = new System.Drawing.Point(449, 26);
            this.num_e_fb_max.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.num_e_fb_max.Minimum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            -2147483648});
            this.num_e_fb_max.Name = "num_e_fb_max";
            this.num_e_fb_max.Size = new System.Drawing.Size(132, 20);
            this.num_e_fb_max.TabIndex = 9;
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
            ((System.ComponentModel.ISupportInitialize)(this.num_e_fbt_max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_e_fb_max)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlForPlot;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelMax;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.NumericUpDown numMaxValue;
        private System.Windows.Forms.NumericUpDown numMinValue;
        private System.Windows.Forms.Label label_e_fbt_max;
        private System.Windows.Forms.Label labelTension;
        private System.Windows.Forms.Label labelPress;
        private System.Windows.Forms.Label label_e_b_max;
        private System.Windows.Forms.NumericUpDown num_e_fbt_max;
        private System.Windows.Forms.NumericUpDown num_e_fb_max;
    }
}