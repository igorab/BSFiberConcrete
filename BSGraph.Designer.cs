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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSGraph));
            this.ChartFaF = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelCalcRes = new System.Windows.Forms.TableLayoutPanel();
            this.lblF05 = new System.Windows.Forms.Label();
            this.lblF25 = new System.Windows.Forms.Label();
            this.lblFeL = new System.Windows.Forms.Label();
            this.numF05 = new System.Windows.Forms.NumericUpDown();
            this.numF25 = new System.Windows.Forms.NumericUpDown();
            this.numFel = new System.Windows.Forms.NumericUpDown();
            this.gridFaF = new System.Windows.Forms.DataGridView();
            this.btnDrawChart = new System.Windows.Forms.Button();
            this.tableLayoutPanelGrid = new System.Windows.Forms.TableLayoutPanel();
            this.btnDSAdd = new System.Windows.Forms.Button();
            this.btnDSSave = new System.Windows.Forms.Button();
            this.faFBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.numDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aFDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ChartFaF)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.tableLayoutPanelCalcRes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numF05)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numF25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFaF)).BeginInit();
            this.tableLayoutPanelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.faFBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ChartFaF
            // 
            chartArea1.Name = "ChartArea1";
            this.ChartFaF.ChartAreas.Add(chartArea1);
            this.ChartFaF.DataSource = this.faFBindingSource;
            this.ChartFaF.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "aFL";
            legend1.Title = "Fi-aFi";
            this.ChartFaF.Legends.Add(legend1);
            this.ChartFaF.Location = new System.Drawing.Point(367, 25);
            this.ChartFaF.Name = "ChartFaF";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.EmptyPointStyle.AxisLabel = "Fi";
            series1.Legend = "aFL";
            series1.Name = "AFSerie";
            series1.XValueMember = "aF";
            series1.YValueMembers = "F";
            this.ChartFaF.Series.Add(series1);
            this.ChartFaF.Size = new System.Drawing.Size(710, 424);
            this.ChartFaF.TabIndex = 0;
            this.ChartFaF.Text = "aF";
            title1.Name = "aF";
            title1.Text = "Нагрузка-перемещение внешних граней надреза";
            this.ChartFaF.Titles.Add(title1);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.67253F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.32748F));
            this.tableLayoutPanel.Controls.Add(this.ChartFaF, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanelCalcRes, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.gridFaF, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.btnDrawChart, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanelGrid, 0, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 5;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.86124F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.13876F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1081, 635);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // tableLayoutPanelCalcRes
            // 
            this.tableLayoutPanelCalcRes.ColumnCount = 2;
            this.tableLayoutPanelCalcRes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.18868F));
            this.tableLayoutPanelCalcRes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.81132F));
            this.tableLayoutPanelCalcRes.Controls.Add(this.lblF05, 0, 0);
            this.tableLayoutPanelCalcRes.Controls.Add(this.lblF25, 0, 1);
            this.tableLayoutPanelCalcRes.Controls.Add(this.lblFeL, 0, 2);
            this.tableLayoutPanelCalcRes.Controls.Add(this.numF05, 1, 0);
            this.tableLayoutPanelCalcRes.Controls.Add(this.numF25, 1, 1);
            this.tableLayoutPanelCalcRes.Controls.Add(this.numFel, 1, 2);
            this.tableLayoutPanelCalcRes.Location = new System.Drawing.Point(367, 511);
            this.tableLayoutPanelCalcRes.Name = "tableLayoutPanelCalcRes";
            this.tableLayoutPanelCalcRes.RowCount = 3;
            this.tableLayoutPanelCalcRes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelCalcRes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelCalcRes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanelCalcRes.Size = new System.Drawing.Size(318, 96);
            this.tableLayoutPanelCalcRes.TabIndex = 1;
            // 
            // lblF05
            // 
            this.lblF05.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblF05.AutoSize = true;
            this.lblF05.Location = new System.Drawing.Point(59, 8);
            this.lblF05.Name = "lblF05";
            this.lblF05.Size = new System.Drawing.Size(34, 13);
            this.lblF05.TabIndex = 0;
            this.lblF05.Text = "F 0,5:";
            // 
            // lblF25
            // 
            this.lblF25.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblF25.AutoSize = true;
            this.lblF25.Location = new System.Drawing.Point(59, 38);
            this.lblF25.Name = "lblF25";
            this.lblF25.Size = new System.Drawing.Size(34, 13);
            this.lblF25.TabIndex = 1;
            this.lblF25.Text = "F 2,5:";
            // 
            // lblFeL
            // 
            this.lblFeL.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFeL.AutoSize = true;
            this.lblFeL.Location = new System.Drawing.Point(66, 71);
            this.lblFeL.Name = "lblFeL";
            this.lblFeL.Size = new System.Drawing.Size(27, 13);
            this.lblFeL.TabIndex = 2;
            this.lblFeL.Text = "F el:";
            // 
            // numF05
            // 
            this.numF05.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numF05.DecimalPlaces = 4;
            this.numF05.Location = new System.Drawing.Point(99, 5);
            this.numF05.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numF05.Name = "numF05";
            this.numF05.Size = new System.Drawing.Size(120, 20);
            this.numF05.TabIndex = 3;
            // 
            // numF25
            // 
            this.numF25.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numF25.DecimalPlaces = 4;
            this.numF25.Location = new System.Drawing.Point(99, 35);
            this.numF25.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numF25.Name = "numF25";
            this.numF25.Size = new System.Drawing.Size(120, 20);
            this.numF25.TabIndex = 4;
            // 
            // numFel
            // 
            this.numFel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numFel.DecimalPlaces = 4;
            this.numFel.Location = new System.Drawing.Point(99, 68);
            this.numFel.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numFel.Name = "numFel";
            this.numFel.Size = new System.Drawing.Size(120, 20);
            this.numFel.TabIndex = 5;
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
            this.gridFaF.Location = new System.Drawing.Point(4, 25);
            this.gridFaF.Name = "gridFaF";
            this.gridFaF.Size = new System.Drawing.Size(356, 424);
            this.gridFaF.TabIndex = 3;
            // 
            // btnDrawChart
            // 
            this.btnDrawChart.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDrawChart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDrawChart.BackgroundImage")));
            this.btnDrawChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDrawChart.Location = new System.Drawing.Point(957, 466);
            this.btnDrawChart.Name = "btnDrawChart";
            this.btnDrawChart.Size = new System.Drawing.Size(120, 28);
            this.btnDrawChart.TabIndex = 2;
            this.btnDrawChart.Text = "Построить";
            this.btnDrawChart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDrawChart.UseVisualStyleBackColor = true;
            this.btnDrawChart.Click += new System.EventHandler(this.btnDrawChart_Click);
            // 
            // tableLayoutPanelGrid
            // 
            this.tableLayoutPanelGrid.ColumnCount = 4;
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanelGrid.Controls.Add(this.btnDSAdd, 3, 0);
            this.tableLayoutPanelGrid.Controls.Add(this.btnDSSave, 1, 0);
            this.tableLayoutPanelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelGrid.Location = new System.Drawing.Point(4, 456);
            this.tableLayoutPanelGrid.Name = "tableLayoutPanelGrid";
            this.tableLayoutPanelGrid.RowCount = 1;
            this.tableLayoutPanelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelGrid.Size = new System.Drawing.Size(356, 48);
            this.tableLayoutPanelGrid.TabIndex = 6;
            // 
            // btnDSAdd
            // 
            this.btnDSAdd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDSAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDSAdd.Location = new System.Drawing.Point(316, 7);
            this.btnDSAdd.Name = "btnDSAdd";
            this.btnDSAdd.Size = new System.Drawing.Size(37, 33);
            this.btnDSAdd.TabIndex = 4;
            this.btnDSAdd.Text = "+";
            this.btnDSAdd.UseVisualStyleBackColor = true;
            this.btnDSAdd.Click += new System.EventHandler(this.btnDSAdd_Click);
            // 
            // btnDSSave
            // 
            this.btnDSSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDSSave.Image = ((System.Drawing.Image)(resources.GetObject("btnDSSave.Image")));
            this.btnDSSave.Location = new System.Drawing.Point(225, 6);
            this.btnDSSave.Name = "btnDSSave";
            this.btnDSSave.Size = new System.Drawing.Size(38, 35);
            this.btnDSSave.TabIndex = 5;
            this.btnDSSave.UseVisualStyleBackColor = true;
            // 
            // faFBindingSource
            // 
            this.faFBindingSource.DataSource = typeof(BSFiberConcrete.FaF);
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
            // BSGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 635);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "BSGraph";
            this.Text = "График \"нагрузка-перемещение внешних граней надреза\"";
            this.Load += new System.EventHandler(this.BSGraph_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ChartFaF)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanelCalcRes.ResumeLayout(false);
            this.tableLayoutPanelCalcRes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numF05)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numF25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFaF)).EndInit();
            this.tableLayoutPanelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.faFBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart ChartFaF;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelCalcRes;
        private System.Windows.Forms.Label lblF05;
        private System.Windows.Forms.Label lblF25;
        private System.Windows.Forms.Label lblFeL;
        private System.Windows.Forms.NumericUpDown numF05;
        private System.Windows.Forms.NumericUpDown numF25;
        private System.Windows.Forms.NumericUpDown numFel;
        private System.Windows.Forms.Button btnDrawChart;
        private System.Windows.Forms.DataGridView gridFaF;
        private System.Windows.Forms.BindingSource faFBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn numDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aFDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnDSAdd;
        private System.Windows.Forms.Button btnDSSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGrid;
    }
}