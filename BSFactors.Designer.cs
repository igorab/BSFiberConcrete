namespace BSFiberConcrete
{
    partial class BSFactors
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridFactors = new System.Windows.Forms.DataGridView();
            this.Factor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Coeff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Img = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Load = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridFactors)).BeginInit();
            this.SuspendLayout();
            // 
            // gridFactors
            // 
            this.gridFactors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridFactors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFactors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Factor,
            this.Descr,
            this.Coeff,
            this.Img,
            this.Load});
            this.gridFactors.Location = new System.Drawing.Point(12, 29);
            this.gridFactors.Name = "gridFactors";
            this.gridFactors.Size = new System.Drawing.Size(722, 312);
            this.gridFactors.TabIndex = 0;
            // 
            // Factor
            // 
            this.Factor.HeaderText = "Фактор (проверка)";
            this.Factor.Name = "Factor";
            // 
            // Descr
            // 
            this.Descr.HeaderText = "Описание";
            this.Descr.Name = "Descr";
            // 
            // Coeff
            // 
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.Coeff.DefaultCellStyle = dataGridViewCellStyle3;
            this.Coeff.HeaderText = "Коэффициент";
            this.Coeff.Name = "Coeff";
            // 
            // Img
            // 
            this.Img.HeaderText = "отображение";
            this.Img.Name = "Img";
            this.Img.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Load
            // 
            this.Load.HeaderText = "Загружение";
            this.Load.Name = "Load";
            this.Load.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Load.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(650, 387);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // BSFactors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 442);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gridFactors);
            this.Name = "BSFactors";
            this.Text = "Диаграмма факторов";
            ((System.ComponentModel.ISupportInitialize)(this.gridFactors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridFactors;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn Factor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Coeff;
        private System.Windows.Forms.DataGridViewLinkColumn Img;
        private System.Windows.Forms.DataGridViewTextBoxColumn Load;
    }
}