namespace BSFiberConcrete.BSRFib
{
    partial class RSRFibDeflection
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
            this.dataGridDefl = new System.Windows.Forms.DataGridView();
            this.Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanelDefl = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.labelBeam = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbBeams = new System.Windows.Forms.ComboBox();
            this.labelBeamId = new System.Windows.Forms.Label();
            this.textBeamId = new System.Windows.Forms.TextBox();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aFDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deflectionfaFBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDefl)).BeginInit();
            this.tableLayoutPanelDefl.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deflectionfaFBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridDefl
            // 
            this.dataGridDefl.AllowUserToAddRows = false;
            this.dataGridDefl.AllowUserToDeleteRows = false;
            this.dataGridDefl.AutoGenerateColumns = false;
            this.dataGridDefl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDefl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.Num,
            this.fColumn,
            this.aFDataGridViewTextBoxColumn});
            this.dataGridDefl.DataSource = this.deflectionfaFBindingSource;
            this.dataGridDefl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridDefl.Location = new System.Drawing.Point(72, 83);
            this.dataGridDefl.Name = "dataGridDefl";
            this.dataGridDefl.Size = new System.Drawing.Size(701, 346);
            this.dataGridDefl.TabIndex = 0;
            this.dataGridDefl.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridDefl_CellValueChanged);
            // 
            // Num
            // 
            this.Num.DataPropertyName = "Num";
            this.Num.HeaderText = "№";
            this.Num.Name = "Num";
            this.Num.ToolTipText = "Номер измерения";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(701, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Зависимость aF- f прогибов";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanelDefl
            // 
            this.tableLayoutPanelDefl.ColumnCount = 3;
            this.tableLayoutPanelDefl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.912189F));
            this.tableLayoutPanelDefl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 91.08781F));
            this.tableLayoutPanelDefl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanelDefl.Controls.Add(this.dataGridDefl, 1, 2);
            this.tableLayoutPanelDefl.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanelDefl.Controls.Add(this.tableLayoutPanel1, 1, 3);
            this.tableLayoutPanelDefl.Controls.Add(this.labelBeam, 0, 1);
            this.tableLayoutPanelDefl.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanelDefl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelDefl.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelDefl.Name = "tableLayoutPanelDefl";
            this.tableLayoutPanelDefl.RowCount = 5;
            this.tableLayoutPanelDefl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelDefl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanelDefl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.88889F));
            this.tableLayoutPanelDefl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanelDefl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanelDefl.Size = new System.Drawing.Size(808, 479);
            this.tableLayoutPanelDefl.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.51515F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.48485F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 91F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnReport, 4, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(72, 435);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(701, 32);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSave.Location = new System.Drawing.Point(623, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(339, 3);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 23);
            this.btnReport.TabIndex = 2;
            this.btnReport.Text = "Отчет";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // labelBeam
            // 
            this.labelBeam.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelBeam.AutoSize = true;
            this.labelBeam.Location = new System.Drawing.Point(15, 51);
            this.labelBeam.Name = "labelBeam";
            this.labelBeam.Size = new System.Drawing.Size(51, 13);
            this.labelBeam.TabIndex = 4;
            this.labelBeam.Text = "Образец";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.05045F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.94955F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 318F));
            this.tableLayoutPanel2.Controls.Add(this.cmbBeams, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelBeamId, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBeamId, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(72, 39);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(634, 38);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // cmbBeams
            // 
            this.cmbBeams.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBeams.FormattingEnabled = true;
            this.cmbBeams.Items.AddRange(new object[] {
            "(новый)"});
            this.cmbBeams.Location = new System.Drawing.Point(3, 8);
            this.cmbBeams.Name = "cmbBeams";
            this.cmbBeams.Size = new System.Drawing.Size(180, 21);
            this.cmbBeams.TabIndex = 2;
            this.cmbBeams.SelectedIndexChanged += new System.EventHandler(this.cmbBeams_SelectedIndexChanged);
            // 
            // labelBeamId
            // 
            this.labelBeamId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBeamId.AutoSize = true;
            this.labelBeamId.Location = new System.Drawing.Point(189, 12);
            this.labelBeamId.Name = "labelBeamId";
            this.labelBeamId.Size = new System.Drawing.Size(123, 13);
            this.labelBeamId.TabIndex = 3;
            this.labelBeamId.Text = "описание";
            this.labelBeamId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBeamId
            // 
            this.textBeamId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBeamId.Location = new System.Drawing.Point(318, 9);
            this.textBeamId.Name = "textBeamId";
            this.textBeamId.Size = new System.Drawing.Size(313, 20);
            this.textBeamId.TabIndex = 4;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // fColumn
            // 
            this.fColumn.DataPropertyName = "f";
            this.fColumn.HeaderText = "f, мм";
            this.fColumn.Name = "fColumn";
            this.fColumn.ToolTipText = "Величина прогиба";
            // 
            // aFDataGridViewTextBoxColumn
            // 
            this.aFDataGridViewTextBoxColumn.DataPropertyName = "aF";
            this.aFDataGridViewTextBoxColumn.HeaderText = "a F, мм";
            this.aFDataGridViewTextBoxColumn.Name = "aFDataGridViewTextBoxColumn";
            this.aFDataGridViewTextBoxColumn.ToolTipText = "значение при испытании, мм";
            // 
            // deflectionfaFBindingSource
            // 
            this.deflectionfaFBindingSource.DataSource = typeof(BSFiberConcrete.Deflection_f_aF);
            // 
            // RSRFibDeflection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 479);
            this.Controls.Add(this.tableLayoutPanelDefl);
            this.Name = "RSRFibDeflection";
            this.Text = "Измерение величны прогибов";
            this.Load += new System.EventHandler(this.RSRFibDeflection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDefl)).EndInit();
            this.tableLayoutPanelDefl.ResumeLayout(false);
            this.tableLayoutPanelDefl.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deflectionfaFBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridDefl;
        private System.Windows.Forms.BindingSource deflectionfaFBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDefl;
        private System.Windows.Forms.ComboBox cmbBeams;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label labelBeam;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelBeamId;
        private System.Windows.Forms.TextBox textBeamId;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Num;
        private System.Windows.Forms.DataGridViewTextBoxColumn fColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aFDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnReport;
    }
}