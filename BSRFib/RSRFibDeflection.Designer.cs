namespace BSFiberConcrete.BSRFib
{
    partial class RSRFibDeflection
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanelDeflection = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aFDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deflectionfaFBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanelDeflection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deflectionfaFBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelDeflection
            // 
            this.tableLayoutPanelDeflection.ColumnCount = 2;
            this.tableLayoutPanelDeflection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.625F));
            this.tableLayoutPanelDeflection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.375F));
            this.tableLayoutPanelDeflection.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanelDeflection.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanelDeflection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelDeflection.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelDeflection.Name = "tableLayoutPanelDeflection";
            this.tableLayoutPanelDeflection.RowCount = 3;
            this.tableLayoutPanelDeflection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.51282F));
            this.tableLayoutPanelDeflection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.48718F));
            this.tableLayoutPanelDeflection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanelDeflection.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanelDeflection.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.aFDataGridViewTextBoxColumn,
            this.fDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.deflectionfaFBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 44);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(679, 343);
            this.dataGridView1.TabIndex = 0;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // aFDataGridViewTextBoxColumn
            // 
            this.aFDataGridViewTextBoxColumn.DataPropertyName = "aF";
            this.aFDataGridViewTextBoxColumn.HeaderText = "aF";
            this.aFDataGridViewTextBoxColumn.Name = "aFDataGridViewTextBoxColumn";
            // 
            // fDataGridViewTextBoxColumn
            // 
            this.fDataGridViewTextBoxColumn.DataPropertyName = "f";
            this.fDataGridViewTextBoxColumn.HeaderText = "f";
            this.fDataGridViewTextBoxColumn.Name = "fDataGridViewTextBoxColumn";
            // 
            // deflectionfaFBindingSource
            // 
            this.deflectionfaFBindingSource.DataSource = typeof(BSFiberConcrete.Deflection_f_aF);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(679, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Зависимость aF- f прогибов";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RSRFibDeflection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanelDeflection);
            this.Name = "RSRFibDeflection";
            this.Text = "Измерение величны прогибов";
            this.Load += new System.EventHandler(this.RSRFibDeflection_Load);
            this.tableLayoutPanelDeflection.ResumeLayout(false);
            this.tableLayoutPanelDeflection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deflectionfaFBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDeflection;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aFDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource deflectionfaFBindingSource;
        private System.Windows.Forms.Label label1;
    }
}