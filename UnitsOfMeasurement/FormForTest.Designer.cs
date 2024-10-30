namespace BSFiberConcrete.UnitsOfMeasurement
{
    partial class FormForTest
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_MomentOfForce2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_Len1 = new System.Windows.Forms.ComboBox();
            this.num_Len = new System.Windows.Forms.NumericUpDown();
            this.num_Force = new System.Windows.Forms.NumericUpDown();
            this.num_MomentOfForce = new System.Windows.Forms.NumericUpDown();
            this.cmb_Force1 = new System.Windows.Forms.ComboBox();
            this.cmb_MomentOfForce1 = new System.Windows.Forms.ComboBox();
            this.res_Len = new System.Windows.Forms.Label();
            this.res_Force = new System.Windows.Forms.Label();
            this.res_MomentOfForce = new System.Windows.Forms.Label();
            this.cmb_Len2 = new System.Windows.Forms.ComboBox();
            this.cmb_Force2 = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_Len)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Force)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_MomentOfForce)).BeginInit();
            this.SuspendLayout();
                                                this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.00049F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0005F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0005F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.9985F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmb_MomentOfForce2, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmb_Len1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.num_Len, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.num_MomentOfForce, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmb_Force1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmb_MomentOfForce1, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.res_Len, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.res_Force, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.res_MomentOfForce, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmb_Len2, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmb_Force2, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.num_Force, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(63, 42);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(538, 76);
            this.tableLayoutPanel1.TabIndex = 0;
                                                this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Длина";
                                                this.cmb_MomentOfForce2.FormattingEnabled = true;
            this.cmb_MomentOfForce2.Location = new System.Drawing.Point(431, 53);
            this.cmb_MomentOfForce2.Name = "cmb_MomentOfForce2";
            this.cmb_MomentOfForce2.Size = new System.Drawing.Size(101, 21);
            this.cmb_MomentOfForce2.TabIndex = 14;
            this.cmb_MomentOfForce2.SelectedIndexChanged += new System.EventHandler(this.cmb_MomentOfForce2_SelectedIndexChanged);
                                                this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Сила";
                                                this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Момент силы";
                                                this.cmb_Len1.FormattingEnabled = true;
            this.cmb_Len1.Location = new System.Drawing.Point(217, 3);
            this.cmb_Len1.Name = "cmb_Len1";
            this.cmb_Len1.Size = new System.Drawing.Size(101, 21);
            this.cmb_Len1.TabIndex = 3;
            this.cmb_Len1.SelectedIndexChanged += new System.EventHandler(this.cmb_Len1_SelectedIndexChanged);
                                                this.num_Len.Location = new System.Drawing.Point(110, 3);
            this.num_Len.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.num_Len.Name = "num_Len";
            this.num_Len.Size = new System.Drawing.Size(101, 20);
            this.num_Len.TabIndex = 4;
            this.num_Len.ValueChanged += new System.EventHandler(this.num_Len_ValueChanged);
                                                this.num_Force.Location = new System.Drawing.Point(110, 28);
            this.num_Force.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.num_Force.Name = "num_Force";
            this.num_Force.Size = new System.Drawing.Size(101, 20);
            this.num_Force.TabIndex = 5;
            this.num_Force.ValueChanged += new System.EventHandler(this.num_Force_ValueChanged);
                                                this.num_MomentOfForce.Location = new System.Drawing.Point(110, 53);
            this.num_MomentOfForce.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.num_MomentOfForce.Name = "num_MomentOfForce";
            this.num_MomentOfForce.Size = new System.Drawing.Size(101, 20);
            this.num_MomentOfForce.TabIndex = 6;
            this.num_MomentOfForce.ValueChanged += new System.EventHandler(this.num_MomentOfForce_ValueChanged);
                                                this.cmb_Force1.FormattingEnabled = true;
            this.cmb_Force1.Location = new System.Drawing.Point(217, 28);
            this.cmb_Force1.Name = "cmb_Force1";
            this.cmb_Force1.Size = new System.Drawing.Size(101, 21);
            this.cmb_Force1.TabIndex = 7;
            this.cmb_Force1.SelectedIndexChanged += new System.EventHandler(this.cmb_Force1_SelectedIndexChanged);
                                                this.cmb_MomentOfForce1.FormattingEnabled = true;
            this.cmb_MomentOfForce1.Location = new System.Drawing.Point(217, 53);
            this.cmb_MomentOfForce1.Name = "cmb_MomentOfForce1";
            this.cmb_MomentOfForce1.Size = new System.Drawing.Size(101, 21);
            this.cmb_MomentOfForce1.TabIndex = 8;
            this.cmb_MomentOfForce1.SelectedIndexChanged += new System.EventHandler(this.cmb_MomentOfForce1_SelectedIndexChanged);
                                                this.res_Len.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.res_Len.AutoSize = true;
            this.res_Len.Location = new System.Drawing.Point(324, 6);
            this.res_Len.Name = "res_Len";
            this.res_Len.Size = new System.Drawing.Size(13, 13);
            this.res_Len.TabIndex = 9;
            this.res_Len.Text = "0";
                                                this.res_Force.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.res_Force.AutoSize = true;
            this.res_Force.Location = new System.Drawing.Point(324, 31);
            this.res_Force.Name = "res_Force";
            this.res_Force.Size = new System.Drawing.Size(13, 13);
            this.res_Force.TabIndex = 10;
            this.res_Force.Text = "0";
                                                this.res_MomentOfForce.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.res_MomentOfForce.AutoSize = true;
            this.res_MomentOfForce.Location = new System.Drawing.Point(324, 56);
            this.res_MomentOfForce.Name = "res_MomentOfForce";
            this.res_MomentOfForce.Size = new System.Drawing.Size(13, 13);
            this.res_MomentOfForce.TabIndex = 11;
            this.res_MomentOfForce.Text = "0";
                                                this.cmb_Len2.FormattingEnabled = true;
            this.cmb_Len2.Location = new System.Drawing.Point(431, 3);
            this.cmb_Len2.Name = "cmb_Len2";
            this.cmb_Len2.Size = new System.Drawing.Size(101, 21);
            this.cmb_Len2.TabIndex = 12;
            this.cmb_Len2.SelectedIndexChanged += new System.EventHandler(this.cmb_Len2_SelectedIndexChanged);
                                                this.cmb_Force2.FormattingEnabled = true;
            this.cmb_Force2.Location = new System.Drawing.Point(431, 28);
            this.cmb_Force2.Name = "cmb_Force2";
            this.cmb_Force2.Size = new System.Drawing.Size(101, 21);
            this.cmb_Force2.TabIndex = 13;
            this.cmb_Force2.SelectedIndexChanged += new System.EventHandler(this.cmb_Force2_SelectedIndexChanged);
                                                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 370);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormForTest";
            this.Text = "FormForTest";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_Len)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Force)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_MomentOfForce)).EndInit();
            this.ResumeLayout(false);
        }
                private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_Len1;
        private System.Windows.Forms.NumericUpDown num_Len;
        private System.Windows.Forms.NumericUpDown num_Force;
        private System.Windows.Forms.NumericUpDown num_MomentOfForce;
        private System.Windows.Forms.ComboBox cmb_Force1;
        private System.Windows.Forms.ComboBox cmb_MomentOfForce1;
        private System.Windows.Forms.ComboBox cmb_MomentOfForce2;
        private System.Windows.Forms.Label res_Len;
        private System.Windows.Forms.Label res_Force;
        private System.Windows.Forms.Label res_MomentOfForce;
        private System.Windows.Forms.ComboBox cmb_Len2;
        private System.Windows.Forms.ComboBox cmb_Force2;
    }
}