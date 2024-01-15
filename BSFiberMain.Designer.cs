namespace BSFiberConcrete
{
    partial class BSFiberMain
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
            this.components = new System.ComponentModel.Container();
            this.tabGeneral = new System.Windows.Forms.TabControl();
            this.tabParams = new System.Windows.Forms.TabPage();
            this.groupVar = new System.Windows.Forms.GroupBox();
            this.groupBeam = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCoefLength = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLength = new System.Windows.Forms.TextBox();
            this.tabConcrete = new System.Windows.Forms.TabPage();
            this.cmbBetonClass = new System.Windows.Forms.ComboBox();
            this.lblBetonClass = new System.Windows.Forms.Label();
            this.lblBetonType = new System.Windows.Forms.Label();
            this.comboBetonType = new System.Windows.Forms.ComboBox();
            this.tabStrength = new System.Windows.Forms.TabPage();
            this.btnCalc = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnRectang = new System.Windows.Forms.Button();
            this.btnTSection = new System.Windows.Forms.Button();
            this.btnIBeam = new System.Windows.Forms.Button();
            this.btnRing = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tbResult = new System.Windows.Forms.TextBox();
            this.tbResultW = new System.Windows.Forms.TextBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblRes0 = new System.Windows.Forms.Label();
            this.picBeton = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numRfbt3n = new System.Windows.Forms.NumericUpDown();
            this.numRfbn = new System.Windows.Forms.NumericUpDown();
            this.numYft = new System.Windows.Forms.NumericUpDown();
            this.numYb1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numYb2 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numYb3 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numYb5 = new System.Windows.Forms.NumericUpDown();
            this.tabGeneral.SuspendLayout();
            this.tabParams.SuspendLayout();
            this.groupBeam.SuspendLayout();
            this.tabConcrete.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBeton)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRfbt3n)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRfbn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYb1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYb2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYb3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYb5)).BeginInit();
            this.SuspendLayout();
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.tabParams);
            this.tabGeneral.Controls.Add(this.tabConcrete);
            this.tabGeneral.Controls.Add(this.tabStrength);
            this.tabGeneral.Location = new System.Drawing.Point(11, 40);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.SelectedIndex = 0;
            this.tabGeneral.Size = new System.Drawing.Size(1106, 333);
            this.tabGeneral.TabIndex = 0;
            // 
            // tabParams
            // 
            this.tabParams.Controls.Add(this.groupVar);
            this.tabParams.Controls.Add(this.groupBeam);
            this.tabParams.Location = new System.Drawing.Point(4, 22);
            this.tabParams.Name = "tabParams";
            this.tabParams.Padding = new System.Windows.Forms.Padding(3);
            this.tabParams.Size = new System.Drawing.Size(1098, 307);
            this.tabParams.TabIndex = 0;
            this.tabParams.Text = "Параметры";
            this.tabParams.UseVisualStyleBackColor = true;
            this.tabParams.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // groupVar
            // 
            this.groupVar.Location = new System.Drawing.Point(418, 31);
            this.groupVar.Name = "groupVar";
            this.groupVar.Size = new System.Drawing.Size(270, 275);
            this.groupVar.TabIndex = 1;
            this.groupVar.TabStop = false;
            this.groupVar.Text = "Варианты расчета";
            // 
            // groupBeam
            // 
            this.groupBeam.Controls.Add(this.checkBox1);
            this.groupBeam.Controls.Add(this.label2);
            this.groupBeam.Controls.Add(this.tbCoefLength);
            this.groupBeam.Controls.Add(this.label1);
            this.groupBeam.Controls.Add(this.tbLength);
            this.groupBeam.Location = new System.Drawing.Point(6, 31);
            this.groupBeam.Name = "groupBeam";
            this.groupBeam.Size = new System.Drawing.Size(350, 276);
            this.groupBeam.TabIndex = 0;
            this.groupBeam.TabStop = false;
            this.groupBeam.Text = "Балка";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(179, 138);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(95, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Армирование";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Коэффициент расчетной длины";
            // 
            // tbCoefLength
            // 
            this.tbCoefLength.FormattingEnabled = true;
            this.tbCoefLength.Location = new System.Drawing.Point(179, 85);
            this.tbCoefLength.Name = "tbCoefLength";
            this.tbCoefLength.Size = new System.Drawing.Size(119, 21);
            this.tbCoefLength.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Длина";
            // 
            // tbLength
            // 
            this.tbLength.Location = new System.Drawing.Point(179, 49);
            this.tbLength.Name = "tbLength";
            this.tbLength.Size = new System.Drawing.Size(119, 20);
            this.tbLength.TabIndex = 0;
            // 
            // tabConcrete
            // 
            this.tabConcrete.Controls.Add(this.groupBox2);
            this.tabConcrete.Controls.Add(this.groupBox1);
            this.tabConcrete.Controls.Add(this.cmbBetonClass);
            this.tabConcrete.Controls.Add(this.lblBetonClass);
            this.tabConcrete.Controls.Add(this.lblBetonType);
            this.tabConcrete.Controls.Add(this.comboBetonType);
            this.tabConcrete.Controls.Add(this.picBeton);
            this.tabConcrete.Location = new System.Drawing.Point(4, 22);
            this.tabConcrete.Name = "tabConcrete";
            this.tabConcrete.Padding = new System.Windows.Forms.Padding(3);
            this.tabConcrete.Size = new System.Drawing.Size(1098, 307);
            this.tabConcrete.TabIndex = 1;
            this.tabConcrete.Text = "Фибробетон";
            this.tabConcrete.UseVisualStyleBackColor = true;
            // 
            // cmbBetonClass
            // 
            this.cmbBetonClass.FormattingEnabled = true;
            this.cmbBetonClass.Location = new System.Drawing.Point(332, 19);
            this.cmbBetonClass.Name = "cmbBetonClass";
            this.cmbBetonClass.Size = new System.Drawing.Size(121, 21);
            this.cmbBetonClass.TabIndex = 4;
            this.cmbBetonClass.SelectedIndexChanged += new System.EventHandler(this.cmbBetonClass_SelectedIndexChanged);
            // 
            // lblBetonClass
            // 
            this.lblBetonClass.AutoSize = true;
            this.lblBetonClass.Location = new System.Drawing.Point(250, 19);
            this.lblBetonClass.Name = "lblBetonClass";
            this.lblBetonClass.Size = new System.Drawing.Size(76, 13);
            this.lblBetonClass.TabIndex = 3;
            this.lblBetonClass.Text = "Класс бетона";
            // 
            // lblBetonType
            // 
            this.lblBetonType.AutoSize = true;
            this.lblBetonType.Location = new System.Drawing.Point(7, 19);
            this.lblBetonType.Name = "lblBetonType";
            this.lblBetonType.Size = new System.Drawing.Size(64, 13);
            this.lblBetonType.TabIndex = 2;
            this.lblBetonType.Text = "Вид бетона";
            // 
            // comboBetonType
            // 
            this.comboBetonType.FormattingEnabled = true;
            this.comboBetonType.Items.AddRange(new object[] {
            "Тяжелый",
            "Мелкозернистый",
            "Легкий"});
            this.comboBetonType.Location = new System.Drawing.Point(92, 19);
            this.comboBetonType.Name = "comboBetonType";
            this.comboBetonType.Size = new System.Drawing.Size(121, 21);
            this.comboBetonType.TabIndex = 1;
            this.comboBetonType.SelectedIndexChanged += new System.EventHandler(this.comboBetonType_SelectedIndexChanged);
            // 
            // tabStrength
            // 
            this.tabStrength.Location = new System.Drawing.Point(4, 22);
            this.tabStrength.Name = "tabStrength";
            this.tabStrength.Padding = new System.Windows.Forms.Padding(3);
            this.tabStrength.Size = new System.Drawing.Size(842, 307);
            this.tabStrength.TabIndex = 2;
            this.tabStrength.Text = "Усилия";
            this.tabStrength.UseVisualStyleBackColor = true;
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(934, 544);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(75, 23);
            this.btnCalc.TabIndex = 1;
            this.btnCalc.Text = "Вычислить";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(1042, 544);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 23);
            this.btnReport.TabIndex = 2;
            this.btnReport.Text = "Отчет";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(11, 396);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(619, 150);
            this.dataGridView1.TabIndex = 4;
            // 
            // btnRectang
            // 
            this.btnRectang.Location = new System.Drawing.Point(687, 399);
            this.btnRectang.Name = "btnRectang";
            this.btnRectang.Size = new System.Drawing.Size(123, 23);
            this.btnRectang.TabIndex = 5;
            this.btnRectang.Text = "Прямоугольник";
            this.btnRectang.UseVisualStyleBackColor = true;
            this.btnRectang.Click += new System.EventHandler(this.btnRectang_Click);
            // 
            // btnTSection
            // 
            this.btnTSection.Enabled = false;
            this.btnTSection.Location = new System.Drawing.Point(687, 437);
            this.btnTSection.Name = "btnTSection";
            this.btnTSection.Size = new System.Drawing.Size(123, 23);
            this.btnTSection.TabIndex = 6;
            this.btnTSection.Text = "Тавр";
            this.btnTSection.UseVisualStyleBackColor = true;
            this.btnTSection.Click += new System.EventHandler(this.btnTSection_Click);
            // 
            // btnIBeam
            // 
            this.btnIBeam.Location = new System.Drawing.Point(687, 479);
            this.btnIBeam.Name = "btnIBeam";
            this.btnIBeam.Size = new System.Drawing.Size(123, 23);
            this.btnIBeam.TabIndex = 7;
            this.btnIBeam.Text = "Двутавр";
            this.btnIBeam.UseVisualStyleBackColor = true;
            this.btnIBeam.Click += new System.EventHandler(this.btnIBeam_Click);
            // 
            // btnRing
            // 
            this.btnRing.Location = new System.Drawing.Point(687, 523);
            this.btnRing.Name = "btnRing";
            this.btnRing.Size = new System.Drawing.Size(123, 23);
            this.btnRing.TabIndex = 8;
            this.btnRing.Text = "Кольцо";
            this.btnRing.UseVisualStyleBackColor = true;
            this.btnRing.Click += new System.EventHandler(this.btnRing_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиToolStripMenuItem,
            this.оПрограммеToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1161, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            this.настройкиToolStripMenuItem.Click += new System.EventHandler(this.настройкиToolStripMenuItem_Click);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.оПрограммеToolStripMenuItem.Text = "Режимы";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem1});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // AboutToolStripMenuItem1
            // 
            this.AboutToolStripMenuItem1.Name = "AboutToolStripMenuItem1";
            this.AboutToolStripMenuItem1.Size = new System.Drawing.Size(149, 22);
            this.AboutToolStripMenuItem1.Text = "О программе";
            this.AboutToolStripMenuItem1.Click += new System.EventHandler(this.AboutToolStripMenuItem1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(934, 498);
            this.tbResult.Name = "tbResult";
            this.tbResult.Size = new System.Drawing.Size(145, 20);
            this.tbResult.TabIndex = 10;
            this.tbResult.Text = "Результат";
            // 
            // tbResultW
            // 
            this.tbResultW.Location = new System.Drawing.Point(934, 437);
            this.tbResultW.Name = "tbResultW";
            this.tbResultW.Size = new System.Drawing.Size(145, 20);
            this.tbResultW.TabIndex = 11;
            // 
            // lblResult
            // 
            this.lblResult.Location = new System.Drawing.Point(931, 470);
            this.lblResult.MinimumSize = new System.Drawing.Size(50, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(148, 13);
            this.lblResult.TabIndex = 12;
            this.lblResult.Text = "res               ";
            this.lblResult.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblRes0
            // 
            this.lblRes0.AutoSize = true;
            this.lblRes0.Location = new System.Drawing.Point(934, 408);
            this.lblRes0.Name = "lblRes0";
            this.lblRes0.Size = new System.Drawing.Size(27, 13);
            this.lblRes0.TabIndex = 13;
            this.lblRes0.Text = "res0";
            // 
            // picBeton
            // 
            this.picBeton.ErrorImage = null;
            this.picBeton.Image = global::BSFiberConcrete.Properties.Resources.FiberBeton;
            this.picBeton.Location = new System.Drawing.Point(757, 43);
            this.picBeton.Name = "picBeton";
            this.picBeton.Size = new System.Drawing.Size(335, 228);
            this.picBeton.TabIndex = 0;
            this.picBeton.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.numYb5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.numYb3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.numYb2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numYb1);
            this.groupBox1.Controls.Add(this.numYft);
            this.groupBox1.Location = new System.Drawing.Point(10, 150);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(725, 133);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Коэффициенты";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numRfbn);
            this.groupBox2.Controls.Add(this.numRfbt3n);
            this.groupBox2.Location = new System.Drawing.Point(9, 53);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(726, 80);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Характеристики";
            // 
            // numRfbt3n
            // 
            this.numRfbt3n.DecimalPlaces = 2;
            this.numRfbt3n.Location = new System.Drawing.Point(59, 34);
            this.numRfbt3n.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numRfbt3n.Name = "numRfbt3n";
            this.numRfbt3n.Size = new System.Drawing.Size(120, 20);
            this.numRfbt3n.TabIndex = 0;
            // 
            // numRfbn
            // 
            this.numRfbn.DecimalPlaces = 2;
            this.numRfbn.Location = new System.Drawing.Point(226, 33);
            this.numRfbn.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numRfbn.Name = "numRfbn";
            this.numRfbn.Size = new System.Drawing.Size(120, 20);
            this.numRfbn.TabIndex = 1;
            // 
            // numYft
            // 
            this.numYft.DecimalPlaces = 1;
            this.numYft.Location = new System.Drawing.Point(43, 48);
            this.numYft.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numYft.Name = "numYft";
            this.numYft.Size = new System.Drawing.Size(85, 20);
            this.numYft.TabIndex = 0;
            // 
            // numYb1
            // 
            this.numYb1.DecimalPlaces = 1;
            this.numYb1.Location = new System.Drawing.Point(174, 48);
            this.numYb1.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numYb1.Name = "numYb1";
            this.numYb1.Size = new System.Drawing.Size(88, 20);
            this.numYb1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Rfbn";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Rfbt3n";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Yft";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(142, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Yb1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(276, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Yb2";
            // 
            // numYb2
            // 
            this.numYb2.DecimalPlaces = 1;
            this.numYb2.Location = new System.Drawing.Point(308, 46);
            this.numYb2.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numYb2.Name = "numYb2";
            this.numYb2.Size = new System.Drawing.Size(88, 20);
            this.numYb2.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(422, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Yb3";
            // 
            // numYb3
            // 
            this.numYb3.DecimalPlaces = 1;
            this.numYb3.Location = new System.Drawing.Point(454, 45);
            this.numYb3.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numYb3.Name = "numYb3";
            this.numYb3.Size = new System.Drawing.Size(88, 20);
            this.numYb3.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(559, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Yb5";
            // 
            // numYb5
            // 
            this.numYb5.DecimalPlaces = 1;
            this.numYb5.Location = new System.Drawing.Point(591, 44);
            this.numYb5.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numYb5.Name = "numYb5";
            this.numYb5.Size = new System.Drawing.Size(88, 20);
            this.numYb5.TabIndex = 8;
            // 
            // BSFiberMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 597);
            this.Controls.Add(this.lblRes0);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.tbResultW);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.btnRing);
            this.Controls.Add(this.btnIBeam);
            this.Controls.Add(this.btnTSection);
            this.Controls.Add(this.btnRectang);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.tabGeneral);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BSFiberMain";
            this.Text = "Фибробетон";
            this.Load += new System.EventHandler(this.BSFiberMain_Load);
            this.tabGeneral.ResumeLayout(false);
            this.tabParams.ResumeLayout(false);
            this.groupBeam.ResumeLayout(false);
            this.groupBeam.PerformLayout();
            this.tabConcrete.ResumeLayout(false);
            this.tabConcrete.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBeton)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRfbt3n)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRfbn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYb1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYb2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYb3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYb5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabGeneral;
        private System.Windows.Forms.TabPage tabParams;
        private System.Windows.Forms.TabPage tabConcrete;
        private System.Windows.Forms.TabPage tabStrength;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.GroupBox groupBeam;
        private System.Windows.Forms.TextBox tbLength;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupVar;
        private System.Windows.Forms.ComboBox tbCoefLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnRectang;
        private System.Windows.Forms.Button btnTSection;
        private System.Windows.Forms.Button btnIBeam;
        private System.Windows.Forms.Button btnRing;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem1;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.TextBox tbResultW;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblRes0;
        private System.Windows.Forms.ComboBox comboBetonType;
        private System.Windows.Forms.ComboBox cmbBetonClass;
        private System.Windows.Forms.Label lblBetonClass;
        private System.Windows.Forms.Label lblBetonType;
        private System.Windows.Forms.PictureBox picBeton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numRfbn;
        private System.Windows.Forms.NumericUpDown numRfbt3n;
        private System.Windows.Forms.NumericUpDown numYb1;
        private System.Windows.Forms.NumericUpDown numYft;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numYb5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numYb3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numYb2;
    }
}

