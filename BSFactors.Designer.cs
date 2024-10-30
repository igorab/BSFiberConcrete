namespace BSFiberConcrete
{
    partial class BSFactors
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

        #region Windows Form Designer generated code

                                        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridFactors = new System.Windows.Forms.DataGridView();
            this.Factor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Coeff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Loading = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Percent = new TestGrid.DataGridViewProgressColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.dataGridViewProgressColumn1 = new TestGrid.DataGridViewProgressColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridFactors)).BeginInit();
            this.SuspendLayout();
                                                this.gridFactors.AllowUserToAddRows = false;
            this.gridFactors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridFactors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFactors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Factor,
            this.Descr,
            this.Coeff,
            this.Loading,
            this.Percent});
            this.gridFactors.Location = new System.Drawing.Point(12, 12);
            this.gridFactors.Name = "gridFactors";
            this.gridFactors.Size = new System.Drawing.Size(569, 274);
            this.gridFactors.TabIndex = 0;
                                                this.Factor.DataPropertyName = "Factor";
            this.Factor.HeaderText = "Фактор (проверка)";
            this.Factor.Name = "Factor";
                                                this.Descr.HeaderText = "Описание";
            this.Descr.Name = "Descr";
                                                dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = "0";
            this.Coeff.DefaultCellStyle = dataGridViewCellStyle1;
            this.Coeff.HeaderText = "Коэффициент";
            this.Coeff.Name = "Coeff";
                                                this.Loading.HeaderText = "Загружение";
            this.Loading.Name = "Loading";
            this.Loading.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Loading.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
                                                this.Percent.DataPropertyName = "Percent";
            this.Percent.HeaderText = "Процент";
            this.Percent.Name = "Percent";
            this.Percent.ReadOnly = true;
                                                this.btnClose.Location = new System.Drawing.Point(506, 292);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
                                                this.dataGridViewProgressColumn1.HeaderText = "Процент";
            this.dataGridViewProgressColumn1.Name = "dataGridViewProgressColumn1";
            this.dataGridViewProgressColumn1.ReadOnly = true;
                                                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 323);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gridFactors);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BSFactors";
            this.Text = "Диаграмма факторов";
            this.Load += new System.EventHandler(this.BSFactors_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridFactors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridFactors;
        private System.Windows.Forms.Button btnClose;
        private TestGrid.DataGridViewProgressColumn dataGridViewProgressColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Factor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Coeff;
        private System.Windows.Forms.DataGridViewTextBoxColumn Loading;
        private TestGrid.DataGridViewProgressColumn Percent;
    }
}