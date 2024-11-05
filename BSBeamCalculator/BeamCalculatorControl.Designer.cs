using System.Windows.Forms;

namespace BSBeamCalculator
{
    partial class BeamCalculatorControl
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BeamCalculatorControl));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numBeamLen = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.rbMovable_Fixed = new System.Windows.Forms.RadioButton();
            this.rbFixed_Fixed = new System.Windows.Forms.RadioButton();
            this.rbFixed_No = new System.Windows.Forms.RadioButton();
            this.rbNo_Fixed = new System.Windows.Forms.RadioButton();
            this.rbFixed_Movable = new System.Windows.Forms.RadioButton();
            this.rbPinned_Movable = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.numLenX = new System.Windows.Forms.NumericUpDown();
            this.radioButton12 = new System.Windows.Forms.RadioButton();
            this.radioButton11 = new System.Windows.Forms.RadioButton();
            this.numForce = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label_Mmax = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label_Mmin = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label_Qmax = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBeamLen)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLenX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numForce)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel8);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(417, 217);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Закрепление в плоскости изгиба";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.03704F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.96296F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(411, 198);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numBeamLen);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 155);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 40);
            this.panel1.TabIndex = 3;
            // 
            // numBeamLen
            // 
            this.numBeamLen.DecimalPlaces = 1;
            this.numBeamLen.Location = new System.Drawing.Point(218, 3);
            this.numBeamLen.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numBeamLen.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numBeamLen.Name = "numBeamLen";
            this.numBeamLen.Size = new System.Drawing.Size(73, 20);
            this.numBeamLen.TabIndex = 1;
            this.numBeamLen.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Длинна балки";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(297, 5);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(21, 13);
            this.label16.TabIndex = 8;
            this.label16.Text = "см";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.rbMovable_Fixed, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.rbFixed_Fixed, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.rbFixed_No, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.rbNo_Fixed, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.rbFixed_Movable, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.rbPinned_Movable, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(405, 146);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // rbMovable_Fixed
            // 
            this.rbMovable_Fixed.AutoSize = true;
            this.rbMovable_Fixed.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbMovable_Fixed.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rbMovable_Fixed.Image = ((System.Drawing.Image)(resources.GetObject("rbMovable_Fixed.Image")));
            this.rbMovable_Fixed.Location = new System.Drawing.Point(205, 99);
            this.rbMovable_Fixed.Name = "rbMovable_Fixed";
            this.rbMovable_Fixed.Size = new System.Drawing.Size(183, 29);
            this.rbMovable_Fixed.TabIndex = 7;
            this.rbMovable_Fixed.UseVisualStyleBackColor = true;
            this.rbMovable_Fixed.CheckedChanged += new System.EventHandler(this.radioButton6_CheckedChanged);
            // 
            // rbFixed_Fixed
            // 
            this.rbFixed_Fixed.AutoSize = true;
            this.rbFixed_Fixed.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbFixed_Fixed.Image = ((System.Drawing.Image)(resources.GetObject("rbFixed_Fixed.Image")));
            this.rbFixed_Fixed.Location = new System.Drawing.Point(3, 3);
            this.rbFixed_Fixed.Name = "rbFixed_Fixed";
            this.rbFixed_Fixed.Size = new System.Drawing.Size(183, 30);
            this.rbFixed_Fixed.TabIndex = 2;
            this.rbFixed_Fixed.UseVisualStyleBackColor = true;
            this.rbFixed_Fixed.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rbFixed_No
            // 
            this.rbFixed_No.AutoSize = true;
            this.rbFixed_No.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbFixed_No.Image = ((System.Drawing.Image)(resources.GetObject("rbFixed_No.Image")));
            this.rbFixed_No.Location = new System.Drawing.Point(3, 51);
            this.rbFixed_No.Name = "rbFixed_No";
            this.rbFixed_No.Size = new System.Drawing.Size(183, 30);
            this.rbFixed_No.TabIndex = 3;
            this.rbFixed_No.UseVisualStyleBackColor = true;
            this.rbFixed_No.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // rbNo_Fixed
            // 
            this.rbNo_Fixed.AutoSize = true;
            this.rbNo_Fixed.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbNo_Fixed.Image = ((System.Drawing.Image)(resources.GetObject("rbNo_Fixed.Image")));
            this.rbNo_Fixed.Location = new System.Drawing.Point(205, 51);
            this.rbNo_Fixed.Name = "rbNo_Fixed";
            this.rbNo_Fixed.Size = new System.Drawing.Size(183, 30);
            this.rbNo_Fixed.TabIndex = 6;
            this.rbNo_Fixed.UseVisualStyleBackColor = true;
            this.rbNo_Fixed.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // rbFixed_Movable
            // 
            this.rbFixed_Movable.AutoSize = true;
            this.rbFixed_Movable.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbFixed_Movable.Image = ((System.Drawing.Image)(resources.GetObject("rbFixed_Movable.Image")));
            this.rbFixed_Movable.Location = new System.Drawing.Point(3, 99);
            this.rbFixed_Movable.Name = "rbFixed_Movable";
            this.rbFixed_Movable.Size = new System.Drawing.Size(183, 30);
            this.rbFixed_Movable.TabIndex = 5;
            this.rbFixed_Movable.UseVisualStyleBackColor = true;
            this.rbFixed_Movable.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // rbPinned_Movable
            // 
            this.rbPinned_Movable.AutoSize = true;
            this.rbPinned_Movable.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbPinned_Movable.Image = ((System.Drawing.Image)(resources.GetObject("rbPinned_Movable.Image")));
            this.rbPinned_Movable.Location = new System.Drawing.Point(205, 3);
            this.rbPinned_Movable.Name = "rbPinned_Movable";
            this.rbPinned_Movable.Size = new System.Drawing.Size(183, 30);
            this.rbPinned_Movable.TabIndex = 4;
            this.rbPinned_Movable.Text = "                         ";
            this.rbPinned_Movable.UseVisualStyleBackColor = true;
            this.rbPinned_Movable.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.numLenX);
            this.groupBox4.Controls.Add(this.radioButton12);
            this.groupBox4.Controls.Add(this.radioButton11);
            this.groupBox4.Controls.Add(this.numForce);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(235, 212);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Характеристика нагрузки";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(157, 103);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(21, 13);
            this.label15.TabIndex = 7;
            this.label15.Text = "см";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(157, 79);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(18, 13);
            this.label14.TabIndex = 6;
            this.label14.Text = "кг";
            // 
            // numLenX
            // 
            this.numLenX.DecimalPlaces = 1;
            this.numLenX.Location = new System.Drawing.Point(86, 101);
            this.numLenX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numLenX.Name = "numLenX";
            this.numLenX.Size = new System.Drawing.Size(65, 20);
            this.numLenX.TabIndex = 4;
            this.numLenX.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // radioButton12
            // 
            this.radioButton12.AutoSize = true;
            this.radioButton12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButton12.Location = new System.Drawing.Point(19, 44);
            this.radioButton12.Name = "radioButton12";
            this.radioButton12.Size = new System.Drawing.Size(158, 17);
            this.radioButton12.TabIndex = 1;
            this.radioButton12.Text = "Распределенная нагрузка";
            this.radioButton12.UseVisualStyleBackColor = true;
            this.radioButton12.CheckedChanged += new System.EventHandler(this.radioButton12_CheckedChanged);
            // 
            // radioButton11
            // 
            this.radioButton11.AutoSize = true;
            this.radioButton11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButton11.Location = new System.Drawing.Point(19, 20);
            this.radioButton11.Name = "radioButton11";
            this.radioButton11.Size = new System.Drawing.Size(143, 17);
            this.radioButton11.TabIndex = 0;
            this.radioButton11.Text = " Сосредоточенная сила";
            this.radioButton11.UseVisualStyleBackColor = true;
            this.radioButton11.CheckedChanged += new System.EventHandler(this.radioButton11_CheckedChanged);
            // 
            // numForce
            // 
            this.numForce.DecimalPlaces = 1;
            this.numForce.Location = new System.Drawing.Point(86, 77);
            this.numForce.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numForce.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numForce.Name = "numForce";
            this.numForce.Size = new System.Drawing.Size(65, 20);
            this.numForce.TabIndex = 3;
            this.numForce.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Величина";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Позиция x";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalculate.Location = new System.Drawing.Point(92, 171);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 1;
            this.btnCalculate.Text = "Расчитать";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(92, 129);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Сбросить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.Controls.Add(this.label_Mmax, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label13, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.label_Mmin, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label11, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.label17, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.label_Qmax, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.label19, 2, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(164, 78);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // label_Mmax
            // 
            this.label_Mmax.AutoSize = true;
            this.label_Mmax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Mmax.Location = new System.Drawing.Point(52, 0);
            this.label_Mmax.Name = "label_Mmax";
            this.label_Mmax.Size = new System.Drawing.Size(59, 26);
            this.label_Mmax.TabIndex = 4;
            this.label_Mmax.Text = "0";
            this.label_Mmax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 26);
            this.label7.TabIndex = 3;
            this.label7.Text = "M max";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 26);
            this.label8.TabIndex = 2;
            this.label8.Text = "M min ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(117, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 26);
            this.label13.TabIndex = 7;
            this.label13.Text = "кг*см";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Mmin
            // 
            this.label_Mmin.AutoSize = true;
            this.label_Mmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Mmin.Location = new System.Drawing.Point(52, 26);
            this.label_Mmin.Name = "label_Mmin";
            this.label_Mmin.Size = new System.Drawing.Size(59, 26);
            this.label_Mmin.TabIndex = 6;
            this.label_Mmin.Text = "0";
            this.label_Mmin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(117, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 26);
            this.label11.TabIndex = 5;
            this.label11.Text = "кг*см";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(3, 52);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(43, 26);
            this.label17.TabIndex = 8;
            this.label17.Text = "Q max";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Qmax
            // 
            this.label_Qmax.AutoSize = true;
            this.label_Qmax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Qmax.Location = new System.Drawing.Point(52, 52);
            this.label_Qmax.Name = "label_Qmax";
            this.label_Qmax.Size = new System.Drawing.Size(59, 26);
            this.label_Qmax.TabIndex = 9;
            this.label_Qmax.Text = "0";
            this.label_Qmax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Location = new System.Drawing.Point(117, 52);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(44, 26);
            this.label19.TabIndex = 10;
            this.label19.Text = "кг";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(143, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Location = new System.Drawing.Point(250, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "0";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.88889F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.11111F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel7, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1105, 453);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(423, 447);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.03357F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.96643F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 226);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 218F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(417, 218);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.btnCalculate, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.button2, 0, 2);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(244, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 4;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(170, 212);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(432, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(670, 447);
            this.tableLayoutPanel7.TabIndex = 2;
            // 
            // BeamCalculatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel3);
            this.MinimumSize = new System.Drawing.Size(1100, 335);
            this.Name = "BeamCalculatorControl";
            this.Size = new System.Drawing.Size(1105, 453);
            this.Load += new System.EventHandler(this.BeamCalculatorControl_Load);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBeamLen)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLenX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numForce)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numBeamLen;
        private System.Windows.Forms.RadioButton rbFixed_Fixed;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbPinned_Movable;
        private System.Windows.Forms.RadioButton rbFixed_No;
        private System.Windows.Forms.Label label1;
        private GroupBox groupBox4;
        private RadioButton radioButton12;
        private RadioButton radioButton11;
        private Label label5;
        private Label label4;
        private NumericUpDown numLenX;
        private NumericUpDown numForce;
        private RadioButton rbFixed_Movable;
        private RadioButton rbMovable_Fixed;
        private RadioButton rbNo_Fixed;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label8;
        private Label label7;
        private Label label_Mmax;
        private Label label10;
        private Label label6;
        private Label label13;
        private Label label_Mmin;
        private Label label11;
        private Label label15;
        private Label label14;
        private Label label16;
        private Label label17;
        private Label label_Qmax;
        private Label label19;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel7;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel8;
    }
}
