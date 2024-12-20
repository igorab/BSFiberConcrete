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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSRFibLabGraph));
            this.ChartFaF = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.gridFaF = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanelGrid = new System.Windows.Forms.TableLayoutPanel();
            this.btnDSSave2File = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnDSDel = new System.Windows.Forms.Button();
            this.btnDSAdd = new System.Windows.Forms.Button();
            this.btnDSDataFromFile = new System.Windows.Forms.Button();
            this.btnDSSave = new System.Windows.Forms.Button();
            this.tableLayoutPanelBarSample = new System.Windows.Forms.TableLayoutPanel();
            this.labelL = new System.Windows.Forms.Label();
            this.numL = new System.Windows.Forms.NumericUpDown();
            this.numB = new System.Windows.Forms.NumericUpDown();
            this.labelB = new System.Windows.Forms.Label();
            this.labelHsp = new System.Windows.Forms.Label();
            this.numHsp = new System.Windows.Forms.NumericUpDown();
            this.cmbBarSample = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDelCalc = new System.Windows.Forms.Button();
            this.txtBarSample = new System.Windows.Forms.TextBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDrawChartAndCalc = new System.Windows.Forms.Button();
            this.tableLayoutPanelCalcRes = new System.Windows.Forms.TableLayoutPanel();
            this.lblF05 = new System.Windows.Forms.Label();
            this.numF05 = new System.Windows.Forms.NumericUpDown();
            this.lblF25 = new System.Windows.Forms.Label();
            this.numF25 = new System.Windows.Forms.NumericUpDown();
            this.lblFeL = new System.Windows.Forms.Label();
            this.numFel = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddBarSample = new System.Windows.Forms.Button();
            this.labelBarSample = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.faFBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.aFDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ChartFaF)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFaF)).BeginInit();
            this.tableLayoutPanelGrid.SuspendLayout();
            this.tableLayoutPanelBarSample.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHsp)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanelCalcRes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numF05)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numF25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFel)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.ChartFaF.Location = new System.Drawing.Point(290, 51);
            this.ChartFaF.Name = "ChartFaF";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.EmptyPointStyle.AxisLabel = "Fi";
            series1.Legend = "aFL";
            series1.Name = "AFSerie";
            series1.XValueMember = "aF";
            series1.YValueMembers = "F";
            this.ChartFaF.Series.Add(series1);
            this.ChartFaF.Size = new System.Drawing.Size(648, 502);
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
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.39773F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.60227F));
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
            this.tableLayoutPanel.Size = new System.Drawing.Size(942, 654);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // gridFaF
            // 
            this.gridFaF.AutoGenerateColumns = false;
            this.gridFaF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFaF.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.aFDataGridViewTextBoxColumn,
            this.fDataGridViewTextBoxColumn});
            this.gridFaF.DataSource = this.faFBindingSource;
            this.gridFaF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFaF.Location = new System.Drawing.Point(4, 51);
            this.gridFaF.Name = "gridFaF";
            this.gridFaF.Size = new System.Drawing.Size(279, 502);
            this.gridFaF.TabIndex = 3;
            // 
            // tableLayoutPanelGrid
            // 
            this.tableLayoutPanelGrid.ColumnCount = 6;
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanelGrid.Controls.Add(this.btnDSSave2File, 3, 0);
            this.tableLayoutPanelGrid.Controls.Add(this.btnDSDel, 5, 0);
            this.tableLayoutPanelGrid.Controls.Add(this.btnDSAdd, 4, 0);
            this.tableLayoutPanelGrid.Controls.Add(this.btnDSDataFromFile, 2, 0);
            this.tableLayoutPanelGrid.Controls.Add(this.btnDSSave, 0, 0);
            this.tableLayoutPanelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelGrid.Location = new System.Drawing.Point(4, 560);
            this.tableLayoutPanelGrid.Name = "tableLayoutPanelGrid";
            this.tableLayoutPanelGrid.RowCount = 1;
            this.tableLayoutPanelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelGrid.Size = new System.Drawing.Size(279, 39);
            this.tableLayoutPanelGrid.TabIndex = 6;
            // 
            // btnDSSave2File
            // 
            this.btnDSSave2File.ImageIndex = 15;
            this.btnDSSave2File.ImageList = this.imageList;
            this.btnDSSave2File.Location = new System.Drawing.Point(138, 3);
            this.btnDSSave2File.Name = "btnDSSave2File";
            this.btnDSSave2File.Size = new System.Drawing.Size(38, 33);
            this.btnDSSave2File.TabIndex = 7;
            this.btnDSSave2File.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDSSave2File.UseVisualStyleBackColor = true;
            this.btnDSSave2File.Click += new System.EventHandler(this.btnDSSave2File_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "CM.png");
            this.imageList.Images.SetKeyName(1, "Command.png");
            this.imageList.Images.SetKeyName(2, "Editions.png");
            this.imageList.Images.SetKeyName(3, "FileOpen.png");
            this.imageList.Images.SetKeyName(4, "Files.png");
            this.imageList.Images.SetKeyName(5, "IconCrossBlacks.png");
            this.imageList.Images.SetKeyName(6, "IconPlusBlack.png");
            this.imageList.Images.SetKeyName(7, "Icons.png");
            this.imageList.Images.SetKeyName(8, "Info.png");
            this.imageList.Images.SetKeyName(9, "Layers.png");
            this.imageList.Images.SetKeyName(10, "Menu.png");
            this.imageList.Images.SetKeyName(11, "Properties.png");
            this.imageList.Images.SetKeyName(12, "Property.png");
            this.imageList.Images.SetKeyName(13, "reabr.png");
            this.imageList.Images.SetKeyName(14, "Remove.png");
            this.imageList.Images.SetKeyName(15, "save.png");
            this.imageList.Images.SetKeyName(16, "Settings.png");
            this.imageList.Images.SetKeyName(17, "Storages.png");
            this.imageList.Images.SetKeyName(18, "TIMClasses.png");
            this.imageList.Images.SetKeyName(19, "Transmute.png");
            this.imageList.Images.SetKeyName(20, "update.png");
            this.imageList.Images.SetKeyName(21, "ViewMap.png");
            this.imageList.Images.SetKeyName(22, "save-button.png");
            this.imageList.Images.SetKeyName(23, "save-icon.png");
            // 
            // btnDSDel
            // 
            this.btnDSDel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDSDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDSDel.Location = new System.Drawing.Point(239, 3);
            this.btnDSDel.Name = "btnDSDel";
            this.btnDSDel.Size = new System.Drawing.Size(37, 33);
            this.btnDSDel.TabIndex = 8;
            this.btnDSDel.Text = "-";
            this.btnDSDel.UseVisualStyleBackColor = true;
            this.btnDSDel.Click += new System.EventHandler(this.btnDSDel_Click);
            // 
            // btnDSAdd
            // 
            this.btnDSAdd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDSAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDSAdd.Location = new System.Drawing.Point(185, 3);
            this.btnDSAdd.Name = "btnDSAdd";
            this.btnDSAdd.Size = new System.Drawing.Size(37, 33);
            this.btnDSAdd.TabIndex = 4;
            this.btnDSAdd.Text = "+";
            this.btnDSAdd.UseVisualStyleBackColor = true;
            this.btnDSAdd.Click += new System.EventHandler(this.btnDSAdd_Click);
            // 
            // btnDSDataFromFile
            // 
            this.btnDSDataFromFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDSDataFromFile.ImageKey = "save-button.png";
            this.btnDSDataFromFile.ImageList = this.imageList;
            this.btnDSDataFromFile.Location = new System.Drawing.Point(94, 3);
            this.btnDSDataFromFile.Name = "btnDSDataFromFile";
            this.btnDSDataFromFile.Size = new System.Drawing.Size(38, 33);
            this.btnDSDataFromFile.TabIndex = 6;
            this.btnDSDataFromFile.UseVisualStyleBackColor = true;
            this.btnDSDataFromFile.Click += new System.EventHandler(this.btnDSOpen_Click);
            // 
            // btnDSSave
            // 
            this.btnDSSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDSSave.ImageIndex = 23;
            this.btnDSSave.ImageList = this.imageList;
            this.btnDSSave.Location = new System.Drawing.Point(5, 3);
            this.btnDSSave.Name = "btnDSSave";
            this.btnDSSave.Size = new System.Drawing.Size(37, 33);
            this.btnDSSave.TabIndex = 5;
            this.btnDSSave.UseVisualStyleBackColor = true;
            this.btnDSSave.Visible = false;
            this.btnDSSave.Click += new System.EventHandler(this.btnDSSave_Click);
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
            this.tableLayoutPanelBarSample.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tableLayoutPanelBarSample.Controls.Add(this.labelL, 1, 0);
            this.tableLayoutPanelBarSample.Controls.Add(this.numL, 2, 0);
            this.tableLayoutPanelBarSample.Controls.Add(this.numB, 4, 0);
            this.tableLayoutPanelBarSample.Controls.Add(this.labelB, 3, 0);
            this.tableLayoutPanelBarSample.Controls.Add(this.labelHsp, 5, 0);
            this.tableLayoutPanelBarSample.Controls.Add(this.numHsp, 6, 0);
            this.tableLayoutPanelBarSample.Controls.Add(this.cmbBarSample, 0, 0);
            this.tableLayoutPanelBarSample.Location = new System.Drawing.Point(290, 4);
            this.tableLayoutPanelBarSample.Name = "tableLayoutPanelBarSample";
            this.tableLayoutPanelBarSample.RowCount = 1;
            this.tableLayoutPanelBarSample.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelBarSample.Size = new System.Drawing.Size(648, 40);
            this.tableLayoutPanelBarSample.TabIndex = 9;
            // 
            // labelL
            // 
            this.labelL.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelL.AutoSize = true;
            this.labelL.Location = new System.Drawing.Point(159, 7);
            this.labelL.Name = "labelL";
            this.labelL.Size = new System.Drawing.Size(23, 26);
            this.labelL.TabIndex = 9;
            this.labelL.Text = "L, мм";
            // 
            // numL
            // 
            this.numL.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numL.DecimalPlaces = 2;
            this.numL.Location = new System.Drawing.Point(188, 10);
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
            this.numB.Location = new System.Drawing.Point(338, 10);
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
            this.labelB.Location = new System.Drawing.Point(297, 13);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(35, 13);
            this.labelB.TabIndex = 12;
            this.labelB.Text = "b, мм";
            // 
            // labelHsp
            // 
            this.labelHsp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelHsp.AutoSize = true;
            this.labelHsp.Location = new System.Drawing.Point(451, 13);
            this.labelHsp.Name = "labelHsp";
            this.labelHsp.Size = new System.Drawing.Size(49, 13);
            this.labelHsp.TabIndex = 13;
            this.labelHsp.Text = "h sp, мм";
            // 
            // numHsp
            // 
            this.numHsp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numHsp.DecimalPlaces = 2;
            this.numHsp.Location = new System.Drawing.Point(506, 10);
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
            // cmbBarSample
            // 
            this.cmbBarSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBarSample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBarSample.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbBarSample.FormattingEnabled = true;
            this.cmbBarSample.Location = new System.Drawing.Point(3, 9);
            this.cmbBarSample.Name = "cmbBarSample";
            this.cmbBarSample.Size = new System.Drawing.Size(142, 21);
            this.cmbBarSample.TabIndex = 15;
            this.cmbBarSample.SelectedIndexChanged += new System.EventHandler(this.cmbBarSample_SelectedIndexChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 239F));
            this.tableLayoutPanel2.Controls.Add(this.btnDelCalc, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtBarSample, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnPrint, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnDrawChartAndCalc, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(290, 560);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(648, 39);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // btnDelCalc
            // 
            this.btnDelCalc.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDelCalc.Image = ((System.Drawing.Image)(resources.GetObject("btnDelCalc.Image")));
            this.btnDelCalc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelCalc.Location = new System.Drawing.Point(207, 8);
            this.btnDelCalc.Name = "btnDelCalc";
            this.btnDelCalc.Size = new System.Drawing.Size(88, 23);
            this.btnDelCalc.TabIndex = 9;
            this.btnDelCalc.Text = "Удалить";
            this.btnDelCalc.UseVisualStyleBackColor = true;
            this.btnDelCalc.Click += new System.EventHandler(this.btnDelCalc_Click);
            // 
            // txtBarSample
            // 
            this.txtBarSample.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtBarSample.Enabled = false;
            this.txtBarSample.Location = new System.Drawing.Point(3, 9);
            this.txtBarSample.Name = "txtBarSample";
            this.txtBarSample.Size = new System.Drawing.Size(194, 20);
            this.txtBarSample.TabIndex = 8;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(412, 8);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(88, 23);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Отчет";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnDrawChartAndCalc
            // 
            this.btnDrawChartAndCalc.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDrawChartAndCalc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDrawChartAndCalc.BackgroundImage")));
            this.btnDrawChartAndCalc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDrawChartAndCalc.Location = new System.Drawing.Point(310, 8);
            this.btnDrawChartAndCalc.Name = "btnDrawChartAndCalc";
            this.btnDrawChartAndCalc.Size = new System.Drawing.Size(84, 23);
            this.btnDrawChartAndCalc.TabIndex = 2;
            this.btnDrawChartAndCalc.Text = "Расчет";
            this.btnDrawChartAndCalc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDrawChartAndCalc.UseVisualStyleBackColor = true;
            this.btnDrawChartAndCalc.Click += new System.EventHandler(this.btnDrawChart_Click);
            // 
            // tableLayoutPanelCalcRes
            // 
            this.tableLayoutPanelCalcRes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanelCalcRes.ColumnCount = 6;
            this.tableLayoutPanelCalcRes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.875F));
            this.tableLayoutPanelCalcRes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.125F));
            this.tableLayoutPanelCalcRes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanelCalcRes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanelCalcRes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanelCalcRes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 199F));
            this.tableLayoutPanelCalcRes.Controls.Add(this.lblF05, 0, 0);
            this.tableLayoutPanelCalcRes.Controls.Add(this.numF05, 1, 0);
            this.tableLayoutPanelCalcRes.Controls.Add(this.lblF25, 2, 0);
            this.tableLayoutPanelCalcRes.Controls.Add(this.numF25, 3, 0);
            this.tableLayoutPanelCalcRes.Controls.Add(this.lblFeL, 4, 0);
            this.tableLayoutPanelCalcRes.Controls.Add(this.numFel, 5, 0);
            this.tableLayoutPanelCalcRes.Location = new System.Drawing.Point(319, 606);
            this.tableLayoutPanelCalcRes.Name = "tableLayoutPanelCalcRes";
            this.tableLayoutPanelCalcRes.RowCount = 1;
            this.tableLayoutPanelCalcRes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.66667F));
            this.tableLayoutPanelCalcRes.Size = new System.Drawing.Size(590, 44);
            this.tableLayoutPanelCalcRes.TabIndex = 1;
            // 
            // lblF05
            // 
            this.lblF05.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblF05.AutoSize = true;
            this.lblF05.Location = new System.Drawing.Point(3, 15);
            this.lblF05.Name = "lblF05";
            this.lblF05.Size = new System.Drawing.Size(55, 13);
            this.lblF05.TabIndex = 0;
            this.lblF05.Text = "F 0,5, Н:";
            // 
            // numF05
            // 
            this.numF05.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numF05.DecimalPlaces = 4;
            this.numF05.Location = new System.Drawing.Point(64, 12);
            this.numF05.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numF05.Name = "numF05";
            this.numF05.Size = new System.Drawing.Size(99, 20);
            this.numF05.TabIndex = 3;
            // 
            // lblF25
            // 
            this.lblF25.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblF25.AutoSize = true;
            this.lblF25.Location = new System.Drawing.Point(172, 15);
            this.lblF25.Name = "lblF25";
            this.lblF25.Size = new System.Drawing.Size(48, 13);
            this.lblF25.TabIndex = 1;
            this.lblF25.Text = "F 2,5, Н:";
            // 
            // numF25
            // 
            this.numF25.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numF25.DecimalPlaces = 4;
            this.numF25.Location = new System.Drawing.Point(226, 12);
            this.numF25.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numF25.Name = "numF25";
            this.numF25.Size = new System.Drawing.Size(107, 20);
            this.numF25.TabIndex = 4;
            // 
            // lblFeL
            // 
            this.lblFeL.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFeL.AutoSize = true;
            this.lblFeL.Location = new System.Drawing.Point(346, 15);
            this.lblFeL.Name = "lblFeL";
            this.lblFeL.Size = new System.Drawing.Size(41, 13);
            this.lblFeL.TabIndex = 2;
            this.lblFeL.Text = "F el, Н:";
            // 
            // numFel
            // 
            this.numFel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numFel.DecimalPlaces = 4;
            this.numFel.Location = new System.Drawing.Point(393, 12);
            this.numFel.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numFel.Name = "numFel";
            this.numFel.Size = new System.Drawing.Size(113, 20);
            this.numFel.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.btnAddBarSample, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelBarSample, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(31, 6);
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
            this.btnAddBarSample.Location = new System.Drawing.Point(213, 3);
            this.btnAddBarSample.Name = "btnAddBarSample";
            this.btnAddBarSample.Size = new System.Drawing.Size(36, 29);
            this.btnAddBarSample.TabIndex = 8;
            this.btnAddBarSample.Text = "+";
            this.btnAddBarSample.UseVisualStyleBackColor = true;
            this.btnAddBarSample.Click += new System.EventHandler(this.btnAddBarSample_Click);
            // 
            // labelBarSample
            // 
            this.labelBarSample.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelBarSample.AutoSize = true;
            this.labelBarSample.Location = new System.Drawing.Point(104, 11);
            this.labelBarSample.Name = "labelBarSample";
            this.labelBarSample.Size = new System.Drawing.Size(102, 13);
            this.labelBarSample.TabIndex = 7;
            this.labelBarSample.Text = "Описание образца";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "FaF";
            // 
            // faFBindingSource
            // 
            this.faFBindingSource.DataSource = typeof(BSFiberConcrete.FaF);
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
            // BSRFibLabGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 654);
            this.Controls.Add(this.tableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BSRFibLabGraph";
            this.Text = "График \"нагрузка-перемещение внешних граней надреза\"";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BSRFibLabGraph_FormClosing);
            this.Load += new System.EventHandler(this.BSGraph_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ChartFaF)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridFaF)).EndInit();
            this.tableLayoutPanelGrid.ResumeLayout(false);
            this.tableLayoutPanelBarSample.ResumeLayout(false);
            this.tableLayoutPanelBarSample.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHsp)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanelCalcRes.ResumeLayout(false);
            this.tableLayoutPanelCalcRes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numF05)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numF25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFel)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.Button btnDrawChartAndCalc;
        private System.Windows.Forms.DataGridView gridFaF;
        private System.Windows.Forms.BindingSource faFBindingSource;
        private System.Windows.Forms.Button btnDSAdd;
        private System.Windows.Forms.Button btnDSSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGrid;
        private System.Windows.Forms.Button btnDSDataFromFile;
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ComboBox cmbBarSample;
        private System.Windows.Forms.Button btnDelCalc;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnAddBarSample;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.DataGridViewTextBoxColumn aFDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fDataGridViewTextBoxColumn;
    }
}