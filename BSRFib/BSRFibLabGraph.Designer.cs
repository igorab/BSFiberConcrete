namespace BSFiberConcrete
{
    partial class BSRFibLabGraph
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSRFibLabGraph));
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
            this.tableLayoutPanelGrid = new System.Windows.Forms.TableLayoutPanel();
            this.btnDSAdd = new System.Windows.Forms.Button();
            this.btnDSSave = new System.Windows.Forms.Button();
            this.btnDSOpen = new System.Windows.Forms.Button();
            this.btnDSSave2File = new System.Windows.Forms.Button();
            this.btnDSDel = new System.Windows.Forms.Button();
            this.labelBarSample = new System.Windows.Forms.Label();
            this.tableLayoutPanelBarSample = new System.Windows.Forms.TableLayoutPanel();
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
            this.cmbBarSample = new System.Windows.Forms.ComboBox();
            this.faFBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.numDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aFDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDelCalc = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddBarSample = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ChartFaF)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.tableLayoutPanelCalcRes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numF05)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numF25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFaF)).BeginInit();
            this.tableLayoutPanelGrid.SuspendLayout();
            this.tableLayoutPanelBarSample.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHsp)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.faFBindingSource)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChartFaF
            // 
            chartArea6.Name = "ChartArea1";
            this.ChartFaF.ChartAreas.Add(chartArea6);
            this.ChartFaF.DataSource = this.faFBindingSource;
            this.ChartFaF.Dock = System.Windows.Forms.DockStyle.Fill;
            legend6.Name = "aFL";
            legend6.Title = "Fi-aFi";
            this.ChartFaF.Legends.Add(legend6);
            this.ChartFaF.Location = new System.Drawing.Point(359, 51);
            this.ChartFaF.Name = "ChartFaF";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.EmptyPointStyle.AxisLabel = "Fi";
            series6.Legend = "aFL";
            series6.Name = "AFSerie";
            series6.XValueMember = "aF";
            series6.YValueMembers = "F";
            this.ChartFaF.Series.Add(series6);
            this.ChartFaF.Size = new System.Drawing.Size(694, 500);
            this.ChartFaF.TabIndex = 0;
            this.ChartFaF.Text = "aF";
            title6.Name = "aF";
            title6.Text = "Нагрузка-перемещение внешних граней надреза";
            this.ChartFaF.Titles.Add(title6);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.67253F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.32747F));
            this.tableLayoutPanel.Controls.Add(this.ChartFaF, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.gridFaF, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanelGrid, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanelBarSample, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel2, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanelCalcRes, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 4;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1057, 652);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // tableLayoutPanelCalcRes
            // 
            this.tableLayoutPanelCalcRes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanelCalcRes.ColumnCount = 6;
            this.tableLayoutPanelCalcRes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.18868F));
            this.tableLayoutPanelCalcRes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.81132F));
            this.tableLayoutPanelCalcRes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tableLayoutPanelCalcRes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tableLayoutPanelCalcRes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanelCalcRes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 131F));
            this.tableLayoutPanelCalcRes.Controls.Add(this.lblF05, 0, 0);
            this.tableLayoutPanelCalcRes.Controls.Add(this.numF05, 1, 0);
            this.tableLayoutPanelCalcRes.Controls.Add(this.lblF25, 2, 0);
            this.tableLayoutPanelCalcRes.Controls.Add(this.numF25, 3, 0);
            this.tableLayoutPanelCalcRes.Controls.Add(this.lblFeL, 4, 0);
            this.tableLayoutPanelCalcRes.Controls.Add(this.numFel, 5, 0);
            this.tableLayoutPanelCalcRes.Location = new System.Drawing.Point(421, 604);
            this.tableLayoutPanelCalcRes.Name = "tableLayoutPanelCalcRes";
            this.tableLayoutPanelCalcRes.RowCount = 1;
            this.tableLayoutPanelCalcRes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.66667F));
            this.tableLayoutPanelCalcRes.Size = new System.Drawing.Size(570, 44);
            this.tableLayoutPanelCalcRes.TabIndex = 1;
            // 
            // lblF05
            // 
            this.lblF05.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblF05.AutoSize = true;
            this.lblF05.Location = new System.Drawing.Point(9, 13);
            this.lblF05.Name = "lblF05";
            this.lblF05.Size = new System.Drawing.Size(48, 13);
            this.lblF05.TabIndex = 0;
            this.lblF05.Text = "F 0,5, Н:";
            // 
            // lblF25
            // 
            this.lblF25.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblF25.AutoSize = true;
            this.lblF25.Location = new System.Drawing.Point(215, 13);
            this.lblF25.Name = "lblF25";
            this.lblF25.Size = new System.Drawing.Size(48, 13);
            this.lblF25.TabIndex = 1;
            this.lblF25.Text = "F 2,5, Н:";
            // 
            // lblFeL
            // 
            this.lblFeL.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFeL.AutoSize = true;
            this.lblFeL.Location = new System.Drawing.Point(394, 13);
            this.lblFeL.Name = "lblFeL";
            this.lblFeL.Size = new System.Drawing.Size(41, 13);
            this.lblFeL.TabIndex = 2;
            this.lblFeL.Text = "F el, Н:";
            // 
            // numF05
            // 
            this.numF05.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numF05.DecimalPlaces = 4;
            this.numF05.Location = new System.Drawing.Point(63, 9);
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
            this.numF25.Location = new System.Drawing.Point(269, 9);
            this.numF25.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numF25.Name = "numF25";
            this.numF25.Size = new System.Drawing.Size(107, 20);
            this.numF25.TabIndex = 4;
            // 
            // numFel
            // 
            this.numFel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numFel.DecimalPlaces = 4;
            this.numFel.Location = new System.Drawing.Point(441, 9);
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
            this.gridFaF.Location = new System.Drawing.Point(4, 51);
            this.gridFaF.Name = "gridFaF";
            this.gridFaF.Size = new System.Drawing.Size(348, 500);
            this.gridFaF.TabIndex = 3;
            // 
            // tableLayoutPanelGrid
            // 
            this.tableLayoutPanelGrid.ColumnCount = 6;
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.27273F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.72727F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanelGrid.Controls.Add(this.btnDSAdd, 5, 0);
            this.tableLayoutPanelGrid.Controls.Add(this.btnDSSave, 2, 0);
            this.tableLayoutPanelGrid.Controls.Add(this.btnDSOpen, 1, 0);
            this.tableLayoutPanelGrid.Controls.Add(this.btnDSSave2File, 3, 0);
            this.tableLayoutPanelGrid.Controls.Add(this.btnDSDel, 4, 0);
            this.tableLayoutPanelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelGrid.Location = new System.Drawing.Point(4, 558);
            this.tableLayoutPanelGrid.Name = "tableLayoutPanelGrid";
            this.tableLayoutPanelGrid.RowCount = 1;
            this.tableLayoutPanelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelGrid.Size = new System.Drawing.Size(348, 39);
            this.tableLayoutPanelGrid.TabIndex = 6;
            // 
            // btnDSAdd
            // 
            this.btnDSAdd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDSAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDSAdd.Location = new System.Drawing.Point(308, 3);
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
            this.btnDSSave.Location = new System.Drawing.Point(104, 3);
            this.btnDSSave.Name = "btnDSSave";
            this.btnDSSave.Size = new System.Drawing.Size(37, 33);
            this.btnDSSave.TabIndex = 5;
            this.btnDSSave.UseVisualStyleBackColor = true;
            this.btnDSSave.Click += new System.EventHandler(this.btnDSSave_Click);
            // 
            // btnDSOpen
            // 
            this.btnDSOpen.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDSOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnDSOpen.Image")));
            this.btnDSOpen.Location = new System.Drawing.Point(49, 3);
            this.btnDSOpen.Name = "btnDSOpen";
            this.btnDSOpen.Size = new System.Drawing.Size(38, 33);
            this.btnDSOpen.TabIndex = 6;
            this.btnDSOpen.UseVisualStyleBackColor = true;
            this.btnDSOpen.Click += new System.EventHandler(this.btnDSOpen_Click);
            // 
            // btnDSSave2File
            // 
            this.btnDSSave2File.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDSSave2File.Image = ((System.Drawing.Image)(resources.GetObject("btnDSSave2File.Image")));
            this.btnDSSave2File.Location = new System.Drawing.Point(164, 3);
            this.btnDSSave2File.Name = "btnDSSave2File";
            this.btnDSSave2File.Size = new System.Drawing.Size(38, 33);
            this.btnDSSave2File.TabIndex = 7;
            this.btnDSSave2File.UseVisualStyleBackColor = true;
            this.btnDSSave2File.Click += new System.EventHandler(this.btnDSSave2File_Click);
            // 
            // btnDSDel
            // 
            this.btnDSDel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDSDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDSDel.Location = new System.Drawing.Point(258, 3);
            this.btnDSDel.Name = "btnDSDel";
            this.btnDSDel.Size = new System.Drawing.Size(37, 33);
            this.btnDSDel.TabIndex = 8;
            this.btnDSDel.Text = "-";
            this.btnDSDel.UseVisualStyleBackColor = true;
            this.btnDSDel.Click += new System.EventHandler(this.btnDSDel_Click);
            // 
            // labelBarSample
            // 
            this.labelBarSample.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelBarSample.AutoSize = true;
            this.labelBarSample.Location = new System.Drawing.Point(147, 11);
            this.labelBarSample.Name = "labelBarSample";
            this.labelBarSample.Size = new System.Drawing.Size(102, 13);
            this.labelBarSample.TabIndex = 7;
            this.labelBarSample.Text = "Описание образца";
            // 
            // tableLayoutPanelBarSample
            // 
            this.tableLayoutPanelBarSample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelBarSample.ColumnCount = 7;
            this.tableLayoutPanelBarSample.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanelBarSample.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelBarSample.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanelBarSample.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanelBarSample.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanelBarSample.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanelBarSample.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 137F));
            this.tableLayoutPanelBarSample.Controls.Add(this.labelL, 1, 0);
            this.tableLayoutPanelBarSample.Controls.Add(this.numL, 2, 0);
            this.tableLayoutPanelBarSample.Controls.Add(this.numB, 4, 0);
            this.tableLayoutPanelBarSample.Controls.Add(this.labelB, 3, 0);
            this.tableLayoutPanelBarSample.Controls.Add(this.labelHsp, 5, 0);
            this.tableLayoutPanelBarSample.Controls.Add(this.numHsp, 6, 0);
            this.tableLayoutPanelBarSample.Controls.Add(this.cmbBarSample, 0, 0);
            this.tableLayoutPanelBarSample.Location = new System.Drawing.Point(359, 4);
            this.tableLayoutPanelBarSample.Name = "tableLayoutPanelBarSample";
            this.tableLayoutPanelBarSample.RowCount = 1;
            this.tableLayoutPanelBarSample.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelBarSample.Size = new System.Drawing.Size(694, 40);
            this.tableLayoutPanelBarSample.TabIndex = 9;
            // 
            // txtBarSample
            // 
            this.txtBarSample.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtBarSample.Location = new System.Drawing.Point(3, 9);
            this.txtBarSample.Name = "txtBarSample";
            this.txtBarSample.Size = new System.Drawing.Size(194, 20);
            this.txtBarSample.TabIndex = 8;
            this.txtBarSample.Text = "Образец 1";
            // 
            // labelL
            // 
            this.labelL.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelL.AutoSize = true;
            this.labelL.Location = new System.Drawing.Point(200, 13);
            this.labelL.Name = "labelL";
            this.labelL.Size = new System.Drawing.Size(35, 13);
            this.labelL.TabIndex = 9;
            this.labelL.Text = "L, мм";
            // 
            // numL
            // 
            this.numL.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numL.DecimalPlaces = 2;
            this.numL.Location = new System.Drawing.Point(241, 10);
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
            // 
            // numB
            // 
            this.numB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numB.DecimalPlaces = 2;
            this.numB.Location = new System.Drawing.Point(391, 10);
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
            // 
            // labelB
            // 
            this.labelB.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelB.AutoSize = true;
            this.labelB.Location = new System.Drawing.Point(350, 13);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(35, 13);
            this.labelB.TabIndex = 12;
            this.labelB.Text = "b, мм";
            // 
            // labelHsp
            // 
            this.labelHsp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelHsp.AutoSize = true;
            this.labelHsp.Location = new System.Drawing.Point(504, 13);
            this.labelHsp.Name = "labelHsp";
            this.labelHsp.Size = new System.Drawing.Size(49, 13);
            this.labelHsp.TabIndex = 13;
            this.labelHsp.Text = "h sp, мм";
            // 
            // numHsp
            // 
            this.numHsp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numHsp.DecimalPlaces = 2;
            this.numHsp.Location = new System.Drawing.Point(559, 10);
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
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 152F));
            this.tableLayoutPanel2.Controls.Add(this.btnDelCalc, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtBarSample, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnPrint, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnDrawChart, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(359, 558);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(694, 39);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(603, 8);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(88, 23);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Отчет";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnDrawChart
            // 
            this.btnDrawChart.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDrawChart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDrawChart.BackgroundImage")));
            this.btnDrawChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDrawChart.Location = new System.Drawing.Point(451, 8);
            this.btnDrawChart.Name = "btnDrawChart";
            this.btnDrawChart.Size = new System.Drawing.Size(88, 23);
            this.btnDrawChart.TabIndex = 2;
            this.btnDrawChart.Text = "График";
            this.btnDrawChart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDrawChart.UseVisualStyleBackColor = true;
            this.btnDrawChart.Click += new System.EventHandler(this.btnDrawChart_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "FaF";
            // 
            // cmbBarSample
            // 
            this.cmbBarSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBarSample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBarSample.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbBarSample.FormattingEnabled = true;
            this.cmbBarSample.Location = new System.Drawing.Point(3, 9);
            this.cmbBarSample.Name = "cmbBarSample";
            this.cmbBarSample.Size = new System.Drawing.Size(185, 21);
            this.cmbBarSample.TabIndex = 15;
            this.cmbBarSample.SelectedIndexChanged += new System.EventHandler(this.cmbBarSample_SelectedIndexChanged);
            // 
            // faFBindingSource
            // 
            this.faFBindingSource.DataSource = typeof(BSFiberConcrete.FaF);
            // 
            // numDataGridViewTextBoxColumn
            // 
            this.numDataGridViewTextBoxColumn.DataPropertyName = "Num";
            this.numDataGridViewTextBoxColumn.HeaderText = "№";
            this.numDataGridViewTextBoxColumn.Name = "numDataGridViewTextBoxColumn";
            this.numDataGridViewTextBoxColumn.ToolTipText = "Номер измерения";
            this.numDataGridViewTextBoxColumn.Width = 50;
            // 
            // aFDataGridViewTextBoxColumn
            // 
            this.aFDataGridViewTextBoxColumn.DataPropertyName = "aF";
            this.aFDataGridViewTextBoxColumn.HeaderText = "aF, мм";
            this.aFDataGridViewTextBoxColumn.Name = "aFDataGridViewTextBoxColumn";
            // 
            // fDataGridViewTextBoxColumn
            // 
            this.fDataGridViewTextBoxColumn.DataPropertyName = "F";
            this.fDataGridViewTextBoxColumn.HeaderText = "F, Н";
            this.fDataGridViewTextBoxColumn.Name = "fDataGridViewTextBoxColumn";
            // 
            // btnDelCalc
            // 
            this.btnDelCalc.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDelCalc.Image = ((System.Drawing.Image)(resources.GetObject("btnDelCalc.Image")));
            this.btnDelCalc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelCalc.Location = new System.Drawing.Point(203, 8);
            this.btnDelCalc.Name = "btnDelCalc";
            this.btnDelCalc.Size = new System.Drawing.Size(88, 23);
            this.btnDelCalc.TabIndex = 9;
            this.btnDelCalc.Text = "Удалить";
            this.btnDelCalc.UseVisualStyleBackColor = true;
            this.btnDelCalc.Click += new System.EventHandler(this.btnDelCalc_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnAddBarSample, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelBarSample, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(100, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(252, 35);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // btnAddBarSample
            // 
            this.btnAddBarSample.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAddBarSample.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddBarSample.Location = new System.Drawing.Point(86, 3);
            this.btnAddBarSample.Name = "btnAddBarSample";
            this.btnAddBarSample.Size = new System.Drawing.Size(37, 29);
            this.btnAddBarSample.TabIndex = 8;
            this.btnAddBarSample.Text = "+";
            this.btnAddBarSample.UseVisualStyleBackColor = true;
            this.btnAddBarSample.Click += new System.EventHandler(this.btnAddBarSample_Click);
            // 
            // BSRFibLabGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 652);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "BSRFibLabGraph";
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
            this.tableLayoutPanelBarSample.ResumeLayout(false);
            this.tableLayoutPanelBarSample.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHsp)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.faFBindingSource)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.Button btnDSAdd;
        private System.Windows.Forms.Button btnDSSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGrid;
        private System.Windows.Forms.Button btnDSOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnDSSave2File;
        private System.Windows.Forms.Label labelBarSample;
        private System.Windows.Forms.TextBox txtBarSample;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBarSample;
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
        private System.Windows.Forms.ComboBox cmbBarSample;
        private System.Windows.Forms.Button btnDelCalc;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnAddBarSample;
    }
}