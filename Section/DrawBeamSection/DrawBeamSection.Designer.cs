namespace BSFiberConcrete.Section.DrawBeamSection
{
    partial class DrawBeamSection
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
            this.pnlForPlot = new System.Windows.Forms.Panel();
            this.PanelInfo = new System.Windows.Forms.TableLayoutPanel();
            this.num_e_st_max = new System.Windows.Forms.NumericUpDown();
            this.num_e_fbt_max = new System.Windows.Forms.NumericUpDown();
            this.label_fbt_max = new System.Windows.Forms.Label();
            this.labelMax = new System.Windows.Forms.Label();
            this.labelMin = new System.Windows.Forms.Label();
            this.numMaxValue = new System.Windows.Forms.NumericUpDown();
            this.numMinValue = new System.Windows.Forms.NumericUpDown();
            this.labelTension = new System.Windows.Forms.Label();
            this.labelPress = new System.Windows.Forms.Label();
            this.label_b_max = new System.Windows.Forms.Label();
            this.num_e_fb_max = new System.Windows.Forms.NumericUpDown();
            this.label_st_max = new System.Windows.Forms.Label();
            this.label_s_max = new System.Windows.Forms.Label();
            this.num_e_s_max = new System.Windows.Forms.NumericUpDown();
            this.comboMode = new System.Windows.Forms.ComboBox();
            this.btnInfo = new System.Windows.Forms.Button();
            this.label_e_st_ult = new System.Windows.Forms.Label();
            this.label_e_s_ult = new System.Windows.Forms.Label();
            this.num_e_st_ult = new System.Windows.Forms.NumericUpDown();
            this.num_e_s_ult = new System.Windows.Forms.NumericUpDown();
            this.PanelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_e_st_max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_e_fbt_max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_e_fb_max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_e_s_max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_e_st_ult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_e_s_ult)).BeginInit();
            this.SuspendLayout();
                                                this.pnlForPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlForPlot.Location = new System.Drawing.Point(0, 96);
            this.pnlForPlot.Name = "pnlForPlot";
            this.pnlForPlot.Size = new System.Drawing.Size(822, 542);
            this.pnlForPlot.TabIndex = 0;
                                                this.PanelInfo.ColumnCount = 10;
            this.PanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.PanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.13475F));
            this.PanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.86525F));
            this.PanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.PanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.PanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.PanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.PanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.PanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.PanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
            this.PanelInfo.Controls.Add(this.num_e_st_max, 8, 0);
            this.PanelInfo.Controls.Add(this.num_e_fbt_max, 6, 0);
            this.PanelInfo.Controls.Add(this.label_fbt_max, 5, 0);
            this.PanelInfo.Controls.Add(this.labelMax, 1, 0);
            this.PanelInfo.Controls.Add(this.labelMin, 1, 1);
            this.PanelInfo.Controls.Add(this.numMaxValue, 2, 0);
            this.PanelInfo.Controls.Add(this.numMinValue, 2, 1);
            this.PanelInfo.Controls.Add(this.labelTension, 0, 0);
            this.PanelInfo.Controls.Add(this.labelPress, 0, 1);
            this.PanelInfo.Controls.Add(this.label_b_max, 5, 1);
            this.PanelInfo.Controls.Add(this.num_e_fb_max, 6, 1);
            this.PanelInfo.Controls.Add(this.label_st_max, 7, 0);
            this.PanelInfo.Controls.Add(this.label_s_max, 7, 1);
            this.PanelInfo.Controls.Add(this.num_e_s_max, 8, 1);
            this.PanelInfo.Controls.Add(this.comboMode, 0, 2);
            this.PanelInfo.Controls.Add(this.btnInfo, 8, 2);
            this.PanelInfo.Controls.Add(this.label_e_st_ult, 3, 0);
            this.PanelInfo.Controls.Add(this.label_e_s_ult, 3, 1);
            this.PanelInfo.Controls.Add(this.num_e_st_ult, 4, 0);
            this.PanelInfo.Controls.Add(this.num_e_s_ult, 4, 1);
            this.PanelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelInfo.Location = new System.Drawing.Point(0, 0);
            this.PanelInfo.Name = "PanelInfo";
            this.PanelInfo.RowCount = 3;
            this.PanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.PanelInfo.Size = new System.Drawing.Size(834, 90);
            this.PanelInfo.TabIndex = 1;
                                                this.num_e_st_max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.num_e_st_max.DecimalPlaces = 6;
            this.num_e_st_max.ForeColor = System.Drawing.SystemColors.WindowText;
            this.num_e_st_max.Location = new System.Drawing.Point(640, 4);
            this.num_e_st_max.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.num_e_st_max.Minimum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            -2147483648});
            this.num_e_st_max.Name = "num_e_st_max";
            this.num_e_st_max.Size = new System.Drawing.Size(101, 20);
            this.num_e_st_max.TabIndex = 13;
                                                this.num_e_fbt_max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.num_e_fbt_max.DecimalPlaces = 6;
            this.num_e_fbt_max.ForeColor = System.Drawing.SystemColors.WindowText;
            this.num_e_fbt_max.Location = new System.Drawing.Point(485, 4);
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
            this.num_e_fbt_max.Size = new System.Drawing.Size(87, 20);
            this.num_e_fbt_max.TabIndex = 8;
                                                this.label_fbt_max.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_fbt_max.AutoSize = true;
            this.label_fbt_max.Location = new System.Drawing.Point(417, 8);
            this.label_fbt_max.Name = "label_fbt_max";
            this.label_fbt_max.Size = new System.Drawing.Size(62, 13);
            this.label_fbt_max.TabIndex = 6;
            this.label_fbt_max.Text = " ε , fbt, max";
                                                this.labelMax.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelMax.AutoSize = true;
            this.labelMax.Location = new System.Drawing.Point(109, 8);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(48, 13);
            this.labelMax.TabIndex = 0;
            this.labelMax.Text = "ε, fbt, ult";
                                                this.labelMin.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelMin.AutoSize = true;
            this.labelMin.Location = new System.Drawing.Point(115, 37);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(42, 13);
            this.labelMin.TabIndex = 1;
            this.labelMin.Text = "ε, b, ult";
                                                this.numMaxValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numMaxValue.DecimalPlaces = 6;
            this.numMaxValue.ForeColor = System.Drawing.Color.Red;
            this.numMaxValue.Location = new System.Drawing.Point(163, 4);
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
            this.numMaxValue.Size = new System.Drawing.Size(80, 20);
            this.numMaxValue.TabIndex = 2;
                                                this.numMinValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numMinValue.DecimalPlaces = 6;
            this.numMinValue.ForeColor = System.Drawing.Color.Purple;
            this.numMinValue.Location = new System.Drawing.Point(163, 33);
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
            this.numMinValue.Size = new System.Drawing.Size(80, 20);
            this.numMinValue.TabIndex = 3;
                                                this.labelTension.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelTension.AutoSize = true;
            this.labelTension.Location = new System.Drawing.Point(25, 8);
            this.labelTension.Name = "labelTension";
            this.labelTension.Size = new System.Drawing.Size(72, 13);
            this.labelTension.TabIndex = 4;
            this.labelTension.Text = "Растяжение:";
                                                this.labelPress.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelPress.AutoSize = true;
            this.labelPress.Location = new System.Drawing.Point(49, 37);
            this.labelPress.Name = "labelPress";
            this.labelPress.Size = new System.Drawing.Size(48, 13);
            this.labelPress.TabIndex = 5;
            this.labelPress.Text = "Сжатие:";
                                                this.label_b_max.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_b_max.AutoSize = true;
            this.label_b_max.Location = new System.Drawing.Point(423, 37);
            this.label_b_max.Name = "label_b_max";
            this.label_b_max.Size = new System.Drawing.Size(56, 13);
            this.label_b_max.TabIndex = 7;
            this.label_b_max.Text = " ε,  b, max";
                                                this.num_e_fb_max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.num_e_fb_max.DecimalPlaces = 6;
            this.num_e_fb_max.ForeColor = System.Drawing.SystemColors.WindowText;
            this.num_e_fb_max.Location = new System.Drawing.Point(485, 33);
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
            this.num_e_fb_max.Size = new System.Drawing.Size(87, 20);
            this.num_e_fb_max.TabIndex = 9;
                                                this.label_st_max.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_st_max.AutoSize = true;
            this.label_st_max.Location = new System.Drawing.Point(582, 8);
            this.label_st_max.Name = "label_st_max";
            this.label_st_max.Size = new System.Drawing.Size(52, 13);
            this.label_st_max.TabIndex = 10;
            this.label_st_max.Text = " ε, st max";
                                                this.label_s_max.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_s_max.AutoSize = true;
            this.label_s_max.Location = new System.Drawing.Point(585, 37);
            this.label_s_max.Name = "label_s_max";
            this.label_s_max.Size = new System.Drawing.Size(49, 13);
            this.label_s_max.TabIndex = 11;
            this.label_s_max.Text = "e, s, max";
                                                this.num_e_s_max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.num_e_s_max.DecimalPlaces = 6;
            this.num_e_s_max.ForeColor = System.Drawing.SystemColors.WindowText;
            this.num_e_s_max.Location = new System.Drawing.Point(640, 33);
            this.num_e_s_max.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.num_e_s_max.Minimum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            -2147483648});
            this.num_e_s_max.Name = "num_e_s_max";
            this.num_e_s_max.Size = new System.Drawing.Size(101, 20);
            this.num_e_s_max.TabIndex = 14;
                                                this.comboMode.FormattingEnabled = true;
            this.comboMode.Items.AddRange(new object[] {
            "Мозаика",
            "Деформации",
            "Напряжения",
            "Деформации 0",
            "Напряжения 0"});
            this.comboMode.Location = new System.Drawing.Point(3, 61);
            this.comboMode.Name = "comboMode";
            this.comboMode.Size = new System.Drawing.Size(94, 21);
            this.comboMode.TabIndex = 12;
            this.comboMode.Text = "Деформации";
            this.comboMode.SelectedIndexChanged += new System.EventHandler(this.comboMode_SelectedIndexChanged);
                                                this.btnInfo.Location = new System.Drawing.Point(640, 61);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(33, 23);
            this.btnInfo.TabIndex = 15;
            this.btnInfo.Text = "?";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
                                                this.label_e_st_ult.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_e_st_ult.AutoSize = true;
            this.label_e_st_ult.Location = new System.Drawing.Point(264, 8);
            this.label_e_st_ult.Name = "label_e_st_ult";
            this.label_e_st_ult.Size = new System.Drawing.Size(44, 13);
            this.label_e_st_ult.TabIndex = 16;
            this.label_e_st_ult.Text = "ε, st, ult";
                                                this.label_e_s_ult.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_e_s_ult.AutoSize = true;
            this.label_e_s_ult.Location = new System.Drawing.Point(267, 37);
            this.label_e_s_ult.Name = "label_e_s_ult";
            this.label_e_s_ult.Size = new System.Drawing.Size(41, 13);
            this.label_e_s_ult.TabIndex = 17;
            this.label_e_s_ult.Text = "ε, s, ult";
                                                this.num_e_st_ult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.num_e_st_ult.DecimalPlaces = 6;
            this.num_e_st_ult.ForeColor = System.Drawing.Color.Red;
            this.num_e_st_ult.Location = new System.Drawing.Point(314, 4);
            this.num_e_st_ult.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.num_e_st_ult.Minimum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            -2147483648});
            this.num_e_st_ult.Name = "num_e_st_ult";
            this.num_e_st_ult.Size = new System.Drawing.Size(95, 20);
            this.num_e_st_ult.TabIndex = 18;
                                                this.num_e_s_ult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.num_e_s_ult.DecimalPlaces = 6;
            this.num_e_s_ult.ForeColor = System.Drawing.Color.Purple;
            this.num_e_s_ult.Location = new System.Drawing.Point(314, 33);
            this.num_e_s_ult.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.num_e_s_ult.Minimum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            -2147483648});
            this.num_e_s_ult.Name = "num_e_s_ult";
            this.num_e_s_ult.Size = new System.Drawing.Size(95, 20);
            this.num_e_s_ult.TabIndex = 19;
                                                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 638);
            this.Controls.Add(this.PanelInfo);
            this.Controls.Add(this.pnlForPlot);
            this.Name = "DrawBeamSection";
            this.Text = "Мозаика сечения";
            this.Load += new System.EventHandler(this.DrawBeamSection_Load);
            this.PanelInfo.ResumeLayout(false);
            this.PanelInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_e_st_max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_e_fbt_max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_e_fb_max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_e_s_max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_e_st_ult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_e_s_ult)).EndInit();
            this.ResumeLayout(false);
        }
                private System.Windows.Forms.Panel pnlForPlot;
        private System.Windows.Forms.TableLayoutPanel PanelInfo;
        private System.Windows.Forms.Label labelMax;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.NumericUpDown numMaxValue;
        private System.Windows.Forms.NumericUpDown numMinValue;
        private System.Windows.Forms.Label label_fbt_max;
        private System.Windows.Forms.Label labelTension;
        private System.Windows.Forms.Label labelPress;
        private System.Windows.Forms.Label label_b_max;
        private System.Windows.Forms.NumericUpDown num_e_fbt_max;
        private System.Windows.Forms.NumericUpDown num_e_fb_max;
        private System.Windows.Forms.NumericUpDown num_e_st_max;
        private System.Windows.Forms.Label label_st_max;
        private System.Windows.Forms.Label label_s_max;
        private System.Windows.Forms.ComboBox comboMode;
        private System.Windows.Forms.NumericUpDown num_e_s_max;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Label label_e_st_ult;
        private System.Windows.Forms.Label label_e_s_ult;
        private System.Windows.Forms.NumericUpDown num_e_st_ult;
        private System.Windows.Forms.NumericUpDown num_e_s_ult;
    }
}