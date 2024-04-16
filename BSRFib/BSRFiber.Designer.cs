namespace BSFiberConcrete
{
    partial class BSRFiber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSRFiber));
            this.label_h = new System.Windows.Forms.Label();
            this.label_b = new System.Windows.Forms.Label();
            this.cmbEtaf = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmb_B = new System.Windows.Forms.ComboBox();
            this.labelB = new System.Windows.Forms.Label();
            this.cmb_RFiber = new System.Windows.Forms.ComboBox();
            this.labelR_f_ser = new System.Windows.Forms.Label();
            this.labelEtaf = new System.Windows.Forms.Label();
            this.label_l_f = new System.Windows.Forms.Label();
            this.label_d_f_red = new System.Windows.Forms.Label();
            this.btnCalc = new System.Windows.Forms.Button();
            this.labelDescr = new System.Windows.Forms.Label();
            this.num_h = new System.Windows.Forms.NumericUpDown();
            this.num_b = new System.Windows.Forms.NumericUpDown();
            this.picFib = new System.Windows.Forms.PictureBox();
            this.num_l_f = new System.Windows.Forms.NumericUpDown();
            this.num_d_f_red = new System.Windows.Forms.NumericUpDown();
            this.lblRes = new System.Windows.Forms.Label();
            this.label_mu_fv = new System.Windows.Forms.Label();
            this.num_mu_fv = new System.Windows.Forms.NumericUpDown();
            this.btnReport = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_h)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_b)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFib)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_l_f)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_d_f_red)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_mu_fv)).BeginInit();
            this.SuspendLayout();
            // 
            // label_h
            // 
            this.label_h.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_h.AutoSize = true;
            this.label_h.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_h.Location = new System.Drawing.Point(205, 102);
            this.label_h.Name = "label_h";
            this.label_h.Size = new System.Drawing.Size(17, 16);
            this.label_h.TabIndex = 5;
            this.label_h.Text = "h:";
            // 
            // label_b
            // 
            this.label_b.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_b.AutoSize = true;
            this.label_b.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_b.Location = new System.Drawing.Point(204, 136);
            this.label_b.Name = "label_b";
            this.label_b.Size = new System.Drawing.Size(18, 16);
            this.label_b.TabIndex = 7;
            this.label_b.Text = "b:";
            // 
            // cmbEtaf
            // 
            this.cmbEtaf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbEtaf.FormattingEnabled = true;
            this.cmbEtaf.Items.AddRange(new object[] {
            "Фибра фрезерованная из слябов",
            "Фибра резанная из стального листа",
            "Фибра рубленная из стальной проволоки"});
            this.cmbEtaf.Location = new System.Drawing.Point(229, 166);
            this.cmbEtaf.Name = "cmbEtaf";
            this.cmbEtaf.Size = new System.Drawing.Size(477, 21);
            this.cmbEtaf.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel1.Controls.Add(this.cmb_B, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.labelB, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.cmb_RFiber, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.labelR_f_ser, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label_h, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label_b, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelEtaf, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.cmbEtaf, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label_l_f, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label_d_f_red, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.btnCalc, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.labelDescr, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.num_h, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.num_b, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.picFib, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.num_l_f, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.num_d_f_red, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this.lblRes, 2, 11);
            this.tableLayoutPanel1.Controls.Add(this.label_mu_fv, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.num_mu_fv, 2, 9);
            this.tableLayoutPanel1.Controls.Add(this.btnReport, 1, 10);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 13;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.83544F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.16456F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(753, 516);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // cmb_B
            // 
            this.cmb_B.FormattingEnabled = true;
            this.cmb_B.Items.AddRange(new object[] {
            "B3.5",
            "B5",
            "B7.5",
            "B10",
            "B12.5",
            "B15",
            "B20",
            "B25",
            "B30",
            "B35",
            "B40",
            "B45",
            "B50",
            "B55",
            "B60",
            "B70",
            "B80",
            "B90",
            "B100"});
            this.cmb_B.Location = new System.Drawing.Point(229, 276);
            this.cmb_B.Name = "cmb_B";
            this.cmb_B.Size = new System.Drawing.Size(121, 21);
            this.cmb_B.TabIndex = 17;
            // 
            // labelB
            // 
            this.labelB.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelB.AutoSize = true;
            this.labelB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelB.Location = new System.Drawing.Point(203, 278);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(19, 16);
            this.labelB.TabIndex = 16;
            this.labelB.Text = "B:";
            // 
            // cmb_RFiber
            // 
            this.cmb_RFiber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_RFiber.FormattingEnabled = true;
            this.cmb_RFiber.Items.AddRange(new object[] {
            "Фибра, фрезерованная из слябов  отвечающая ТУ 0882-193-46854090",
            "Фибра, резанная из листа F1BREX отвечающая ТУ 0991-123-53832025 1 класса по прочн" +
                "ости",
            "Фибра, резанная из листа F1BREX отвечающая ТУ 0991-123-53832025 2 класса по прочн" +
                "ости",
            "Фибра, резанная из листа F1BREX отвечающая ТУ 0991-123-53832025 3 класса по прочн" +
                "ости",
            "Фибра, резанная из листа F1BREX отвечающая ТУ 1276-001-70832021 1 класса по прочн" +
                "ости",
            "Фибра, резанная из листа F1BREX отвечающая ТУ 1276-001-70832021 2 класса по прочн" +
                "ости",
            "Фибра, резанная из листа F1BREX отвечающая ТУ 1276-001-70832021 3 класса по прочн" +
                "ости",
            "Фибра, резанная из листа F1BREX отвечающая ТУ 1276-001-70832021 4 класса по прочн" +
                "ости",
            "Фибра, резанная из листа F1BREX отвечающая ТУ 1276-001-70832021 5 класса по прочн" +
                "ости",
            "Фибра, рубленная из проволки ХЕНДИКС (HENDIX) отвечающая ТУ 1211-205-46854090",
            "Фибра, рубленная из проволки МИКСАРМ (M1XARM) отвечающая ТУ 1211-205-46854090",
            "Фибра, рубленная из проволки ФИБЕКС (F1BAX)-1 /50 отвечающая ТУ 1211-205-46854090" +
                "",
            "Фибра, рубленная из проволки ФИБЕКС (FIBAX)-1,3/50  отвечающая ТУ 1211-205-468540" +
                "90",
            "Фибра, рубленная из проволки ТВИНФЛЭТ (TWINFLAT) отвечающая ТУ 1211-205-46854090",
            "Фибра, рубленная из проволки «МИКАС» ФАСк 30/0,5 отвечающая ТУ 1276-001-56707303",
            "Фибра, рубленная из проволки «МИКАС» ФАО 50/1,0 отвечающая ТУ 1276-001-56707303",
            "Фибра, рубленная из проволки «МИКАС» ФАО 60/0,75 отвечающая ТУ 1276-001-56707303",
            "Фибра, рубленная из проволки «МИКАС» ФАСк 60/0,75 отвечающая ТУ 1276-001-56707303" +
                "",
            "Фибра, рубленная из проволки  от 0,5 мм до 0,8 мм отвечающая ТУ 1276-001-56707303" +
                "",
            "Фибра, рубленная из проволки  от 0,85 мм  1,2 мм отвечающая ТУ 1276-001-56707303",
            "Фибра, рубленная из проволки  от 1,25 мм  1,6 мм\r отвечающая ТУ 1276-001-56707303" +
                ""});
            this.cmb_RFiber.Location = new System.Drawing.Point(229, 241);
            this.cmb_RFiber.Name = "cmb_RFiber";
            this.cmb_RFiber.Size = new System.Drawing.Size(477, 21);
            this.cmb_RFiber.TabIndex = 15;
            // 
            // labelR_f_ser
            // 
            this.labelR_f_ser.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelR_f_ser.AutoSize = true;
            this.labelR_f_ser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelR_f_ser.Location = new System.Drawing.Point(171, 244);
            this.labelR_f_ser.Name = "labelR_f_ser";
            this.labelR_f_ser.Size = new System.Drawing.Size(51, 16);
            this.labelR_f_ser.TabIndex = 14;
            this.labelR_f_ser.Text = "R f, ser:";
            // 
            // labelEtaf
            // 
            this.labelEtaf.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelEtaf.AutoSize = true;
            this.labelEtaf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelEtaf.Location = new System.Drawing.Point(128, 171);
            this.labelEtaf.Name = "labelEtaf";
            this.labelEtaf.Size = new System.Drawing.Size(94, 16);
            this.labelEtaf.TabIndex = 11;
            this.labelEtaf.Text = "η f;  γ fb1; γ fb2:";
            // 
            // label_l_f
            // 
            this.label_l_f.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_l_f.AutoSize = true;
            this.label_l_f.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_l_f.Location = new System.Drawing.Point(203, 205);
            this.label_l_f.Name = "label_l_f";
            this.label_l_f.Size = new System.Drawing.Size(19, 16);
            this.label_l_f.TabIndex = 13;
            this.label_l_f.Text = "l f:";
            // 
            // label_d_f_red
            // 
            this.label_d_f_red.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_d_f_red.AutoSize = true;
            this.label_d_f_red.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_d_f_red.Location = new System.Drawing.Point(172, 309);
            this.label_d_f_red.Name = "label_d_f_red";
            this.label_d_f_red.Size = new System.Drawing.Size(50, 16);
            this.label_d_f_red.TabIndex = 18;
            this.label_d_f_red.Text = "d f, red:";
            // 
            // btnCalc
            // 
            this.btnCalc.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCalc.Image = ((System.Drawing.Image)(resources.GetObject("btnCalc.Image")));
            this.btnCalc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCalc.Location = new System.Drawing.Point(114, 412);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(101, 28);
            this.btnCalc.TabIndex = 12;
            this.btnCalc.Text = "Рассчитать";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // labelDescr
            // 
            this.labelDescr.AutoSize = true;
            this.labelDescr.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.labelDescr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDescr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDescr.Location = new System.Drawing.Point(229, 20);
            this.labelDescr.Name = "labelDescr";
            this.labelDescr.Size = new System.Drawing.Size(477, 74);
            this.labelDescr.TabIndex = 29;
            this.labelDescr.Text = "Определение сопротивлений сталефибробетона растяжению и сжатию с учетом влияния ф" +
    "ибрового армирования.";
            // 
            // num_h
            // 
            this.num_h.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.num_h.DecimalPlaces = 2;
            this.num_h.Location = new System.Drawing.Point(229, 100);
            this.num_h.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.num_h.Name = "num_h";
            this.num_h.Size = new System.Drawing.Size(120, 20);
            this.num_h.TabIndex = 30;
            this.num_h.ValueChanged += new System.EventHandler(this.num_h_ValueChanged);
            // 
            // num_b
            // 
            this.num_b.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.num_b.DecimalPlaces = 2;
            this.num_b.Location = new System.Drawing.Point(229, 134);
            this.num_b.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.num_b.Name = "num_b";
            this.num_b.Size = new System.Drawing.Size(120, 20);
            this.num_b.TabIndex = 31;
            this.num_b.ValueChanged += new System.EventHandler(this.num_b_ValueChanged);
            // 
            // picFib
            // 
            this.picFib.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.picFib.Image = global::BSFiberConcrete.Properties.Resources.balka_rect;
            this.picFib.Location = new System.Drawing.Point(108, 23);
            this.picFib.Name = "picFib";
            this.picFib.Size = new System.Drawing.Size(114, 68);
            this.picFib.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picFib.TabIndex = 32;
            this.picFib.TabStop = false;
            // 
            // num_l_f
            // 
            this.num_l_f.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.num_l_f.DecimalPlaces = 3;
            this.num_l_f.Location = new System.Drawing.Point(229, 203);
            this.num_l_f.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.num_l_f.Name = "num_l_f";
            this.num_l_f.Size = new System.Drawing.Size(120, 20);
            this.num_l_f.TabIndex = 33;
            // 
            // num_d_f_red
            // 
            this.num_d_f_red.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.num_d_f_red.DecimalPlaces = 3;
            this.num_d_f_red.Location = new System.Drawing.Point(229, 307);
            this.num_d_f_red.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.num_d_f_red.Name = "num_d_f_red";
            this.num_d_f_red.Size = new System.Drawing.Size(120, 20);
            this.num_d_f_red.TabIndex = 34;
            // 
            // lblRes
            // 
            this.lblRes.AutoSize = true;
            this.lblRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRes.Location = new System.Drawing.Point(229, 409);
            this.lblRes.Name = "lblRes";
            this.lblRes.Size = new System.Drawing.Size(477, 62);
            this.lblRes.TabIndex = 35;
            this.lblRes.Text = "---   ";
            // 
            // label_mu_fv
            // 
            this.label_mu_fv.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_mu_fv.AutoSize = true;
            this.label_mu_fv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_mu_fv.Location = new System.Drawing.Point(192, 343);
            this.label_mu_fv.Name = "label_mu_fv";
            this.label_mu_fv.Size = new System.Drawing.Size(30, 16);
            this.label_mu_fv.TabIndex = 25;
            this.label_mu_fv.Text = "μ fv:";
            // 
            // num_mu_fv
            // 
            this.num_mu_fv.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.num_mu_fv.DecimalPlaces = 3;
            this.num_mu_fv.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.num_mu_fv.Location = new System.Drawing.Point(229, 341);
            this.num_mu_fv.Maximum = new decimal(new int[] {
            18,
            0,
            0,
            196608});
            this.num_mu_fv.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.num_mu_fv.Name = "num_mu_fv";
            this.num_mu_fv.Size = new System.Drawing.Size(120, 20);
            this.num_mu_fv.TabIndex = 36;
            this.num_mu_fv.Value = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            // 
            // btnReport
            // 
            this.btnReport.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnReport.Image = ((System.Drawing.Image)(resources.GetObject("btnReport.Image")));
            this.btnReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReport.Location = new System.Drawing.Point(114, 371);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(101, 28);
            this.btnReport.TabIndex = 37;
            this.btnReport.Text = "Отчет";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // BSRFiber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 516);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BSRFiber";
            this.Text = "Rfbt3; Rfb с учетом фибры";
            this.Load += new System.EventHandler(this.BSRFiber_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_h)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_b)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFib)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_l_f)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_d_f_red)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_mu_fv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_h;
        private System.Windows.Forms.Label label_b;
        private System.Windows.Forms.ComboBox cmbEtaf;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelEtaf;
        private System.Windows.Forms.Label label_l_f;
        private System.Windows.Forms.Label label_mu_fv;
        private System.Windows.Forms.ComboBox cmb_B;
        private System.Windows.Forms.Label labelB;
        private System.Windows.Forms.ComboBox cmb_RFiber;
        private System.Windows.Forms.Label labelR_f_ser;
        private System.Windows.Forms.Label label_d_f_red;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Label labelDescr;
        private System.Windows.Forms.NumericUpDown num_h;
        private System.Windows.Forms.NumericUpDown num_b;
        private System.Windows.Forms.PictureBox picFib;
        private System.Windows.Forms.NumericUpDown num_l_f;
        private System.Windows.Forms.NumericUpDown num_d_f_red;
        private System.Windows.Forms.Label lblRes;
        private System.Windows.Forms.NumericUpDown num_mu_fv;
        private System.Windows.Forms.Button btnReport;
    }
}