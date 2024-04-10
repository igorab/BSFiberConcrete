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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RFiberTensileStrength));
            this.dataGridFFF = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.labelRfbt2n = new System.Windows.Forms.Label();
            this.labelRfbt3n = new System.Windows.Forms.Label();
            this.labelRFel = new System.Windows.Forms.Label();
            this.numRfbt2n = new System.Windows.Forms.NumericUpDown();
            this.numRfbt3n = new System.Windows.Forms.NumericUpDown();
            this.numRFel = new System.Windows.Forms.NumericUpDown();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.felDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f05DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f25DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fibLabBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFFF)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRfbt2n)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRfbt3n)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRFel)).BeginInit();
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
            this.dataGridFFF.Size = new System.Drawing.Size(457, 489);
            this.dataGridFFF.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.90752F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.09249F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridFFF, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(692, 576);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.32287F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.67713F));
            this.tableLayoutPanel2.Controls.Add(this.labelRfbt2n, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.labelRfbt3n, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.labelRFel, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.numRfbt2n, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.numRfbt3n, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.numRFel, 1, 4);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(466, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.3871F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.6129F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(223, 292);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.btnCalculate, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 498);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(457, 75);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalculate.Image = ((System.Drawing.Image)(resources.GetObject("btnCalculate.Image")));
            this.btnCalculate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCalculate.Location = new System.Drawing.Point(361, 3);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(93, 23);
            this.btnCalculate.TabIndex = 0;
            this.btnCalculate.Text = "Рассчитать";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // labelRfbt2n
            // 
            this.labelRfbt2n.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelRfbt2n.AutoSize = true;
            this.labelRfbt2n.Location = new System.Drawing.Point(32, 60);
            this.labelRfbt2n.Name = "labelRfbt2n";
            this.labelRfbt2n.Size = new System.Drawing.Size(45, 13);
            this.labelRfbt2n.TabIndex = 0;
            this.labelRfbt2n.Text = "Rfbt2, n";
            // 
            // labelRfbt3n
            // 
            this.labelRfbt3n.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelRfbt3n.AutoSize = true;
            this.labelRfbt3n.Location = new System.Drawing.Point(32, 97);
            this.labelRfbt3n.Name = "labelRfbt3n";
            this.labelRfbt3n.Size = new System.Drawing.Size(45, 13);
            this.labelRfbt3n.TabIndex = 1;
            this.labelRfbt3n.Text = "Rfbt3, n";
            // 
            // labelRFel
            // 
            this.labelRFel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelRFel.AutoSize = true;
            this.labelRFel.Location = new System.Drawing.Point(45, 134);
            this.labelRFel.Name = "labelRFel";
            this.labelRFel.Size = new System.Drawing.Size(32, 13);
            this.labelRFel.TabIndex = 2;
            this.labelRFel.Text = "R Fel";
            // 
            // numRfbt2n
            // 
            this.numRfbt2n.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numRfbt2n.DecimalPlaces = 4;
            this.numRfbt2n.Location = new System.Drawing.Point(83, 56);
            this.numRfbt2n.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numRfbt2n.Name = "numRfbt2n";
            this.numRfbt2n.Size = new System.Drawing.Size(120, 20);
            this.numRfbt2n.TabIndex = 3;
            // 
            // numRfbt3n
            // 
            this.numRfbt3n.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numRfbt3n.DecimalPlaces = 4;
            this.numRfbt3n.Location = new System.Drawing.Point(83, 94);
            this.numRfbt3n.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numRfbt3n.Name = "numRfbt3n";
            this.numRfbt3n.Size = new System.Drawing.Size(120, 20);
            this.numRfbt3n.TabIndex = 4;
            // 
            // numRFel
            // 
            this.numRFel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numRFel.DecimalPlaces = 4;
            this.numRFel.Location = new System.Drawing.Point(83, 131);
            this.numRFel.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numRFel.Name = "numRFel";
            this.numRFel.Size = new System.Drawing.Size(120, 20);
            this.numRFel.TabIndex = 5;
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
            // fibLabBindingSource
            // 
            this.fibLabBindingSource.DataSource = typeof(BSFiberConcrete.FibLab);
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
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numRfbt2n)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRfbt3n)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRFel)).EndInit();
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelRfbt2n;
        private System.Windows.Forms.Label labelRfbt3n;
        private System.Windows.Forms.Label labelRFel;
        private System.Windows.Forms.NumericUpDown numRfbt2n;
        private System.Windows.Forms.NumericUpDown numRfbt3n;
        private System.Windows.Forms.NumericUpDown numRFel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnCalculate;
    }
}