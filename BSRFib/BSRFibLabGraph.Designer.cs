namespace BSFiberConcrete
{
    partial class BSRFibLabGraph
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSRFibLabGraph));
            this.ChartFaF = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.faFBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelCalcRes = new System.Windows.Forms.TableLayoutPanel();
            this.lblF05 = new System.Windows.Forms.Label();
            this.lblF25 = new System.Windows.Forms.Label();
            this.lblFeL = new System.Windows.Forms.Label();
            this.numF05 = new System.Windows.Forms.NumericUpDown();
            this.numF25 = new System.Windows.Forms.NumericUpDown();
            this.numFel = new System.Windows.Forms.NumericUpDown();
            this.gridFaF = new System.Windows.Forms.DataGridView();
            this.numDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aFDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanelGrid = new System.Windows.Forms.TableLayoutPanel();
            this.btnDSAdd = new System.Windows.Forms.Button();
            this.btnDSSave = new System.Windows.Forms.Button();
            this.btnDSOpen = new System.Windows.Forms.Button();
            this.btnDSSave2File = new System.Windows.Forms.Button();
            this.btnDSDel = new System.Windows.Forms.Button();
            this.labelBarSample = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtBarSample = new System.Windows.Forms.TextBox();
            this.labelL = new System.Windows.Forms.Label();
            this.numL = new System.Windows.Forms.NumericUpDown();
            this.numB = new System.Windows.Forms.NumericUpDown();
            this.labelB = new System.Windows.Forms.Label();
            this.labelHsp = new System.Windows.Forms.Label();
            this.numHsp = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDrawChart = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.ChartFaF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.faFBindingSource)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.tableLayoutPanelCalcRes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numF05)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numF25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFaF)).BeginInit();
            this.tableLayoutPanelGrid.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHsp)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
                                                chartArea1.Name = "ChartArea1";
            this.ChartFaF.ChartAreas.Add(chartArea1);
            this.ChartFaF.DataSource = this.faFBindingSource;
            this.ChartFaF.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "aFL";
            legend1.Title = "Fi-aFi";
            this.ChartFaF.Legends.Add(legend1);
            this.ChartFaF.Location = new System.Drawing.Point(367, 51);
            this.ChartFaF.Name = "ChartFaF";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.EmptyPointStyle.AxisLabel = "Fi";
            series1.Legend = "aFL";
            series1.Name = "AFSerie";
            series1.XValueMember = "aF";
            series1.YValueMembers = "F";
            this.ChartFaF.Series.Add(series1);
            this.ChartFaF.Size = new System.Drawing.Size(710, 403);
            this.ChartFaF.TabIndex = 0;
            this.ChartFaF.Text = "aF";
            title1.Name = "aF";
            title1.Text = "Нагрузка-перемещение внешних граней надреза";
            this.ChartFaF.Titles.Add(title1);
                                                this.faFBindingSource.DataSource = typeof(BSFiberConcrete.FaF);
                                                this.tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.67253F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.32748F));
            this.tableLayoutPanel.Controls.Add(this.ChartFaF, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanelCalcRes, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.gridFaF, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanelGrid, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.labelBarSample, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel2, 1, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 5;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.86124F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.13876F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1081, 635);
            this.tableLayoutPanel.TabIndex = 1;
                                                this.tableLayoutPanelCalcRes.ColumnCount = 2;
            this.tableLayoutPanelCalcRes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.18868F));
            this.tableLayoutPanelCalcRes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.81132F));
            this.tableLayoutPanelCalcRes.Controls.Add(this.lblF05, 0, 0);
            this.tableLayoutPanelCalcRes.Controls.Add(this.lblF25, 0, 1);
            this.tableLayoutPanelCalcRes.Controls.Add(this.lblFeL, 0, 2);
            this.tableLayoutPanelCalcRes.Controls.Add(this.numF05, 1, 0);
            this.tableLayoutPanelCalcRes.Controls.Add(this.numF25, 1, 1);
            this.tableLayoutPanelCalcRes.Controls.Add(this.numFel, 1, 2);
            this.tableLayoutPanelCalcRes.Location = new System.Drawing.Point(367, 516);
            this.tableLayoutPanelCalcRes.Name = "tableLayoutPanelCalcRes";
            this.tableLayoutPanelCalcRes.RowCount = 3;
            this.tableLayoutPanelCalcRes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelCalcRes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelCalcRes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanelCalcRes.Size = new System.Drawing.Size(318, 91);
            this.tableLayoutPanelCalcRes.TabIndex = 1;
                                                this.lblF05.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblF05.AutoSize = true;
            this.lblF05.Location = new System.Drawing.Point(45, 7);
            this.lblF05.Name = "lblF05";
            this.lblF05.Size = new System.Drawing.Size(48, 13);
            this.lblF05.TabIndex = 0;
            this.lblF05.Text = "F 0,5, Н:";
                                                this.lblF25.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblF25.AutoSize = true;
            this.lblF25.Location = new System.Drawing.Point(45, 35);
            this.lblF25.Name = "lblF25";
            this.lblF25.Size = new System.Drawing.Size(48, 13);
            this.lblF25.TabIndex = 1;
            this.lblF25.Text = "F 2,5, Н:";
                                                this.lblFeL.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFeL.AutoSize = true;
            this.lblFeL.Location = new System.Drawing.Point(52, 67);
            this.lblFeL.Name = "lblFeL";
            this.lblFeL.Size = new System.Drawing.Size(41, 13);
            this.lblFeL.TabIndex = 2;
            this.lblFeL.Text = "F el, Н:";
                                                this.numF05.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numF05.DecimalPlaces = 4;
            this.numF05.Location = new System.Drawing.Point(99, 4);
            this.numF05.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numF05.Name = "numF05";
            this.numF05.Size = new System.Drawing.Size(120, 20);
            this.numF05.TabIndex = 3;
                                                this.numF25.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numF25.DecimalPlaces = 4;
            this.numF25.Location = new System.Drawing.Point(99, 32);
            this.numF25.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numF25.Name = "numF25";
            this.numF25.Size = new System.Drawing.Size(120, 20);
            this.numF25.TabIndex = 4;
                                                this.numFel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numFel.DecimalPlaces = 4;
            this.numFel.Location = new System.Drawing.Point(99, 63);
            this.numFel.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numFel.Name = "numFel";
            this.numFel.Size = new System.Drawing.Size(120, 20);
            this.numFel.TabIndex = 5;
                                                this.gridFaF.AutoGenerateColumns = false;
            this.gridFaF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFaF.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numDataGridViewTextBoxColumn,
            this.aFDataGridViewTextBoxColumn,
            this.fDataGridViewTextBoxColumn});
            this.gridFaF.DataSource = this.faFBindingSource;
            this.gridFaF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFaF.Location = new System.Drawing.Point(4, 51);
            this.gridFaF.Name = "gridFaF";
            this.gridFaF.Size = new System.Drawing.Size(356, 403);
            this.gridFaF.TabIndex = 3;
                                                this.numDataGridViewTextBoxColumn.DataPropertyName = "Num";
            this.numDataGridViewTextBoxColumn.HeaderText = "№";
            this.numDataGridViewTextBoxColumn.Name = "numDataGridViewTextBoxColumn";
            this.numDataGridViewTextBoxColumn.ToolTipText = "Номер измерения";
            this.numDataGridViewTextBoxColumn.Width = 50;
                                                this.aFDataGridViewTextBoxColumn.DataPropertyName = "aF";
            this.aFDataGridViewTextBoxColumn.HeaderText = "aF, мм";
            this.aFDataGridViewTextBoxColumn.Name = "aFDataGridViewTextBoxColumn";
                                                this.fDataGridViewTextBoxColumn.DataPropertyName = "F";
            this.fDataGridViewTextBoxColumn.HeaderText = "F, Н";
            this.fDataGridViewTextBoxColumn.Name = "fDataGridViewTextBoxColumn";
                                                this.tableLayoutPanelGrid.ColumnCount = 6;
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.05732F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.94268F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tableLayoutPanelGrid.Controls.Add(this.btnDSAdd, 5, 0);
            this.tableLayoutPanelGrid.Controls.Add(this.btnDSSave, 2, 0);
            this.tableLayoutPanelGrid.Controls.Add(this.btnDSOpen, 1, 0);
            this.tableLayoutPanelGrid.Controls.Add(this.btnDSSave2File, 3, 0);
            this.tableLayoutPanelGrid.Controls.Add(this.btnDSDel, 4, 0);
            this.tableLayoutPanelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelGrid.Location = new System.Drawing.Point(4, 461);
            this.tableLayoutPanelGrid.Name = "tableLayoutPanelGrid";
            this.tableLayoutPanelGrid.RowCount = 1;
            this.tableLayoutPanelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelGrid.Size = new System.Drawing.Size(356, 48);
            this.tableLayoutPanelGrid.TabIndex = 6;
                                                this.btnDSAdd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDSAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDSAdd.Location = new System.Drawing.Point(316, 7);
            this.btnDSAdd.Name = "btnDSAdd";
            this.btnDSAdd.Size = new System.Drawing.Size(37, 33);
            this.btnDSAdd.TabIndex = 4;
            this.btnDSAdd.Text = "+";
            this.btnDSAdd.UseVisualStyleBackColor = true;
            this.btnDSAdd.Click += new System.EventHandler(this.btnDSAdd_Click);
                                                this.btnDSSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDSSave.Image = ((System.Drawing.Image)(resources.GetObject("btnDSSave.Image")));
            this.btnDSSave.Location = new System.Drawing.Point(115, 6);
            this.btnDSSave.Name = "btnDSSave";
            this.btnDSSave.Size = new System.Drawing.Size(31, 35);
            this.btnDSSave.TabIndex = 5;
            this.btnDSSave.UseVisualStyleBackColor = true;
            this.btnDSSave.Click += new System.EventHandler(this.btnDSSave_Click);
                                                this.btnDSOpen.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDSOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnDSOpen.Image")));
            this.btnDSOpen.Location = new System.Drawing.Point(71, 6);
            this.btnDSOpen.Name = "btnDSOpen";
            this.btnDSOpen.Size = new System.Drawing.Size(38, 35);
            this.btnDSOpen.TabIndex = 6;
            this.btnDSOpen.UseVisualStyleBackColor = true;
            this.btnDSOpen.Click += new System.EventHandler(this.btnDSOpen_Click);
                                                this.btnDSSave2File.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDSSave2File.Image = ((System.Drawing.Image)(resources.GetObject("btnDSSave2File.Image")));
            this.btnDSSave2File.Location = new System.Drawing.Point(160, 6);
            this.btnDSSave2File.Name = "btnDSSave2File";
            this.btnDSSave2File.Size = new System.Drawing.Size(38, 35);
            this.btnDSSave2File.TabIndex = 7;
            this.btnDSSave2File.UseVisualStyleBackColor = true;
            this.btnDSSave2File.Click += new System.EventHandler(this.btnDSSave2File_Click);
                                                this.btnDSDel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDSDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDSDel.Location = new System.Drawing.Point(219, 7);
            this.btnDSDel.Name = "btnDSDel";
            this.btnDSDel.Size = new System.Drawing.Size(37, 33);
            this.btnDSDel.TabIndex = 8;
            this.btnDSDel.Text = "-";
            this.btnDSDel.UseVisualStyleBackColor = true;
            this.btnDSDel.Click += new System.EventHandler(this.btnDSDel_Click);
                                                this.labelBarSample.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelBarSample.AutoSize = true;
            this.labelBarSample.Location = new System.Drawing.Point(258, 17);
            this.labelBarSample.Name = "labelBarSample";
            this.labelBarSample.Size = new System.Drawing.Size(102, 13);
            this.labelBarSample.TabIndex = 7;
            this.labelBarSample.Text = "Описание образца";
                                                this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 137F));
            this.tableLayoutPanel1.Controls.Add(this.txtBarSample, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelL, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.numL, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.numB, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelB, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelHsp, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.numHsp, 6, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(367, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(710, 40);
            this.tableLayoutPanel1.TabIndex = 9;
                                                this.txtBarSample.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtBarSample.Location = new System.Drawing.Point(3, 10);
            this.txtBarSample.Name = "txtBarSample";
            this.txtBarSample.Size = new System.Drawing.Size(198, 20);
            this.txtBarSample.TabIndex = 8;
            this.txtBarSample.Text = "Образец 1";
                                                this.labelL.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelL.AutoSize = true;
            this.labelL.Location = new System.Drawing.Point(217, 13);
            this.labelL.Name = "labelL";
            this.labelL.Size = new System.Drawing.Size(35, 13);
            this.labelL.TabIndex = 9;
            this.labelL.Text = "L, мм";
                                                this.numL.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numL.DecimalPlaces = 2;
            this.numL.Location = new System.Drawing.Point(258, 10);
            this.numL.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numL.Name = "numL";
            this.numL.Size = new System.Drawing.Size(100, 20);
            this.numL.TabIndex = 10;
            this.numL.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
                                                this.numB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numB.DecimalPlaces = 2;
            this.numB.Location = new System.Drawing.Point(408, 10);
            this.numB.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numB.Name = "numB";
            this.numB.Size = new System.Drawing.Size(91, 20);
            this.numB.TabIndex = 11;
            this.numB.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
                                                this.labelB.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelB.AutoSize = true;
            this.labelB.Location = new System.Drawing.Point(367, 13);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(35, 13);
            this.labelB.TabIndex = 12;
            this.labelB.Text = "b, мм";
                                                this.labelHsp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelHsp.AutoSize = true;
            this.labelHsp.Location = new System.Drawing.Point(521, 13);
            this.labelHsp.Name = "labelHsp";
            this.labelHsp.Size = new System.Drawing.Size(49, 13);
            this.labelHsp.TabIndex = 13;
            this.labelHsp.Text = "h sp, мм";
                                                this.numHsp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numHsp.DecimalPlaces = 2;
            this.numHsp.Location = new System.Drawing.Point(576, 10);
            this.numHsp.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numHsp.Name = "numHsp";
            this.numHsp.Size = new System.Drawing.Size(116, 20);
            this.numHsp.TabIndex = 14;
            this.numHsp.Value = new decimal(new int[] {
            125,
            0,
            0,
            0});
                                                this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnPrint, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnDrawChart, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(367, 461);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(710, 48);
            this.tableLayoutPanel2.TabIndex = 10;
                                                this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(607, 9);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 30);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Отчет";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
                                                this.btnDrawChart.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDrawChart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDrawChart.BackgroundImage")));
            this.btnDrawChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDrawChart.Location = new System.Drawing.Point(252, 10);
            this.btnDrawChart.Name = "btnDrawChart";
            this.btnDrawChart.Size = new System.Drawing.Size(100, 28);
            this.btnDrawChart.TabIndex = 2;
            this.btnDrawChart.Text = "График";
            this.btnDrawChart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDrawChart.UseVisualStyleBackColor = true;
            this.btnDrawChart.Click += new System.EventHandler(this.btnDrawChart_Click);
                                                this.openFileDialog.FileName = "FaF";
                                                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 635);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "BSRFibLabGraph";
            this.Text = "График \"нагрузка-перемещение внешних граней надреза\"";
            this.Load += new System.EventHandler(this.BSGraph_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ChartFaF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.faFBindingSource)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.tableLayoutPanelCalcRes.ResumeLayout(false);
            this.tableLayoutPanelCalcRes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numF05)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numF25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFaF)).EndInit();
            this.tableLayoutPanelGrid.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHsp)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
        }
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
        private System.Windows.Forms.Button btnDSAdd;
        private System.Windows.Forms.Button btnDSSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGrid;
        private System.Windows.Forms.Button btnDSOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnDSSave2File;
        private System.Windows.Forms.Label labelBarSample;
        private System.Windows.Forms.TextBox txtBarSample;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelL;
        private System.Windows.Forms.NumericUpDown numL;
        private System.Windows.Forms.NumericUpDown numB;
        private System.Windows.Forms.Label labelB;
        private System.Windows.Forms.Button btnDSDel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label labelHsp;
        private System.Windows.Forms.NumericUpDown numHsp;
        private System.Windows.Forms.DataGridViewTextBoxColumn numDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aFDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fDataGridViewTextBoxColumn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnPrint;
    }
}