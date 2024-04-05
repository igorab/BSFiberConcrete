namespace BSFiberConcrete
{
    partial class BSGraph
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblF05 = new System.Windows.Forms.Label();
            this.lblF25 = new System.Windows.Forms.Label();
            this.lblFeL = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.btnDrawChart = new System.Windows.Forms.Button();
            this.gridFaF = new System.Windows.Forms.DataGridView();
            this.numDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aFDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.faFBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Chart)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFaF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.faFBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Chart
            // 
            chartArea2.Name = "ChartArea1";
            this.Chart.ChartAreas.Add(chartArea2);
            this.Chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "aFL";
            this.Chart.Legends.Add(legend2);
            this.Chart.Location = new System.Drawing.Point(367, 3);
            this.Chart.Name = "Chart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "aFL";
            series2.Name = "AFSerie";
            this.Chart.Series.Add(series2);
            this.Chart.Size = new System.Drawing.Size(711, 483);
            this.Chart.TabIndex = 0;
            this.Chart.Text = "aF";
            title2.Name = "aF";
            title2.Text = "Нагрузка-перемещение внешних граней надреза";
            this.Chart.Titles.Add(title2);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.67253F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.32748F));
            this.tableLayoutPanel.Controls.Add(this.Chart, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel1, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.btnDrawChart, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.gridFaF, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75.61729F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.38272F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1081, 648);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.18868F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.81132F));
            this.tableLayoutPanel1.Controls.Add(this.lblF05, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblF25, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblFeL, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDown1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDown2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDown3, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(367, 492);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(318, 100);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lblF05
            // 
            this.lblF05.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblF05.AutoSize = true;
            this.lblF05.Location = new System.Drawing.Point(59, 9);
            this.lblF05.Name = "lblF05";
            this.lblF05.Size = new System.Drawing.Size(34, 13);
            this.lblF05.TabIndex = 0;
            this.lblF05.Text = "F 0,5:";
            // 
            // lblF25
            // 
            this.lblF25.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblF25.AutoSize = true;
            this.lblF25.Location = new System.Drawing.Point(59, 41);
            this.lblF25.Name = "lblF25";
            this.lblF25.Size = new System.Drawing.Size(34, 13);
            this.lblF25.TabIndex = 1;
            this.lblF25.Text = "F 2,5:";
            // 
            // lblFeL
            // 
            this.lblFeL.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFeL.AutoSize = true;
            this.lblFeL.Location = new System.Drawing.Point(66, 75);
            this.lblFeL.Name = "lblFeL";
            this.lblFeL.Size = new System.Drawing.Size(27, 13);
            this.lblFeL.TabIndex = 2;
            this.lblFeL.Text = "F el:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(99, 3);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 3;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(99, 35);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown2.TabIndex = 4;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(99, 67);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown3.TabIndex = 5;
            // 
            // btnDrawChart
            // 
            this.btnDrawChart.Location = new System.Drawing.Point(3, 492);
            this.btnDrawChart.Name = "btnDrawChart";
            this.btnDrawChart.Size = new System.Drawing.Size(75, 23);
            this.btnDrawChart.TabIndex = 2;
            this.btnDrawChart.Text = "button1";
            this.btnDrawChart.UseVisualStyleBackColor = true;
            this.btnDrawChart.Click += new System.EventHandler(this.btnDrawChart_Click);
            // 
            // gridFaF
            // 
            this.gridFaF.AutoGenerateColumns = false;
            this.gridFaF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFaF.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numDataGridViewTextBoxColumn,
            this.aFDataGridViewTextBoxColumn,
            this.fDataGridViewTextBoxColumn});
            this.gridFaF.DataSource = this.faFBindingSource;
            this.gridFaF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFaF.Location = new System.Drawing.Point(3, 3);
            this.gridFaF.Name = "gridFaF";
            this.gridFaF.Size = new System.Drawing.Size(358, 483);
            this.gridFaF.TabIndex = 3;
            // 
            // numDataGridViewTextBoxColumn
            // 
            this.numDataGridViewTextBoxColumn.DataPropertyName = "Num";
            this.numDataGridViewTextBoxColumn.HeaderText = "Num";
            this.numDataGridViewTextBoxColumn.Name = "numDataGridViewTextBoxColumn";
            // 
            // aFDataGridViewTextBoxColumn
            // 
            this.aFDataGridViewTextBoxColumn.DataPropertyName = "aF";
            this.aFDataGridViewTextBoxColumn.HeaderText = "aF";
            this.aFDataGridViewTextBoxColumn.Name = "aFDataGridViewTextBoxColumn";
            // 
            // fDataGridViewTextBoxColumn
            // 
            this.fDataGridViewTextBoxColumn.DataPropertyName = "F";
            this.fDataGridViewTextBoxColumn.HeaderText = "F";
            this.fDataGridViewTextBoxColumn.Name = "fDataGridViewTextBoxColumn";
            // 
            // faFBindingSource
            // 
            this.faFBindingSource.DataSource = typeof(BSFiberConcrete.FaF);
            // 
            // BSGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 648);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "BSGraph";
            this.Text = "BSGraph";
            this.Load += new System.EventHandler(this.BSGraph_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Chart)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFaF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.faFBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart Chart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblF05;
        private System.Windows.Forms.Label lblF25;
        private System.Windows.Forms.Label lblFeL;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Button btnDrawChart;
        private System.Windows.Forms.DataGridView gridFaF;
        private System.Windows.Forms.DataGridViewTextBoxColumn numDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aFDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource faFBindingSource;
    }
}