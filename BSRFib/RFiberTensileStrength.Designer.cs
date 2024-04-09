namespace BSFiberConcrete.BSRFib
{
    partial class RFiberTensileStrength
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
            this.dataGridFFF = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.fibLabBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.felDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f05DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f25DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFFF)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fibLabBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridFFF
            // 
            this.dataGridFFF.AutoGenerateColumns = false;
            this.dataGridFFF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridFFF.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.felDataGridViewTextBoxColumn,
            this.f05DataGridViewTextBoxColumn,
            this.f25DataGridViewTextBoxColumn});
            this.dataGridFFF.DataSource = this.fibLabBindingSource;
            this.dataGridFFF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridFFF.Location = new System.Drawing.Point(3, 3);
            this.dataGridFFF.Name = "dataGridFFF";
            this.dataGridFFF.Size = new System.Drawing.Size(510, 489);
            this.dataGridFFF.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.63415F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.36585F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridFFF, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(692, 576);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // fibLabBindingSource
            // 
            this.fibLabBindingSource.DataSource = typeof(BSFiberConcrete.FibLab);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // felDataGridViewTextBoxColumn
            // 
            this.felDataGridViewTextBoxColumn.DataPropertyName = "Fel";
            this.felDataGridViewTextBoxColumn.HeaderText = "Fel";
            this.felDataGridViewTextBoxColumn.Name = "felDataGridViewTextBoxColumn";
            // 
            // f05DataGridViewTextBoxColumn
            // 
            this.f05DataGridViewTextBoxColumn.DataPropertyName = "F05";
            this.f05DataGridViewTextBoxColumn.HeaderText = "F05";
            this.f05DataGridViewTextBoxColumn.Name = "f05DataGridViewTextBoxColumn";
            // 
            // f25DataGridViewTextBoxColumn
            // 
            this.f25DataGridViewTextBoxColumn.DataPropertyName = "F25";
            this.f25DataGridViewTextBoxColumn.HeaderText = "F25";
            this.f25DataGridViewTextBoxColumn.Name = "f25DataGridViewTextBoxColumn";
            // 
            // RFiberTensileStrength
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 576);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RFiberTensileStrength";
            this.Text = "Определение прочности на растяжение";
            this.Load += new System.EventHandler(this.RFiberTensileStrength_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFFF)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fibLabBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridFFF;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.BindingSource fibLabBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn felDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn f05DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn f25DataGridViewTextBoxColumn;
    }
}