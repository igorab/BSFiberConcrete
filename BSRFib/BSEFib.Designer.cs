namespace BSFiberConcrete.BSRFib
{
    partial class BSEFib
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelEfb = new System.Windows.Forms.Label();
            this.labelEb = new System.Windows.Forms.Label();
            this.labelEf = new System.Windows.Forms.Label();
            this.labelMu_fv = new System.Windows.Forms.Label();
            this.numEb = new System.Windows.Forms.NumericUpDown();
            this.numEf = new System.Windows.Forms.NumericUpDown();
            this.numMu_fv = new System.Windows.Forms.NumericUpDown();
            this.numEfb = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMu_fv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEfb)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.15232F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.84768F));
            this.tableLayoutPanel.Controls.Add(this.labelEfb, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.labelEb, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.labelEf, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.numEb, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.numEf, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.numMu_fv, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.numEfb, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.labelMu_fv, 0, 3);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 5;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(346, 238);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // labelEfb
            // 
            this.labelEfb.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelEfb.AutoSize = true;
            this.labelEfb.Location = new System.Drawing.Point(64, 206);
            this.labelEfb.Name = "labelEfb";
            this.labelEfb.Size = new System.Drawing.Size(26, 13);
            this.labelEfb.TabIndex = 6;
            this.labelEfb.Text = "E fb";
            // 
            // labelEb
            // 
            this.labelEb.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelEb.AutoSize = true;
            this.labelEb.Location = new System.Drawing.Point(67, 64);
            this.labelEb.Name = "labelEb";
            this.labelEb.Size = new System.Drawing.Size(23, 13);
            this.labelEb.TabIndex = 0;
            this.labelEb.Text = "E b";
            // 
            // labelEf
            // 
            this.labelEf.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelEf.AutoSize = true;
            this.labelEf.Location = new System.Drawing.Point(70, 111);
            this.labelEf.Name = "labelEf";
            this.labelEf.Size = new System.Drawing.Size(20, 13);
            this.labelEf.TabIndex = 1;
            this.labelEf.Text = "E f";
            // 
            // labelMu_fv
            // 
            this.labelMu_fv.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelMu_fv.AutoSize = true;
            this.labelMu_fv.Location = new System.Drawing.Point(65, 158);
            this.labelMu_fv.Name = "labelMu_fv";
            this.labelMu_fv.Size = new System.Drawing.Size(25, 13);
            this.labelMu_fv.TabIndex = 2;
            this.labelMu_fv.Text = "μ fv";
            // 
            // numEb
            // 
            this.numEb.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numEb.DecimalPlaces = 4;
            this.numEb.Location = new System.Drawing.Point(96, 60);
            this.numEb.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numEb.Minimum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            -2147483648});
            this.numEb.Name = "numEb";
            this.numEb.Size = new System.Drawing.Size(120, 20);
            this.numEb.TabIndex = 3;
            this.numEb.ValueChanged += new System.EventHandler(this.numEb_ValueChanged);
            // 
            // numEf
            // 
            this.numEf.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numEf.DecimalPlaces = 4;
            this.numEf.Location = new System.Drawing.Point(96, 107);
            this.numEf.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numEf.Minimum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            -2147483648});
            this.numEf.Name = "numEf";
            this.numEf.Size = new System.Drawing.Size(120, 20);
            this.numEf.TabIndex = 4;
            this.numEf.ValueChanged += new System.EventHandler(this.numEf_ValueChanged);
            // 
            // numMu_fv
            // 
            this.numMu_fv.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numMu_fv.DecimalPlaces = 4;
            this.numMu_fv.Location = new System.Drawing.Point(96, 154);
            this.numMu_fv.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numMu_fv.Minimum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            -2147483648});
            this.numMu_fv.Name = "numMu_fv";
            this.numMu_fv.Size = new System.Drawing.Size(120, 20);
            this.numMu_fv.TabIndex = 5;
            this.numMu_fv.ValueChanged += new System.EventHandler(this.numMu_fv_ValueChanged);
            // 
            // numEfb
            // 
            this.numEfb.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numEfb.DecimalPlaces = 4;
            this.numEfb.Location = new System.Drawing.Point(96, 203);
            this.numEfb.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numEfb.Minimum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            -2147483648});
            this.numEfb.Name = "numEfb";
            this.numEfb.ReadOnly = true;
            this.numEfb.Size = new System.Drawing.Size(120, 20);
            this.numEfb.TabIndex = 7;
            // 
            // BSEFib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 238);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BSEFib";
            this.Text = "Модуль упругости сталефибробетона";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMu_fv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEfb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelEb;
        private System.Windows.Forms.Label labelEf;
        private System.Windows.Forms.Label labelMu_fv;
        private System.Windows.Forms.NumericUpDown numEb;
        private System.Windows.Forms.NumericUpDown numEf;
        private System.Windows.Forms.NumericUpDown numMu_fv;
        private System.Windows.Forms.Label labelEfb;
        private System.Windows.Forms.NumericUpDown numEfb;
    }
}