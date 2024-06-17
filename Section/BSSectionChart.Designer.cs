namespace BSFiberConcrete.Section
{
    partial class BSSectionChart
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnDraw = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.labelSection = new System.Windows.Forms.Label();
            this.labelRods = new System.Windows.Forms.Label();
            this.bSRodDataGridView = new System.Windows.Forms.DataGridView();
            this.numDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pointBS = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RodBS = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bSRodDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RodBS)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 4;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.83517F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 225F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.16483F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel.Controls.Add(this.btnSave, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.chart, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.btnDraw, 2, 2);
            this.tableLayoutPanel.Controls.Add(this.dataGrid, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.labelSection, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelRods, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.bSRodDataGridView, 1, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.865772F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.13423F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1131, 717);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Location = new System.Drawing.Point(129, 615);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(41, 35);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "s";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnDel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnAdd, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 615);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(120, 99);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDel.Location = new System.Drawing.Point(63, 3);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(41, 35);
            this.btnDel.TabIndex = 3;
            this.btnDel.Text = "-";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(41, 35);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // chart
            // 
            this.chart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chart.BorderlineWidth = 3;
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(354, 32);
            this.chart.Name = "chart";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Сечение";
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.EmptyPointStyle.BorderWidth = 3;
            series2.Legend = "Legend1";
            series2.MarkerSize = 10;
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series2.Name = "Армирование";
            this.chart.Series.Add(series1);
            this.chart.Series.Add(series2);
            this.chart.Size = new System.Drawing.Size(718, 577);
            this.chart.TabIndex = 0;
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(354, 615);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(75, 23);
            this.btnDraw.TabIndex = 1;
            this.btnDraw.Text = "Рисовать";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // dataGrid
            // 
            this.dataGrid.AutoGenerateColumns = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numDataGridViewTextBoxColumn,
            this.xDataGridViewTextBoxColumn,
            this.yDataGridViewTextBoxColumn});
            this.dataGrid.DataSource = this.pointBS;
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.Location = new System.Drawing.Point(3, 32);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.Size = new System.Drawing.Size(120, 577);
            this.dataGrid.TabIndex = 2;
            // 
            // labelSection
            // 
            this.labelSection.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelSection.AutoSize = true;
            this.labelSection.Location = new System.Drawing.Point(3, 8);
            this.labelSection.Name = "labelSection";
            this.labelSection.Size = new System.Drawing.Size(49, 13);
            this.labelSection.TabIndex = 6;
            this.labelSection.Text = "Сечение";
            // 
            // labelRods
            // 
            this.labelRods.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelRods.AutoSize = true;
            this.labelRods.Location = new System.Drawing.Point(129, 8);
            this.labelRods.Name = "labelRods";
            this.labelRods.Size = new System.Drawing.Size(76, 13);
            this.labelRods.TabIndex = 7;
            this.labelRods.Text = "Армирование";
            // 
            // bSRodDataGridView
            // 
            this.bSRodDataGridView.AutoGenerateColumns = false;
            this.bSRodDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bSRodDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.bSRodDataGridView.DataSource = this.RodBS;
            this.bSRodDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bSRodDataGridView.Location = new System.Drawing.Point(129, 32);
            this.bSRodDataGridView.Name = "bSRodDataGridView";
            this.bSRodDataGridView.RowHeadersVisible = false;
            this.bSRodDataGridView.Size = new System.Drawing.Size(219, 577);
            this.bSRodDataGridView.TabIndex = 7;
            // 
            // numDataGridViewTextBoxColumn
            // 
            this.numDataGridViewTextBoxColumn.DataPropertyName = "Num";
            this.numDataGridViewTextBoxColumn.HeaderText = "Num";
            this.numDataGridViewTextBoxColumn.Name = "numDataGridViewTextBoxColumn";
            this.numDataGridViewTextBoxColumn.Width = 20;
            // 
            // xDataGridViewTextBoxColumn
            // 
            this.xDataGridViewTextBoxColumn.DataPropertyName = "X";
            this.xDataGridViewTextBoxColumn.HeaderText = "X";
            this.xDataGridViewTextBoxColumn.Name = "xDataGridViewTextBoxColumn";
            this.xDataGridViewTextBoxColumn.Width = 30;
            // 
            // yDataGridViewTextBoxColumn
            // 
            this.yDataGridViewTextBoxColumn.DataPropertyName = "Y";
            this.yDataGridViewTextBoxColumn.HeaderText = "Y";
            this.yDataGridViewTextBoxColumn.Name = "yDataGridViewTextBoxColumn";
            this.yDataGridViewTextBoxColumn.Width = 30;
            // 
            // pointBS
            // 
            this.pointBS.DataSource = typeof(BSFiberConcrete.Section.BSPoint);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ToolTipText = "Номер стержня";
            this.dataGridViewTextBoxColumn1.Width = 20;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "CG_X";
            this.dataGridViewTextBoxColumn2.HeaderText = "X";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ToolTipText = "X, см";
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "CG_Y";
            this.dataGridViewTextBoxColumn3.HeaderText = "Y";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ToolTipText = "Y, см";
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "D";
            this.dataGridViewTextBoxColumn4.HeaderText = "D";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ToolTipText = "Диаметр, см";
            this.dataGridViewTextBoxColumn4.Width = 50;
            // 
            // RodBS
            // 
            this.RodBS.DataSource = typeof(BSFiberConcrete.BSRod);
            // 
            // BSSectionChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 717);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "BSSectionChart";
            this.Text = "Сечение";
            this.Load += new System.EventHandler(this.BSSectionChart_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bSRodDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RodBS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.BindingSource pointBS;
        private System.Windows.Forms.DataGridViewTextBoxColumn numDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn yDataGridViewTextBoxColumn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label labelSection;
        private System.Windows.Forms.Label labelRods;
        private System.Windows.Forms.DataGridView bSRodDataGridView;
        private System.Windows.Forms.BindingSource RodBS;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}