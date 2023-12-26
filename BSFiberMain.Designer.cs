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
            this.tabGeneral = new System.Windows.Forms.TabControl();
            this.tabParams = new System.Windows.Forms.TabPage();
            this.groupVar = new System.Windows.Forms.GroupBox();
            this.groupBeam = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCoefLength = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLength = new System.Windows.Forms.TextBox();
            this.tabConcrete = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabStrength = new System.Windows.Forms.TabPage();
            this.btnCalc = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnRectang = new System.Windows.Forms.Button();
            this.btnTSection = new System.Windows.Forms.Button();
            this.btnIBeam = new System.Windows.Forms.Button();
            this.btnRing = new System.Windows.Forms.Button();
            this.tabGeneral.SuspendLayout();
            this.tabParams.SuspendLayout();
            this.groupBeam.SuspendLayout();
            this.tabConcrete.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.tabParams);
            this.tabGeneral.Controls.Add(this.tabConcrete);
            this.tabGeneral.Controls.Add(this.tabStrength);
            this.tabGeneral.Location = new System.Drawing.Point(7, 4);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.SelectedIndex = 0;
            this.tabGeneral.Size = new System.Drawing.Size(854, 369);
            this.tabGeneral.TabIndex = 0;
            // 
            // tabParams
            // 
            this.tabParams.Controls.Add(this.groupVar);
            this.tabParams.Controls.Add(this.groupBeam);
            this.tabParams.Location = new System.Drawing.Point(4, 22);
            this.tabParams.Name = "tabParams";
            this.tabParams.Padding = new System.Windows.Forms.Padding(3);
            this.tabParams.Size = new System.Drawing.Size(846, 343);
            this.tabParams.TabIndex = 0;
            this.tabParams.Text = "Параметры";
            this.tabParams.UseVisualStyleBackColor = true;
            this.tabParams.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // groupVar
            // 
            this.groupVar.Location = new System.Drawing.Point(418, 31);
            this.groupVar.Name = "groupVar";
            this.groupVar.Size = new System.Drawing.Size(390, 275);
            this.groupVar.TabIndex = 1;
            this.groupVar.TabStop = false;
            this.groupVar.Text = "Варианты расчета";
            this.groupVar.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBeam
            // 
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Коэффициент расчетной длины";
            this.label2.Click += new System.EventHandler(this.label2_Click);
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
            this.tabConcrete.Controls.Add(this.pictureBox1);
            this.tabConcrete.Location = new System.Drawing.Point(4, 22);
            this.tabConcrete.Name = "tabConcrete";
            this.tabConcrete.Padding = new System.Windows.Forms.Padding(3);
            this.tabConcrete.Size = new System.Drawing.Size(830, 422);
            this.tabConcrete.TabIndex = 1;
            this.tabConcrete.Text = "Фибробетон";
            this.tabConcrete.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::BSFiberConcrete.Properties.Resources.FiberBeton;
            this.pictureBox1.Location = new System.Drawing.Point(455, 167);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(328, 209);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabStrength
            // 
            this.tabStrength.Location = new System.Drawing.Point(4, 22);
            this.tabStrength.Name = "tabStrength";
            this.tabStrength.Padding = new System.Windows.Forms.Padding(3);
            this.tabStrength.Size = new System.Drawing.Size(830, 422);
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
            // BSFiberMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 597);
            this.Controls.Add(this.btnRing);
            this.Controls.Add(this.btnIBeam);
            this.Controls.Add(this.btnTSection);
            this.Controls.Add(this.btnRectang);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.tabGeneral);
            this.Name = "BSFiberMain";
            this.Text = "Фибробетон";
            this.Load += new System.EventHandler(this.BSFiberMain_Load);
            this.tabGeneral.ResumeLayout(false);
            this.tabParams.ResumeLayout(false);
            this.groupBeam.ResumeLayout(false);
            this.groupBeam.PerformLayout();
            this.tabConcrete.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox tbCoefLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnRectang;
        private System.Windows.Forms.Button btnTSection;
        private System.Windows.Forms.Button btnIBeam;
        private System.Windows.Forms.Button btnRing;
    }
}

