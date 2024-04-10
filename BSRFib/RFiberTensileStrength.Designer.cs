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
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.L = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.felDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f05DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f25DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fibLabBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelRfbt2n = new System.Windows.Forms.Label();
            this.labelRfbt3n = new System.Windows.Forms.Label();
            this.labelRFbtn = new System.Windows.Forms.Label();
            this.numRfbt2n = new System.Windows.Forms.NumericUpDown();
            this.numRfbt3n = new System.Windows.Forms.NumericUpDown();
            this.numRFbtn = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCalculate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFFF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fibLabBindingSource)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRfbt2n)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRfbt3n)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRFbtn)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridFFF
            // 
            this.dataGridFFF.AutoGenerateColumns = false;
            this.dataGridFFF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridFFF.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.L,
            this.B,
            this.felDataGridViewTextBoxColumn,
            this.f05DataGridViewTextBoxColumn,
            this.f25DataGridViewTextBoxColumn});
            this.dataGridFFF.DataSource = this.fibLabBindingSource;
            this.dataGridFFF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridFFF.Location = new System.Drawing.Point(3, 3);
            this.dataGridFFF.Name = "dataGridFFF";
            this.dataGridFFF.Size = new System.Drawing.Size(663, 489);
            this.dataGridFFF.TabIndex = 0;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // L
            // 
            this.L.DataPropertyName = "L";
            this.L.HeaderText = "L";
            this.L.Name = "L";
            // 
            // B
            // 
            this.B.DataPropertyName = "B";
            this.B.HeaderText = "B";
            this.B.Name = "B";
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1000, 576);
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
            this.tableLayoutPanel2.Controls.Add(this.labelRFbtn, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.numRfbt2n, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.numRfbt3n, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.numRFbtn, 1, 4);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(672, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.3871F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.6129F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(325, 292);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // labelRfbt2n
            // 
            this.labelRfbt2n.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelRfbt2n.AutoSize = true;
            this.labelRfbt2n.Location = new System.Drawing.Point(67, 60);
            this.labelRfbt2n.Name = "labelRfbt2n";
            this.labelRfbt2n.Size = new System.Drawing.Size(48, 13);
            this.labelRfbt2n.TabIndex = 0;
            this.labelRfbt2n.Text = "R fbt2, n";
            // 
            // labelRfbt3n
            // 
            this.labelRfbt3n.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelRfbt3n.AutoSize = true;
            this.labelRfbt3n.Location = new System.Drawing.Point(67, 97);
            this.labelRfbt3n.Name = "labelRfbt3n";
            this.labelRfbt3n.Size = new System.Drawing.Size(48, 13);
            this.labelRfbt3n.TabIndex = 1;
            this.labelRfbt3n.Text = "R fbt3, n";
            // 
            // labelRFbtn
            // 
            this.labelRFbtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelRFbtn.AutoSize = true;
            this.labelRFbtn.Location = new System.Drawing.Point(73, 134);
            this.labelRFbtn.Name = "labelRFbtn";
            this.labelRFbtn.Size = new System.Drawing.Size(42, 13);
            this.labelRFbtn.TabIndex = 2;
            this.labelRFbtn.Text = "R fbt, n";
            // 
            // numRfbt2n
            // 
            this.numRfbt2n.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numRfbt2n.DecimalPlaces = 4;
            this.numRfbt2n.Location = new System.Drawing.Point(121, 56);
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
            this.numRfbt3n.Location = new System.Drawing.Point(121, 94);
            this.numRfbt3n.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numRfbt3n.Name = "numRfbt3n";
            this.numRfbt3n.Size = new System.Drawing.Size(120, 20);
            this.numRfbt3n.TabIndex = 4;
            // 
            // numRFbtn
            // 
            this.numRFbtn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numRFbtn.DecimalPlaces = 4;
            this.numRFbtn.Location = new System.Drawing.Point(121, 131);
            this.numRFbtn.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numRFbtn.Name = "numRFbtn";
            this.numRFbtn.Size = new System.Drawing.Size(120, 20);
            this.numRFbtn.TabIndex = 5;
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
            this.tableLayoutPanel3.Size = new System.Drawing.Size(663, 75);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalculate.Image = ((System.Drawing.Image)(resources.GetObject("btnCalculate.Image")));
            this.btnCalculate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCalculate.Location = new System.Drawing.Point(567, 3);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(93, 23);
            this.btnCalculate.TabIndex = 0;
            this.btnCalculate.Text = "Рассчитать";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // RFiberTensileStrength
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 576);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RFiberTensileStrength";
            this.Text = "Определение прочности на растяжение";
            this.Load += new System.EventHandler(this.RFiberTensileStrength_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFFF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fibLabBindingSource)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRfbt2n)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRfbt3n)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRFbtn)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridFFF;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.BindingSource fibLabBindingSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelRfbt2n;
        private System.Windows.Forms.Label labelRfbt3n;
        private System.Windows.Forms.Label labelRFbtn;
        private System.Windows.Forms.NumericUpDown numRfbt2n;
        private System.Windows.Forms.NumericUpDown numRfbt3n;
        private System.Windows.Forms.NumericUpDown numRFbtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn L;
        private System.Windows.Forms.DataGridViewTextBoxColumn B;
        private System.Windows.Forms.DataGridViewTextBoxColumn felDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn f05DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn f25DataGridViewTextBoxColumn;
    }
}