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
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSSectionChart));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labelSection = new System.Windows.Forms.Label();
            this.labelRods = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCalc = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnMesh = new System.Windows.Forms.Button();
            this.btnSaveChart = new System.Windows.Forms.Button();
            this.btnDraw = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.bSRodDataGridView = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dnom = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.D = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RodBS = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.numDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pointBS = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelRod = new System.Windows.Forms.Button();
            this.btnAddRod = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelArea = new System.Windows.Forms.Label();
            this.numArea = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.labelAreaRebarUnits = new System.Windows.Forms.Label();
            this.numAreaRebar = new System.Windows.Forms.NumericUpDown();
            this.labelAreaUnits = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bSRodDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RodBS)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointBS)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAreaRebar)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 6;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.34884F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.27907F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.62791F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.651163F));
            this.tableLayoutPanel.Controls.Add(this.chart, 4, 1);
            this.tableLayoutPanel.Controls.Add(this.labelSection, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelRods, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel2, 5, 1);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel3, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel5, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.dataGrid, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel4, 3, 1);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel1, 4, 0);
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.942623F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.05738F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(998, 488);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // chart
            // 
            this.chart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chart.BorderlineWidth = 3;
            chartArea1.AxisX.Title = "X, см";
            chartArea1.AxisY.Title = "Y, см";
            chartArea1.Name = "ChartArea";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend";
            legend1.Title = "Контуры";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(444, 32);
            this.chart.Name = "chart";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend";
            series1.Name = "Сечение";
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.EmptyPointStyle.BorderWidth = 3;
            series2.Legend = "Legend";
            series2.MarkerSize = 10;
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series2.Name = "Армирование";
            series3.ChartArea = "ChartArea";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend";
            series3.Name = "сечение";
            series4.ChartArea = "ChartArea";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Green;
            series4.Legend = "Legend";
            series4.Name = "Поперечное арм";
            this.chart.Series.Add(series1);
            this.chart.Series.Add(series2);
            this.chart.Series.Add(series3);
            this.chart.Series.Add(series4);
            this.chart.Size = new System.Drawing.Size(507, 453);
            this.chart.TabIndex = 0;
            title1.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title1.Name = "TitleX";
            title1.Text = "Геометрия сечения и распололжение арматуры";
            this.chart.Titles.Add(title1);
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
            this.labelRods.ForeColor = System.Drawing.Color.Blue;
            this.labelRods.Location = new System.Drawing.Point(184, 8);
            this.labelRods.Name = "labelRods";
            this.labelRods.Size = new System.Drawing.Size(76, 13);
            this.labelRods.TabIndex = 7;
            this.labelRods.Text = "Армирование";
            this.labelRods.Click += new System.EventHandler(this.labelRods_Click);
            this.labelRods.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelRods_MouseMove);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnCalc, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.btnMesh, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnSaveChart, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnDraw, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(957, 32);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(36, 163);
            this.tableLayoutPanel2.TabIndex = 12;
            // 
            // btnCalc
            // 
            this.btnCalc.ImageKey = "CM.png";
            this.btnCalc.ImageList = this.imageList;
            this.btnCalc.Location = new System.Drawing.Point(3, 124);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(30, 36);
            this.btnCalc.TabIndex = 4;
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
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
            this.imageList.Images.SetKeyName(5, "Icons.png");
            this.imageList.Images.SetKeyName(6, "Info.png");
            this.imageList.Images.SetKeyName(7, "Layers.png");
            this.imageList.Images.SetKeyName(8, "Menu.png");
            this.imageList.Images.SetKeyName(9, "Properties.png");
            this.imageList.Images.SetKeyName(10, "Property.png");
            this.imageList.Images.SetKeyName(11, "Remove.png");
            this.imageList.Images.SetKeyName(12, "Settings.png");
            this.imageList.Images.SetKeyName(13, "Storages.png");
            this.imageList.Images.SetKeyName(14, "TIMClasses.png");
            this.imageList.Images.SetKeyName(15, "Transmute.png");
            this.imageList.Images.SetKeyName(16, "ViewMap.png");
            this.imageList.Images.SetKeyName(17, "balka_rect.ico");
            this.imageList.Images.SetKeyName(18, "Mesh.png");
            this.imageList.Images.SetKeyName(19, "MeshRed.png");
            this.imageList.Images.SetKeyName(20, "MeshBlack.png");
            this.imageList.Images.SetKeyName(21, "update.png");
            this.imageList.Images.SetKeyName(22, "save.png");
            this.imageList.Images.SetKeyName(23, "reabr.png");
            // 
            // btnMesh
            // 
            this.btnMesh.ImageKey = "Mesh.png";
            this.btnMesh.ImageList = this.imageList;
            this.btnMesh.Location = new System.Drawing.Point(3, 87);
            this.btnMesh.Name = "btnMesh";
            this.btnMesh.Size = new System.Drawing.Size(30, 31);
            this.btnMesh.TabIndex = 5;
            this.btnMesh.UseVisualStyleBackColor = true;
            this.btnMesh.Click += new System.EventHandler(this.btnMesh_Click);
            // 
            // btnSaveChart
            // 
            this.btnSaveChart.ImageIndex = 22;
            this.btnSaveChart.ImageList = this.imageList;
            this.btnSaveChart.Location = new System.Drawing.Point(3, 45);
            this.btnSaveChart.Name = "btnSaveChart";
            this.btnSaveChart.Size = new System.Drawing.Size(30, 34);
            this.btnSaveChart.TabIndex = 2;
            this.btnSaveChart.UseVisualStyleBackColor = true;
            this.btnSaveChart.Click += new System.EventHandler(this.btnSaveChart_Click);
            // 
            // btnDraw
            // 
            this.btnDraw.ImageKey = "update.png";
            this.btnDraw.ImageList = this.imageList;
            this.btnDraw.Location = new System.Drawing.Point(3, 3);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(30, 36);
            this.btnDraw.TabIndex = 1;
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.bSRodDataGridView, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(184, 32);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.64463F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(218, 359);
            this.tableLayoutPanel3.TabIndex = 13;
            // 
            // bSRodDataGridView
            // 
            this.bSRodDataGridView.AllowUserToAddRows = false;
            this.bSRodDataGridView.AllowUserToDeleteRows = false;
            this.bSRodDataGridView.AllowUserToResizeRows = false;
            this.bSRodDataGridView.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.bSRodDataGridView.AutoGenerateColumns = false;
            this.bSRodDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bSRodDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.X,
            this.Y,
            this.Dnom,
            this.D});
            this.bSRodDataGridView.DataSource = this.RodBS;
            this.bSRodDataGridView.Location = new System.Drawing.Point(3, 3);
            this.bSRodDataGridView.Name = "bSRodDataGridView";
            this.bSRodDataGridView.RowHeadersVisible = false;
            this.bSRodDataGridView.Size = new System.Drawing.Size(212, 353);
            this.bSRodDataGridView.TabIndex = 7;
            this.bSRodDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.bSRodDataGridView_CellValueChanged);
            this.bSRodDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.bSRodDataGridView_CellValueChanged);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ToolTipText = "Номер стержня";
            this.Id.Visible = false;
            this.Id.Width = 20;
            // 
            // X
            // 
            this.X.DataPropertyName = "CG_X";
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = "0";
            this.X.DefaultCellStyle = dataGridViewCellStyle1;
            this.X.HeaderText = "X (h)";
            this.X.Name = "X";
            this.X.ToolTipText = "X, по ширине сечения, см";
            this.X.Width = 50;
            // 
            // Y
            // 
            this.Y.DataPropertyName = "CG_Y";
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = "0";
            this.Y.DefaultCellStyle = dataGridViewCellStyle2;
            this.Y.HeaderText = "Y (b)";
            this.Y.Name = "Y";
            this.Y.ToolTipText = "Y, по высоте сечения, см";
            this.Y.Width = 50;
            // 
            // Dnom
            // 
            this.Dnom.DataPropertyName = "Dnom";
            this.Dnom.HeaderText = "D, ном, мм";
            this.Dnom.Items.AddRange(new object[] {
            "0",
            "6",
            "8",
            "10",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "25",
            "28",
            "32",
            "36",
            "40"});
            this.Dnom.Name = "Dnom";
            this.Dnom.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Dnom.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Dnom.ToolTipText = "Номинальный диаметр";
            this.Dnom.Width = 50;
            // 
            // D
            // 
            this.D.DataPropertyName = "D";
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.D.DefaultCellStyle = dataGridViewCellStyle3;
            this.D.HeaderText = "D";
            this.D.Name = "D";
            this.D.ToolTipText = "Диаметр, см";
            this.D.Width = 30;
            // 
            // RodBS
            // 
            this.RodBS.DataSource = typeof(BSFiberConcrete.BSRod);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.btnAdd, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.btnDel, 0, 2);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(144, 32);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.05882F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.94118F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(34, 118);
            this.tableLayoutPanel5.TabIndex = 15;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAdd.Location = new System.Drawing.Point(3, 38);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(28, 30);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDel.Location = new System.Drawing.Point(3, 77);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(28, 30);
            this.btnDel.TabIndex = 3;
            this.btnDel.Text = "-";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Visible = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dataGrid.AutoGenerateColumns = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numDataGridViewTextBoxColumn,
            this.xDataGridViewTextBoxColumn,
            this.yDataGridViewTextBoxColumn});
            this.dataGrid.DataSource = this.pointBS;
            this.dataGrid.Location = new System.Drawing.Point(3, 32);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.Size = new System.Drawing.Size(135, 359);
            this.dataGrid.TabIndex = 2;
            // 
            // numDataGridViewTextBoxColumn
            // 
            this.numDataGridViewTextBoxColumn.DataPropertyName = "Num";
            this.numDataGridViewTextBoxColumn.HeaderText = "N";
            this.numDataGridViewTextBoxColumn.Name = "numDataGridViewTextBoxColumn";
            this.numDataGridViewTextBoxColumn.ToolTipText = "Номер узла";
            this.numDataGridViewTextBoxColumn.Width = 20;
            // 
            // xDataGridViewTextBoxColumn
            // 
            this.xDataGridViewTextBoxColumn.DataPropertyName = "X";
            this.xDataGridViewTextBoxColumn.HeaderText = "X";
            this.xDataGridViewTextBoxColumn.Name = "xDataGridViewTextBoxColumn";
            this.xDataGridViewTextBoxColumn.ToolTipText = "X, см";
            this.xDataGridViewTextBoxColumn.Width = 30;
            // 
            // yDataGridViewTextBoxColumn
            // 
            this.yDataGridViewTextBoxColumn.DataPropertyName = "Y";
            this.yDataGridViewTextBoxColumn.HeaderText = "Y";
            this.yDataGridViewTextBoxColumn.Name = "yDataGridViewTextBoxColumn";
            this.yDataGridViewTextBoxColumn.ToolTipText = "Y, см";
            this.yDataGridViewTextBoxColumn.Width = 30;
            // 
            // pointBS
            // 
            this.pointBS.DataSource = typeof(BSFiberConcrete.Section.BSPoint);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btnSave, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.btnDelRod, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.btnAddRod, 0, 2);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(408, 32);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 5;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.76119F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.23881F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(30, 160);
            this.tableLayoutPanel4.TabIndex = 16;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.ImageIndex = 22;
            this.btnSave.ImageList = this.imageList;
            this.btnSave.Location = new System.Drawing.Point(3, 35);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(24, 29);
            this.btnSave.TabIndex = 4;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelRod
            // 
            this.btnDelRod.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDelRod.Location = new System.Drawing.Point(3, 107);
            this.btnDelRod.Name = "btnDelRod";
            this.btnDelRod.Size = new System.Drawing.Size(24, 30);
            this.btnDelRod.TabIndex = 6;
            this.btnDelRod.Text = "-";
            this.btnDelRod.UseVisualStyleBackColor = true;
            this.btnDelRod.Click += new System.EventHandler(this.btnDelRod_Click);
            // 
            // btnAddRod
            // 
            this.btnAddRod.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddRod.Location = new System.Drawing.Point(3, 70);
            this.btnAddRod.Name = "btnAddRod";
            this.btnAddRod.Size = new System.Drawing.Size(24, 31);
            this.btnAddRod.TabIndex = 5;
            this.btnAddRod.Text = "+";
            this.btnAddRod.UseVisualStyleBackColor = true;
            this.btnAddRod.Click += new System.EventHandler(this.btnAddRod_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.05882F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.94118F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanel1.Controls.Add(this.labelArea, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.numArea, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelAreaRebarUnits, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.numAreaRebar, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelAreaUnits, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(444, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(483, 23);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // labelArea
            // 
            this.labelArea.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelArea.AutoSize = true;
            this.labelArea.Location = new System.Drawing.Point(5, 5);
            this.labelArea.Name = "labelArea";
            this.labelArea.Size = new System.Drawing.Size(54, 13);
            this.labelArea.TabIndex = 0;
            this.labelArea.Text = "Площадь";
            // 
            // numArea
            // 
            this.numArea.AccessibleName = "NumArea";
            this.numArea.DecimalPlaces = 4;
            this.numArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numArea.Location = new System.Drawing.Point(65, 3);
            this.numArea.Maximum = new decimal(new int[] {
            -1486618624,
            232830643,
            0,
            0});
            this.numArea.Name = "numArea";
            this.numArea.Size = new System.Drawing.Size(64, 20);
            this.numArea.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Площадь арматуры";
            // 
            // labelAreaRebarUnits
            // 
            this.labelAreaRebarUnits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAreaRebarUnits.AutoSize = true;
            this.labelAreaRebarUnits.Location = new System.Drawing.Point(386, 5);
            this.labelAreaRebarUnits.Name = "labelAreaRebarUnits";
            this.labelAreaRebarUnits.Size = new System.Drawing.Size(27, 13);
            this.labelAreaRebarUnits.TabIndex = 4;
            this.labelAreaRebarUnits.Text = "см2";
            // 
            // numAreaRebar
            // 
            this.numAreaRebar.AccessibleName = "NumArea";
            this.numAreaRebar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numAreaRebar.DecimalPlaces = 4;
            this.numAreaRebar.Location = new System.Drawing.Point(293, 3);
            this.numAreaRebar.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numAreaRebar.Name = "numAreaRebar";
            this.numAreaRebar.Size = new System.Drawing.Size(87, 20);
            this.numAreaRebar.TabIndex = 5;
            // 
            // labelAreaUnits
            // 
            this.labelAreaUnits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAreaUnits.AutoSize = true;
            this.labelAreaUnits.Location = new System.Drawing.Point(135, 5);
            this.labelAreaUnits.Name = "labelAreaUnits";
            this.labelAreaUnits.Size = new System.Drawing.Size(27, 13);
            this.labelAreaUnits.TabIndex = 2;
            this.labelAreaUnits.Text = "см2";
            // 
            // BSSectionChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "BSSectionChart";
            this.Size = new System.Drawing.Size(1005, 493);
            this.Load += new System.EventHandler(this.BSSectionChart_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bSRodDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RodBS)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointBS)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAreaRebar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.BindingSource pointBS;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label labelSection;
        private System.Windows.Forms.Label labelRods;
        private System.Windows.Forms.DataGridView bSRodDataGridView;
        private System.Windows.Forms.BindingSource RodBS;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelArea;
        private System.Windows.Forms.NumericUpDown numArea;
        private System.Windows.Forms.Label labelAreaUnits;
        private System.Windows.Forms.DataGridViewTextBoxColumn numDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn yDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelAreaRebarUnits;
        private System.Windows.Forms.NumericUpDown numAreaRebar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewComboBoxColumn Dnom;
        private System.Windows.Forms.DataGridViewTextBoxColumn D;
        private System.Windows.Forms.Button btnSaveChart;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Button btnMesh;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button btnDelRod;
        private System.Windows.Forms.Button btnAddRod;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    }
}