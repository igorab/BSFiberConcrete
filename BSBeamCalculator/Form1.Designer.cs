using System.Drawing;
using System.Windows.Forms;

namespace BSBeamCalculator
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea11 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend11 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea12 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend12 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton12 = new System.Windows.Forms.RadioButton();
            this.radioButton11 = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button2 = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.34231F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.65769F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(-1, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.41045F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.58955F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(891, 538);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.06329F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.93671F));
            this.tableLayoutPanel2.Controls.Add(this.panel3, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.65701F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.34299F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(395, 425);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Location = new System.Drawing.Point(260, 226);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(132, 36);
            this.panel3.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.Location = new System.Drawing.Point(75, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(37, 22);
            this.button4.TabIndex = 1;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.Location = new System.Drawing.Point(8, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(37, 22);
            this.button3.TabIndex = 0;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(251, 212);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Закрепление в плоскости изгиба";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.radioButton5);
            this.panel1.Controls.Add(this.radioButton4);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Location = new System.Drawing.Point(11, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(218, 150);
            this.panel1.TabIndex = 3;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButton6.BackgroundImage")));
            this.radioButton6.Location = new System.Drawing.Point(112, 55);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(100, 17);
            this.radioButton6.TabIndex = 7;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "                         ";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.CheckedChanged += new System.EventHandler(this.radioButton6_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Длинна балки";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 1;
            this.numericUpDown1.Location = new System.Drawing.Point(124, 102);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(73, 20);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButton5.BackgroundImage")));
            this.radioButton5.Location = new System.Drawing.Point(112, 32);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(100, 17);
            this.radioButton5.TabIndex = 6;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "                         ";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButton4.BackgroundImage")));
            this.radioButton4.Location = new System.Drawing.Point(2, 55);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(100, 17);
            this.radioButton4.TabIndex = 5;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "                         ";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButton3.BackgroundImage")));
            this.radioButton3.Location = new System.Drawing.Point(112, 9);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(100, 17);
            this.radioButton3.TabIndex = 4;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "                         ";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButton2.BackgroundImage")));
            this.radioButton2.Location = new System.Drawing.Point(4, 32);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(100, 17);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "                         ";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButton1.BackgroundImage")));
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(4, 9);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(100, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "                         ";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(260, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(132, 212);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioButton10);
            this.panel2.Controls.Add(this.radioButton9);
            this.panel2.Location = new System.Drawing.Point(6, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(102, 105);
            this.panel2.TabIndex = 1;
            // 
            // radioButton10
            // 
            this.radioButton10.AutoSize = true;
            this.radioButton10.Location = new System.Drawing.Point(4, 32);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(65, 17);
            this.radioButton10.TabIndex = 1;
            this.radioButton10.TabStop = true;
            this.radioButton10.Text = "двутавр";
            this.radioButton10.UseVisualStyleBackColor = true;
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.Checked = true;
            this.radioButton9.Location = new System.Drawing.Point(4, 9);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(48, 17);
            this.radioButton9.TabIndex = 0;
            this.radioButton9.TabStop = true;
            this.radioButton9.Text = "тавр";
            this.radioButton9.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "тип сечения балки";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.72775F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.27225F));
            this.tableLayoutPanel3.Controls.Add(this.groupBox4, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 226);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.70968F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.29032F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(241, 186);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton12);
            this.groupBox4.Controls.Add(this.panel4);
            this.groupBox4.Controls.Add(this.radioButton11);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(205, 149);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Характеристика нагрузки";
            // 
            // radioButton12
            // 
            this.radioButton12.AutoSize = true;
            this.radioButton12.Location = new System.Drawing.Point(19, 44);
            this.radioButton12.Name = "radioButton12";
            this.radioButton12.Size = new System.Drawing.Size(159, 17);
            this.radioButton12.TabIndex = 1;
            this.radioButton12.TabStop = true;
            this.radioButton12.Text = "Распределенная нагрузка";
            this.radioButton12.UseVisualStyleBackColor = true;
            this.radioButton12.CheckedChanged += new System.EventHandler(this.radioButton12_CheckedChanged);
            // 
            // radioButton11
            // 
            this.radioButton11.AutoSize = true;
            this.radioButton11.Checked = true;
            this.radioButton11.Location = new System.Drawing.Point(19, 20);
            this.radioButton11.Name = "radioButton11";
            this.radioButton11.Size = new System.Drawing.Size(144, 17);
            this.radioButton11.TabIndex = 0;
            this.radioButton11.TabStop = true;
            this.radioButton11.Text = " Сосредоточенная сила";
            this.radioButton11.UseVisualStyleBackColor = true;
            this.radioButton11.CheckedChanged += new System.EventHandler(this.radioButton11_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.numericUpDown4);
            this.panel4.Controls.Add(this.numericUpDown3);
            this.panel4.Controls.Add(this.numericUpDown2);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(14, 68);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(185, 75);
            this.panel4.TabIndex = 1;
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Enabled = false;
            this.numericUpDown4.Location = new System.Drawing.Point(120, 46);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(65, 20);
            this.numericUpDown4.TabIndex = 5;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(120, 24);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(65, 20);
            this.numericUpDown3.TabIndex = 4;
            this.numericUpDown3.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(120, 2);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(65, 20);
            this.numericUpDown2.TabIndex = 3;
            this.numericUpDown2.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Конечная позиция";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Начальная позиция";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Величина";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chart2);
            this.groupBox1.Controls.Add(this.chart1);
            this.groupBox1.Location = new System.Drawing.Point(407, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(481, 425);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // chart2
            // 
            chartArea11.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea11);
            legend11.Name = "Legend1";
            this.chart2.Legends.Add(legend11);
            this.chart2.Location = new System.Drawing.Point(6, 204);
            this.chart2.Name = "chart2";
            this.chart2.Size = new System.Drawing.Size(475, 221);
            this.chart2.TabIndex = 1;
            this.chart2.Text = "chart2";
            // 
            // chart1
            // 
            chartArea12.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea12);
            legend12.Name = "Legend1";
            this.chart1.Legends.Add(legend12);
            this.chart1.Location = new System.Drawing.Point(6, 8);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(481, 195);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(407, 435);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Сбросить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.button1);
            this.panel5.Location = new System.Drawing.Point(3, 435);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(398, 100);
            this.panel5.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(320, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Расчитать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 543);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton10;
        private Panel panel3;
        private Button button4;
        private Button button3;
        private TableLayoutPanel tableLayoutPanel3;
        private GroupBox groupBox4;
        private RadioButton radioButton12;
        private RadioButton radioButton11;
        private Panel panel4;
        private Label label6;
        private Label label5;
        private Label label4;
        private NumericUpDown numericUpDown4;
        private NumericUpDown numericUpDown3;
        private NumericUpDown numericUpDown2;
        private RadioButton radioButton4;
        private Panel panel5;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private RadioButton radioButton6;
        private RadioButton radioButton5;
    }
}

