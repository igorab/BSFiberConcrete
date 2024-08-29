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
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnDraw = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.labelSection = new System.Windows.Forms.Label();
            this.labelRods = new System.Windows.Forms.Label();
            this.bSRodDataGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelArea = new System.Windows.Forms.Label();
            this.numArea = new System.Windows.Forms.NumericUpDown();
            this.labelAreaUnits = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelAreaRebarUnits = new System.Windows.Forms.Label();
            this.numAreaRebar = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutButtonRods = new System.Windows.Forms.TableLayoutPanel();
            this.btnDelRod = new System.Windows.Forms.Button();
            this.btnAddRod = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.Dnom = new System.Windows.Forms.DataGridViewComboBoxColumn();
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
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAreaRebar)).BeginInit();
            this.tableLayoutButtonRods.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pointBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RodBS)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 4;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.chart, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.btnDraw, 2, 2);
            this.tableLayoutPanel.Controls.Add(this.dataGrid, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.labelSection, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelRods, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.bSRodDataGridView, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel1, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutButtonRods, 1, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.865772F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.13423F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1104, 717);
            this.tableLayoutPanel.TabIndex = 0;
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(104, 99);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDel.Location = new System.Drawing.Point(55, 3);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(35, 35);
            this.btnDel.TabIndex = 3;
            this.btnDel.Text = "-";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Visible = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(35, 35);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // chart
            // 
            this.chart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chart.BorderlineWidth = 3;
            chartArea1.AxisX.Title = "X, см";
            chartArea1.AxisY.Title = "Y, см";
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            legend1.Title = "Легенда";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(333, 32);
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
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "сечение";
            this.chart.Series.Add(series1);
            this.chart.Series.Add(series2);
            this.chart.Series.Add(series3);
            this.chart.Size = new System.Drawing.Size(711, 577);
            this.chart.TabIndex = 0;
            title1.Name = "TitleX";
            title1.Text = "Геометрия сечения и распололжение арматуры";
            this.chart.Titles.Add(title1);
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(333, 615);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(132, 38);
            this.btnDraw.TabIndex = 1;
            this.btnDraw.Text = "Построить сечение";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.dataGrid.Size = new System.Drawing.Size(104, 577);
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
            this.labelRods.ForeColor = System.Drawing.Color.Blue;
            this.labelRods.Location = new System.Drawing.Point(113, 8);
            this.labelRods.Name = "labelRods";
            this.labelRods.Size = new System.Drawing.Size(76, 13);
            this.labelRods.TabIndex = 7;
            this.labelRods.Text = "Армирование";
            this.labelRods.Click += new System.EventHandler(this.labelRods_Click);
            this.labelRods.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelRods_MouseMove);
            // 
            // bSRodDataGridView
            // 
            this.bSRodDataGridView.AllowUserToAddRows = false;
            this.bSRodDataGridView.AllowUserToDeleteRows = false;
            this.bSRodDataGridView.AllowUserToResizeRows = false;
            this.bSRodDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bSRodDataGridView.AutoGenerateColumns = false;
            this.bSRodDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bSRodDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Dnom,
            this.dataGridViewTextBoxColumn4});
            this.bSRodDataGridView.DataSource = this.RodBS;
            this.bSRodDataGridView.Location = new System.Drawing.Point(113, 32);
            this.bSRodDataGridView.Name = "bSRodDataGridView";
            this.bSRodDataGridView.RowHeadersVisible = false;
            this.bSRodDataGridView.Size = new System.Drawing.Size(214, 577);
            this.bSRodDataGridView.TabIndex = 7;
            this.bSRodDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.bSRodDataGridView_CellValueChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.44944F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.55056F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayoutPanel1.Controls.Add(this.labelArea, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.numArea, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelAreaUnits, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelAreaRebarUnits, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.numAreaRebar, 4, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(333, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(544, 23);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // labelArea
            // 
            this.labelArea.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelArea.AutoSize = true;
            this.labelArea.Location = new System.Drawing.Point(15, 5);
            this.labelArea.Name = "labelArea";
            this.labelArea.Size = new System.Drawing.Size(54, 13);
            this.labelArea.TabIndex = 0;
            this.labelArea.Text = "Площадь";
            // 
            // numArea
            // 
            this.numArea.AccessibleName = "NumArea";
            this.numArea.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numArea.DecimalPlaces = 4;
            this.numArea.Location = new System.Drawing.Point(75, 3);
            this.numArea.Maximum = new decimal(new int[] {
            -1486618624,
            232830643,
            0,
            0});
            this.numArea.Name = "numArea";
            this.numArea.Size = new System.Drawing.Size(100, 20);
            this.numArea.TabIndex = 1;
            // 
            // labelAreaUnits
            // 
            this.labelAreaUnits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAreaUnits.AutoSize = true;
            this.labelAreaUnits.Location = new System.Drawing.Point(181, 5);
            this.labelAreaUnits.Name = "labelAreaUnits";
            this.labelAreaUnits.Size = new System.Drawing.Size(27, 13);
            this.labelAreaUnits.TabIndex = 2;
            this.labelAreaUnits.Text = "см2";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(229, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Площадь арматуры";
            // 
            // labelAreaRebarUnits
            // 
            this.labelAreaRebarUnits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAreaRebarUnits.AutoSize = true;
            this.labelAreaRebarUnits.Location = new System.Drawing.Point(460, 5);
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
            this.numAreaRebar.Location = new System.Drawing.Point(342, 3);
            this.numAreaRebar.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.numAreaRebar.Name = "numAreaRebar";
            this.numAreaRebar.Size = new System.Drawing.Size(109, 20);
            this.numAreaRebar.TabIndex = 5;
            // 
            // tableLayoutButtonRods
            // 
            this.tableLayoutButtonRods.ColumnCount = 3;
            this.tableLayoutButtonRods.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.04167F));
            this.tableLayoutButtonRods.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.95833F));
            this.tableLayoutButtonRods.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutButtonRods.Controls.Add(this.btnDelRod, 2, 0);
            this.tableLayoutButtonRods.Controls.Add(this.btnAddRod, 1, 0);
            this.tableLayoutButtonRods.Controls.Add(this.btnSave, 0, 0);
            this.tableLayoutButtonRods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutButtonRods.Location = new System.Drawing.Point(113, 615);
            this.tableLayoutButtonRods.Name = "tableLayoutButtonRods";
            this.tableLayoutButtonRods.RowCount = 2;
            this.tableLayoutButtonRods.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutButtonRods.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutButtonRods.Size = new System.Drawing.Size(214, 99);
            this.tableLayoutButtonRods.TabIndex = 9;
            // 
            // btnDelRod
            // 
            this.btnDelRod.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDelRod.Location = new System.Drawing.Point(133, 3);
            this.btnDelRod.Name = "btnDelRod";
            this.btnDelRod.Size = new System.Drawing.Size(35, 35);
            this.btnDelRod.TabIndex = 6;
            this.btnDelRod.Text = "-";
            this.btnDelRod.UseVisualStyleBackColor = true;
            this.btnDelRod.Click += new System.EventHandler(this.btnDelRod_Click);
            // 
            // btnAddRod
            // 
            this.btnAddRod.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddRod.Location = new System.Drawing.Point(69, 3);
            this.btnAddRod.Name = "btnAddRod";
            this.btnAddRod.Size = new System.Drawing.Size(35, 35);
            this.btnAddRod.TabIndex = 5;
            this.btnAddRod.Text = "+";
            this.btnAddRod.UseVisualStyleBackColor = true;
            this.btnAddRod.Click += new System.EventHandler(this.btnAddRod_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(35, 35);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "s";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Dnom
            // 
            this.Dnom.DataPropertyName = "Dnom";
            this.Dnom.HeaderText = "D, ном, мм";
            this.Dnom.Items.AddRange(new object[] {
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
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ToolTipText = "Номер стержня";
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 20;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "CG_X";
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = "0";
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn2.HeaderText = "X (h)";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ToolTipText = "X, по ширине сечения, см";
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "CG_Y";
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = "0";
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn3.HeaderText = "Y (b)";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ToolTipText = "Y, по высоте сечения, см";
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "D";
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn4.HeaderText = "D";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ToolTipText = "Диаметр, см";
            this.dataGridViewTextBoxColumn4.Width = 30;
            // 
            // RodBS
            // 
            this.RodBS.DataSource = typeof(BSFiberConcrete.BSRod);
            // 
            // BSSectionChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 717);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "BSSectionChart";
            this.Text = "Сечение";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BSSectionChart_FormClosed);
            this.Load += new System.EventHandler(this.BSSectionChart_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bSRodDataGridView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAreaRebar)).EndInit();
            this.tableLayoutButtonRods.ResumeLayout(false);
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnSave;
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutButtonRods;
        private System.Windows.Forms.Button btnDelRod;
        private System.Windows.Forms.Button btnAddRod;
        private System.Windows.Forms.DataGridViewTextBoxColumn numDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn yDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelAreaRebarUnits;
        private System.Windows.Forms.NumericUpDown numAreaRebar;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewComboBoxColumn Dnom;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}